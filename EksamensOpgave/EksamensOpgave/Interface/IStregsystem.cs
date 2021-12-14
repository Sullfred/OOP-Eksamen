using System;
using System.Collections.Generic;
using EksamensOpgave.Models;
using EksamensOpgave.Exceptions;

namespace EksamensOpgave.Interface
{
    public interface IStregsystem
    {
        IEnumerable<Product> ActiveProducts { get; }
        BuyTransaction BuyProduct(User user, Product product);
        InsertCashTransaction AddCreditsToAccount(User user, int amount);
        Product GetProductByID(int id);
        IEnumerable<Transaction> GetTransactions(User user, int count);
        IEnumerable<User> GetUsers(Func<User, bool> predicate);
        User GetUserByUsername(string username);
        void ExecuteTransaction(Transaction transaction);
        event Action<User> UserBalanceWarning;
    }
}
