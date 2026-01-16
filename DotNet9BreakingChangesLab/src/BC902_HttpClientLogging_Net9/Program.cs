using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Http;

Console.WriteLine("BC902 - HttpClientFactory logging (net9.0)\n");
Console.WriteLine("Goal: in .NET 9, logged header values may be redacted (*) by default.\n");

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
Console.WriteLine("\nIf you see '*' instead of 'secret-value' in logs, that's the .NET 9 change.");

sealed class StubHandler : HttpMessageHandler
{
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent("stub")
        });
    }
}
