﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetaPoco;

namespace benandkatiegetmarried.DAL.Guest.Commands
{
    public class GuestQueries : BaseQueries.CrudQueries<Models.Guest, Guid>, IGuestQueries
    {
        public GuestQueries(IDatabase db) : base(db)
        {
        }
    }
}
