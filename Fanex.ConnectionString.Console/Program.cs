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
            ConnectionString service = ConnectionString.InstanceCreation();
            
            while (true)
            {
                System.Console.WriteLine(service.GetConnectionString("DB206.Main"));
                
                Thread.Sleep(2000);
            }           
        }
    }
}
