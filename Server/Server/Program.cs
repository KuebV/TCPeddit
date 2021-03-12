using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            ProgramInfo info = new ProgramInfo();
            Config config = new Config();
            CC.Message(ConsoleColor.Red, "Starting up TCPeddit...");

            CC.Message(ConsoleColor.Green, $"Author : {info.Author}\nVersion : {info.Version}");
            if (Config.firstRun())
            {
                CC.Message(ConsoleColor.Yellow, "Running first time setup");
                Config.setupConfig();
            }
            CC.Message(ConsoleColor.Green, "Config Loaded!");
            CC.Message(ConsoleColor.Green, $"IP : {Config.getIP()}:{Config.getPort()}");
            string getToken = Config.getSecretToken();
            if (getToken.Contains("SecretToken")){
                CC.Message(ConsoleColor.Red, "Invalid Secret Token! Aborting..." + getToken);
                Thread.Sleep(10000);
                Environment.Exit(0);
            }
            CC.Message(ConsoleColor.Green, $"Logged into Reddit Account : {RedditAPI.getUsername()}");
            CC.Message(ConsoleColor.Yellow, "Launching TCP Server");
            Server.StartServer();
        }
    }

    class ProgramInfo
    {
        public Version Version { get; set; } = new Version(1, 0, 0);
        public string Author { get; set; } = "KuebV";
    }
}
