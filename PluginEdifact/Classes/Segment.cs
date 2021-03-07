using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PluginApp.Classes
{
    public class Segment
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public List<Composite> Composites;
        public List<Composite> Segments;

        public Segment()
        {

        }

        public Segment(string name, string content)
        {
            Name = name;
            Content = content;
        }

        public static TreeNode[] GetDataElement(string DataElementName, char DataElementSeparator)
        {
            string[] DataElements = DataElementName.Split(DataElementSeparator);
            TreeNode[] tns = new TreeNode[DataElements.Length - 1];
            for (int i = 0; i < tns.Length; i++)
            {
                TreeNode tn = new TreeNode(DataElements[i + 1]);
                tn.Tag = "POS:" + ((i + 1) * 10).ToString("0#0");
                tns.SetValue(tn, i);
            }
            return tns;
        }
    }
}
