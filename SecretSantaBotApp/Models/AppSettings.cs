using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace SecretSantaBotApp.Models
{
    public static class AppSettings
    {
        public static string Url { get; set; } = ConfigurationManager.AppSettings["SecretSantaBotUrl"];
        public static string Name { get; set; } = "privy_santa_bot";
        public static string Key { get; set; } = ConfigurationManager.AppSettings["SecretSantaBotKey"];
    }
}