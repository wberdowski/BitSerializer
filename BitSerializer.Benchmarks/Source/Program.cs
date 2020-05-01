using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace BitSerializer.Benchmarks
{
    internal class Program
    {
        private const int Iterations = 1_000_000;

        private static void Main()
        {
            var dataSchema = new SampleSchema(
                0,
                SampleEnum.Value1,
                "sample text",
                Encoding.UTF8.GetBytes("This is an encoded message")
            );

            Console.WriteLine("Running BitSerializer benchmark...");
            RunBenchmark_BitSerializer(dataSchema);

            Console.WriteLine("Running Newtonsoft.Json benchmark...");
            RunBenchmark_NewtonsoftJson(dataSchema);

            Console.WriteLine("Running BinaryFormatter benchmark...");
            RunBenchmark_BinaryFormatter(dataSchema);

            Console.WriteLine("Benchamrking finished.");

            Console.ReadKey();
        }

        private static void RunBenchmark_BitSerializer(SampleSchema dataSchema)
        {
            byte[] serialized = null;
            double lowestSerializationTime = double.MaxValue;
            double lowestDeserializationTime = double.MaxValue;

            // Serialization
            for (int i = 0; i < Iterations; i++)
            {
                dataSchema.SampleInt = i;

                var sw = Stopwatch.StartNew();
                serialized = BinarySerializer.Serialize(dataSchema);
                sw.Stop();

                if (sw.Elapsed.TotalMilliseconds < lowestSerializationTime)
                {
                    lowestSerializationTime = sw.Elapsed.TotalMilliseconds;
                }
            }

            // Deserialization
            for (int i = 0; i < Iterations; i++)
            {
                var sw = Stopwatch.StartNew();
                BinarySerializer.Deserialize<SampleSchema>(serialized);
                sw.Stop();

                if (sw.Elapsed.TotalMilliseconds < lowestDeserializationTime)
                {
                    lowestDeserializationTime = sw.Elapsed.TotalMilliseconds;
                }
            }

            PrintResults("BitSerializer (Serialization)", lowestSerializationTime);
            PrintResults("BitSerializer (Deserialization)", lowestDeserializationTime);
        }

        private static void RunBenchmark_NewtonsoftJson(SampleSchema dataSchema)
        {
            string serialized = null;
            double lowestSerializationTime = double.MaxValue;
            double lowestDeserializationTime = double.MaxValue;

            // Serialization
            for (int i = 0; i < Iterations; i++)
            {
                dataSchema.SampleInt = i;

                var sw = Stopwatch.StartNew();
                serialized = JsonConvert.SerializeObject(dataSchema);
                sw.Stop();

                if (sw.Elapsed.TotalMilliseconds < lowestSerializationTime)
                {
                    lowestSerializationTime = sw.Elapsed.TotalMilliseconds;
                }
            }

            // Deserialization
            for (int i = 0; i < Iterations; i++)
            {
                var sw = Stopwatch.StartNew();
                JsonConvert.DeserializeObject<SampleSchema>(serialized);
                sw.Stop();

                if (sw.Elapsed.TotalMilliseconds < lowestDeserializationTime)
                {
                    lowestDeserializationTime = sw.Elapsed.TotalMilliseconds;
                }
            }

            PrintResults("Newtonsoft.Json (Serialization)", lowestSerializationTime);
            PrintResults("Newtonsoft.Json (Deserialization)", lowestDeserializationTime);
        }

        private static void RunBenchmark_BinaryFormatter(SampleSchema dataSchema)
        {
            using (var stream = new MemoryStream())
            {
                double lowestSerializationTime = double.MaxValue;
                double lowestDeserializationTime = double.MaxValue;

                // Serialization
                for (int i = 0; i < Iterations; i++)
                {
                    dataSchema.SampleInt = i;

                    var sw = Stopwatch.StartNew();
                    new BinaryFormatter().Serialize(stream, dataSchema);
                    sw.Stop();

                    stream.Position = 0;

                    if (sw.Elapsed.TotalMilliseconds < lowestSerializationTime)
                    {
                        lowestSerializationTime = sw.Elapsed.TotalMilliseconds;
                    }
                }

                // Deserialization
                for (int i = 0; i < Iterations; i++)
                {
                    var sw = Stopwatch.StartNew();
                    new BinaryFormatter().Deserialize(stream);
                    sw.Stop();

                    stream.Position = 0;

                    if (sw.Elapsed.TotalMilliseconds < lowestDeserializationTime)
                    {
                        lowestDeserializationTime = sw.Elapsed.TotalMilliseconds;
                    }
                }

                PrintResults("BinaryFormatter (Serialization)", lowestSerializationTime);
                PrintResults("BinaryFormatter (Deserialization)", lowestDeserializationTime);
            }
        }

        private static void PrintResults(string caption, double lowestTime)
        {
            Console.WriteLine($"{caption.PadRight(50)} MIN: {lowestTime} ms ({lowestTime * 1000} us)");
        }
    }
}
