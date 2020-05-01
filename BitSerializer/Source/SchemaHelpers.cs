using System;
using System.Collections.Generic;
using System.Reflection;

namespace BitSerializer
{
    public static class SchemaHelpers
    {
        private static readonly Dictionary<Type, FieldInfo[]> schemaCache = new Dictionary<Type, FieldInfo[]>();

        public static FieldInfo[] GetSchemaMembers(Type type)
        {
            if (!schemaCache.TryGetValue(type, out FieldInfo[] fields))
            {
                fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            }

            return fields;
        }
    }
}
