using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BitSerializer.Tests
{
    [TestClass]
    public class GeneralTests
    {
        [TestMethod]
        public void Check_Serialization_Deserialization_Errors()
        {
            var input = DataSchema.GenerateRandom();

            byte[] bytes = BinarySerializer.Serialize(input);
            var output = BinarySerializer.Deserialize<DataSchema>(bytes);

            Assert.IsTrue(input.Equals(output));
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
