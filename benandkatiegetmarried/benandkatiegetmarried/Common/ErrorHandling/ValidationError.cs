﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;

namespace benandkatiegetmarried.Common.ErrorHandling
{
    public class ValidationError : Error
    {
        public IList<ValidationFailure> Errors { get; set; }
    }
}
