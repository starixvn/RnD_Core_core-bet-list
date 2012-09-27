using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Configuration;

namespace TestMSMQ
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var machineName = ConfigurationManager.AppSettings["machineName"].ToString();
                var privateQueues = MessageQueue.GetPrivateQueuesByMachine(machineName);
                Console.WriteLine("Queues in machine: " + machineName);
                foreach (var queue in privateQueues)
                {
                    Console.WriteLine("------------------------------------");
                    Console.WriteLine("QueueName: " + queue.QueueName);                 
                    Console.WriteLine("FormatName: " + queue.FormatName);
                   // Console.WriteLine("Formatter: " + queue.Formatter.ToString());
                    Console.WriteLine("Path: " + queue.Path.ToString());
                    Console.WriteLine("CanRead : " + queue.CanRead);
                    Console.WriteLine("CanWrite : " + queue.CanWrite);

                    
                }


                Console.WriteLine("Write message to queue: " + privateQueues[0].Path);
                var msQueue = new MessageQueue(@"FormatName:DIRECT=OS:devsrv-201\\private$\\entlib");
                
               // msQueue.Category = "Anders-Test-Category";
                
               // msQueue.Label = "Anders-Test-Label";
                
                
           msQueue.Send("Hello world " + DateTime.Now.ToString());
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
            
        }
    }
}
