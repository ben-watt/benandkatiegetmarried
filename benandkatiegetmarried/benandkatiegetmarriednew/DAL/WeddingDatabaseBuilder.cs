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
            var conn = ConfigurationManager.ConnectionStrings["local"].ConnectionString;
            return DatabaseConfiguration.Build()
                .UsingProvider<SqlServerDatabaseProvider>()
                .UsingConnectionString(conn)
                .UsingIsolationLevel(System.Data.IsolationLevel.ReadUncommitted)
                .UsingDefaultMapper<ConventionMapper>(x =>
                {
                    x.InflectTableName = (In, tableName) => String.Concat("core.", tableName, "s");
                    x.InflectColumnName = (In, col) => col.ToLower();
                })
                .Create();
        }
    }
}
