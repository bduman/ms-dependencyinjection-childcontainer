# DependencyInjection.ChildContainer

![Nuget Push](https://github.com/bduman/ms-dependencyinjection-childcontainer/workflows/Nuget%20Push/badge.svg) [![Nuget](https://img.shields.io/nuget/v/DependencyInjection.ChildContainer)](https://www.nuget.org/packages/DependencyInjection.ChildContainer/)

Coded as an add-on to existing microsoft dependency injection library.
The creation of new concepts was avoided as much as possible.

### Nuget

To use child container feature, install the NuGet package:
```
dotnet add package DependencyInjection.ChildContainer
```

### Capabilities of Child Container -Child Service Provider-

- Child container can use parent container's dependencies.
  (Both as service construct operations and direct access to parent's service provider.)
- The parent container can indirectly access the services of the child container.
  (Just knowing the child container's name is enough.)
- Child container is not allowed to override parent container's dependency.
  (A precaution to avoid misrepresentations and ambiguity.)
- Child containers are built with lazy behavior. 
  (If Microsoft had opened the service provider's build with an interface, maybe the opposite -eager- would have been possible.)

### Usage

```
IServiceCollection parentCollection = new ServiceCollection();

// Create a child container
IServiceCollection childCollection = parentCollection.CreateChildCollection("child1");

// Build parent collection to get service provider
IServiceProvider parentServiceProvider = parentCollection.BuildServiceProvider();

// Access to child service provider from parent
IServiceProvider childServiceProvider = parentServiceProvider.GetChildServiceProvider("child1");

// Access to parent service provider from child
IServiceProvider parentServiceProvider = childServiceProvider.GetServices<IParentServiceProvider>();
```