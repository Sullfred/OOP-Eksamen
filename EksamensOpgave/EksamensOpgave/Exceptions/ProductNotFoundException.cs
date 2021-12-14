using System;
namespace EksamensOpgave.Exceptions
{
    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException(int id)
            :base ($"The product with the id: {id} does currently not exist")
        {

        }
    }
}
