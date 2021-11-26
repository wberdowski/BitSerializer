namespace BitSerializer.Sample
{
    public class DataPacket
    {
        public int Id;
        public byte[] Payload;

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
