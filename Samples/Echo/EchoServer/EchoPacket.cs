namespace BitSerializer.Samples.Echo.EchoServer
{
    public class EchoPacket
    {
        public int Id { get; set; }
        public EchoPacketType Type { get; set; }
        public byte[] Payload { get; set; }

        public EchoPacket()
        {

        }

        public EchoPacket(int id, EchoPacketType type, byte[] payload)
        {
            Id = id;
            Type = type;
            Payload = payload;
        }
    }

    public enum EchoPacketType : byte
    {
        Request,
        Response
    }
}
