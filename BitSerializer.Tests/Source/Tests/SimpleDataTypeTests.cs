using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BitSerializer.Tests
{
    [TestClass]
    public class SimpleDataTypeTests
    {
        [TestMethod]
        [DataRow(10, typeof(sbyte))]
        [DataRow(10, typeof(byte))]
        [DataRow(10, typeof(short))]
        [DataRow(10, typeof(ushort))]
        [DataRow(10, typeof(int))]
        [DataRow(10, typeof(uint))]
        [DataRow(10, typeof(long))]
        [DataRow(10, typeof(ulong))]
        [DataRow('a', typeof(char))]
        [DataRow(10, typeof(float))]
        [DataRow(10, typeof(double))]
        [DataRow(10, typeof(decimal))]
        [DataRow(10, typeof(bool))]
        public void Check_TypeSupport_Primitive(object obj, Type type)
        {
            obj = Convert.ChangeType(obj, type);

            using BinaryWriter writer = new BinaryWriter();
            writer.Write(obj, type);

            using BinaryReader reader = new BinaryReader(writer.PackBytes());
            Assert.AreEqual(obj, reader.Read(type));
        }

        [TestMethod]
        public void Check_TypeSupport_String()
        {
            string obj = "test";

            using BinaryWriter writer = new BinaryWriter();
            writer.Write(obj, typeof(string));

            using BinaryReader reader = new BinaryReader(writer.PackBytes());
            Assert.AreEqual(obj, (string)reader.Read(typeof(string)));
        }

        [TestMethod]
        public void Check_TypeSupport_Enum()
        {
            SampleEnum obj = SampleEnum.Value2;

            using BinaryWriter writer = new BinaryWriter();
            writer.Write(obj, typeof(SampleEnum));

            using BinaryReader reader = new BinaryReader(writer.PackBytes());
            Assert.AreEqual(obj, (SampleEnum)reader.Read(typeof(SampleEnum)));
        }

        [TestMethod]
        public void Check_TypeSupport_Struct()
        {
            SampleStruct obj = new SampleStruct()
            {
                someInt = 13,
                someString = "test"
            };

            using BinaryWriter writer = new BinaryWriter();
            writer.Write(obj, typeof(SampleStruct));

            using BinaryReader reader = new BinaryReader(writer.PackBytes());
            Assert.AreEqual(obj.GetHashCode(), reader.Read(typeof(SampleStruct)).GetHashCode());
        }

        [TestMethod]
        public void Check_TypeSupport_Class()
        {
            SampleClass obj = new SampleClass()
            {
                TestInt = 14,
                TestFloat = 12.34f
            };

            using BinaryWriter writer = new BinaryWriter();
            writer.Write(obj, typeof(SampleClass));

            using BinaryReader reader = new BinaryReader(writer.PackBytes());
            Assert.AreEqual(obj.GetHashCode(), reader.Read(typeof(SampleClass)).GetHashCode());
        }

        [TestMethod]
        public void Check_TypeSupport_DateTime()
        {
            var input = new DateTimeClass(DateTime.Now);

            byte[] bytes = BinarySerializer.Serialize(input);
            var output = BinarySerializer.Deserialize<DateTimeClass>(bytes);

            Assert.AreEqual(input, output);
        }
    }
}
