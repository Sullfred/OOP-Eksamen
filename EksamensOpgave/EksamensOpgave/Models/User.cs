using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using EksamensOpgave.Exceptions;

namespace EksamensOpgave.Models
{
    public class User : IComparable<User>
    {
        public int ID { get; }
        private static int _ID = 0;

        public string FirstName { get; }

        public string LastName { get; }

        //Regex used to validate username
        //It reads the string from start, and only allows letters from a-z and 0-9. does not allow A-Z
        public string UserName { get; }
        private Regex validateUserName = new Regex(@"^[a-z0-9_]+$");


        public int Balance { get; set; }

        //Regex used to validate email
        //It reads the string from start and allows a-z, A-Z, 0-9, ".", "_" and "-". The en checks for a @.
        //After the @ it allows a-z, A-Z, 0-9, "." and "-". it then checks for a "." followed by letters in a-z and A-Z
        //[^] means not
        public string Email { get; }
        private Regex validateEmail = new Regex(@"^[a-zA-Z0-9._-]+@[^-.][a-zA-Z0-9.-]+\.[a-zA-Z][^.-]+$");


        public User(string firstname, string lastname, string username, int balance, string email)
        {
            ID = _ID++;
            FirstName = firstname;
            LastName = lastname;

            if (!validateUserName.IsMatch(username))
                throw new InvalidUsername(username);

            UserName = username;


            Balance = balance;

            if (!validateEmail.IsMatch(email))
                throw new InvalidEmail(email);

            Email = email;

        }

        public int CompareTo(User obj) => this.ID - obj.ID;

        public override string ToString()
        {
            string str = $"{FirstName} {LastName} ({Email})";
            return str;
        }

        public override int GetHashCode() => ID.GetHashCode();


    }
}
