using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EksamensOpgave.Data
{
    public class TransactionsPath
    {
        public string FilePath = Path.Combine(Directory.GetCurrentDirectory(), "Transactions.txt");
    }
}