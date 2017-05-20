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
using benandkatiegetmarried.Common.Validation;

namespace benandkatiegetmarried.Modules
{
    public class WeddingModule : CrudModule<Wedding, Guid>
    {

        private IWeddingCommands _commands;
        private IWeddingQueries _queries;

        public WeddingModule(IWeddingCommands weddingCommands
            , IWeddingQueries weddingqueries
            , IValidator<Wedding> weddingValidator) 
            : base("weddings", weddingqueries, weddingCommands, weddingValidator)
        {
            _queries = weddingqueries;
            _commands = weddingCommands;
        }
    }
}
