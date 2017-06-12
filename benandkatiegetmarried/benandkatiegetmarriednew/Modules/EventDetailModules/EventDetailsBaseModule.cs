﻿using benandkatiegetmarried.Common.Validation;
using benandkatiegetmarried.DAL.BaseCommands;
using benandkatiegetmarried.DAL.BaseQueries;
using FluentValidation;
using FluentValidation.Results;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;
using Nancy.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace benandkatiegetmarried.Modules
{
    public abstract class EventDetailsBaseModule<TEntity, TKey> 
        : NancyModule where TEntity : class
    {
        private IEventCrudQueries<TEntity, TKey> _queries;
        private ICrudCommands<TEntity, TKey> _commands;
        private IValidator<TEntity> _validator;
        private ISession _session;

        protected EventDetailsBaseModule(string modulePath
            , IEventCrudQueries<TEntity, TKey> queries
            , ICrudCommands<TEntity, TKey> commands
            , IValidator<TEntity> validator
            , ISession session) : base("api/events/{eventId}/" + modulePath)
        {
            this.RequiresAuthentication();
            this.RequiresClaims("User");

            _queries = queries;
            _commands = commands;
            _validator = validator;
            _session = session;

            Get["/"] = _ => GetAll();
            Get["/{id}"] = p => GetById(p.Id);
            Post["/"] = _ => Create();
            Put["/"] = _ => Update();
            Delete["/{id}"] = p => Remove(p.Id); 
        }

        private dynamic GetAll()
        {
            var userEvents = GetCurrentUsersEventsFromSession();
            return _queries.GetAll(userEvents);
        }

        private IEnumerable<TKey> GetCurrentUsersEventsFromSession()
        {
            return (IEnumerable<TKey>)_session["user-eventIds"];
        }

        private dynamic GetById(dynamic Id)
        {
            var userEvents = (IEnumerable<TKey>)_session["user-events"];
            return _queries.GetById(Id, userEvents);
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