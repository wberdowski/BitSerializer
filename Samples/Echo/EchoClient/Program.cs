using BitSerializer.Samples.Echo.EchoServer;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Reflection;

namespace BitSerializer.Samples.Echo.EchoClient
{
    internal class Program
    {
        private static Socket socket = new Socket(SocketType.Dgram, ProtocolType.IP)
        {
            ReceiveTimeout = 5000,
            SendTimeout = 5000
        };
        private static readonly byte[] recvBuffer = new byte[1024];

        private static void Main(string[] args)
        {
            string version = Assembly.GetExecutingAssembly().GetName().Version.ToString(3);
            Console.WriteLine($"EchoClient {version}");
            Console.WriteLine("Type \"echo [ip address]\"");

            socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            socket.Bind(new IPEndPoint(IPAddress.Any, 50000));

            // Read commands from console
            while (true)
            {
                string[] cmd = Console.ReadLine().Split(' ');
                if (cmd[0] == "echo")
                {
                    if (cmd.Length == 2)
                    {
                        if (IPAddress.TryParse(cmd[1], out IPAddress ip))
                        {
                            SendEcho(ip);
                        }
                        else
                        {
                            Console.WriteLine("Incorrect ip address.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Usage: ping [ip address]");
                    }
                }
                else if (cmd[0] == "exit")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Type \"exit\" to close the program.");
                }
            }

            socket.Close();
            socket.Dispose();
        }

        private static void SendEcho(IPAddress ip)
        {
            try
            {
                Console.WriteLine($"Sending echo to {ip}...");

                var packet = new EchoPacket(1, EchoPacketType.Request, new byte[] { 0x01, 0x02, 0x03 });
                var bytes = BinarySerializer.Serialize(packet);

                var sw = Stopwatch.StartNew();
                socket.SendTo(bytes, new IPEndPoint(ip, 50000));
                int len = socket.Receive(recvBuffer);
                sw.Stop();

                var recvPacket = BinarySerializer.Deserialize<EchoPacket>(recvBuffer);
                if (recvPacket.Id == packet.Id && recvPacket.Type == EchoPacketType.Response)
                {
                    Console.WriteLine($"Received echo. Time: {sw.Elapsed.TotalMilliseconds} ms");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
