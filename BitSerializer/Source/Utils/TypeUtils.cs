using System;

namespace BitSerializer.Utils
{
    public static class TypeUtils
    {
        public static bool IsBlittable(this Type type)
        {
            return type.IsPrimitive &&
                type != typeof(bool) &&
                type != typeof(char) &&
                type != typeof(object) &&
                type != typeof(string);
        }
    }
}
