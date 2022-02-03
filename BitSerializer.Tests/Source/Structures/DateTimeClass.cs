using System;

namespace BitSerializer.Tests
{
    public class DateTimeClass
    {
        public DateTime DateTime { get; set; }

        public DateTimeClass()
        {

        }

        public DateTimeClass(DateTime dateTime)
        {
            DateTime = dateTime;
        }

        public override bool Equals(object obj)
        {
            return obj is DateTimeClass @class &&
                   DateTime == @class.DateTime;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(DateTime);
        }
    }
}
