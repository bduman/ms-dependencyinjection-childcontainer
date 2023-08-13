using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjection.ChildContainer.Extensions
{
    public static class ServiceProviderExtensions
    {
        public static IServiceProvider GetChildServiceProvider(this IServiceProvider parentProvider, string name)
        {
            IEnumerable<INamedContainer> namedContainers = parentProvider.GetServices<INamedContainer>();
            INamedContainer childContainer = namedContainers.First(c => c.Name == name);

            IServiceProvider result = childContainer.ServiceProvider;
            return result;
        }
    }
}