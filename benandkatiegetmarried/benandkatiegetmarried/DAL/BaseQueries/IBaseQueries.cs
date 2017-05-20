﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace benandkatiegetmarried.DAL.BaseQueries
{
    public interface ICrudQueries<T, TKey>
    {
        IEnumerable<T> GetAll();
        T GetById(TKey Id);
    }
}