using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetaPoco;
using System.Configuration;

namespace benandkatiegetmarried.DAL
{
    public static class WeddingDatabaseBuilder
    {
        public static IDatabase Default()
        {
            var conn = ConfigurationManager.ConnectionStrings["postgres"].ConnectionString;
            return DatabaseConfiguration.Build()
                .UsingProvider<PostgreSQLDatabaseProvider>()
                .UsingConnectionString(conn)
                .UsingIsolationLevel(System.Data.IsolationLevel.ReadCommitted)
                .Create();
        }
    }
}
