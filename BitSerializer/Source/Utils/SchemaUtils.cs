using System;
using System.Collections.Generic;
using System.Reflection;

namespace BitSerializer.Utils
{
    public static class SchemaUtils
    {
        public static FieldInfo[] GetSchemaMembers(Type type)
        {
            return type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        }
    }
}
