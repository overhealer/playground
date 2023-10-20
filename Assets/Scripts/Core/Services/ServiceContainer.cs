using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace playground
{
    public class ServiceContainer : NoMonoSingleton<ServiceContainer>
    {
        private static Dictionary<Type, IService> _services = new Dictionary<Type, IService>();

        public void Set<TService>(TService service) where TService : IService
        {
            _services.Add(typeof(TService), service);
        }

        public TService Get<TService>() where TService : class, IService
        {
            return _services[typeof(TService)] as TService;
        }
    }
}
