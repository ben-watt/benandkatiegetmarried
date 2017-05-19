using benandkatiegetmarried.DAL.BaseCommands;
using benandkatiegetmarried.DAL.BaseQueries;
using Nancy;
using Nancy.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace benandkatiegetmarried.Modules
{
    public abstract class CrudModule<TEntity, TKey> : NancyModule
    {
        private ICrudQueries<TEntity, TKey> _queries;
        private ICrudCommands<TEntity, TKey> _commands;

        protected CrudModule(string modulePath
            , ICrudQueries<TEntity, TKey> queries
            , ICrudCommands<TEntity, TKey> commands) : base(modulePath)
        {
            _queries = queries;
            _commands = commands;
            
            Get["/"] = _ => GetAll();
            Get["/{id}"] = p => GetById(p.Id);
            Post["/"] = _ => Create();
            Put["/"] = _ => Update();
            Delete["/{id}"] = p => Remove(p.Id); 
        }

        private dynamic GetAll()
        {
           return _queries.GetAll();
        }

        private dynamic GetById(dynamic Id)
        {
            return _queries.GetById(Id);
        }

        private dynamic Create()
        {
            var model = this.Bind<IEnumerable<TEntity>>();
            _commands.Create(model);
            return HttpStatusCode.OK;
        }

        private dynamic Remove(dynamic Id)
        {
            Guid outId;
            if(Guid.TryParse(Id, out outId))
            {
                _commands.Remove(Id);
                return HttpStatusCode.NoContent;
            }
            return HttpStatusCode.BadRequest;
        }

        private dynamic Update()
        {
            var model = this.Bind<IEnumerable<TEntity>>();
            _commands.Update(model);
            return HttpStatusCode.NoContent;
        }
    }
}
