namespace BitSerializer.Sample
{
    public class DataPacket
    {
        public int Id { get; set; }
        public byte[] Payload { get; set; }

        public DataPacket()
        {

        }

        public DataPacket(int id, byte[] payload)
        {
            Id = id;
            Payload = payload;
        }
    }
}
