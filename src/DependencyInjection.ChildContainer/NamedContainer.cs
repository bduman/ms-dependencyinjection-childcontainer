using System;

namespace DependencyInjection.ChildContainer
{
    internal class NamedContainer : INamedContainer
    {
        public string Name { get; }
        public IServiceProvider ServiceProvider { get; }

        public NamedContainer(string name, IServiceProvider serviceProvider)
        {
            Name = name;
            ServiceProvider = serviceProvider;
        }
    }
}