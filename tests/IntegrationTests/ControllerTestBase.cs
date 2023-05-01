namespace IntegrationTests;

[Collection(nameof(SharedTestCollection))]
public abstract class ControllerTestBase : IAsyncLifetime
{

    protected readonly HttpClient Client;

    protected ControllerTestBase(ApplicationWebFactory apiFactory)
    {
        Client = apiFactory.HttpClient;
    }

    public Task InitializeAsync() => Task.CompletedTask;

    public Task DisposeAsync() => Task.CompletedTask;
}