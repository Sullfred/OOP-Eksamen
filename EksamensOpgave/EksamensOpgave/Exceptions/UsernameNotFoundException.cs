using System;
namespace EksamensOpgave.Exceptions
{
    public class UsernameNotFoundException : Exception
    {
        public UsernameNotFoundException(string username)
            :base($"A user with the username: {username} does currently not exist")
        {
        }
    }
}
