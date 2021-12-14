using System;
using EksamensOpgave.Models;

namespace EksamensOpgave.Exceptions
{
    public class InsufficientCreditsException : Exception
    {

        public User User { get; }
        public Product Product { get; }

        public InsufficientCreditsException(User user, Product product)
            :base(string.Format($"{user.UserName} tried to buy {product.Name} but has insufficient credits"))
        {

        }
    }
}
