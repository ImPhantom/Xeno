using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xeno.Utilities
{
    public class Configuration
    {
        public char cmdPrefix { get; set; }
        public ulong[] botOwners { get; set; }
        public string botToken { get; set; }
        public string logChannel { get; set; }

        public Configuration()
        {
            cmdPrefix = '~';
            botOwners = new ulong[] { 0 };
            botToken = "";
            logChannel = "serverlog";
        }

        public void Save(string dir = "cfg/config.json")
        {
            File.WriteAllText(dir, ToJson());
        }

        public static Configuration Load(string dir = "cfg/config.json") => JsonConvert.DeserializeObject<Configuration>(File.ReadAllText(dir));
        public string ToJson() => JsonConvert.SerializeObject(this, Formatting.Indented);
    }
}
