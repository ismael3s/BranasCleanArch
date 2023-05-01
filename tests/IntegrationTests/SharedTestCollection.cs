namespace IntegrationTests;
[CollectionDefinition(nameof(SharedTestCollection))]
public class SharedTestCollection : ICollectionFixture<ApplicationWebFactory>
{ }
