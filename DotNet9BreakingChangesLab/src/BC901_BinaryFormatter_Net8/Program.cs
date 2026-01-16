#pragma warning disable SYSLIB0011
using System.Runtime.Serialization.Formatters.Binary;

Console.WriteLine("BC901 - BinaryFormatter (net8.0)\n");

var payload = new Payload { Name = "Hello", Value = 123 };

try
{
    using var ms = new MemoryStream();
    var bf = new BinaryFormatter();
    bf.Serialize(ms, payload);
    ms.Position = 0;
    var roundtrip = (Payload)bf.Deserialize(ms);

    Console.WriteLine("Serialize/Deserialize OK (net8.0)");
    Console.WriteLine($"  Name={roundtrip.Name}, Value={roundtrip.Value}");
    Console.WriteLine();
    Console.WriteLine("Expected in .NET 9: this will throw at runtime.");
}
catch (Exception ex)
{
    Console.WriteLine("Unexpected exception on net8.0:");
    Console.WriteLine(ex);
}

[Serializable]
public sealed class Payload
{
    public string Name { get; set; } = "";
    public int Value { get; set; }
}
