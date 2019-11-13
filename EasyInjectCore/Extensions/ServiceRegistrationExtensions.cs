using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Grax32.EasyInjectCore
{
    public static class ServiceRegistrationExtensions
    {
        public static void RegisterAssemblyByAttribute(this IServiceCollection services, Assembly assembly)
        {
            var allTypes = assembly.DefinedTypes
                .Select(v => new RegistrationTypeInfo
                {
                    Type = v,
                    Register = v.GetCustomAttribute<RegisterAttribute>()
                })
                .ToList();
            HandleRegisterAttribute(services, allTypes);
        }
        private class RegistrationTypeInfo
        {
            public Type Type { get; set; }
            public RegisterAttribute Register { get; set; }
        }
        private static void HandleRegisterAttribute(IServiceCollection services, List<RegistrationTypeInfo> typeList)
        {
            foreach (var typeInfo in typeList.Where(v => v.Register != null))
            {
                var type = typeInfo.Type;
                var registrationType = typeInfo.Register.RegistrationType;
                var registrationScope = typeInfo.Register.Scope;
                ServiceLifetime lifetime;
                switch (registrationScope)
                {
                    case RegistrationScopeType.Transient:
                        lifetime = ServiceLifetime.Transient;
                        break;
                    case RegistrationScopeType.SingleInstance:
                        lifetime = ServiceLifetime.Singleton;
                        break;
                    case RegistrationScopeType.PerRequest:
                        lifetime = ServiceLifetime.Scoped;
                        break;
                    default:
                        throw new ServiceRegistrationException("Unknown scope type");
                }
                if (!registrationType.HasAnyFlag())
                {
                    throw new ServiceRegistrationException("Unset or unknown registration type");
                }
                if (registrationType.HasFlag(RegistrationType.ImplementedInterfaces))
                {
                    foreach (var iface in type.GetInterfaces())
                    {
                        services.Add(new ServiceDescriptor(iface, type, lifetime));
                    }
                }
                if (registrationType.HasFlag(RegistrationType.Self))
                {
                    services.Add(new ServiceDescriptor(type, type, lifetime));
                }
            }
        }
    }
}
