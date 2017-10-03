using benandkatiegetmarried.Common.Security;
using DbUp;
using System;
using System.Collections.Generic;
using System.Configuration;
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

            var conn = "database";

            if (args.Count() > 0)
                conn = args[0];

            var connectionString = ConfigurationManager.ConnectionStrings[conn].ConnectionString;

            if(connectionString == null)
            {
                throw new ArgumentException("No database connection string provided");
            }

            EnsureDatabase.For.SqlDatabase(connectionString);

            var upgrader = DeployChanges.To
                .SqlDatabase(connectionString)
                .WithScriptsAndCodeEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                .WithVariablesDisabled()
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
