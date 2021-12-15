using System;
using System.Collections.Generic;
using EksamensOpgave.Models;
using EksamensOpgave.Interface;

namespace EksamensOpgave.UI
{
    public class StregsystemCLI : IStregsystemUI
    {
        //variabels used in functionality
        private bool Running;

        private IStregsystem stregsystem;

        // **constructor**
        public StregsystemCLI(IStregsystem stregsystem)
        {
            this.stregsystem = stregsystem;
            Running = false;
        }

        // **functionality**
        public void Start()
        {
            if (Running)
                return;

            Running = true;
            string command;

            while (Running)
            {
                ConsoleUI();

                command = Console.ReadLine();
                Console.WriteLine("\n");

                CommandEntered(command);

            }

        }

        //Creates the UI the user sees in the console/cmd/terminal
        private void ConsoleUI()
        {
            //Print empty lines and clear to produce space between individual updates
            /*for (int i = 0; i < 50; i++) Console.WriteLine("");
            Console.Clear();*/


            Console.WriteLine($"| {"Id",-3} | {"Name",-40} | {"Price",-7} |");
            Console.WriteLine(("").PadRight(60, '-'));

            foreach( Product product in stregsystem.ActiveProducts)
            {
                Console.WriteLine($"| {product.ID,-3} | {product.Name,-40} | {product.Price/100,-7} |");
            }
            Console.WriteLine("\n");



        }

        public void DisplayUsers()
        {
            DisplayGeneralMessage("Stregsystem users:");
            foreach(User user in stregsystem.GetUsers(user => true))
            {
                DisplayGeneralMessage($"{user.ID}: {user} - Balance: {user.Balance}");
            }
        }

        public void DisplayUserNotFound(string username)
        {
            DisplayGeneralMessage($"{username} not found! Check if written correctly.\n");
        }

        public void DisplayProductNotFound(string product)
        {
            DisplayGeneralMessage($"{product} not found!\n");
        }

        public void DisplayUserInfo(User user)
        {
            DisplayGeneralMessage($"Username: {user.UserName}\n");
            DisplayGeneralMessage($"Name: {user.FirstName} {user.LastName}\n");
            DisplayGeneralMessage($"Email: {user.Email}\n");
            DisplayGeneralMessage($"Balance: {user.Balance/100} kr\n");
            if (user.Balance < 50)
                DisplayGeneralMessage($"Warning: {user.UserName} has less than 50 kr.\n");

        }

        public void DisplayTooManyArgumentsError(string command)
        {
            DisplayGeneralMessage($"Command: {command} was given too many arguments!\n");
        }

        public void DisplayAdminCommandNotFoundMessage(string adminCommand)
        {
            DisplayGeneralMessage($"Admin Command: {adminCommand} was not found!\n");
        }

        public void DisplayUserBuysProduct(BuyTransaction transaction)
        {
            DisplayGeneralMessage(transaction.ToString()+"\n");
        }


        public void DisplayUserBuysProduct(int count, BuyTransaction transaction)
        {
            DisplayGeneralMessage($"{transaction} x {count}");
        }

        public void DisplayInsufficientCash(User user, Product product)
        {
            DisplayGeneralMessage($"{user.UserName} tried to buy {product.Name} but has insuficcient credits.\n");
        }

        public void DisplayGeneralError(string errorString)
        {
            DisplayGeneralMessage($"Error: {errorString}\n");
        }

        public void DisplayGeneralMessage(string msg)
        {
            Console.WriteLine(msg);
        }

        public void Close()
        {
            Running = false;
            Console.Clear();
        }

        //public event StregsystemEvent CommandEntered;
        public event Action<string> CommandEntered;

    }
}
