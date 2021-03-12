using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    public class Server
    {
        private static TcpListener _listener;
        public static bool shutDownServer = false;
        public static void StartServer()
        {
            Config config = new Config();
            _listener = new TcpListener(IPAddress.Any, Config.getPort());
            _listener.Start();

            CC.Message(ConsoleColor.Green, "TCP Server Launched!");
            CC.Message(ConsoleColor.Gray, $"Listening on {Config.getIP()}:{Config.getPort()}");
            while (!shutDownServer)
            {
                TcpClient _client = _listener.AcceptTcpClient();
                NetworkStream stream = _client.GetStream();
                Task.Run(() =>
                {
                    while (true)
                    {
                        if (stream.DataAvailable)
                        {
                            byte[] receive = ReadToEnd(stream);
                            string fData = Encoding.UTF8.GetString(receive);
                            CC.Message(ConsoleColor.Gray, "Incoming Data : " + fData);
                            switch (fData)
                            {
                                case "inc_Conn":
                                    Write(stream, Encoding.UTF8.GetBytes("recv_Conn"));
                                    break;
                                default:
                                    CC.Message(ConsoleColor.Red, "Unknown Connection");
                                    break;
                            }
                            if (fData.Contains("r/"))
                                RedditAPI.getPosts(fData.Replace("r/", ""));
                        }
                        else
                        {
                            Thread.Sleep(7500);
                            CC.Message(ConsoleColor.Gray, "Waiting for Data...");
                        }
                    }
                });
            }
            _listener.Stop();
            CC.Message(ConsoleColor.Red, "Closing Connections");
        }

        private static byte[] ReadToEnd(NetworkStream stream)
        {
            List<byte> receivedBytes = new List<byte>();
            while (stream.DataAvailable)
            {
                byte[] buffer = new byte[1024];

                stream.Read(buffer, 0, buffer.Length);

                receivedBytes.AddRange(buffer);
            }

            receivedBytes.RemoveAll(b => b == 0);

            return receivedBytes.ToArray();
        }

        public static void Write(NetworkStream stream, byte[] data)
        {
            stream.Write(data, 0, data.Length);

        }
    }
}
