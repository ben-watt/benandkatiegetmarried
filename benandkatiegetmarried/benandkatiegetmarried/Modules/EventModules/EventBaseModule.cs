using benandkatiegetmarried.Common.ModuleExtensions;
using benandkatiegetmarried.Common.Validation;
using benandkatiegetmarried.DAL.BaseCommands;
using benandkatiegetmarried.DAL.BaseQueries;
using FluentValidation;
using FluentValidation.Results;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;
using Nancy.Session;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace benandkatiegetmarried.Modules
{
    public abstract class EventBaseModule<TEntity, TKey> 
        : NancyModule where TEntity : class where TKey : struct
    {
        private IEventCrudQueries<TEntity, TKey> _queries;
        private ICrudCommands<TEntity, TKey> _commands;
        private IValidator<TEntity> _validator;
        private ISession _session;

        protected IEnumerable<TKey> _userEventIds => (IEnumerable<TKey>)_session["user-eventIds"];

        protected EventBaseModule(string modulePath
            , IEventCrudQueries<TEntity, TKey> queries
            , ICrudCommands<TEntity, TKey> commands
            , IValidator<TEntity> validator
            , ISession session) : base(modulePath)
        {

            this.Before += (ctx) =>
            {
                var con = ctx as NancyContext;
                return null;
            };

            this.After += (ctx) => {
                var con = ctx as NancyContext;
            };

            this.RequiresAuthentication();
            this.RequiresClaims("User");
            _session = session;
            _queries = queries;
            _commands = commands;
            _validator = validator;
            
            Get["/"] = _ => GetAll();
            Get["/{id}"] = p => GetById(p.Id);
            Post["/"] = _ => Create();
            Put["/"] = _ => Update();
            Delete["/{id}"] = p => Remove(p.Id); 
        }

        private dynamic GetAll()
        {
            if (_userEventIds.Count() > 0)
            {
                return _queries.GetAll(_userEventIds);
            }
            return HttpStatusCode.OK;
        }

        private dynamic GetById(dynamic Id)
        {
            if(_userEventIds.Count() > 0)
            {
                return _queries.GetById(Id, _userEventIds);
            }
            return HttpStatusCode.BadRequest;
            
        }

        private dynamic Create()
        {
            var model = this.Bind<IEnumerable<TEntity>>();
            var result = ValidateModel(model);
            if (result.IsValid)
            {
                _commands.Create(model);
                return HttpStatusCode.OK;
            }
            return Negotiate.WithModel(result.Errors)
                .WithStatusCode(HttpStatusCode.BadRequest);
        }

        private ValidationResult ValidateModel(IEnumerable<TEntity> model)
        {
            ValidationResult result = new ValidationResult();
            foreach (var item in model)
            {
                result = _validator.Validate(item);
                if (!result.IsValid)
                {
                    return result;
                }
            }
            return result;
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
            var result = this.ValidateModel(model);
            if (result.IsValid)
            {
                _commands.Update(model);
                return HttpStatusCode.NoContent;
            }
            return Negotiate.WithModel(result.Errors)
                .WithStatusCode(HttpStatusCode.BadRequest);
        }
    }
}
