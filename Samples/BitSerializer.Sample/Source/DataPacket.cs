using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BitSerializer.Sample
{
    public class DataPacket
    {
        public int Id;
        [NonSerialized]
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
