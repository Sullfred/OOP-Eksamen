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

        public StregsystemController(IStregsystem stregsystem, IStregsystemUI stregsystemUI)
        {
            Stregsystem = stregsystem;
            StregsystemUI = stregsystemUI;

        }


        public void ParseCommand(string command)
        {
            string[] words = command.Split(' ');
            string cmd = words.First();
            string[] args = words.Skip(1).ToArray();

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


    }
}
