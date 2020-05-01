using System;

namespace BitSerializer.Benchmarks
{
    [Serializable]
    public class SampleSchema
    {
        public int SampleInt { get; set; }
        public SampleEnum SampleEnum { get; set; }
        public string SampleString { get; set; }
        public byte[] SampleByteArr { get; set; }

        public SampleSchema()
        {
            
        }

        public SampleSchema(int sampleInt, SampleEnum sampleEnum, string sampleString, byte[] sampleByteArr)
        {
            SampleInt = sampleInt;
            SampleEnum = sampleEnum;
            SampleString = sampleString;
            SampleByteArr = sampleByteArr;
        }
    }

    public enum SampleEnum
    {
        Value0,
        Value1,
        Value2
    }
}
