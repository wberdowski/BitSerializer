using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace BitSerializer
{
    public unsafe class BinaryWriter : IDisposable
    {
        private byte* start;
        private byte* buffer;

        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryWriter"/> class with a default 128K buffer size.
        /// </summary>
        public BinaryWriter() : this(128 * 1024)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryWriter"/> class with a specified buffer size.
        /// </summary>
        public BinaryWriter(int bufferSize)
        {
            buffer = start = (byte*)Marshal.AllocCoTaskMem(bufferSize);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryWriter"/> class with a specified output buffer pointer.
        /// </summary>
        public BinaryWriter(byte* buffer)
        {
            this.buffer = buffer;
        }

        public void WriteSchema(object obj, Type type)
        {
            FieldInfo[] fields = SchemaHelpers.GetSchemaMembers(type);

            for (int i = 0; i < fields.Length; i++)
            {
                var field = fields[i];
                var dataType = field.FieldType;
                var value = field.GetValue(obj);

                Write(value, dataType);
            }
        }

        /// <summary>
        /// Writes an object of the supported type to the output buffer.
        /// </summary>
        /// <param name="obj">Object to write.</param>
        /// <param name="type">Type of the object.</param>
        public void Write(object obj, Type type, bool includeArrayLength = true)
        {
            if (type.IsPrimitive || type == typeof(decimal))
            {
                WritePrimitive(obj, type);
                return;
            }
            else if (type.IsArray)
            {
                if (type.GetArrayRank() > 1)
                {
                    WriteMDArray((Array)obj, type);
                }
                else
                {
                    Write1DArray((Array)obj, type, includeArrayLength);
                }
                return;
            }
            else if (type == typeof(string))
            {
                WriteString((string)obj);
                return;
            }
            else if (type.IsEnum)
            {
                var enumType = type.GetEnumUnderlyingType();
                WritePrimitive(Convert.ChangeType(obj, enumType), enumType);
                return;
            }
            else if (type.IsValueType || type.IsClass)
            {
                WriteSchema(obj, type);
                return;
            }

            throw new NotSupportedException($"Type {type.Name} is not supported.");
        }

        private void WriteMDArray(Array arr, Type type)
        {
            var eleType = type.GetElementType();

            // Write array length
            for (int i = 0; i < arr.Rank; i++)
                WriteVarInt(arr.GetLength(i));

            foreach (var ele in arr)
            {
                Write(ele, eleType, false);
            }
        }

        private void Write1DArray(Array arr, Type type, bool includeArrayLength = true)
        {
            var eleType = type.GetElementType();

            if (includeArrayLength)
            {
                // Write array length
                WriteVarInt(arr.Length);
            }

            if (eleType.IsBlittable())
            {
                var size = arr.Length * Marshal.SizeOf(eleType);

                var handle = GCHandle.Alloc(arr, GCHandleType.Pinned);
                var ptr = (int*)handle.AddrOfPinnedObject();

                Buffer.MemoryCopy(ptr, buffer, size, size);
                buffer += size;

                handle.Free();
            }
            else
            {
                for (int i = 0; i < arr.Length; i++)
                {
                    Write(arr.GetValue(i), eleType);
                }
            }
        }

        private void WriteString(string str)
        {
            if (str == null)
            {
                // Write 0 length for null string
                WriteVarInt(0);
            }
            else
            {
                // Write string length
                WriteVarInt(str.Length);
                byte[] bytes = Encoding.UTF8.GetBytes(str);

                Marshal.Copy(bytes, 0, (IntPtr)buffer, bytes.Length);
                buffer += bytes.Length;
            }
        }

        private void WriteByte(byte value)
        {
            *buffer++ = value;
        }

        private void WritePrimitive(object obj, Type type)
        {
            if (!type.IsBlittable())
            {
                WritePrimitiveNB(obj, type);
                return;
            }

            var size = Marshal.SizeOf(type);
            var handle = GCHandle.Alloc(obj, GCHandleType.Pinned);
            var ptr = (int*)handle.AddrOfPinnedObject();

            Buffer.MemoryCopy(ptr, buffer, size, size);
            buffer += size;

            handle.Free();
        }

        /// <summary>
        /// Writes a non-blittable primitve.
        /// </summary>
        private void WritePrimitiveNB(object obj, Type type)
        {
            int size = Marshal.SizeOf(type);
            IntPtr ptr = Marshal.AllocCoTaskMem(size);
            Marshal.StructureToPtr(obj, ptr, false);

            byte* p = (byte*)ptr;
            //for (int i = 0; i < size; i++)
            //    *buffer++ = p[i];

            Buffer.MemoryCopy(p, buffer, size, size);
            buffer += size;

            Marshal.FreeCoTaskMem(ptr);
        }

        private void WriteVarInt(int value)
        {
            uint v = (uint)value;
            do
            {
                byte temp = (byte)(v & 0b01111111);
                v >>= 7;
                if (v != 0)
                {
                    temp |= 0b10000000;
                }
                WriteByte(temp);
            } while (v != 0);
        }

        /// <summary>
        /// Packs the output buffer data into a byte array.
        /// </summary>
        public byte[] PackBytes()
        {
            int len = (int)(buffer - start);
            var bytes = new byte[len];
            Marshal.Copy((IntPtr)start, bytes, 0, len);
            return bytes;
        }

        /// <summary>
        /// Releases all resources used by the <see cref="BinaryWriter"/>.
        /// </summary>
        public void Dispose()
        {
            Marshal.FreeCoTaskMem((IntPtr)start);
            start = null;
            buffer = null;
        }
    }
}
