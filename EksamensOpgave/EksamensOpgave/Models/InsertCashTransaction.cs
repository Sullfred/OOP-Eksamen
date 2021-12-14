using System;
namespace EksamensOpgave.Models
{
    //Inherets from abstract class Transaction
    public class InsertCashTransaction : Transaction
    {
        public InsertCashTransaction(User user, int amount) : base(user, amount)
        {

        }

        //Add credits to user
        public override void Execute() => User.Balance += Amount;

        // **ToString**
        public override string ToString()
        {
            string str = $"{ID}: User {User.UserName} har inserted {Amount} on his balance at {Date}";
            return str;
        }

    }
}
