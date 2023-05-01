namespace IntegrationTests;

[Collection(nameof(SharedTestCollection))]
public abstract class ControllerTestBase : IAsyncLifetime
{
    private readonly Func<Task> _resetDatabase;

    protected readonly HttpClient Client;

    protected ControllerTestBase(ApplicationWebFactory apiFactory)
    {
        Client = apiFactory.HttpClient;
        _resetDatabase = apiFactory.ResetDatabaseAsync;
    }

    public Task InitializeAsync() => Task.CompletedTask;

    public Task DisposeAsync() => Task.CompletedTask;
}