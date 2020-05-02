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
                Encoding.UTF8.GetBytes("This is an encoded message. Sample text is this elo pomelo 320.This is an encoded message. Sample text is this elo pomelo 320.This is an encoded message. Sample text is this elo pomelo 320.")
            );

            Console.WriteLine("Running BitSerializer benchmark...");
            RunBenchmark_BitSerializer(dataSchema);
            Console.WriteLine();

            Console.WriteLine("Running Newtonsoft.Json benchmark...");
            RunBenchmark_NewtonsoftJson(dataSchema);
            Console.WriteLine();

            Console.WriteLine("Running BinaryFormatter benchmark...");
            RunBenchmark_BinaryFormatter(dataSchema);
            Console.WriteLine();

            Console.WriteLine("Benchamarking finished.");

            Console.ReadKey();
        }

        private static void RunBenchmark_BitSerializer(SampleSchema dataSchema)
        {
            var totalSw = Stopwatch.StartNew();
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
            totalSw.Stop();

            PrintResults("BitSerializer (Serialization)", lowestSerializationTime);
            PrintResults("BinarySerializer (Bytes)", serialized.Length, false);
            PrintResults("BitSerializer (Deserialization)", lowestDeserializationTime);
            PrintResults("BinarySerializer (Total)", totalSw.Elapsed.TotalMilliseconds);
        }

        private static void RunBenchmark_NewtonsoftJson(SampleSchema dataSchema)
        {
            var totalSw = Stopwatch.StartNew();
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
            totalSw.Stop();

            PrintResults("Newtonsoft.Json (Serialization)", lowestSerializationTime);
            PrintResults("Newtonsoft.Json (Bytes)", serialized.Length, false);
            PrintResults("Newtonsoft.Json (Deserialization)", lowestDeserializationTime);
            PrintResults("Newtonsoft.Json (Total)", totalSw.Elapsed.TotalMilliseconds);
        }

        private static void RunBenchmark_BinaryFormatter(SampleSchema dataSchema)
        {
            var totalSw = Stopwatch.StartNew();
            long len = 0;
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

                    len = stream.Position;
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
                totalSw.Stop();

                PrintResults("BinaryFormatter (Serialization)", lowestSerializationTime);
                PrintResults("BinaryFormatter (Bytes)", len, false);
                PrintResults("BinaryFormatter (Deserialization)", lowestDeserializationTime);
                PrintResults("BinaryFormatter (Total)", totalSw.Elapsed.TotalMilliseconds);
            }
        }

        private static void PrintResults(string caption, double value, bool isTime = true)
        {
            if (isTime)
            {
                Console.WriteLine($"{caption.PadRight(50)}{value:0.0000} ms ({value * 1000:0.00} us)");
            }
            else
            {
                Console.WriteLine($"{caption.PadRight(50)}{value}");
            }
        }
    }
}
