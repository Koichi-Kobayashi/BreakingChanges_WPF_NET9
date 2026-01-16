using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Http;

Console.WriteLine("BC902 - HttpClientFactory logging (net8.0)\n");
Console.WriteLine("Goal: check if logged header values are visible.\n");

var services = new ServiceCollection();
services.AddLogging(b =>
{
    b.AddSimpleConsole(o =>
    {
        o.SingleLine = true;
        o.TimestampFormat = "HH:mm:ss ";
    });
    b.SetMinimumLevel(LogLevel.Trace);
});

services.AddHttpClient("demo")
    .ConfigurePrimaryHttpMessageHandler(() => new StubHandler());

using var sp = services.BuildServiceProvider();
var factory = sp.GetRequiredService<IHttpClientFactory>();
var client = factory.CreateClient("demo");

client.DefaultRequestHeaders.Add("X-Demo-Secret", "secret-value");

var resp = await client.GetAsync("https://example.invalid/test");
Console.WriteLine($"\nResponse status: {(int)resp.StatusCode} {resp.StatusCode}");

sealed class StubHandler : HttpMessageHandler
{
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        // Return a dummy response without network access.
        return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent("stub")
        });
    }
}
