using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fanex.ConnectionString;
using System.Threading;
using System.Configuration;

namespace Fanex.ConnectionString.Console
{
    class Program
    {
        static void Main(string[] args)
        {

            while (true)
            {
                var collection = ConnectionStringManager.ConnectionStrings;

                foreach (ConnectionStringSettings item in collection)
                {
                    System.Console.WriteLine("alias:{0},connection:{1}", item.Name, item.ConnectionString);                  
                }
                System.Console.WriteLine("----------");

                Thread.Sleep(5000);

            }
        }


    }
}
