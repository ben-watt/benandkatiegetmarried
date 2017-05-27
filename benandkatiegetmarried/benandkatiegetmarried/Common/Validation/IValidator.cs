using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace benandkatiegetmarried.Common.Validation
{
    public interface IValidator<T> where T : class
    {
        ValidationResult Validate(T entity);
    }
}
