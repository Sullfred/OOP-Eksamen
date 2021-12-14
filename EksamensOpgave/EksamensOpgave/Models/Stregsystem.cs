using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using EksamensOpgave.Exceptions;
using EksamensOpgave.Interface;
using EksamensOpgave.CsvFileReader;

namespace EksamensOpgave.Models
{
    public class Stregsystem : IStregsystem
    {
        public List<Transaction> Transactions { get; }
        public List<Product> Products { get; }
        public List<User> Users { get; }

        public UsersCsv UsersCsv = new UsersCsv();
        public ProductsCsv ProductsCsv = new ProductsCsv();

        public Stregsystem()
        {
            Users = UsersCsv.GetUsers(UsersCsv.FilePath);
            Products = ProductsCsv.GetProducts(ProductsCsv.FilePath);

        }

        public BuyTransaction BuyProduct(User user, Product product) => new BuyTransaction(user, product);

        public InsertCashTransaction AddCreditsToAccount(User user, int amount) => new InsertCashTransaction(user, amount);

        public void ExecuteTransaction(Transaction transaction)
        {
            transaction.Execute();
            Transactions.Add(transaction);
        }

        //.exists and .find checks and finds by ID ind list (found on docs.microsoft)
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
