using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BitSerializer.Tests
{
    public class HelperMethods
    {
        public static bool ArrayEquals(Array a, Array b)
        {
            if (a.Length != b.Length) throw new ArgumentException("Arrays are not the same length.");
            if (a.GetType() != b.GetType()) throw new ArgumentException("Arrays are not of the same type.");

            if (a.Rank == 1)
            {
                if (a.GetType().GetElementType().IsArray)
                {
                    for (int i = 0; i < a.GetLength(0); i++)
                    {
                        if (!ArrayEquals((Array)a.GetValue(i), (Array)b.GetValue(i)))
                        {
                            return false;
                        }
                    }

                    return true;
                }
                else
                {
                    for (int i = 0; i < a.Length; i++)
                    {
                        if (!a.GetValue(i).Equals(b.GetValue(i)))
                        {
                            return false;
                        }
                    }

                    return true;
                }
            }
            else
            {
                var enum1 = a.GetEnumerator();
                var enum2 = b.GetEnumerator();

                while (enum1.MoveNext() && enum2.MoveNext())
                {
                    if (!enum1.Current.Equals(enum2.Current)) return false;
                }

                return true;
            }
        }

        public static void TestArray(Array arr)
        {
            using BinaryWriter writer = new BinaryWriter();
            writer.Write(arr, arr.GetType());

            using BinaryReader reader = new BinaryReader(writer.PackBytes());
            var arr2 = Convert.ChangeType(reader.Read(arr.GetType()), arr.GetType());

            if (!ArrayEquals(arr, (Array)arr2))
            {
                Assert.Fail("Arrays not equal.");
            }
        }
    }
}
