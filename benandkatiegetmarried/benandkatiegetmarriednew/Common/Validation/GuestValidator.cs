using benandkatiegetmarried.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace benandkatiegetmarried.Common.Validation
{
    public class GuestValidator : AbstractValidator<Guest>, IValidator<Guest>
    {
        public GuestValidator()
        { 
        }
    }
}
