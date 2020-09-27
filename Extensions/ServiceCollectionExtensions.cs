using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NetCoreOptionSample.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSingletonConfig<TConfig>(this IServiceCollection services, IConfiguration section) where TConfig : class
        {
            services.AddSingleton(p => BindConfigInstance<TConfig>(section));
            return services;
        }

        public static IServiceCollection AddScopedConfig<TConfig>(this IServiceCollection services, IConfiguration section) where TConfig : class
        {
            services.AddScoped(p => BindConfigInstance<TConfig>(section));
            return services;
        }

        public static IServiceCollection AddTransientConfig<TConfig>(this IServiceCollection services, IConfiguration section) where TConfig : class
        {
            services.AddTransient(p => BindConfigInstance<TConfig>(section));
            return services;
        }

        public static IServiceCollection AddConfig<TConfig>(this IServiceCollection services, IConfiguration section, ServiceLifetime lifetime) where TConfig : class
        {
            switch (lifetime)
            {
                case ServiceLifetime.Singleton:
                    services.AddSingleton(p => BindConfigInstance<TConfig>(section));
                    break;
                case ServiceLifetime.Scoped:
                    services.AddScoped(p => BindConfigInstance<TConfig>(section));
                    break;
                case ServiceLifetime.Transient:
                    services.AddTransient(p => BindConfigInstance<TConfig>(section));
                    break;
                default:
                    throw new UnexpectedEnumValueException($"Value of enum {typeof(ServiceLifetime)}: {nameof(ServiceLifetime)} is not supported.");
            }

            return services;
        }

        private static TConfig BindConfigInstance<TConfig>(IConfiguration section) where TConfig : class
        {
            var instance = Activator.CreateInstance<TConfig>();
            section.Bind(instance);
            return instance;
        }
    }

    public class UnexpectedEnumValueException : Exception
    {
        public UnexpectedEnumValueException(string message) : base(message)
        {
            throw new NotImplementedException();
        }
    }

}