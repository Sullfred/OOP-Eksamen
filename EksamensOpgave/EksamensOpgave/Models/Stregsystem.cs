using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using EksamensOpgave.Exceptions;
using EksamensOpgave.Interface;
using EksamensOpgave.CsvFileReader;
using EksamensOpgave.Data;

namespace EksamensOpgave.Models
{
    public class Stregsystem : IStregsystem
    {
        // **lists**
        private List<Transaction> Transactions { get; }
        private List<Product> Products { get; }
        private List<User> Users { get; }

        // **FileReading classes**
        private UsersCsv UsersCsv = new UsersCsv();
        private ProductsCsv ProductsCsv = new ProductsCsv();
        private TransactionsPath TransactionsPath = new TransactionsPath();


        // **constructor**
        public Stregsystem()
        {
            Users = UsersCsv.GetUsersFromFile(UsersCsv.FilePath);
            Users.Sort();

            Products = ProductsCsv.GetProductsFromFile(ProductsCsv.FilePath);
            Products.Sort();

            Transactions = new List<Transaction>();

        }

        // **Functionality**
        public BuyTransaction BuyProduct(User user, Product product) => new BuyTransaction(user, product);

        public InsertCashTransaction AddCreditsToAccount(User user, int amount) => new InsertCashTransaction(user, amount);

        //Executes a transtaction and adds it to the Transactions.txt
        public void ExecuteTransaction(Transaction transaction)
        {
            transaction.Execute();
            Transactions.Add(transaction);

            if (transaction.User.Balance < 50)
                UserBalanceWarning(transaction.User);

            using StreamWriter file = File.AppendText(TransactionsPath.FilePath);
            file.WriteLine(transaction);

        }

        //.exists and .find checks and finds by ID in lists
        public Product GetProductByID(int id)
        {
            Product product = Products.SingleOrDefault(p => p.ID == id);
            if(product == null)
                throw new ProductNotFoundException(id);
            return product;
        }

        public IEnumerable<User> GetUsers(Func<User,bool> predicate) => Users.Where(predicate);

        public User GetUserByUsername(string username)
        {
            User user = Users.SingleOrDefault(u => u.UserName == username);
            if (user == null)
                throw new UsernameNotFoundException(username);
            return user;

        }

        //.Take to get from list and .Reverse to get the last
        public IEnumerable<Transaction> GetTransactions(User user, int count)
        {
            return Transactions.Where(x => x.User == user).Take(count).Reverse();
        }

        //.where is from system.linq
        public IEnumerable<Product> ActiveProducts
        {
            get => Products.Where(x => x.Active == true);
        }

        public event Action<User> UserBalanceWarning;

    }
}
