using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BitSerializer.Tests
{
    [TestClass]
    public class GeneralTests
    {
        [TestMethod]
        public void Check_Serialization_Deserialization_Errors()
        {
            var data = DataSchema.GenerateRandom();

            byte[] bytes = BinarySerializer.Serialize(data);
            BinarySerializer.Deserialize<DataSchema>(bytes);
        }

        //[TestMethod]
        //public void Check_Serialization_Deserialization_Integrity()
        //{
        //    var data = DataSchema.GenerateRandom();

        //    byte[] bytes = BitSerializer.Serialize(data);
        //    var deserialized = BitSerializer.Deserialize<DataSchema>(bytes);

        //    if (!EqualityComparer<DataSchema>.Default.Equals(data, deserialized))
        //    {
        //        Assert.Fail("Not equal");
        //    }
        //}
    }
}
