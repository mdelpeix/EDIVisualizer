using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EDIVisualizer.Interfaces;
using System.IO;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Text.RegularExpressions;

namespace PluginApp
{
    public partial class PluginClass : UserControl, IPlugin
    {


        private int startSearch;
        private bool bCancel = false;

        public PluginClass()
        {
            InitializeComponent();
            this.Name = "Vda";
        }

        private string messageName { get; set; }
        private string messageFile { get; set; }
        private StringBuilder errorLog { get; set; }

        private string messagesPath
        {
            get
            {
                return string.Format("{0}\\Plugins\\Messages", Application.StartupPath);
            }
        }

        #region IPlugin Membres

        public bool SearchInParent
        {
            get { return true; }
        }

        public string Comment
        {
            get
            {
                return string.Empty;
            }
        }

        public string Extension
        {
            get
            {
                return "vda".ToLower();
            }
        }

        public string ExtensionFilter
        {
            get
            {
                return "Vda files|*.vda";
            }
        }

        public EDIVisualizer.Interfaces.Type PluginType
        {
            get
            {
                return EDIVisualizer.Interfaces.Type.Norme;
            }
        }

        public void LoadFile(string fileName)
        {

            if (fileName != string.Empty && File.Exists(fileName))
            {
                errorLog = new StringBuilder();
                messageFile = fileName;
                messageName = this.GetMessage(fileName);
                switch (Path.GetFileName(messageName))
                {
                    case "4913.xml":
                        btnAnalyz.Visible = true;
                        break;
                    default:
                        btnAnalyz.Visible = false;
                        break;
                }
                if (messageName != string.Empty)
                {
                    rtbVDAFile.LoadFile(fileName, RichTextBoxStreamType.PlainText);
                    Process();
                }
                else
                    throw new Exception(string.Format("Inappropriate {0} file", this.Name));
            }

        }

        public bool Autodect(string fileName)
        {

            bool blResult = false;
            if (fileName != string.Empty && File.Exists(fileName))
            {
                using (var sr = new StreamReader(fileName))
                {
                    Regex r = new Regex(@"^\d\d\d");
                    Match m = r.Match(sr.ReadLine());
                    blResult = m.Success;
                }
            }

            return blResult;

        }

        private string GetMessage(string fileName)
        {

            string result = string.Empty;
            DirectoryInfo di = new DirectoryInfo(messagesPath);
            FileInfo[] fis = di.GetFiles("*.xml");


            foreach (FileInfo fi in fis)
            {

                XPathNodeIterator iterator = requestXmlFile("vda/keyWord", fi.FullName);
                while (iterator.MoveNext())
                {

                    using (StreamReader sr = new StreamReader(fileName))
                    {
                        string firstWord = sr.ReadLine().Substring(0, iterator.Current.Value.Length);
                        if (firstWord == iterator.Current.Value)
                        {
                            result = fi.FullName;
                            break;
                        }
                    }

                }

            }


            if (result != string.Empty)
            {
                //Structure validation 
                this.ValideXml(result);
                if (errorLog.Length > 0)
                    MessageBox.Show(errorLog.ToString().Substring(0, 1000), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return result;

        }

        public bool Search(string searchString)
        {

            try
            {

                int indexOfSearchText = rtbVDAFile.Find(searchString, startSearch, RichTextBoxFinds.None);
                if (indexOfSearchText != -1)
                {
                    int endindex = searchString.Length;
                    rtbVDAFile.Focus();
                    rtbVDAFile.Select(indexOfSearchText, endindex);
                    startSearch = indexOfSearchText + endindex;
                    return true;
                }
                else
                    return false;

            }
            catch (Exception)
            {
                return false;
            }

        }

        public void reset()
        {
            lblInfos.Text = string.Empty;
            rtbVDAFile.ResetText();
            WebBrowser.DocumentText = string.Empty;
            btnSegment.Visible = false;
            btnAnalyz.Visible = false;
        }

        public void LoadSession(string sessionNumber, string environnement)
        {
            throw new NotImplementedException();
        }

        public int Index { get { return 5; } }

        #endregion

        #region Interface et EventArgs

        private void rtbVDAFile_MouseClick(object sender, MouseEventArgs e)
        {
            if (rtbVDAFile.Text.Length > 0)
                Process();
        }

        private void rtbVDAFile_KeyUp(object sender, KeyEventArgs e)
        {
            if (rtbVDAFile.Text.Length > 0)
                Process();
        }

        private void XmlValidationError(object sender, ValidationEventArgs e)
        {
            errorLog.AppendLine(string.Format("Line {0} position {1} : {2}", e.Exception.LineNumber, e.Exception.LinePosition, e.Exception.Message));
        }

        private void WebBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            int i;
            for (i = 0; i < WebBrowser.Document.Links.Count; i++)
                WebBrowser.Document.Links[i].Click += new HtmlElementEventHandler(this.LinkClick);
        }

        private void WebBrowser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            if (bCancel == true)
            {
                e.Cancel = true;
                bCancel = false;
            }
        }

        private void rtbVDAFile_SelectionChanged(object sender, EventArgs e)
        {
            startSearch = rtbVDAFile.SelectionStart;
        }

        private void copyCms_Click(object sender, EventArgs e)
        {
            try
            {
                DataObject dto = new DataObject();
                dto.SetText(rtbVDAFile.SelectedText.Trim(), TextDataFormat.UnicodeText);
                Clipboard.Clear();
                Clipboard.SetDataObject(dto);
            }
            catch (Exception) { }

        }

        #endregion

        #region Fonctions

