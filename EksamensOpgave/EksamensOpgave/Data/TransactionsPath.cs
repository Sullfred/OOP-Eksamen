using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EksamensOpgave.Data
{
    public class TransactionsPath
    {
        //Gets path to Transactions.txt (Used in Stregsystem.cs to append new transactions)
        public string FilePath = Path.Combine(Directory.GetCurrentDirectory(), "Transactions.txt");
    }
}