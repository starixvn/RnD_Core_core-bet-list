using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fanex.ConnectionString;
using System.Threading.Tasks;
using System.Threading;
using System.Configuration;

namespace Fanex.ConnectionString.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            DoAction();
        }

        static void DoAction()
        {
            while (true)
            {
                var collection = ConnectionStringManager.ConnectionStrings;

                //var config = ConfigurationManager.ConnectionStrings["abcd"];

                //System.Console.WriteLine(config);
                //System.Console.WriteLine(collection);

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
