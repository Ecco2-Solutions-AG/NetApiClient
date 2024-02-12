# NetApiClient
API client for the Ecco2 public API in .NET Standard 2.0.

# Usage
The client library provides a client builder allowing to create all available API clients:
```csharp
// build the client
var builder = new ApiClientBuilder();
builder.WithCredentials("MyClientId", "MyClientSecret");
var client = builder.Build<IDataBrokerClient>();

// authenticate, use it
var myGuid = Guid.Parse("04458d05-3bc5-4472-b081-d9ffdace1b67");
await client.AuthenticateAsync();
await client.PublishAsync(myGuid, 25.48);
var processPoint = await client.GetAsync(myGuid);
```

# Available clients
As of now, the following clients are available:
 - Data broker
 - Historian
