﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text.RegularExpressions;

namespace Xeno.Utilities
{
    public class Strings
    {
        // Config Strings
        public static string logChannel = Configuration.Load().logChannel;

        // Static Strings
        public static string infoPrefix = "[Info] [Console] ";
        public static string infoEvent = "[Info] [Event] ";
        public static string tokenError = "[Error] You havent set your token. Put your bot token in Xeno.exe.config (type 'config') to open.";

        public static string adminError = " you must have **Administrator** permissions to run that command.";



        // Help Strings
        public static string setHelp = @"**set status** <string>    
    *Sets the bots Game/Status.*
**set avatar** <url>       
    *Sets the bots Avatar.*
**set nick** <string>      
    *Sets the bots Nickname.*
**set username** <string>  
    *Sets the bots Username. (limited)*";
        


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
