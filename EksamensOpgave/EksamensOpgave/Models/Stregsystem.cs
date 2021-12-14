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
        private List<Transaction> Transactions;
        private List<Product> Products;
        public List<User> Users;

        // **FileReading classes**
        private UsersCsv UsersCsv = new UsersCsv();
        private ProductsCsv ProductsCsv = new ProductsCsv();
        private TransactionsPath TransactionsPath = new TransactionsPath();


        // **constructor**
        public Stregsystem()
        {
            Users = UsersCsv.GetUsersFromFile(UsersCsv.FilePath);
            Products = ProductsCsv.GetProductsFromFile(ProductsCsv.FilePath);

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
            if (Products.Exists(x => x.ID != id))
                throw new ProductNotFoundException(id);

            return Products.Find(x => x.ID == id);
        }

        public IEnumerable<User> GetUsers(Func<User, bool> predicate) => Users.Where(predicate);

        public User GetUserByUsername(string username)
        {
            if (Users.Exists(x => x.UserName != username))
                throw new UsernameNotFoundException(username);

            return Users.Find(x => x.UserName == username);
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
