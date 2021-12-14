using System;
using System.Collections.Generic;
using System.Linq;
using EksamensOpgave.Models;
using EksamensOpgave.Interface;
using EksamensOpgave.Exceptions;
using EksamensOpgave.UI;

namespace EksamensOpgave.UI
{
    public class StregsystemController
    {
        public IStregsystemUI StregsystemUI { get; }
        public IStregsystem Stregsystem { get; }

        private Dictionary<string, Action<string[]>> AdminCmds;

        public StregsystemController(IStregsystem stregsystem, IStregsystemUI stregsystemUI)
        {
            Stregsystem = stregsystem;
            StregsystemUI = stregsystemUI;

            AdminCmds = new Dictionary<string, Action<string[]>>();

            //Creating admin commands
            AdminCmds.Add(":quit", Quit);
            AdminCmds.Add(":q", QuitQ);
            AdminCmds.Add(":activate", Activate);
            AdminCmds.Add(":deactivate", Deactivate);
            AdminCmds.Add(":crediton", CreditOn);
            AdminCmds.Add(":creditoff", CreditOff);
            AdminCmds.Add(":addcredits", AddCredits);
            AdminCmds.Add(":users", args => StregsystemUI.DisplayUsers());

            StregsystemUI.CommandEntered += (command) => ParseCommand(command);

        }


        public void ParseCommand(string command)
        {
            string[] words = command.ToLower().Split(' ');
            string cmd = words.First();
            string[] args = words.Skip(1).ToArray();

            if (cmd.Contains(':'))
            {
                //check if command is a registered admin command
                if (AdminCmds.ContainsKey(cmd))
                    AdminCmds[cmd](args);
                else
                    StregsystemUI.DisplayAdminCommandNotFoundMessage(cmd);
                return;
            }

            switch (args.Length)
            {
                case 0:
                    GetUserInfo(cmd);
                    break;
                case 1:
                    QuickBuy(cmd, args[0]);
                    break;
                case 2:
                    MultiBuy(cmd, args[0], args[1]);
                    break;
                default:
                    StregsystemUI.DisplayTooManyArgumentsError(cmd);
                    break;

            }
        }


        private void GetUserInfo(string username)
        {
            Console.WriteLine("test");
            try
            {
                User user = Stregsystem.GetUserByUsername(username);
                StregsystemUI.DisplayUserInfo(user);
            }
            catch(UsernameNotFoundException)
            {
                StregsystemUI.DisplayUserNotFound(username);
            }

        }

        private void QuickBuy(string username, string id)
        {
            try
            {
                User user = Stregsystem.GetUserByUsername(username);
                Product product = Stregsystem.GetProductByID(Int32.Parse(id));

                BuyTransaction buyTransaction = new BuyTransaction(user, product);
                Stregsystem.ExecuteTransaction(buyTransaction);
                StregsystemUI.DisplayUserBuysProduct(buyTransaction);
            }
            catch(UsernameNotFoundException)
            {
                StregsystemUI.DisplayUserNotFound(username);
            }
            catch(ProductNotFoundException)
            {
                StregsystemUI.DisplayProductNotFound(id);
            }
            catch(InsufficientCreditsException e)
            {
                StregsystemUI.DisplayInsufficientCash(e.User, e.Product);
            }
            catch(Exception e)
            {
                StregsystemUI.DisplayGeneralError(e.Message);
            }
        }

        private void MultiBuy(string username, string count, string id)
        {
            try
            {
                User user = Stregsystem.GetUserByUsername(username);
                Product product = Stregsystem.GetProductByID(Int32.Parse(id));

                for(int i = 0; i < Int32.Parse(count); i++)
                {
                    BuyTransaction transaction = new BuyTransaction(user, product);
                    Stregsystem.ExecuteTransaction(transaction);
                    StregsystemUI.DisplayUserBuysProduct(Int32.Parse(count), transaction);
                }
            }
            catch (UsernameNotFoundException)
            {
                StregsystemUI.DisplayUserNotFound(username);
            }
            catch (ProductNotFoundException)
            {
                StregsystemUI.DisplayProductNotFound(id);
            }
            catch (InsufficientCreditsException e)
            {
                StregsystemUI.DisplayInsufficientCash(e.User, e.Product);
            }
            catch (Exception e)
            {
                StregsystemUI.DisplayGeneralError(e.Message);
            }
        }

        private void Quit(string[] args) => GeneralQuit(":quit", args);
        private void QuitQ(string[] args) => GeneralQuit(":quit", args);
        private void GeneralQuit(string cmd, string[] args)
        {
            if (args.Length > 0)
            {
                StregsystemUI.DisplayTooManyArgumentsError(cmd);
            }
            else
            {
                StregsystemUI.Close();
            }
        }

        private void Activate(string[] args) => ChangeActive(":activate", true, args);
        private void Deactivate(string[] args) => ChangeActive(":deactivate", false, args);
        private void ChangeActive(string cmd, bool val, string[] args)
        {
            if (args.Length != 1)
            {
                StregsystemUI.DisplayTooManyArgumentsError(cmd);
                return;
            }

            try
            {
                // try to parse arguments
                int id = Int32.Parse(args[0]);
                Product product = Stregsystem.GetProductByID(id);

                // if product is not seasonal the change status
                if(!(product is SeasonalProduct)) { 
                    product.Active = val;
                    StregsystemUI.DisplayGeneralMessage($"Active value of product {product.Name} is now set to: {val}");
                }
            }
            catch (InactiveProductException e)
            {
                StregsystemUI.DisplayGeneralMessage(e.Message);
            }
            catch (System.Exception e)
            {
                StregsystemUI.DisplayGeneralError(e.Message);
            }

        }

        private void CreditOn(string[] args) => ChangeCredit(":crediton", true, args);
        private void CreditOff(string[] args) => ChangeCredit(":creditoff", false, args);
        private void ChangeCredit(string cmd, bool val, string[] args)
        {
            if (args.Length != 1)
            {
                StregsystemUI.DisplayTooManyArgumentsError(cmd);
                return;
            }

            try
            {
                // try to parse arguments
                int id = Int32.Parse(args.First());
                Product product = Stregsystem.GetProductByID(id);

                // if successful change value and notify user
                product.CanBeBoughtOnCredit = val;
                StregsystemUI.DisplayGeneralMessage($"Can be bought on credit value of {product.Name} is now set to: {val}");
            }
            catch (ProductNotFoundException)
            {
                StregsystemUI.DisplayProductNotFound(args.First());
            }
            catch (System.Exception e)
            {
                StregsystemUI.DisplayGeneralError(e.Message);
            }
        }


        private void AddCredits(string[] args)
        {
            if (args.Length != 2)
            {
                StregsystemUI.DisplayTooManyArgumentsError(":addcredits");
                return;
            }

            string username = args.First();

            try
            {

                int credits = Int32.Parse(args.Last());
                User user = Stregsystem.GetUserByUsername(username);
                Transaction transaction = new InsertCashTransaction(user, credits);

                Stregsystem.ExecuteTransaction(transaction);

                StregsystemUI.DisplayGeneralMessage(transaction.ToString());
            }
            catch (UsernameNotFoundException)
            {
                StregsystemUI.DisplayUserNotFound(username);
            }
            catch (System.Exception e)
            {
                StregsystemUI.DisplayGeneralError(e.Message);
            }
        }


    }
}
