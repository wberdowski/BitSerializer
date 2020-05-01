using System;

namespace BitSerializer.Tests
{
    [Serializable]
    public class MultidimArrayTestClass
    {
        public int[,] Intarray { get; set; }

        public MultidimArrayTestClass()
        {

        }

        public MultidimArrayTestClass(int[,] intarray)
        {
            Intarray = intarray;
        }
    }
}
