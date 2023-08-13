namespace DependencyInjection.ChildContainer.Tests.Models;

internal class Hi : IGreeter
{
    public string Say(string to)
    {
        return "Hi, " + to;
    }
}