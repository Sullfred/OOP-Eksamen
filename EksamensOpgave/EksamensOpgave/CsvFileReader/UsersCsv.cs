using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using EksamensOpgave.Models;
using EksamensOpgave.Exceptions;

namespace EksamensOpgave.CsvFileReader
{
    public class UsersCsv
    {
        public string FilePath = Path.Combine(Directory.GetCurrentDirectory(), "users.csv");


        public List<User> GetUsersFromFile(string file)
        {
            List<User> users = new List<User>();
            foreach(string line in File.ReadLines(file).Skip(1))
            {
                string[] fileInfo = line.Split(',');

                //int ID = Int32.Parse(fileInfo[0]);
                string FirstName = fileInfo[1];
                string LastName = fileInfo[2];
                string UserName = fileInfo[3];
                int Balance = Int32.Parse(fileInfo[4]);
                string Email = fileInfo[5];

                User user = new User(FirstName, LastName, UserName, Balance, Email);
                users.Add(user);
            }

            return users;
        }
        
    }
}
