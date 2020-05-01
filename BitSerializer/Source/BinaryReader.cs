using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace BitSerializer
{
    public unsafe class BinaryReader : IDisposable
    {
        private byte* buffer;

        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryReader"/> class with a specified input buffer data.
        /// </summary>
        public BinaryReader(byte[] bytes) : this(bytes, bytes.Length, 0)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryReader"/> class with a specified input buffer data, length and offset.
        /// </summary>
        public BinaryReader(byte[] bytes, int length, int offset)
        {
            buffer = (byte*)Marshal.AllocCoTaskMem(length);
            Marshal.Copy(bytes, offset, (IntPtr)buffer, length);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryReader"/> class with a specified input buffer pointer.
        /// </summary>
        public BinaryReader(byte* buffer)
        {
            this.buffer = buffer;
        }

        public object ReadSchema(Type type)
        {
            FieldInfo[] fields = SchemaHelpers.GetSchemaMembers(type);
            object obj = Activator.CreateInstance(type);

            for (int i = 0; i < fields.Length; i++)
            {
                var field = fields[i];
                var dataType = field.FieldType;

                field.SetValue(obj, Read(dataType));
            }

            return obj;
        }

        /// <summary>
        /// Reads an object of the supported type from the input buffer.
        /// </summary>
        /// <param name="type">Type of the object.</param>
        public object Read(Type type)
        {
            if (type.IsPrimitive || type == typeof(decimal))
            {
                if (type == typeof(int))
                {
                    return ReadVarInt();
                }

                return ReadPrimitive(type);
            }
            else if (type.IsArray)
            {
                if (type.GetArrayRank() > 1)
                {
                    return ReadMDArray(type);
                }
                else
                {
                    return Read1DArray(type);
                }
            }
            else if (type == typeof(string))
            {
                return ReadString();
            }
            else if (type.IsEnum)
            {
                return Enum.ToObject(type, ReadPrimitive(type.GetEnumUnderlyingType()));
            }
            else if (type.IsValueType || type.IsClass)
            {
                return ReadSchema(type);
            }

            throw new NotSupportedException($"Type {type.Name} is not supported.");
        }

        private Array ReadMDArray(Type type)
        {
            Type eleType = type.GetElementType();

            int[] lengths = new int[type.GetArrayRank()];

            for (int i = 0; i < lengths.Length; i++)
                lengths[i] = ReadVarInt();

            Array arr = Array.CreateInstance(eleType, lengths);

            for (int i = 0; i < arr.Length; i++)
            {
                arr.SetValue(Read(eleType), MDArrayIndexToIndices(arr, i));
            }

            return arr;
        }

        private int[] MDArrayIndexToIndices(Array a, int index)
        {
            int[] indices = new int[a.Rank];
            for (int d = a.Rank - 1; d >= 0; d--)
            {
                var len = a.GetLength(d);
                indices[d] = index % len;
                index /= len;
            }
            return indices;
        }

        private Array Read1DArray(Type type)
        {
            Type eleType = type.GetElementType();
            int len = ReadVarInt();

            Array arr = Array.CreateInstance(eleType, len);

            if (eleType.IsBlittable())
            {
                var size = arr.Length * Marshal.SizeOf(eleType);

                var handle = GCHandle.Alloc(arr, GCHandleType.Pinned);
                var ptr = (int*)handle.AddrOfPinnedObject();

                Buffer.MemoryCopy(buffer, ptr, size, size);
                buffer += size;

                handle.Free();
            }
            else
            {
                for (int i = 0; i < len; i++)
                {
                    arr.SetValue(Read(eleType), i);
                }
            }

            return arr;
        }

        private string ReadString()
        {
            // Read length
            int len = ReadVarInt();
            string str;

            if (len == 0)
            {
                str = null;
            }
            else
            {
                str = Encoding.UTF8.GetString(buffer, len);
                buffer += len;
            }

            return str;
        }

        private object ReadPrimitive(Type type)
        {
            int size = Marshal.SizeOf(type);
            IntPtr ptr = Marshal.AllocCoTaskMem(size);
            ReadPrimitive((byte*)ptr, size);
            object obj = Marshal.PtrToStructure(ptr, type);
            Marshal.FreeCoTaskMem(ptr);
            return obj;
        }

        private void ReadPrimitive(byte* ptr, int bytes)
        {
            for (int i = 0; i < bytes; i++)
                ptr[i] = *buffer++;
        }

        private byte ReadByte()
        {
            return *buffer++;
        }

        private int ReadVarInt()
        {
            int numRead = 0;
            int result = 0;
            byte read;
            do
            {
                read = ReadByte();
                int value = (read & 0b01111111);
                result |= (value << (7 * numRead));

                numRead++;
                if (numRead > 5)
                {
                    throw new ArgumentException("VarInt is too big");
                }
            } while ((read & 0b10000000) != 0);

            return result;
        }

        /// <summary>
        /// Releases all resources used by the <see cref="BinaryReader"/>.
        /// </summary>
        public void Dispose()
        {
            buffer = null;
        }
    }
}
