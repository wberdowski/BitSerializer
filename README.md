# ⚠️ This project is no longer maintained!
Please note that this project is **no longer actively maintained or supported**. While I appreciate your interest in this project, I can no longer provide updates and bug fixes. Thank you for your understanding and support.

> **Note**
As an alternative, I highly recommend that you consider migrating to the **MessagePack-CSharp**, which provides an extended set of features and is actively maintained by a dedicated team of developers.

# BitSerializer
Binary serializer built with low latency network communication in mind.
# NuGet package
https://www.nuget.org/packages/BitSerializer/

# Key features
* Easy to use
* Lightweight
* Fast serialization and deserialization (see [Performance](#performance) section)
* Supports nested schema types
* No external compiler required
* Classes/Structs as schema
* Included sample projects
# Performance
![chart](https://i.imgur.com/Dfi1rz6.png)

[See benchmark source code](BitSerializer.Benchmarks/Source/Program.cs)
# How to use?
## Samples
The best way to learn how to use BitSerializer is to take a look at the provided [sample projects](Samples).

## Schema definition
Define a schema class/struct containing fields you want to serialize.
#### Example
```cs
public class DataPacket
{
    // Fields or properties you want to serialize
    public int Id { get; set; }
    public byte[] Payload { get; set; }

    // Remember to always include a parameterless constructor, otherwise the deserializer won't be able
    // to create a new instance of the class.
    public DataPacket()
    {

    }

    public DataPacket(int id, byte[] payload)
    {
        Id = id;
        Payload = payload;
    }
}
```
## Serialization
Create an instance of the schema class and pass it to the ```BinarySerializer.Serialize``` method.
#### Example
```cs
DataPacket packet = new DataPacket(
    id: 1,
    payload: new byte[] { 0x1, 0x2, 0x3 }
);
```
```cs
byte[] bytes = BinarySerializer.Serialize(packet);
```
## Deserialization
Pass bytes returned by the ```BinarySerializer.Serialize``` to the ```BinarySerializer.Deserialize<T>``` method specifying the type of the schema class. 
#### Example
```cs
DataPacket packet = BinarySerializer.Deserialize<DataPacket>(bytes);
```
