using System;
using System.Collections.Generic;

namespace BitSerializer.Tests
{
    public class DataSchema
    {
        // Singular
        public sbyte SByteVal;
        public byte ByteVal;
        public short ShortVal;
        public ushort UShortVal;
        public int IntVal;
        public uint UIntVal;
        public long LongVal;
        public ulong ULongVal;
        public char CharVal;
        public float FloatVal;
        public double DoubleVal;
        public bool BoolVal;
        public string StringVal;
        public decimal DecimalVal;
        public SampleEnum EnumVal;
        public SampleStruct StructVal;
        public SampleClass ClassVal;

        // 1D Arrays
        public int[] IntArray;
        public float[] FloatArray;
        public char[] CharArray;

        public DataSchema()
        {

        }

        public static DataSchema GenerateRandom()
        {
            var schema = new DataSchema()
            {
                SByteVal = sbyte.MaxValue,
                ByteVal = byte.MaxValue,
                ShortVal = short.MaxValue,
                UShortVal = ushort.MaxValue,
                IntVal = int.MaxValue,
                UIntVal = uint.MaxValue,
                LongVal = long.MaxValue,
                ULongVal = ulong.MaxValue,
                CharVal = 'a',
                FloatVal = float.MaxValue,
                DoubleVal = double.MaxValue,
                BoolVal = true,
                StringVal = "sampletext123",
                DecimalVal = decimal.MaxValue,
                EnumVal = SampleEnum.Value2,
                StructVal = new SampleStruct() { someInt = 10, someString = "test" },
                ClassVal = new SampleClass(10, 12.34f),

                IntArray = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 },
                FloatArray = new float[] { 1.1f, 2.2f, 3.3f, 4.4f },
                CharArray = new char[] { 'a', 'b', 'c', 'd' },

            };
            return schema;
        }

        public override bool Equals(object obj)
        {
            return obj is DataSchema schema &&
                   SByteVal == schema.SByteVal &&
                   ByteVal == schema.ByteVal &&
                   ShortVal == schema.ShortVal &&
                   UShortVal == schema.UShortVal &&
                   IntVal == schema.IntVal &&
                   UIntVal == schema.UIntVal &&
                   LongVal == schema.LongVal &&
                   ULongVal == schema.ULongVal &&
                   CharVal == schema.CharVal &&
                   FloatVal == schema.FloatVal &&
                   DoubleVal == schema.DoubleVal &&
                   BoolVal == schema.BoolVal &&
                   StringVal == schema.StringVal &&
                   DecimalVal == schema.DecimalVal &&
                   EnumVal == schema.EnumVal &&
                   EqualityComparer<SampleStruct>.Default.Equals(StructVal, schema.StructVal) &&
                   EqualityComparer<SampleClass>.Default.Equals(ClassVal, schema.ClassVal) &&
                   EqualityComparer<int[]>.Default.Equals(IntArray, schema.IntArray) &&
                   EqualityComparer<float[]>.Default.Equals(FloatArray, schema.FloatArray) &&
                   EqualityComparer<char[]>.Default.Equals(CharArray, schema.CharArray);
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(SByteVal);
            hash.Add(ByteVal);
            hash.Add(ShortVal);
            hash.Add(UShortVal);
            hash.Add(IntVal);
            hash.Add(UIntVal);
            hash.Add(LongVal);
            hash.Add(ULongVal);
            hash.Add(CharVal);
            hash.Add(FloatVal);
            hash.Add(DoubleVal);
            hash.Add(BoolVal);
            hash.Add(StringVal);
            hash.Add(DecimalVal);
            hash.Add(EnumVal);
            hash.Add(StructVal);
            hash.Add(ClassVal);
            hash.Add(IntArray);
            hash.Add(FloatArray);
            hash.Add(CharArray);
            return hash.ToHashCode();
        }
    }
}
