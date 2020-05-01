using System;

namespace BitSerializer
{
    public abstract unsafe class BinarySerializer
    {
        /// <summary>
        /// Serializes an object of the supported type to a byte array.
        /// </summary>
        public static byte[] Serialize<T>(T obj)
        {
            return Serialize(obj, typeof(T));
        }

        /// <summary>
        /// Deserializes a byte array to the object of the supported type.
        /// </summary>
        public static T Deserialize<T>(byte[] bytes)
        {
            return (T)Deserialize(bytes, typeof(T));
        }

        /// <summary>
        /// Serializes an object of the supported type to a byte array.
        /// </summary>
        public static byte[] Serialize(object obj, Type type)
        {
            using (var writer = new BinaryWriter())
            {
                writer.WriteSchema(obj, type);
                return writer.PackBytes();
            }
        }

        /// <summary>
        /// Serializes an object of the supported type to the output buffer.
        /// </summary>
        public static void Serialize(byte* buffer, object obj, Type type)
        {
            using (var writer = new BinaryWriter(buffer))
            {
                writer.WriteSchema(obj, type);
            }
        }

        /// <summary>
        /// Deserializes a byte array to the object of the supported type.
        /// </summary>
        public static object Deserialize(byte[] bytes, Type type)
        {
            using (var reader = new BinaryReader(bytes, bytes.Length, 0))
            {
                return reader.ReadSchema(type);
            }
        }

        /// <summary>
        /// Deserializes the input buffer data to the object of the supported type.
        /// </summary>
        public static object Deserialize(byte* buffer, Type type)
        {
            using (var reader = new BinaryReader(buffer))
            {
                return reader.ReadSchema(type);
            }
        }
    }
}
