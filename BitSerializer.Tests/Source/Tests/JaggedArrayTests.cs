using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BitSerializer.Tests
{
    [TestClass]
    public class JaggedArrayTests
    {
        [TestMethod]
        public void Check_TypeSupport_Primitive_Jagged_Array()
        {
            var arr = new int[][]{
                new int[]
                {
                    1,2,3,4
                },
                new int[]
                {
                    7,8,9
                }
            };

            HelperMethods.TestArray(arr);
        }

        [TestMethod]
        public void Check_TypeSupport_Enum_Jagged_Array()
        {
            var arr = new SampleEnum[][]
            {
                new SampleEnum[]{
                    SampleEnum.Value2,
                    SampleEnum.Value1,
                },
                new SampleEnum[]{
                    SampleEnum.Value1,
                    SampleEnum.Value2,
                    SampleEnum.Value3
                }
            };

            HelperMethods.TestArray(arr);
        }

        [TestMethod]
        public void Check_TypeSupport_String_Jagged_Array()
        {
            var arr = new string[][] {
                new string[]{
                    "aa", "bb" },
                new string[]{
                    "cc", "dd"
                }
            };

            HelperMethods.TestArray(arr);
        }

        [TestMethod]
        public void Check_TypeSupport_Struct_Jagged_Array()
        {
            var arr = new SampleStruct[][]
            {
                new SampleStruct[]{
                    new SampleStruct(1,"test1"),
                    new SampleStruct(2,"test2")
                },
                new SampleStruct[]{
                    new SampleStruct(3,"test3")
                }
            };

            HelperMethods.TestArray(arr);
        }

        [TestMethod]
        public void Check_TypeSupport_Class_Jagged_Array()
        {
            var arr = new SampleClass[][]
            {
                new SampleClass[] {
                    new SampleClass(1,1.1f),
                },
                    new SampleClass[]{
                    new SampleClass(2,1.2f),
                    new SampleClass(3,1.3f)
                }
            };

            HelperMethods.TestArray(arr);
        }
    }
}
