namespace DependencyInjection.ChildContainer.Tests.Models;

internal class OnlyRootService : IOnlyRoot
{
    public string Execute(string text)
    {
        return "OnlyRootServiceExecuted " + text;
    }
}