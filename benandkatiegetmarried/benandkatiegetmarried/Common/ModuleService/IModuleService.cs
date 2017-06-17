using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace benandkatiegetmarried.Common.ModuleService
{
    public interface IModuleService
    {
        IEnumerable<INancyModule> GetAllModules();
    }
}
