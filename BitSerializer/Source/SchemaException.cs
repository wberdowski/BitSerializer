using System;
using System.Runtime.Serialization;

namespace BitSerializer
{
    [Serializable]
    internal class SchemaException : Exception
    {
        Type SchemaType { get; }

        public SchemaException(Type schemaType)
        {
            SchemaType = schemaType;
        }

        public SchemaException(Type schemaType, string message) : base(message)
        {
            SchemaType = schemaType;
        }

        public SchemaException(Type schemaType, string message, Exception innerException) : base(message, innerException)
        {
            SchemaType = schemaType;
        }

        protected SchemaException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}