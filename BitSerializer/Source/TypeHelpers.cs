using System;

namespace BitSerializer
{
    public static class TypeHelpers
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
