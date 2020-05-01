using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BitSerializer.Tests
{
    [TestClass]
    public class MultiDimensionalArrayTests
    {
        [TestMethod]
        public void Check_TypeSupport_Primitive_Multi_Dimensional_Array()
        {
            var arr = new int[,]{
                {1,2,3 },
                {4,5,6 }
            };

            HelperMethods.TestArray(arr);
        }

        [TestMethod]
        public void Check_TypeSupport_Enum_Multi_Dimensional_Array()
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
        public void Check_TypeSupport_String_Multi_Dimensional_Array()
        {
            var arr = new string[,] {
                {"aa","bb","cc" },
                {"dd","ee","ff" }
            };

            HelperMethods.TestArray(arr);
        }

        [TestMethod]
        public void Check_TypeSupport_Struct_Multi_Dimensional_Array()
        {
            var arr = new SampleStruct[,]
            {
                {
                    new SampleStruct(1,"test1"),
                    new SampleStruct(2,"test2")
                },
                {
                    new SampleStruct(3,"test3"),
                    new SampleStruct(4,"test4")
                }
            };

            HelperMethods.TestArray(arr);
        }

        [TestMethod]
        public void Check_TypeSupport_Class_Multi_Dimensional_Array()
        {
            var arr = new SampleClass[,]
            {
                {
                    new SampleClass(4,1.4f),
                    new SampleClass(1,1.1f),
                },
                 {
                    new SampleClass(2,1.2f),
                    new SampleClass(3,1.3f)
                }
            };

            HelperMethods.TestArray(arr);
        }
    }
}
