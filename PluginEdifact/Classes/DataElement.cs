using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PluginApp.Classes
{
    public class DataElement
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public DataElement(string content)
        {
            if (content.Trim() != string.Empty)
            {
                Name = content.Trim();
                Content = Name;
            }
        }
    }
}
