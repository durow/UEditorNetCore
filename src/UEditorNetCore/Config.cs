using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;

namespace UEditorNetCore
{
    public static class Config
    {
        public static string WebRootPath { get; set; }
        public static string ConfigFile { set; get; } = "config.json";
        public static bool noCache { set; get; } = true;
        private static JObject BuildItems()
        {
            var json = File.ReadAllText(ConfigFile);
            return JObject.Parse(json);
        }

        public static JObject Items
        {
            get
            {
                if (noCache || _Items == null)
                {
                    _Items = BuildItems();
                }
                return _Items;
            }
        }
        private static JObject _Items;


        public static T GetValue<T>(string key)
        {
            return Items[key].Value<T>();
        }

        public static String[] GetStringList(string key)
        {
            return Items[key].Select(x => x.Value<String>()).ToArray();
        }

        public static String GetString(string key)
        {
            return GetValue<String>(key);
        }

        public static int GetInt(string key)
        {
            return GetValue<int>(key);
        }
    }
}