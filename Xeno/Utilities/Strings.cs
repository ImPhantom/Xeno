using System;
using System.Configuration;

namespace Xeno.Utilities
{
    class Strings
    {
        // Config Strings
        public static string botToken = ConfigurationManager.AppSettings["token"];
        public static string appName = ConfigurationManager.AppSettings["appName"];
        public static string appUrl = ConfigurationManager.AppSettings["appUrl"];
        public static string serverName = ConfigurationManager.AppSettings["serverName"];
        public static string startStatus = ConfigurationManager.AppSettings["status"];
        public static string logChannel = ConfigurationManager.AppSettings["logChannel"];

        public static char prefixChar = Convert.ToChar(ConfigurationManager.AppSettings["commandPrefix"]);

        // Static Strings
        public static string infoPrefix = "[Info] [Console] ";
        public static string infoEvent = "[Info] [Event] ";
        public static string tokenError = "[Error] You havent set your token. Put the bot token in App.config.";
    }
}
