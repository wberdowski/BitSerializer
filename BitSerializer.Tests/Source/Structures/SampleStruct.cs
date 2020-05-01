using System;

namespace BitSerializer.Tests
{
    public struct SampleStruct
    {
        public int someInt;
        public string someString;

        public SampleStruct(int someInt, string someString)
        {
            this.someInt = someInt;
            this.someString = someString;
        }

        public override bool Equals(object obj)
        {
            var other = (SampleStruct)obj;
            if (someInt != other.someInt) return false;
            if (someString != other.someString) return false;
            return true;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(someInt, someString);
        }
    }
}
