using Ecco2.Cloud.PublicApi.Client.V3;


namespace Ecco2.Cloud.PublicApi.Client.Tests.Unit;

public class ClientBuilderTests
{
    private readonly ApiClientBuilder _sut = new();


    [Fact]
    public void Builder_ShouldThrow_WithoutCredentials()
    {
        // arrange

        // assert
        _sut.Invoking(s => s.Build<IDataBrokerClient>())
            .Should().Throw<InvalidOperationException>();
    }

    [Theory]
    [InlineData("", "")]
    [InlineData("AnIdDoesTheTrick", "")]
    [InlineData("", "AnSecretDoesTheTrick")]
    [InlineData(null, null)]
    public void Builder_ShouldThrow_WithInvalidCredentials(string clientId, string clientSecret)
    {
        // act
        _sut.WithCredentials(clientId, clientSecret);

        // assert
        _sut.Invoking(s => s.Build<IDataBrokerClient>())
            .Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void Builder_ShouldReturnDataBroker_WithValidCredentials()
    {
        // arrange
        const string clientId = "AnyIdDoesTheTrick";
        const string clientSecret = "AnySecretDoesTheTrick";

        // act
        _sut.WithCredentials(clientId, clientSecret);

        // assert
        _sut.Invoking(s => s.Build<IDataBrokerClient>())
            .Should().NotThrow()
            .Should().NotBeNull();
    }

    [Fact]
    public void Builder_ShouldReturnHistorian_WithValidCredentials()
    {
        // arrange
        const string clientId = "AnyIdDoesTheTrick";
        const string clientSecret = "AnySecretDoesTheTrick";

        // act
        _sut.WithCredentials(clientId, clientSecret);

        // assert
        _sut.Invoking(s => s.Build<IHistorianClient>())
            .Should().NotThrow()
            .Should().NotBeNull();
    }
}
