using System;
namespace EksamensOpgave.Models
{
    public abstract class Transaction
    {
        public int ID { get; }
        private static int _ID = 1;

        public User User { get; }

        public DateTime Date { get; }

        public int Amount { get; }

        // **constructor**
        public Transaction(User user, int amount)
        {
            ID = _ID++;

            User = user;

            Amount = amount;

            Date = DateTime.Now;

        }

        // **ToString**
        public override string ToString()
        {
            string str = $"{ID}: made a transaction at {Date}. {User} made the transaction with the amount: {Amount}.";
            return str;
        }

        public abstract void Execute();
    }
}
