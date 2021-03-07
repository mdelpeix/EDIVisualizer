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
using System.Text.RegularExpressions;
using System.Xml.XPath;
using System.Xml;
using System.Xml.Schema;
using PluginApp.Classes;
using System.Xml.Xsl;

namespace PluginApp
{
    public partial class PluginClass : UserControl, IPlugin
    {

        private Interchange interchange;
        public string messagesPath
        {
            get
            {
                return string.Format("{0}\\Plugins\\Messages", Application.StartupPath);
            }
        }
        private StringBuilder errorLog { get; set; }
        bool bNavigate = false;
        HtmlElement bNavigateElem;
        string bNavigateValue = string.Empty;
        string bNavigateSegment = string.Empty;
        WebBrowser WebBrowserOdette;
        string htmlStyle = "background-color:red;color:white;padding-left:2px;padding-right:2px;";
        string MessageFile;
        string MessageName;

        public PluginClass()
        {
            InitializeComponent();
            this.Name = "Odette";
        }


        public string Comment
        {
            get { return string.Empty; }
        }

        public string Extension
        {
            get
            {
                return "odtt".ToLower();
            }
        }

        public string ExtensionFilter
        {
            get
            {
                return "Odette files|*.odtt";
            }
        }

        public EDIVisualizer.Interfaces.Type PluginType
        {
            get { return EDIVisualizer.Interfaces.Type.Norme; }
        }

        public int Index
        {
            get { return 5; }
        }

        public bool SearchInParent
        {
            get { return true; }
        }

        public void LoadFile(string fileName)
        {

            if (fileName != string.Empty && File.Exists(fileName))
            {
                this.reset();
                errorLog = new StringBuilder();
                MessageFile = this.GetMessage(fileName);
                if (MessageFile != string.Empty)
                {
                    MessageName = this.GetTitle();
                    TreeNode[] tn = this.load(fileName);
                    tv.Nodes.AddRange(tn);
                    tv.CollapseAll();

                    this.populatePanelInfo();
                }
                else
                    throw new Exception(string.Format("Unapropriate {0} format", this.Name));


            }
            else
                throw new Exception(string.Format("Unapropriate {0} file", this.Name));
        }

        public void LoadSession(string sessionNumber, string environnement)
        {
            throw new NotImplementedException();
        }

        public void reset()
        {
            tv.Nodes.Clear();
            StatusLabelMessNumber.Text = string.Empty;
            StatusLabelSegmtNumber.Text = string.Empty;
            bNavigate = false;
            string bNavigateValue = string.Empty;
            string bNavigateSegment = string.Empty;
            CreateWebBrowser();
        }

        public bool Autodect(string fileName)
        {
            bool blResult = false;
            if (fileName != string.Empty && File.Exists(fileName))
                using (var sr = new StreamReader(fileName))
                    for (int i = 0; i < 3; i++)
                    {
                        string tmp = sr.ReadLine();
                        if (tmp.StartsWith("UNH"))
                        {
                            Regex r = new Regex(@"UNH\+.*\:OD\'");
                            Match m = r.Match(tmp);
                            blResult = m.Success;
                        }
                    }

            return blResult;
        }

        public bool Search(string searchString)
        {

            try
            {
                TreeNodeCollection nodes = tv.Nodes;

                //Clean treeview
                foreach (TreeNode tn in nodes)
                {
                    FormatTreeview(searchString, false, tn);
                    RecursiveTreeview(tn, searchString, false);
                }

                //Search
                foreach (TreeNode tn in nodes)
                {
                    FormatTreeview(searchString, true, tn);
                    RecursiveTreeview(tn, searchString, true);
                }

                return true;

            }
            catch (Exception)
            {
                return false;
            }

        }



