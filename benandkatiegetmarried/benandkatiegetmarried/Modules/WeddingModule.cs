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
        private IWeddingQueries _queries;

        public WeddingModule(IWeddingCommands weddingCommands
            , IWeddingQueries weddingqueries) : base("weddings")
        {
            _queries = weddingqueries;
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
                _commands.Update(wedding);
                return HttpStatusCode.NoContent;
            }
            return HttpStatusCode.BadRequest;
        }

        private dynamic GetById(dynamic id)
        {
            Guid weddingId;
            if (Guid.TryParse(id, out weddingId))
            {
                return _queries.GetById(weddingId);
            }
            return HttpStatusCode.BadRequest;
        }

        private dynamic GetAll()
        {
            return _queries.GetAll();
        }

        private dynamic PostWedding()
        {
            var request = this.Bind<Models.Wedding>();
            _commands.Create(request);
            return HttpStatusCode.NoContent;
        }
    }
}
