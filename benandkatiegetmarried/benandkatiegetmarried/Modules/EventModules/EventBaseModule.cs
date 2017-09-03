using benandkatiegetmarried.Common.Security;
using benandkatiegetmarried.DAL.BaseCommands;
using benandkatiegetmarried.DAL.BaseQueries;
using benandkatiegetmarried.DAL.Event;
using FluentValidation;
using FluentValidation.Results;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;
using System;
using System.Collections.Generic;

namespace benandkatiegetmarried.Modules
{
    public abstract class EventBaseModule<TEntity, TKey> 
        : NancyModule where TEntity : Models.Event where TKey : struct
    {
        private IEventCrudQueries<TEntity, TKey> _queries;
        private ICrudCommands<TEntity, TKey> _commands;
        private IValidator<TEntity> _validator;
        private IEventCommands<TEntity> _eventCommands;
        private IIdentity _user;


        public EventBaseModule(string modulePath
            , IEventCrudQueries<TEntity, TKey> queries
            , ICrudCommands<TEntity, TKey> commands
            , IValidator<TEntity> validator
            , IEventCommands<TEntity> eventCommands) : base(modulePath)
        {
            this.Before.AddItemToEndOfPipeline(ctx =>
            {
                _user = (IIdentity)ctx.CurrentUser;
                return null;
            });

            this.RequiresAuthentication();
            this.RequiresClaims("User");

            _queries = queries;
            _commands = commands;
            _validator = validator;
            _eventCommands = eventCommands;

            
            Get["/"] = _ => GetAll();
            Get["/{id}"] = p => GetById(p.Id);
            Post["/"] = _ => Create();
            Put["/"] = _ => Update();
            Delete["/{id}"] = p => Remove(p.Id); 
        }

        private dynamic GetAll()
        {            
            if (this._user != null)
            {
                return _queries.GetAll(_user.Id);
            }
            return HttpStatusCode.BadRequest;
        }

        private dynamic GetById(dynamic Id)
        {
            if(this._user != null)
            {
                return _queries.GetById(Id, _user.Id);
            }
            return HttpStatusCode.BadRequest;
            
        }

        private dynamic Create()
        {
            var model = this.Bind<IEnumerable<TEntity>>();
            var result = ValidateModel(model);
            if (result.IsValid && this._user != null)
            {
                _eventCommands.Create(model, this._user.Id);
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