        private string GetMessage(string fileName)
        {

            string result = string.Empty;
            DirectoryInfo di = new DirectoryInfo(messagesPath);
            FileInfo[] fis = di.GetFiles("*.xml");

            foreach (FileInfo fi in fis)
            {

                if (result == string.Empty)
                {

                    XPathNodeIterator iterator = requestXmlFile("Message/Regex", fi.FullName);
                    while (iterator.MoveNext())
                    {

                        using (StreamReader sr = new StreamReader(fileName))
                        {
                            string tmp = sr.ReadToEnd();
                            string expr = iterator.Current.Value.Trim();

                            Regex r = new Regex(string.Format("{0}", expr), RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant | RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);
                            if (r.IsMatch(tmp))
                            {
                                result = fi.FullName;
                                break;
                            }

                        }

                    }

                }
                else
                    break;

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

        private XPathNodeIterator requestXmlFile(string expression, string Filename)
        {

            XPathDocument doc = new XPathDocument(Filename);
            XPathNavigator nav = doc.CreateNavigator();

            XPathExpression expr;
            expr = nav.Compile(expression);
            XPathNodeIterator iterator = nav.Select(expr);

            return iterator;
        }

        private void ValideXml(string file)
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.Schemas.Add(null, Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), @"Plugins\PluginOdette.xsd"));
            settings.ValidationType = ValidationType.Schema;
            settings.ValidationEventHandler += new ValidationEventHandler(XmlValidationError);
            XmlReader reader = XmlReader.Create(file, settings);
            while (reader.Read()) ;
        }

        private void XmlValidationError(object sender, ValidationEventArgs e)
        {
            errorLog.AppendLine(string.Format("Line {0} position {1} : {2}", e.Exception.LineNumber, e.Exception.LinePosition, e.Exception.Message));
        }

        private TreeNode[] load(string ediFile)
        {

            TreeNode[] nodeCollection = null;
            string content = string.Empty;

            content = File.ReadAllText(ediFile);

            if (content != string.Empty)
            {

                interchange = new Interchange(content.Substring(0, 9), content);

                if (interchange.HasFunctionnalGroups())
                    throw new NotImplementedException();
                else
                {

                    nodeCollection = new TreeNode[3];
                    int index = 0;
                    int indexMessage = 0;

                    TreeNode tnUNA = new TreeNode(interchange.UNA.Name);
                    tnUNA.Tag = 0;
                    tnUNA.Nodes.Add(interchange.UNA.Content);
                    nodeCollection.SetValue(tnUNA, index);
                    index++;

                    TreeNode tnUNB = new TreeNode(interchange.UNB.Name);
                    tnUNB.Tag = 0;
                    tnUNB.Nodes.AddRange(Segment.GetDataElement(interchange.UNB.Content, interchange.DataElementSeparator));

                    foreach (PluginApp.Classes.Message mess in interchange.Messages)
                    {

                        int indexSegment = 0;
                        TreeNode tnMessages = new TreeNode(mess.UNH.Name);

                        tnMessages.Tag = indexMessage;
                        indexMessage++;

                        tnMessages.Nodes.AddRange(Segment.GetDataElement(mess.UNH.Content, interchange.DataElementSeparator));
                        tnUNB.Nodes.Add(tnMessages);

                        TreeNode tnSegment = null;
                        foreach (Segment segment in mess.Segments)
                        {
                            tnSegment = new TreeNode(segment.Name);

                            tnSegment.Tag = indexMessage;
                            indexSegment++;

                            tnSegment.Nodes.AddRange(Segment.GetDataElement(segment.Content, interchange.DataElementSeparator));
                            tnMessages.Nodes.Add(tnSegment);
                        }

                        TreeNode tnUNT = new TreeNode(interchange.UNT.Name);
                        tnUNT.Tag = 0;
                        tnUNT.Nodes.AddRange(Segment.GetDataElement(interchange.UNT.Content, interchange.DataElementSeparator));
                        tnMessages.Nodes.Add(tnUNT);

                        nodeCollection.SetValue(tnMessages, index);

                    }

                    nodeCollection.SetValue(tnUNB, index);
                    index++;

                    TreeNode tnUNZ = new TreeNode(interchange.UNZ.Name);
                    tnUNZ.Tag = 0;
                    tnUNZ.Nodes.AddRange(Segment.GetDataElement(interchange.UNZ.Content, interchange.DataElementSeparator));
                    nodeCollection.SetValue(tnUNZ, index);
                    index++;

                }

            }

            return nodeCollection;

        }

