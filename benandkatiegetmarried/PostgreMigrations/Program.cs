using DbUp;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PostgreMigrations
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString =
                args.FirstOrDefault() 
                ?? @"Server=localhost;Database=weddingApp;Trusted_Connection=yes;";

            EnsureDatabase.For.SqlDatabase(connectionString);

            var upgrader = DeployChanges.To
                .SqlDatabase(connectionString)
                .WithScriptsAndCodeEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                .LogToConsole()
                .Build();

            var result =  upgrader.PerformUpgrade();

            if (!result.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(result.Error);
                Console.ResetColor();
                Console.ReadLine();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Success!");
            Console.ResetColor();
            return;
        }
    }
}
