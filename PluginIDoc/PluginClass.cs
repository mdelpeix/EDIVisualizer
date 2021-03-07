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
using System.Xml;
using System.Xml.Schema;
using System.Text.RegularExpressions;
using System.Xml.Xsl;

namespace PluginApp
{
    public partial class PluginClass : UserControl, IPlugin
    {

        #region Properties

        private int startSearch;
        private string messageName { get; set; }
        private StringBuilder errorLog { get; set; }
        private string messagesPath
        {
            get
            {
                return string.Format("{0}\\Plugins\\Messages", Application.StartupPath);
            }
        }
        private bool bCancel = false;

        #endregion

        public PluginClass()
        {
            InitializeComponent();
            this.Name = "IDoc";
        }

        #region IPlugin Membres

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
                return "idoc".ToLower();
            }
        }

        public string ExtensionFilter
        {
            get
            {
                return "IDoc files|*.idoc";
            }
        }

        public EDIVisualizer.Interfaces.Type PluginType
        {
            get
            {
                return EDIVisualizer.Interfaces.Type.Norme;
            }
        }

        public bool SearchInParent
        {
            get { return true; }
        }

        public void LoadFile(string fileName)
        {

            if (fileName != string.Empty && File.Exists(fileName))
            {
                errorLog = new StringBuilder();
                messageName = this.GetMessage(fileName);
                if (messageName != string.Empty)
                {
                    rtbFile.LoadFile(fileName, RichTextBoxStreamType.PlainText);
                    Process();
                }
                else
                    throw new Exception(string.Format("File {0} not managed by EDIVisualizer", this.Name));
            }
        }

        private void Process()
        {

            /*
            * Récuperer la position du curseur (ligne, colonne)
            * http://stackoverflow.com/questions/2425847/current-line-and-column-numbers-in-a-richtextbox-in-a-winforms-application
            */

            // ligne
            int index = rtbFile.SelectionStart;
            int line = rtbFile.GetLineFromCharIndex(index);

            // colonne
            int firstChar = rtbFile.GetFirstCharIndexFromLine(line);
            int column = (index - firstChar) + 1;

            // Segment
            if (rtbFile.Lines[line].Length > 0)
            {

                string segmentName = rtbFile.Lines[line].Substring(0, 7);
                if (line == 0 || rtbFile.Lines[line].Substring(0, Properties.Settings.Default.MasterSegment.Length) == Properties.Settings.Default.MasterSegment)
                    segmentName = rtbFile.Lines[line].Substring(0, Properties.Settings.Default.MasterSegment.Length);
                /* cas particulier : le segment dans le schéma diffère d'un caractère dans les fichiers finaux (ex : E2EDK09 et E1EDK09) 
                * on remplace le deuxième caractère "2" par "1" pour retrouver sa correspondance dans le schéma */
                if (line != 0 && rtbFile.Lines[line].Substring(0, Properties.Settings.Default.MasterSegment.Length) != Properties.Settings.Default.MasterSegment)
                    segmentName = string.Concat(segmentName.Substring(0, 1), "1", segmentName.Substring(2, segmentName.Length - 2));

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
                argsList.AddParam("masterSegment", "", Properties.Settings.Default.MasterSegment);
                argsList.AddParam("HeaderSegmentPosition", "", Properties.Settings.Default.HeaderSegmentPosition);


                XslCompiledTransform transform = new XslCompiledTransform();
                transform.Load(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), @"Plugins\PluginIDoc.xslt"));

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

        public void reset()
        {
            lblInfos.Text = string.Empty;
            rtbFile.ResetText();
            WebBrowser.DocumentText = string.Empty;
        }

        public bool Autodect(string fileName)
        {

            bool blResult = false;
            if (fileName != string.Empty && File.Exists(fileName))
            {
                using (var sr = new StreamReader(fileName))
                {
                    Regex r = new Regex(@"^" + Properties.Settings.Default.MasterSegment);
                    Match m = r.Match(sr.ReadLine());
                    blResult = m.Success;
                }
            }

            return blResult;

        }

        public bool Search(string searchString)
        {

            try
            {

                int indexOfSearchText = rtbFile.Find(searchString, startSearch, RichTextBoxFinds.None);
                if (indexOfSearchText != -1)
                {
                    int endindex = searchString.Length;
                    rtbFile.Focus();
                    rtbFile.Select(indexOfSearchText, endindex);
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

        public void LoadSession(string sessionNumber, string environnement)
        {
            throw new NotImplementedException();
        }

        public int Index { get { return 5; } }

        #endregion

        #region Interface et EventArgs

        private void rtbFile_MouseClick(object sender, MouseEventArgs e)
        {
            if (rtbFile.Text.Length > 0)
                Process();
        }

        private void rtbFile_KeyUp(object sender, KeyEventArgs e)
        {
            if (rtbFile.Text.Length > 0)
                Process();
        }

        private void WebBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            int i;
            for (i = 0; i < WebBrowser.Document.Links.Count; i++)
                WebBrowser.Document.Links[i].Click += new HtmlElementEventHandler(this.LinkClick);

        }

        private void WebBrowser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            if (bCancel)
            {
                e.Cancel = true;
                bCancel = false;
            }
        }

        private void rtbFile_SelectionChanged(object sender, EventArgs e)
        {
            startSearch = rtbFile.SelectionStart;
        }

        private void copyCms_Click(object sender, EventArgs e)
        {
            try
            {
                DataObject dto = new DataObject();
                dto.SetText(rtbFile.SelectedText.Trim(), TextDataFormat.UnicodeText);
                Clipboard.Clear();
                Clipboard.SetDataObject(dto);
            }
            catch (Exception) { }
        }

        #endregion

        #region Functions

        private string GetMessage(string fileName)
        {

            string result = string.Empty;
            DirectoryInfo di = new DirectoryInfo(messagesPath);
            FileInfo[] fis = di.GetFiles("*.xml");


            string idocName = string.Empty;
            using (StreamReader sr = new StreamReader(fileName))
            {
                Match match = Regex.Match(sr.ReadLine(), Properties.Settings.Default.MasterSegment + @".{31}(\w{8})");
                if (match.Success)
                    idocName = match.Groups[1].Value;
            }

            foreach (FileInfo fi in fis)
            {

                string sfileName = Path.GetFileNameWithoutExtension(fi.Name);

                if (sfileName.ToUpper().Trim() == idocName.ToUpper().Trim())
                {
                    result = fi.FullName;
                    break;
                }

            }

            return result;

        }

        private XPathNodeIterator requestXmlFile(string expression, string Filename)
        {

            XPathDocument doc = new XPathDocument(Filename);
            XPathNavigator nav = doc.CreateNavigator();

            XmlNamespaceManager mgr = new XmlNamespaceManager(nav.NameTable);
            mgr.AddNamespace("xsd", "http://www.w3.org/2001/XMLSchema");

            XPathNodeIterator iterator = nav.Select(expression, mgr);

            return iterator;

        }

        private void selectInWebBrowser(XsltArgumentList argsList, XslCompiledTransform transform)
        {

            StringBuilder output;
            using (XmlWriter xw = XmlWriter.Create(output = new StringBuilder()))
            {
                transform.Transform(messageName, argsList, xw);
                xw.Flush();
                WebBrowser.DocumentText = output.ToString();
                Application.DoEvents();
            }

        }

        private void LinkClick(object sender, System.EventArgs e)
        {

            //href captured Object
            HtmlElement element = ((HtmlElement)sender);
            string href = element.GetAttribute("href");

            //Element for RichTextBoxSelection
            string segmentName = string.Empty;
            int position = 0;
            int longueur = 0;

            //Work on capture
            string[] linkElements = href.Replace("about:", "").Split(':');

            //Affectation
            segmentName = linkElements[0].Split('-')[0];
            position = linkElements[1] != string.Empty ? int.Parse(linkElements[1]) : 0;
            longueur = linkElements[2] != string.Empty ? int.Parse(linkElements[2]) : 0;

            //Select in RichTextBox
            selectInRichTextBox(position, segmentName, ref position, ref longueur);

            bCancel = true;

        }

        private void selectInRichTextBox(int column, string segmentName, ref int position, ref int longueur)
        {

            try
            {


                if (segmentName != Properties.Settings.Default.MasterSegment && column < int.Parse(Properties.Settings.Default.HeaderSegmentPosition))
                {
                    rtbFile.Focus();
                    rtbFile.Select(rtbFile.GetFirstCharIndexOfCurrentLine() - 1, int.Parse(Properties.Settings.Default.HeaderSegmentPosition));
                }
                else
                {

                    //Parse le fichier HTML dans le webbrowser
                    HtmlElementCollection elems = WebBrowser.Document.Links;

                    for (int i = 0; i < elems.Count; i++)
                    {

                        string linkCurrent = elems[i].GetAttribute("href").Replace("about:", "");
                        string linkLast = elems[elems.Count - 1].GetAttribute("href").Replace("about:", "");
                        string linkNext = linkLast;

                        int testPosition = int.Parse(linkCurrent.ToString().Split(':')[1]);
                        int testPositionLast = int.Parse(linkLast.ToString().Split(':')[1]);
                        int testPositionNext = testPositionLast;
                        if (!(i >= elems.Count - 1))
                        {
                            linkNext = elems[i + 1].GetAttribute("href").Replace("about:", "");
                            testPositionNext = int.Parse(linkNext.ToString().Split(':')[1]);
                        }

                        if ((column >= (testPosition) && column < (testPositionNext)) || (column >= testPositionLast && i >= elems.Count - 1))
                        {

                            longueur = int.Parse(linkCurrent.ToString().Split(':')[2]);

                            position = rtbFile.GetFirstCharIndexOfCurrentLine() + testPosition - 1;

                            rtbFile.SelectionStart = position;
                            rtbFile.SelectionLength = longueur;
                            rtbFile.Focus();
                            rtbFile.Select(position, longueur);

                            break;
                        }

                    }

                }


            }
            catch (Exception) { }

        }

        #endregion

        private void btnSegment_Click(object sender, EventArgs e)
        {
            if (File.Exists(messageName))
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
            int cursorPosition = rtbFile.SelectionStart;
            int lineIndex = rtbFile.GetLineFromCharIndex(cursorPosition);
            string lineText = rtbFile.Lines[lineIndex];
            return lineText;
        }


    }
}
