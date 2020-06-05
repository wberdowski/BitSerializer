using System;
using System.Net;
using System.Net.Sockets;
using System.Reflection;

namespace BitSerializer.Samples.Echo.EchoServer
{
    internal class Program
    {
        private static readonly Socket socket = new Socket(SocketType.Dgram, ProtocolType.IP);
        private static readonly byte[] recvBuffer = new byte[1024];
        private static EndPoint ep = new IPEndPoint(IPAddress.Any, 50000);

        private static void Main(string[] args)
        {
            string version = Assembly.GetExecutingAssembly().GetName().Version.ToString(3);
            Console.WriteLine($"EchoServer {version}");

            socket.Bind(new IPEndPoint(IPAddress.Any, 50000));
            Console.WriteLine($"Listening at {socket.LocalEndPoint}");

            // Start receiving data
            socket.BeginReceiveFrom(recvBuffer, 0, recvBuffer.Length, SocketFlags.None, ref ep, OnReceive, null);

            // Read command from console
            while (Console.ReadLine() != "exit")
            {
                Console.WriteLine("Type \"exit\" to close the program.");
            }

            socket.Close();
            socket.Dispose();
        }

        private static void OnReceive(IAsyncResult ar)
        {
            EndPoint ep2 = new IPEndPoint(IPAddress.Any, 0);
            var len = socket.EndReceiveFrom(ar, ref ep2);

            var recvPacket = BinarySerializer.Deserialize<EchoPacket>(recvBuffer);
            if (recvPacket.Type == EchoPacketType.Request)
            {
                var resPacket = new EchoPacket(recvPacket.Id, EchoPacketType.Response, recvPacket.Payload);
                var bytes = BinarySerializer.Serialize(resPacket);
                socket.SendTo(bytes, (IPEndPoint)ep2);
                Console.WriteLine($"Sending echo response to {(IPEndPoint)ep2}");
            }

            // Start receiving data
            socket.BeginReceiveFrom(recvBuffer, 0, recvBuffer.Length, SocketFlags.None, ref ep, OnReceive, null);
        }
    }
}
