using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fanex.ConnectionString.DataAccess;
using System.Threading;

namespace Fanex.ConnectionString.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            IConnnectionString connectionString = ConnectionStringManager.InstanceCreation();
            
            while (true)
            {
                System.Console.WriteLine(connectionString.GetConnectionString("DB206.Main"));
                
                Thread.Sleep(2000);
            }           
        }
    }
}
