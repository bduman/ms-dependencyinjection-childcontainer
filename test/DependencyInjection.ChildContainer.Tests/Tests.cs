using DependencyInjection.ChildContainer.Extensions;
using DependencyInjection.ChildContainer.Tests.Models;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace DependencyInjection.ChildContainer.Tests;

public class Tests
{
    [Fact]
    public void ParentContainerCreateChildContainer()
    {
        IServiceCollection rootCollection = new ServiceCollection();
        IServiceCollection childCollection = rootCollection.CreateChildCollection("child1");
        childCollection.AddSingleton<IGreeter, Hi>();

        IServiceProvider rootServiceProvider = rootCollection.BuildServiceProvider();
        
        IServiceProvider childServiceProvider = rootServiceProvider.GetChildServiceProvider("child1");
        IGreeter? childGreeter = childServiceProvider.GetService<IGreeter>();
        Assert.IsType<Hi>(childGreeter);
    }

    [Fact]
    public void ChildContainerIsIsolatedFromParent()
    {
        IServiceCollection rootCollection = new ServiceCollection();
        IServiceCollection childCollection = rootCollection.CreateChildCollection("child1");
        childCollection.AddSingleton<IGreeter, Hi>();

        IServiceProvider rootServiceProvider = rootCollection.BuildServiceProvider();
        
        IServiceProvider childServiceProvider = rootServiceProvider.GetChildServiceProvider("child1");
        Assert.Null(rootServiceProvider.GetService<IGreeter>());
    }

    [Fact]
    public void ChildContainerGetsAccessParentServices()
    {
        IServiceCollection rootCollection = new ServiceCollection();
        rootCollection.AddSingleton<IOnlyRoot, OnlyRootService>();
        IServiceCollection childCollection = rootCollection.CreateChildCollection("child1");
        childCollection.AddSingleton<IGreeter, Hello>();
        
        IServiceProvider rootServiceProvider = rootCollection.BuildServiceProvider();

        IServiceProvider childServiceProvider = rootServiceProvider.GetChildServiceProvider("child1");
        IGreeter? childGreeter = childServiceProvider.GetService<IGreeter>();
        Assert.IsType<Hello>(childGreeter);

        IOnlyRoot? onlyRootFromChild = ((Hello)childGreeter)?.Root;
        Assert.NotNull(onlyRootFromChild);

        IOnlyRoot? onlyRootFromRoot = rootServiceProvider.GetService<IOnlyRoot>();
        Assert.Equal(onlyRootFromChild, onlyRootFromRoot);
    }

    [Fact]
    public void ChildContainerDoesNotOverrideParent()
    {
        IServiceCollection rootCollection = new ServiceCollection();
        rootCollection.AddSingleton<IOnlyRoot, OnlyRootService>();
        rootCollection.AddSingleton<IGreeter, Hello>();
        IServiceCollection childCollection = rootCollection.CreateChildCollection("child1");
        childCollection.AddSingleton<IGreeter, Hi>();
        
        IServiceProvider rootServiceProvider = rootCollection.BuildServiceProvider();
        
        InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => rootServiceProvider.GetChildServiceProvider("child1"));
        Assert.Equal("Service type already registered to parent service provider. [ChildName: child1 ServiceType:" + typeof(IGreeter).FullName + "]", 
            exception.Message);
    }
}