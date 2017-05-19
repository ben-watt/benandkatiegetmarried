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
using benandkatiegetmarried.Models;

namespace benandkatiegetmarried.Modules
{
    public class WeddingModule : CrudModule<Wedding, Guid>
    {

        private IWeddingCommands _commands;
        private IWeddingQueries _queries;

        public WeddingModule(IWeddingCommands weddingCommands
            , IWeddingQueries weddingqueries) : base("weddings", weddingqueries, weddingCommands)
        {
            _queries = weddingqueries;
            _commands = weddingCommands;
        }
    }
}
