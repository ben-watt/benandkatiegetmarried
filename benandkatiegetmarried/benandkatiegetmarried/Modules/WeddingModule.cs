using Nancy;
using Nancy.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using benandkatiegetmarried.DAL.Weddings.Query;
using benandkatiegetmarried.DAL;
using benandkatiegetmarried.DAL.Weddings.Commands;

namespace benandkatiegetmarried.Modules
{
    public class WeddingModule : NancyModule 
    {

        private IWeddingCommands _commands;
        private IWeddingQueries _querys;

        public WeddingModule(IWeddingCommands weddingCommands
            , IWeddingQueries weddingqueries) : base("weddings")
        {
            _querys = weddingqueries;
            _commands = weddingCommands;

            Put["/"] = _ => UpdateWedding();
            Post["/"] = _ => PostWedding();
            Get["/"] = _ => GetAll();
            Get["/{id}"] = p => GetById(p.id);
        }

        private dynamic UpdateWedding()
        {
            var wedding = this.Bind<Models.Wedding>();
            if (wedding != null)
            {
                _commands.UpdateWedding(wedding);
                return HttpStatusCode.NoContent;
            }
            return HttpStatusCode.BadRequest;
        }

        private dynamic GetById(dynamic id)
        {
            var weddingId = id ?? Guid.NewGuid();
            return _querys.GetById(id);
        }

        private dynamic GetAll()
        {
            return _querys.GetAll();
        }

        private dynamic PostWedding()
        {
            var request = this.Bind<Models.Wedding>();
            _commands.Create(request);
            return HttpStatusCode.NoContent;
        }
    }
}
