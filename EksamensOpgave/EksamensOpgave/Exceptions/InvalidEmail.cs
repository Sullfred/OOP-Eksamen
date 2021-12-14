using System;

namespace EksamensOpgave.Exceptions
{
    public class InvalidEmail : Exception
    {
        public InvalidEmail(string mail)
            : base(string.Format($"{mail} is an invalid email"))
        {

        }


    }
}
