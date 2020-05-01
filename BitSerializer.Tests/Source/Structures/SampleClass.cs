using System;

namespace BitSerializer.Tests
{
    public class SampleClass
    {
        public int TestInt { get; set; }
        public float TestFloat { get; set; }

        public SampleClass()
        {

        }

        public SampleClass(int testInt, float testFloat)
        {
            this.TestInt = testInt;
            this.TestFloat = testFloat;
        }

        public override bool Equals(object obj)
        {
            return obj is SampleClass @class &&
                   TestInt == @class.TestInt &&
                   TestFloat == @class.TestFloat;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(TestInt, TestFloat);
        }
    }
}
