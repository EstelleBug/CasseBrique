using System;
using System.Collections.Generic;

namespace Services
{
    public static class ServiceLocator
    {
        private static Dictionary<Type, object> _services = new Dictionary<Type, object>();

        public static void Register<T>(object service)
        {
            _services[typeof(T)] = service; // _services.Add(typeof(T), service);
        }

        public static T Get<T>()
        {
            if (_services.ContainsKey(typeof(T))) return (T)_services[typeof(T)];
            else return default(T);
        }
    }
}

