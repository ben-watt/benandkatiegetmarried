using benandkatiegetmarried.Common.JsonSerialization;
using Nancy;
using Nancy.Responses;
using Nancy.Serialization.JsonNet;
using System;
using FluentValidation.Results;
using System.Collections.Generic;

namespace benandkatiegetmarried.Common.ErrorHandling
{
    internal class ErrorResponse : JsonResponse
    {
        private ErrorResponse(object model) 
            : base(model, new JsonNetSerializer()) {}

        public static ErrorResponse FromError(Error error)
        {
            return new ErrorResponse(error);
        }
        public static ErrorResponse FromException(Exception e)
        {
            var error = new Error() { ErrorMessage = e.Message, ErrorDetail = e.ToString() };
            return new ErrorResponse(error);
        }

        internal static dynamic ValidationError(IList<ValidationFailure> errors)
        {
            var error = new ValidationError() { ErrorMessage = "Validation Failure", Errors = errors };
            return new ErrorResponse(error);
        }
    }
}