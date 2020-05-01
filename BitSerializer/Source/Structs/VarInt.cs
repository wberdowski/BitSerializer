namespace BitSerializer.Structures
{
    public struct VarInt
    {
        public int Value { get; set; }

        public VarInt(int value)
        {
            Value = value;
        }

        public static implicit operator VarInt(int value)
        {
            return new VarInt(value);
        }

        public static VarInt operator +(VarInt a, VarInt b)
        {
            return a.Value + b.Value;
        }

        public static VarInt operator -(VarInt a, VarInt b)
        {
            return a.Value - b.Value;
        }

        public static VarInt operator *(VarInt a, VarInt b)
        {
            return a.Value * b.Value;
        }

        public static VarInt operator /(VarInt a, VarInt b)
        {
            return a.Value * b.Value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public override bool Equals(object obj)
        {
            return Value.Equals(obj);
        }

        public override int GetHashCode()
        {
            return -1937169414 + Value.GetHashCode();
        }
    }
}
