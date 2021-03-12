using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            if (ClientConfig.firstRun())
            {
                Console.WriteLine("Running First Time Setup");
                ClientConfig.setupConfig();
            }
            CC.Message(ConsoleColor.Green, "Config Loaded");

            CC.Message(ConsoleColor.Yellow, "Attempting to Connect to HostIP");
            Client.StartClient();
        }
    }
}
