# BitSerializer
Binary serializer built with low latency network communication in mind.
# How to use it?
## Schema definition
The first step is to define a schema class containing the fields you want to serialize. It specifies in which order to serialize your data and deserialize it later.
#### Example
```cs
public class DataPacket
{
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
Create an instance of the schema class created earlier and pass it to the ```BinarySerializer.Serialize``` method.
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
Pass the bytes returned by the ```BinarySerializer.Serialize``` to the ```BinarySerializer.Deserialize<T>``` method specyfing the type of the schema class. 
#### Example
```cs
DataPacket packet = BinarySerializer.Deserialize<DataPacket>(bytes);
```
