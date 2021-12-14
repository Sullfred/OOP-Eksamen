using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using EksamensOpgave.Models;
using EksamensOpgave.Interface;
using EksamensOpgave.UI;


namespace EksamensOpgave
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("test");

            IStregsystem stregsystem = new Stregsystem();
            IStregsystemUI stregsystemUI = new StregsystemCLI(stregsystem);
            StregsystemController controller = new StregsystemController(stregsystem, stregsystemUI);

            controller.StregsystemUI.Start();
            
        }
    }
}
