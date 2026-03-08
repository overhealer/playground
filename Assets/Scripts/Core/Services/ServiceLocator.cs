using Assets.Scripts.Game.Services;
using System;
using System.Collections.Generic;

namespace playground.Assets.Scripts.Core.Services
{
    public class ServiceLocator :
            NoMonoSingleton<ServiceLocator>
    {
        public Dictionary<Type, IService> Services => services;
        private static Dictionary<Type, IService> services = new Dictionary<Type, IService>();

        public void Add(Type type, IService service)
        {
            services.Add(type, service);
        }

        public TService Get<TService>() where TService : class, IService
        {
            return services[typeof(TService)] as TService;
        }

        public bool IsServiceExist<TService>(Type key) where TService : class, IService
        {
            return services.ContainsKey(key);
        }
    }
}