using System;
using EksamensOpgave.Exceptions;

namespace EksamensOpgave.Models
{
    public class BuyTransaction : Transaction
    {
        Product Product { get; }

        public BuyTransaction(User user, Product product) : base(user, product.Price)
        {
            Product = product;
        }

        public override void Execute()
        {
            if (User.Balance < Product.Price)
                throw new InsufficientCreditsException(User, Product);
            else if (Product.Active == false)
                throw new InactiveProductException(Product.Name);
            else
                User.Balance -= Product.Price;
            
        }

        public override string ToString()
        {
            string str = $"{ID}: User {User.UserName} bought {Product.Name} at {Date} for {Product.Price}";
            return str;
        }
    }
}
