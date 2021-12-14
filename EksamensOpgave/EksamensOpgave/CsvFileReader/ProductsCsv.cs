using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using EksamensOpgave.Models;
using EksamensOpgave.Exceptions;

namespace EksamensOpgave.CsvFileReader
{
    public class ProductsCsv
    {
        public string FilePath = Path.Combine(Directory.GetCurrentDirectory(), "products.csv");

        public List<Product> GetProductsFromFile(string file)
        {
            List<Product> products = new List<Product>();
            foreach(string line in File.ReadLines(file).Skip(1))
            {
                string[] fileInfo = line.Split(';');

                //Regex to remove html tags
                string Name = Regex.Replace(fileInfo[1], @"<[^>]+>", m => "").Replace("\"", "");
                int Price = Int32.Parse(fileInfo[2]);
                bool Active = fileInfo[3] == "1";

                Product product = new Product(Name, Price, Active);
                products.Add(product);
            }

            return products;
        }
    }
}
