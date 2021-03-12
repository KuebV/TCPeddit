using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Config
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
                IP = "127.0.0.1",
                appID = "AppID",
                refreshToken = "refreshToken",
                SecretToken = "SecretToken"
            };
            string json = JsonConvert.SerializeObject(settings);
            File.WriteAllText(Config, json.Replace(",", ",\n"));
        }

        public static int getPort()
        {
            string LocalDir = Directory.GetCurrentDirectory();
            string Config = Path.Combine(LocalDir, @"config.json");
            string cfg = File.ReadAllText(Config);

            Settings result = JsonConvert.DeserializeObject<Settings>(cfg);
            return result.Port;
        }
        public static string getIP()
        {
            string LocalDir = Directory.GetCurrentDirectory();
            string Config = Path.Combine(LocalDir, @"config.json");
            string cfg = File.ReadAllText(Config);

            Settings result = JsonConvert.DeserializeObject<Settings>(cfg);
            return result.IP;
        }
        public static string getAppID()
        {
            string LocalDir = Directory.GetCurrentDirectory();
            string Config = Path.Combine(LocalDir, @"config.json");
            string cfg = File.ReadAllText(Config);

            Settings result = JsonConvert.DeserializeObject<Settings>(cfg);
            return result.appID;
        }

        public static string getRefreshToken()
        {
            string LocalDir = Directory.GetCurrentDirectory();
            string Config = Path.Combine(LocalDir, @"config.json");
            string cfg = File.ReadAllText(Config);

            Settings result = JsonConvert.DeserializeObject<Settings>(cfg);
            return result.refreshToken;
        }
        public static string getSecretToken()
        {
            string LocalDir = Directory.GetCurrentDirectory();
            string Config = Path.Combine(LocalDir, @"config.json");
            string cfg = File.ReadAllText(Config);

            Settings result = JsonConvert.DeserializeObject<Settings>(cfg);
            return result.SecretToken;
        }



    }

    class Settings
    {
        public int Port { get; set; }
        public string IP { get; set; }
        public string appID { get; set; }
        public string refreshToken { get; set; }
        public string SecretToken { get; set; }

        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}, {3}, {4}", Port, IP, appID, refreshToken, SecretToken);
        }
    }
}
