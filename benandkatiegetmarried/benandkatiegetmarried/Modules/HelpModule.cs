using benandkatiegetmarried.Common.ModuleService;
using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace benandkatiegetmarried.Modules
{
    public class HelpModule : NancyModule
    {
        private IModuleService _modules;

        public HelpModule(IModuleService modules) 
            : base("api/help")
        {
            _modules = modules;

            Get["/"] = _ => View["HelpApi", _modules.GetAllModules()];
        }
    }
}
