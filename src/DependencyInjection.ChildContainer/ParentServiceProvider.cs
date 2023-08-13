using System;

namespace DependencyInjection.ChildContainer
{
    internal class ParentServiceProvider : IParentServiceProvider
    {
        private readonly IServiceProvider _inner;

        public ParentServiceProvider(IServiceProvider inner)
        {
            _inner = inner;
        }

        public object GetService(Type serviceType)
        {
            return _inner.GetService(serviceType);
        }
    }
}