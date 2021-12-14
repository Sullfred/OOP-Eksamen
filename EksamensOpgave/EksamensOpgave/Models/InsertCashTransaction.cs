using System;
namespace EksamensOpgave.Models
{
    public class InsertCashTransaction : Transaction
    {
        public InsertCashTransaction(User user, int amount) : base(user, amount)
        {

        }

        public override void Execute() => User.Balance += Amount;

        public override string ToString()
        {
            string str = $"{ID}: User {User.UserName} har inserted {Amount} on his balance at {Date}";
            return str;
        }

    }
}
