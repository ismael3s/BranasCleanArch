using System.Diagnostics;

namespace Application;

public interface ISomeService
{
    void Do();
}
public class SomeService : ISomeService
{
    public void Do()
    {
        Debug.WriteLine("Prodution code");
    }
}
