using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.IO;
using System.Threading.Tasks;
using System.Net;

namespace Client
{
    public class Client
    {
        public static void StartClient()
        {
            TcpClient client = new TcpClient();
            ClientConfig config = new ClientConfig();
            var Host = config.HostIP();
            var Port = config.HostPort();

            bool shutdown = false;
            try
            {
                client.Connect(Host, Port);
            }
            catch
            {
                CC.Message(ConsoleColor.Red, "Connection Failed. Aborting!");
                shutdown = true;
            }
            CC.Message(ConsoleColor.Green, "Connected to Host!");
            CC.Message(ConsoleColor.Yellow, "Trying Connection...");
            Write(client.GetStream(), Encoding.UTF8.GetBytes("inc_Conn"));
            CC.Message(ConsoleColor.Yellow, "Sent Connection, Waiting for Response");

            NetworkStream stream = client.GetStream();

            while (!shutdown)
            {
                CC.Message(ConsoleColor.Cyan, "Enter in a subreddit you would like to enter \nExample : r/pics");
                string subReddit = Console.ReadLine();
                Write(client.GetStream(), Encoding.UTF8.GetBytes(subReddit));
                if (stream.DataAvailable)
                {
                    byte[] data = ReadToEnd(stream);
                    Console.WriteLine(Encoding.UTF8.GetString(data));
                }
            }

            Thread.Sleep(5000);
        }

        private static void StartHandler(NetworkStream stream)
        {
            while (true)
            {
                if (stream.DataAvailable)
                {
                    byte[] data = ReadToEnd(stream);

                    Console.WriteLine(Encoding.UTF8.GetString(data));

                    stream.Close();
                }
                else
                {
                    for (int i = 0; i > 25; i++)
                    {
                        CC.Message(ConsoleColor.Red, "Connection not made, Attempt #" + i);
                        Thread.Sleep(1000);
                        i++;
                    }
                }
            }
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

        private static void Write(NetworkStream stream, byte[] data)
        {
            stream.Write(data, 0, data.Length);
        }

    }
}
