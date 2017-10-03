using Nancy.Bootstrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace benandkatiegetmarried
{
    public static class Register
    {
        public static TypeRegistration PerRequest<TContract, TImplementation>()
        {
            return new TypeRegistration(typeof(TContract), typeof(TImplementation), Lifetime.PerRequest);
        }
        public static TypeRegistration PerRequest(Type TContract, Type TImplementation)
        {
            return new TypeRegistration(TContract, TImplementation, Lifetime.PerRequest);
        }
        public static TypeRegistration Singleton<TContract, TImplementation>()
        {
            return new TypeRegistration(typeof(TContract), typeof(TImplementation), Lifetime.Singleton);
        }

        //public static IEnumerable<TypeRegistration> RegisterAllTypes()
        //{
        //    var typeRegistrations = new List<TypeRegistration>();
        //    Assembly here = Assembly.GetExecutingAssembly();
        //    foreach (var classInAssmbly in here.GetTypes())
        //    {
        //        if (classInAssmbly.IsClass == true)
        //        {
        //            var contract = classInAssmbly;
        //            var implementation = contract.GetInterface("I" + contract.Name);
        //            if (implementation != null)
        //            {
        //                typeRegistrations.Add(new TypeRegistration(contract, implementation, Lifetime.PerRequest));
        //            }

        //        };
        //    }

        //    return typeRegistrations;

        //}
    }
}
