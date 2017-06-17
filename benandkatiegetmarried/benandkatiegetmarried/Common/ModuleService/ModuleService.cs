using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace benandkatiegetmarried.Common.ModuleService
{
    public class ModuleService : IModuleService
    {
        private IEnumerable<INancyModule> _modules;

        public ModuleService(IEnumerable<INancyModule> modules)
        {
            _modules = modules;
        }

        public IEnumerable<INancyModule> GetAllModules()
        {
            return _modules;
        }
    }
}
