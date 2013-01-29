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
                System.Console.WriteLine(ConnectionStringManager.ConnectionStrings["DB206.NAP"].ConnectionString);

                Thread.Sleep(2000);

            }
        }


    }
}
