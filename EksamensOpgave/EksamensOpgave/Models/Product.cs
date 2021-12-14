using System;

namespace EksamensOpgave.Models
{
    public class Product : IComparable<Product>
    {
        public int ID { get; }
        private static int _ID = 1;

        public string Name { get; }

        public int Price { get; }

        public virtual bool Active { get; set; }

        public bool CanBeBoughtOnCredit { get; set; }


        public Product(string name, int price, bool active)
        {
            ID = _ID++;
            Name = name;
            Price = price;
            Active = active;
            CanBeBoughtOnCredit = false;
        }

        public override string ToString()
        {
            string str = $"{ID} {Name} - {(double) Price / 100} kr";
            return str;
        }

        public int CompareTo(Product obj) => this.ID - obj.ID;
    }
}
