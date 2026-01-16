#pragma warning disable SYSLIB0011
using System.Runtime.Serialization.Formatters.Binary;

Console.WriteLine("BC901 - BinaryFormatter (net9.0, broken)\n");

var payload = new Payload { Name = "Hello", Value = 123 };

try
{
    using var ms = new MemoryStream();
    var bf = new BinaryFormatter();
    bf.Serialize(ms, payload); // .NET 9: should throw

    ms.Position = 0;
    var roundtrip = (Payload)bf.Deserialize(ms);

    Console.WriteLine("UNEXPECTED: Serialize/Deserialize succeeded.");
    Console.WriteLine($"  Name={roundtrip.Name}, Value={roundtrip.Value}");
}
catch (Exception ex)
{
    Console.WriteLine("EXPECTED: BinaryFormatter is disabled in .NET 9.");
    Console.WriteLine("Exception:");
    Console.WriteLine(ex.GetType().FullName);
    Console.WriteLine(ex.Message);
}

[Serializable]
public sealed class Payload
{
    public string Name { get; set; } = "";
    public int Value { get; set; }
}
