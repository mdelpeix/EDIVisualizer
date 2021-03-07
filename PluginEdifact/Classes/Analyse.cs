using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PluginApp.Classes
{
    public class Analyse
    {
        public string Message { get; set; }
        public string Version { get; set; }
        public string urlMessage { get; set; }
        public string urlSegment { get; set; }
        public Analyse(string message, string version)
        {
            Message = message;
            Version = version;
            urlMessage = string.Format(Properties.Settings.Default.urlMessage, Version.ToLower(), Message.ToLower());
        }
    }
}
