using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjection.ChildContainer.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection CreateChildCollection(this IServiceCollection parentCollection, string name)
        {
            IServiceCollection result = new ServiceCollection();

            parentCollection.AddSingleton<INamedContainer>(parentServiceProvider =>
            {

                foreach (ServiceDescriptor serviceDescriptor in parentCollection)
                {
                    Type serviceType = serviceDescriptor.ServiceType;
                    bool serviceTypeAlreadyRegistered = result.Any(s => s.ServiceType == serviceType);

                    if (serviceTypeAlreadyRegistered)
                    {
                        string message = $"Service type already registered to parent service provider. " +
                                         $"[ChildName: {name} ServiceType:{serviceType}]";
                        throw new InvalidOperationException(message);
                    }

                    ServiceLifetime serviceLifetime = serviceDescriptor.Lifetime;
                    ServiceDescriptor mirrorServiceDescriptor = new ServiceDescriptor(serviceType,
                        _ => parentServiceProvider.GetService(serviceType),
                        serviceLifetime);

                    result.Add(mirrorServiceDescriptor);
                }

                result.AddSingleton<IParentServiceProvider>(new ParentServiceProvider(parentServiceProvider));
                IServiceProvider childServiceProvider = result.BuildServiceProvider();

                return new NamedContainer(name, childServiceProvider);
            });

            return result;
        }
    }
}