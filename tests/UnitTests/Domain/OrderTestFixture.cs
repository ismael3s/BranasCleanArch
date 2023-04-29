using Bogus;
using Domain.VO;
using UnitTests.Common;

namespace UnitTests.Domain;
public class OrderTestFixture : BaseFixture
{
    public OrderTestFixture() : base()
    {
        Randomizer.Seed = new Random();
    }

    public Cpf RandomValidCpf()
    {
        var validsCpfs = new string[]
        {
            "00861120078",
            "12270553071",
            "82615204041",
            "146.515.600-32"
        };

        return Cpf.Create(Faker.Random.ArrayElement(validsCpfs));
    }
}


[CollectionDefinition(nameof(OrderTestFixture))]
public class OrderTestFixtureCollection : ICollectionFixture<OrderTestFixture>
{
}