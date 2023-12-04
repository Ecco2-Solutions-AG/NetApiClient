using Ecco2.Cloud.PublicApi.Client.V3;


namespace Ecco2.Cloud.PublicApi.Client.Tests.Unit;

public class DataBrokerClientTester
{
    private readonly DataBrokerClientBuilder _sut = new();


    [Theory]
    [InlineData("", "")]
    [InlineData(null, null)]
    [InlineData("AnyIdDoesTheTrick", "AnySecretDoesTheTrick")]
    public void Builder_ShouldReturn_NoMatterTheCredentials(string clientId, string clientSecret)
    {
        // arrange
        _sut.WithCredentials(clientId, clientSecret);

        // act
        var client = _sut.Build();

        // assert
        client.Should().NotBeNull();
    }
}
