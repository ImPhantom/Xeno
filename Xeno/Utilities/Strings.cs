using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text.RegularExpressions;

namespace Xeno.Utilities
{
    public class Strings
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
        public static string tokenError = "[Error] You havent set your token. Put your bot token in App.config.";

        


        // String Util
        public static List<string> getLinks(string message)
        {
            List<string> list = new List<string>();
            Regex urlRx = new Regex(@"((https?|ftp|file)\://|www.)[A-Za-z0-9\.\-]+(/[A-Za-z0-9\?\&\=;\+!'\(\)\*\-\._~%]*)*", RegexOptions.IgnoreCase);

            MatchCollection matches = urlRx.Matches(message);
            foreach (Match match in matches)
            {
                list.Add(match.Value);
            }
            return list;
        }

        public static string replaceLinks(string message, List<string> list)
        {
            var replaced = string.Empty;
            foreach (string n in list)
            {
                replaced = message.Replace(n, "*-link-*");
            }
            return replaced;
        }

    }
}