        private void populatePanelInfo()
        {
            StatusLabelMessNumber.Text = string.Format("{0} messages", interchange.Messages.Count);
        }

        private void expandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tv.SelectedNode.Expand();
        }

        private void collapseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tv.SelectedNode.Collapse();
        }

        private void expandAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tv.SelectedNode.ExpandAll();
        }

        private void collapseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tv.CollapseAll();
        }

        private void CreateWebBrowser()
        {
            this.pnlInfosWebBrowser.SuspendLayout();
            if (WebBrowserOdette != null)
                WebBrowserOdette.Dispose();
            WebBrowserOdette = new WebBrowser();
            WebBrowserOdette.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.WebBrowserOdette_Navigating);
            WebBrowserOdette.Navigate("about:blank");
            pnlInfosWebBrowser.Controls.Add(WebBrowserOdette);
            this.pnlInfosWebBrowser.ResumeLayout(false);
            WebBrowserOdette.Dock = DockStyle.Fill;
        }

        private void WebBrowserOdette_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            if (bNavigate)
            {
                HtmlDocument wb = ((WebBrowser)sender).Document;
                if (wb.Body != null)
                {
                    HtmlElementCollection hec = wb.Body.GetElementsByTagName("TD");
                    foreach (HtmlElement elem in hec)
                        if (elem.InnerHtml == bNavigateValue)
                        {
                            elem.Style = htmlStyle;
                            PopulateDataElement(wb, elem, hec);
                            bNavigateElem = elem;
                            e.Cancel = true;
                        }
                        else
                            elem.Style = string.Empty;
                }
            }
        }

        private void PopulateDataElement(HtmlDocument wb, HtmlElement elem, HtmlElementCollection hec)
        {
            string dataElements = tv.SelectedNode.Text;

            HtmlElementCollection tmpH = wb.Body.GetElementsByTagName("td");
            int z = 0;

            foreach (HtmlElement tmp in tmpH)
            {
                if (tmp.InnerHtml != null)
                {
                    int posH = tmp.InnerHtml.IndexOf("EVDataElement");
                    if (posH != -1 & z > 0)
                        tmp.InnerHtml = string.Empty;
                }
                z++;
            }

            for (int i = 0; i < hec.Count; i++)
            {
                if (hec[i] == elem)
                {
                    int y = i;
                    int x = 0;

                    while ((y + 6) < hec.Count && (hec[y + 6].InnerHtml != null || hec[y + 14].InnerHtml != null))
                    {
                        int decalage = 7;
                        if ((y + 15) < hec.Count && hec[y + 14].InnerHtml != null)
                            decalage = 15;
                        HtmlElement tmpElem = null;
                        if (y + decalage < hec.Count)
                            tmpElem = hec[y + decalage];
                        if (tmpElem != null && dataElements.Split(':').Length > x)
                            tmpElem.InnerHtml = string.Format("<div id='EVDataElement' style='{1}'>{0}</div>", dataElements.Split(':')[x], htmlStyle);
                        x++;
                        y = y + 8;
                    }
                }
            }
        }

        private void tv_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {

            if (e.Node.Tag != null)
            {

                TreeNode tmpNode = e.Node;
                bNavigate = false;
                bNavigateSegment = tmpNode.Text;
                StringBuilder datas = new StringBuilder();

                if (tmpNode.Nodes.Count > 0)
                {
                    int y = 0;
                    if (tmpNode.Nodes[y].Nodes.Count < 1)
                    {

                        XPathNodeIterator itrZone = requestXmlFile(string.Format(@"//Enreg[Label='{0}']/Zones/Zone", tmpNode.Text), MessageFile);
                        foreach (XPathNavigator xpnZn in itrZone)
                        {
                            int i = 0;

                            XPathNavigator nav = xpnZn.SelectSingleNode("Zone");
                            string ZoneReference = xpnZn.SelectSingleNode("ZoneReference").Value;
                            string ZoneLabel = xpnZn.SelectSingleNode("ZoneLabel").Value;

                            XPathNodeIterator itrField = requestXmlFile(string.Format(@"//Zone[ZoneReference='{0}' and ZoneLabel='{1}' and ../../Label='{2}']/Fields/Field", ZoneReference, ZoneLabel, tmpNode.Text), MessageFile);
                            if (itrField.Count > 0)
                                foreach (XPathNavigator xpnFld in itrField)
                                {
                                    string FieldReference = xpnFld.SelectSingleNode("FieldReference").Value;
                                    datas.Append(string.Format("{0}^{1}^{2}^{3}^{4}|", tmpNode.Text, ZoneReference, ZoneLabel, FieldReference, GetValue(tmpNode, y, i)));
                                    i++;
                                }
                            else
                            {
                                datas.Append(string.Format("{0}^{1}^{2}^{1}^{3}|", tmpNode.Text, ZoneReference, ZoneLabel, GetValue(tmpNode, y, i)));
                                i++;
                            }
                            y++;
                        }

                    }

                    Classes.Message mess = interchange.Messages.First();
                    if (tmpNode.Text == "UNH")
                        mess = interchange.Messages[int.Parse(tmpNode.Tag.ToString())];


                    XsltSettings settings = new XsltSettings(false, true);

                    XsltArgumentList argsList = new XsltArgumentList();
                    argsList.AddParam("segmentName", "", bNavigateSegment);
                    argsList.AddParam("datas", "", datas.ToString());

                    XslCompiledTransform transform = new XslCompiledTransform();
                    transform.Load(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), @"Plugins\PluginOdette.xslt"), settings, new XmlUrlResolver());

                    selectInWebBrowser(argsList, transform);

                }

            }

        }

        private static string GetValue(TreeNode tmpNode, int y, int i)
        {

            string returnValue = string.Empty;

            if (y < tmpNode.Nodes.Count && tmpNode.Nodes[y].Nodes.Count < 1)
                if (i < tmpNode.Nodes[y].Text.Split(':').Length)
                    returnValue = tmpNode.Nodes[y].Text.Split(':')[i];

            return returnValue;

        }

        private void selectInWebBrowser(XsltArgumentList argsList, XslCompiledTransform transform)
        {

            StringBuilder output;
            using (XmlWriter xw = XmlWriter.Create(output = new StringBuilder()))
            {
                transform.Transform(MessageFile, argsList, xw);
                xw.Flush();
                WebBrowserOdette.DocumentText = output.ToString();
                Application.DoEvents();
            }

        }



        private void navigateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = (ToolStripMenuItem)sender;
            System.Diagnostics.Process.Start(tsmi.Tag.ToString());
        }

        private void FormatTreeview(string search, bool isSearch, TreeNode tn)
        {

            if (isSearch)
            {
                if (tn.Text == search.Trim() && search.Trim() != string.Empty)
                {
                    tn.BackColor = Color.Navy;
                    tn.ForeColor = Color.White;
                    tv.SelectedNode = tn;
                }

            }
            else
            {
                if (tn.BackColor == Color.Navy && tn.ForeColor == Color.White)
                {
                    tn.BackColor = Color.White;
                    tn.ForeColor = Color.Black;
                }
            }
        }

        private void RecursiveTreeview(TreeNode treeNode, string search, bool isSearch)
        {
            foreach (TreeNode tn in treeNode.Nodes)
            {
                FormatTreeview(search, isSearch, tn);
                RecursiveTreeview(tn, search, isSearch);
            }
        }

        private string GetTitle()
        {
            string returnValue = string.Empty;
            if (MessageFile != string.Empty)
            {
                XmlDocument xml = new XmlDocument();
                xml.Load(MessageFile);
                XmlNodeList xnList = xml.SelectNodes("/Message");
                foreach (XmlNode xn in xnList)
                {
                    XmlNode anode = xn.SelectSingleNode("fileType");
                    if (anode != null)
                        returnValue = anode.InnerText.Trim();
                }
            }

            return returnValue;
        }

        public string splitAndReturn(string datas, string reference)
        {
            string[] datasArray = datas.Split('|');
            string returnValue = string.Empty;
            foreach (string str in datasArray)
                if (str.Trim() != string.Empty && str.Split('.')[3] == reference)
                    returnValue = str.Split('.')[4].Trim();
            return returnValue;
        }

    }
}
