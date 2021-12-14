using System;

namespace EksamensOpgave.Exceptions
{
    public class InvalidUsername : Exception
    {
        public InvalidUsername(string name)
            :base(string.Format($"{name} is an invalid username"))
        {

        }
        


    }
}
