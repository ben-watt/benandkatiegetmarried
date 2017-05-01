using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace benandkatiegetmarried
{
    class Program
    {
        static void Main(string[] args)
        {
            var port = "8080";
            var url = $"http://*:{port}";
            ManualResetEvent exit = new ManualResetEvent(false);

            Console.CancelKeyPress += (o,e) => {
                e.Cancel = true;
                exit.Set();         
            };

            using (WebApp.Start<StartUp>(url))
            {
                Console.WriteLine("Starting Server...");
                Console.WriteLine("Press Ctrl + C to exit");
                exit.WaitOne();
                Console.WriteLine("Exiting");
            }
        }
    }
}
