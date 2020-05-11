using System;

namespace BitSerializer.Sample
{
    internal class Program
    {
        private static void Main()
        {
            // Create a new instance of the packet object
            DataPacket packet = new DataPacket(
                id: 1,
                payload: new byte[] { 0x1, 0x2, 0x3 }
            );

            // Serialize packet object
            byte[] bytes = BinarySerializer.Serialize(packet);

            // Deserialize packet object
            DataPacket packet2 = BinarySerializer.Deserialize<DataPacket>(bytes);

            // Print out deserialized data
            Console.WriteLine(
                $"Id: {packet2.Id}" + Environment.NewLine +
                $"Payload: {BitConverter.ToString(packet2.Payload)}"
            );

            Console.ReadKey();
        }
    }
}
