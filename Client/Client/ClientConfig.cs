using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Client
{
    public class ClientConfig
    {

        public static bool firstRun()
        {
            string Config = Path.Combine(Directory.GetCurrentDirectory(), "config.json");
            if (!File.Exists(Config))
                return true;
            else
                return false;
        }

        public static void setupConfig()
        {
            string LocalDir = Directory.GetCurrentDirectory();
            string Config = Path.Combine(LocalDir, "config.json");
            if (!File.Exists(Config))
                File.Create(Config).Close();

            Settings settings = new Settings()
            {
                Port = 9000,
                HostIP = "127.0.0.1"
            };
            string json = JsonConvert.SerializeObject(settings);
            File.WriteAllText(Config, json);
        }

        public string HostIP()
        {
            string LocalDir = Directory.GetCurrentDirectory();
            string Config = Path.Combine(LocalDir, @"config.json");
            string cfg = File.ReadAllText(Config);

            Settings result = JsonConvert.DeserializeObject<Settings>(cfg);
            return result.HostIP;
        }

        public int HostPort()
        {
            string LocalDir = Directory.GetCurrentDirectory();
            string Config = Path.Combine(LocalDir, @"config.json");
            string cfg = File.ReadAllText(Config);

            Settings result = JsonConvert.DeserializeObject<Settings>(cfg);
            return result.Port;
        }
    }


    class Settings
    {
        public int Port { get; set; } = 9000;
        public string HostIP { get; set; } = "127.0.0.1";

    }
}
