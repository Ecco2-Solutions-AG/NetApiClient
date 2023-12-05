# NetApiClient
API client for the Ecco2 public API in .net

# Usage
The client library provides a client builder allow to create all available clients.
    var builder = new ApiClientBuilder();
    builder.WithCredentials("MyClientId", "MyClientSecret");
    var client = builder.Build<IDataBrokerClient>();
    var processPoint = await client.GetAsync(myGuid);

# Available clients
As of now, the following clients are available:
 - Data broker
 - Historian