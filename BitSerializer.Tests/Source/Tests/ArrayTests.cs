using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BitSerializer.Tests
{
    [TestClass]
    public class ArrayTests
    {
        [TestMethod]
        [DataRow(new sbyte[] { 1, 2, 3 }, typeof(sbyte[]))]
        [DataRow(new byte[] { 1, 2, 3 }, typeof(byte[]))]
        [DataRow(new short[] { 1, 2, 3 }, typeof(short[]))]
        [DataRow(new ushort[] { 1, 2, 3 }, typeof(ushort[]))]
        [DataRow(new int[] { 1, 2, 3 }, typeof(int[]))]
        [DataRow(new uint[] { 1, 2, 3 }, typeof(uint[]))]
        [DataRow(new long[] { 1, 2, 3 }, typeof(long[]))]
        [DataRow(new ulong[] { 1, 2, 3 }, typeof(ulong[]))]
        [DataRow(new char[] { 'a', 'b', 'c' }, typeof(char[]))]
        [DataRow(new float[] { 1, 2, 3 }, typeof(float[]))]
        [DataRow(new double[] { 1, 2, 3 }, typeof(double[]))]
        [DataRow(new double[] { 1.2f, 2.3f, 3.4f }, typeof(decimal[]))]
        [DataRow(new bool[] { true, false, true }, typeof(bool[]))]
        public void Check_TypeSupport_Primitive_Array(object obj, Type type)
        {
            if (type == typeof(decimal[]))
            {
                obj = Array.ConvertAll((double[])obj, x => (decimal)x);
            }
            obj = Convert.ChangeType(obj, type);
            var arr = (Array)obj;

            HelperMethods.TestArray(arr);
        }

        [TestMethod]
        public void Check_TypeSupport_Enum_Array()
        {
            var arr = new SampleEnum[]
            {
                SampleEnum.Value2,
                SampleEnum.Value1,
                SampleEnum.Value1
            };

            HelperMethods.TestArray(arr);
        }

        [TestMethod]
        public void Check_TypeSupport_String_Array()
        {
            var arr = new string[] { "aa", "bb", "cc" };

            HelperMethods.TestArray(arr);
        }

        [TestMethod]
        public void Check_TypeSupport_Struct_Array()
        {
            var arr = new SampleStruct[]
            {
                new SampleStruct(1,"test1"),
                new SampleStruct(2,"test2"),
                new SampleStruct(3,"test3")
            };

            HelperMethods.TestArray(arr);
        }

        [TestMethod]
        public void Check_TypeSupport_Class_Array()
        {
            var arr = new SampleClass[]
            {
                new SampleClass(1,1.1f),
                new SampleClass(2,1.2f),
                new SampleClass(3,1.3f)
            };

            HelperMethods.TestArray(arr);
        }
    }
}
