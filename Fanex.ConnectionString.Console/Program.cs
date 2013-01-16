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

            var temp = Test["tin5"];

            while (true)
            {
                System.Console.WriteLine(ConnectionStringManager.GetConnectionString("DB206.Main"));

                Thread.Sleep(2000);
                
            }
        }

        static ConnectionStringSettingsCollection Test
        {
            get
            {
                var collection = new ConnectionStringSettingsCollection();

                for (int i = 0; i < 10; i++)
                {
                    var item = new ConnectionStringSettings
                    {
                        Name = "tin" + i.ToString(),
                        ConnectionString = "connection" + i.ToString(),                        
                    };
                    collection.Add(item);
                }

                return collection;
            }
        }
    }
}
