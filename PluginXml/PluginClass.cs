using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EDIVisualizer.Interfaces;
using System.Xml;
using System.Xml.Schema;
using System.Collections;

namespace PluginApp
{

    public partial class PluginClass : UserControl, IPlugin
    {

        private ArrayList errors = new ArrayList();
        private bool bValid;

        public PluginClass()
        {
            InitializeComponent();
            this.Name = "Xml";
        }

        #region IPlugin Membres

        public bool SearchInParent
        {
            get { return false; }
        }

        public string Comment
        {
            get { return ""; }
        }

        public string Extension
        {
            get
            {
                return "xml".ToLower();
            }
        }

        public string ExtensionFilter
        {
            get
            {
                return "Xml files|*.xml";
            }
        }

        public EDIVisualizer.Interfaces.Type PluginType
        {
            get { return EDIVisualizer.Interfaces.Type.Norme; }
        }

        public void LoadFile(string fileName)
        {
            wbXml.Navigate(fileName);
        }

        public void reset()
        {
            wbXml.DocumentText = string.Empty;
        }

        public bool Autodect(string fileName)
        {

            try
            {

                errors.Clear();
                bValid = true;

                XmlTextReader txtreader = new XmlTextReader(fileName);
                using (XmlValidatingReader reader = new XmlValidatingReader(txtreader))
                {
                    reader.ValidationEventHandler += new ValidationEventHandler(ValidationCallBack);
                    while (reader.Read()) { }
                }

            }
            catch (Exception e)
            {
                bValid = false;
                errors.Add(e.Message);
            }

            return bValid;

        }

        public bool Search(string searchString)
        {
            if (wbXml.Document.Url.ToString()!="about:blank")
            {
                wbXml.Select();
                SendKeys.Send("^f");
                return true;
            }
            else
            {
                return false;
            }
        }

        public void LoadSession(string sessionNumber, string environnement)
        {
            throw new NotImplementedException();
        }

        public int Index { get { return 5; } }

        #endregion

        private void ValidationCallBack(object sender, ValidationEventArgs args)
        {
            bValid = false;
            errors.Add(args.Message);
        }

    }

}
