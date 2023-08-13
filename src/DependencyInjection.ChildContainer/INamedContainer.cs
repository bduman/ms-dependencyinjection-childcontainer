using System;

namespace DependencyInjection.ChildContainer
{
    internal interface INamedContainer
    {
        string Name { get; }
        IServiceProvider ServiceProvider { get; }
    }
}