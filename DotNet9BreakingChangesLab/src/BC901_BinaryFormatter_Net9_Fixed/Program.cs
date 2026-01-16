using System.Text.Json;

Console.WriteLine("BC901 - BinaryFormatter replacement (net9.0, fixed)\n");

var payload = new Payload { Name = "Hello", Value = 123 };

var json = JsonSerializer.Serialize(payload);
var roundtrip = JsonSerializer.Deserialize<Payload>(json)!;

Console.WriteLine("System.Text.Json OK (net9.0)");
Console.WriteLine($"  JSON={json}");
Console.WriteLine($"  Name={roundtrip.Name}, Value={roundtrip.Value}");

public sealed class Payload
{
    public string Name { get; set; } = "";
    public int Value { get; set; }
}
