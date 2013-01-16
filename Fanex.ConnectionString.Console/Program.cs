using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fanex.ConnectionString;
using System.Threading;

namespace Fanex.ConnectionString.Console
{
    class Program
    {
        static void Main(string[] args)
        {           
            while (true)
            {
                System.Console.WriteLine(ConnectionStringManager.GetConnectionString("DB206.Main"));
                
                Thread.Sleep(2000);
            }           
        }
    }
}
