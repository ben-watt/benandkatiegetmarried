using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace benandkatiegetmarried.Common.Validation
{
    public interface IValidator<T>
    {
        ValidationResult Validate(T entity);
    }
}
