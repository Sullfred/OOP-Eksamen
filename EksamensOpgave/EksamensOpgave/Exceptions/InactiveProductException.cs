using System;
namespace EksamensOpgave.Exceptions
{
    public class InactiveProductException : Exception
    {
        public InactiveProductException(string product)
        : base(string.Format($"The product {product} is currently inactive"))
        {
        }
    }
}
