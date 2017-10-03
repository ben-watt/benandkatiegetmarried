using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace benandkatiegetmarried.DAL
{
    public class WeddingDatabase : Database, IWeddingDatabase
    {
        public WeddingDatabase() : base(WeddingDatabaseBuilder.Default()) { }
    }
}
