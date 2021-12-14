using System;
using System.Collections.Generic;
using EksamensOpgave.Models;
using EksamensOpgave.Interface;

namespace EksamensOpgave.UI
{
    public class StregsystemCLI : IStregsystemUI
    {
        private List<string> Response = new List<string>();

        private bool Running;

        private IStregsystem stregsystem;

        public StregsystemCLI(IStregsystem stregsystem)
        {
            this.stregsystem = stregsystem;
            Running = false;
        }

        public void Start()
        {
            if (Running)
                return;

            Running = true;
            string command;

            while (Running == true)
            {
                ConsoleUI();
                //command = Console.ReadLine();
                //CommandEntered += ParseCommand(CommandEntered);
                //CommandEntered(command);

            }

        }

        private void ConsoleUI()
        {
            foreach( Product product in stregsystem.ActiveProducts)
            {
                Console.WriteLine(product);
            }
        }

        public void DisplayUserNotFound(string username)
        {
            Response.Add($"{username} not found! Check if written correctly.\n");
        }

        public void DisplayProductNotFound(string product)
        {
            Response.Add($"{product} not found!\n");
        }

        public void DisplayUserInfo(User user)
        {
            Response.Add($"Username: {user.UserName}, Balance: {user.Balance}\n");
            if (user.Balance < 50)
                Response.Add($"Warning: {user.UserName} has less than 50 kr.\n");

        }

        public void DisplayTooManyArgumentsError(string command)
        {
            Response.Add($"Command: {command} was given too many arguments!\n");
        }

        public void DisplayAdminCommandNotFoundMessage(string adminCommand)
        {
            Response.Add($"Admin Command: {adminCommand} was not found!\n");
        }

        public void DisplayUserBuysProduct(BuyTransaction transaction)
        {
            Response.Add(transaction.ToString()+"\n");
        }


        public void DisplayUserBuysProduct(int count, BuyTransaction transaction)
        {
            Response.Add($"{transaction} x {count}");
        }

        public void DisplayInsufficientCash(User user, Product product)
        {
            Response.Add($"{user.UserName} tried to buy {product.Name} but has insuficcient credits.\n");
        }

        public void DisplayGeneralError(string errorString)
        {
            Response.Add($"Error: {errorString}\n");
        }

        public void Close()
        {
          
        }

        public event StregsystemEvent CommandEntered;

    }
}
