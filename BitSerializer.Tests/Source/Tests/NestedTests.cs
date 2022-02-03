using BitSerializer.Tests.Source.Structures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BitSerializer.Tests
{
    [TestClass]
    public class NestedTests
    {
        [TestMethod]
        public void Check_NestedArraySupport()
        {
            var input = new NestedDataSchema(10, "test", new SampleClass[]
            {
                new SampleClass(11, 12.3f),
                new SampleClass(22, 23.4f),
            });

            byte[] bytes = BinarySerializer.Serialize(input);
            var output = BinarySerializer.Deserialize<NestedDataSchema>(bytes);

            Assert.AreEqual(input, output);
        }
    }
}