        private void Process()
        {

            /*
            * Récuperer la position du curseur (ligne, colonne)
            * http://stackoverflow.com/questions/2425847/current-line-and-column-numbers-in-a-richtextbox-in-a-winforms-application
            */

            // ligne
            int index = rtbVDAFile.SelectionStart;
            int line = rtbVDAFile.GetLineFromCharIndex(index);

            // colonne
            int firstChar = rtbVDAFile.GetFirstCharIndexFromLine(line);
            int column = (index - firstChar) + 1;

            // Segment
            if (rtbVDAFile.Lines[line].Length > 0)
            {

                string segmentName = rtbVDAFile.Lines[line].Substring(0, 3);

                lblInfos.Text = string.Format("Segment {0} \r\nPosition {1}", segmentName, column);

                // Position
                int position = 0;

                // Longueur
                int longueur = 0;


                /*
                * Tansformation XSL avec VDA.xslt pour afficher les informations du sugnment et de la zone sélectionnée
                * http://www.codeproject.com/Articles/9494/Manipulate-XML-data-with-XPath-and-XmlDocument-C
                */

                XsltArgumentList argsList = new XsltArgumentList();
                argsList.AddParam("segmentName", "", segmentName);
                argsList.AddParam("position", "", column);

                XslCompiledTransform transform = new XslCompiledTransform();
                transform.Load(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), @"Plugins\PluginVda.xslt"));

                /*
                * Afficher les informations dans le webbrowser
                */
                selectInWebBrowser(argsList, transform);

                /*
                * Sélection de la zone dans le RichTextBox
                */
                selectInRichTextBox(column, segmentName, ref position, ref longueur);

            }

        }

        private void selectInWebBrowser(XsltArgumentList argsList, XslCompiledTransform transform)
        {

            StringBuilder output;
            using (XmlWriter xw = XmlWriter.Create(output = new StringBuilder()))
            {
                transform.Transform(messageName, argsList, xw);
                xw.Flush();
                WebBrowser.DocumentText = output.ToString();
                btnSegment.Visible = true;
                Application.DoEvents();
            }

        }


        private XPathNodeIterator requestXmlFile(string expression, string Filename)
        {

            XPathDocument doc = new XPathDocument(Filename);
            XPathNavigator nav = doc.CreateNavigator();

            XPathExpression expr;
            expr = nav.Compile(expression);
            XPathNodeIterator iterator = nav.Select(expr);

            return iterator;

        }

        public void ValideXml(string file)
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.Schemas.Add(null, Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), @"Plugins\PluginVda.xsd"));
            settings.ValidationType = ValidationType.Schema;
            settings.ValidationEventHandler += new ValidationEventHandler(XmlValidationError);
            XmlReader reader = XmlReader.Create(file, settings);
            while (reader.Read()) ;
        }

        private void LinkClick(object sender, System.EventArgs e)
        {

            //href captured Object
            HtmlElement element = ((HtmlElement)sender);
            string href = element.GetAttribute("href").Replace("about:", "").Trim();

            //Element for RichTextBoxSelection
            string segmentName = string.Empty;
            int position = 0;
            int longueur = 0;

            //Work on capture
            string[] linkElements = href.Split(':');

            //Affectation
            segmentName = linkElements[0].Split('-')[0];
            position = int.Parse(linkElements[1]);
            longueur = int.Parse(linkElements[2]);

            //Select in RichTextBox
            selectInRichTextBox(position, segmentName, ref position, ref longueur);

            bCancel = true;

        }

        private void selectInRichTextBox(int column, string segmentName, ref int position, ref int longueur)
        {

            try
            {

                XmlDocument doc = new XmlDocument();
                doc.Load(messageName);
                XmlNode node = doc.SelectSingleNode(string.Format("//field[../../rowName='{0}'][./position<={1}][following::position[../../../rowName='{0}']>{1} or count(following-sibling::field)=0][last()]", segmentName, column));

                if (node != null)
                {

                    int lineNumber = rtbVDAFile.GetLineFromCharIndex(rtbVDAFile.GetFirstCharIndexOfCurrentLine());
                    int pos = int.Parse(node["position"].InnerText);
                    longueur = int.Parse(node["size"].InnerText);

                    if (pos + longueur > rtbVDAFile.Lines[lineNumber].Length)
                        longueur = rtbVDAFile.Lines[lineNumber].Length - pos +1 ;

                    int longueurLine = rtbVDAFile.Lines[lineNumber].Length + 1;
                    position = rtbVDAFile.GetFirstCharIndexOfCurrentLine() + pos - 1;

                    rtbVDAFile.SelectionStart = position;
                    rtbVDAFile.SelectionLength = longueur;
                    rtbVDAFile.Focus();
                    rtbVDAFile.Select(position, longueur);

                }

            }
            catch (Exception) { }

        }

        #endregion

        private void btnAnalyz_Click(object sender, EventArgs e)
        {
            if (File.Exists(messageFile))
            {
                frm4913 frm = new frm4913();
                frm.VDA4913File = messageFile;
                frm.ShowDialog(this);
                frm.Dispose();
            }
        }

        private void btnSegment_Click(object sender, EventArgs e)
        {
            if (File.Exists(messageFile) && File.Exists(messageName))
            {
                frmReport frm = new frmReport();
                frm.PivotFile = messageName;
                frm.MessageLine = this.GetLineText();
                frm.ShowDialog();
                frm.Dispose();
            }
        }

        private string GetLineText()
        {
            int cursorPosition = rtbVDAFile.SelectionStart;
            int lineIndex = rtbVDAFile.GetLineFromCharIndex(cursorPosition);
            string lineText = rtbVDAFile.Lines[lineIndex];
            return lineText;
        }

    }
}
