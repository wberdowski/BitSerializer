using System;
using System.Collections.Generic;

namespace BitSerializer.Tests.Source.Structures
{
    public class NestedDataSchema
    {
        public int Int1 { get; set; }
        public string Str1 { get; set; }
        public SampleClass[] ClassArrr { get; set; }

        public NestedDataSchema()
        {

        }

        public NestedDataSchema(int int1, string str1, SampleClass[] classArrr)
        {
            Int1 = int1;
            Str1 = str1;
            ClassArrr = classArrr;
        }

        public override bool Equals(object obj)
        {
            return obj is NestedDataSchema schema &&
                   Int1 == schema.Int1 &&
                   Str1 == schema.Str1 &&
                   EqualityComparer<SampleClass[]>.Default.Equals(ClassArrr, schema.ClassArrr);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Int1, Str1, ClassArrr);
        }
    }
}
