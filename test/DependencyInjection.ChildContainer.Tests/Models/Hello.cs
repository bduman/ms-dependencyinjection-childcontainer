namespace DependencyInjection.ChildContainer.Tests.Models;

internal class Hello : IGreeter
{
    public IOnlyRoot Root { get; }

    public Hello(IOnlyRoot root)
    {
        Root = root;
    }

    public string Say(string to)
    {
        return "Hello, " + to;
    }
}