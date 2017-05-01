using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace benandkatiegetmarried.DAL
{
    public abstract class QueryHandler<TQuery, TResult> : IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        protected IDbContext db { get; set; }
        public QueryHandler(IDbContext db)
        {
            this.db = db;
        }
        public abstract TResult Handle(TQuery query);
    }
}
