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
    }
}
