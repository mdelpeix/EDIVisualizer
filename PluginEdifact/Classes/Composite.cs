using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PluginApp.Classes
{
    public class Composite
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public List<DataElement> DataElements;
        public Composite(string content)
        {
            Content = content;
            DataElements = new List<DataElement>();
            string[] deColl = content.Split(':');
            foreach (string item in deColl)
            {
                if (item.Trim() != string.Empty)
                {
                    Name = item.Trim();
                    DataElement dataElement = new DataElement(Name);
                    DataElements.Add(dataElement);
                }

            }
        }
    }
}
