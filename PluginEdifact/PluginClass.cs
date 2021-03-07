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
using PluginApp.Classes;

namespace PluginApp
{
    public partial class PluginClass : UserControl, IPlugin
    {

        private Interchange interchange;
        bool bNavigate = false;
        string bNavigateValue = string.Empty;
        string bNavigateSegment = string.Empty;
        HtmlElement bNavigateElem;
        WebBrowser WebBrowserEdifact;
        string htmlStyle = "background-color:red;color:white;padding-left:2px;padding-right:2px;";


        public PluginClass()
        {
            InitializeComponent();
            this.Name = "Edifact";
            CreateWebBrowser();
        }

        #region IPlugin Members

        public bool SearchInParent
        {
            get { return true; }
        }

        public string Comment
        {
            get
            {
                return "";
            }
        }

        public string Extension
        {
            get
            {
                return "edi".ToLower();
            }
        }

        public string ExtensionFilter
        {
            get
            {
                return "Edi files|*.edi";
            }
        }

        public EDIVisualizer.Interfaces.Type PluginType
        {
            get
            {
                return EDIVisualizer.Interfaces.Type.Norme;
            }
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

        public void LoadFile(string fileName)
        {


            if (fileName != string.Empty && File.Exists(fileName))
            {
                this.reset();
                TreeNode[] tn = this.load(fileName);
                tv.Nodes.AddRange(tn);
                tv.CollapseAll();

                this.populatePanelInfo(interchange.Messages.First().Title);

            }
            else
                throw new Exception(string.Format("Unapropriate {0} file", this.Name));

        }

        public bool Autodect(string fileName)
        {

            bool blResult = false;
            if (fileName != string.Empty && File.Exists(fileName))
            {
                using (var sr = new StreamReader(fileName))
                {
                    string tmp = sr.ReadLine();
                    string first = string.Empty;
                    if (!String.IsNullOrWhiteSpace(tmp))
                        first = tmp.Substring(0, 6);
                    if (!String.IsNullOrWhiteSpace(first) && first != "UNBXBA")
                        if (first.Substring(0, 3) == "UNA" || first.Substring(0, 3) == "UNB")
                            if (!Regex.Match(sr.ReadToEnd(), @"UNH\+.*\:OD\'").Success)
                                blResult = true;
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

        public void reset()
        {
            tv.Nodes.Clear();
            lblMessage.Text = string.Empty;
            StatusLabelMessNumber.Text = string.Empty;
            StatusLabelSegmtNumber.Text = string.Empty;
            bNavigate = false;
            string bNavigateValue = string.Empty;
            string bNavigateSegment = string.Empty;
            CreateWebBrowser();
        }

        public void LoadSession(string sessionNumber, string environnement)
        {
            throw new NotImplementedException();
        }

        public int Index { get { return 5; } }

        #endregion


        private void populatePanelInfo(string messageTitle)
        {
            lblMessage.Text = messageTitle;
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

        private void tv_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {


            string url = string.Empty;

            if (e.Node.Tag != null)
            {

                TreeNode tmpNode = e.Node;
                if (e.Node.Tag.ToString().StartsWith("POS"))
                {
                    bNavigateValue = e.Node.Tag.ToString().Split(':')[1];
                    tmpNode = e.Node.Parent;
                    if (tmpNode.Text != bNavigateSegment)
                        bNavigate = false;
                    else
                        bNavigate = true;
                }
                else
                    bNavigate = false;

                bNavigateSegment = tmpNode.Text;

                Classes.Message mess = interchange.Messages.First();
                if (tmpNode.Text == "UNH")
                    mess = interchange.Messages[int.Parse(tmpNode.Tag.ToString())];

                switch (tmpNode.Text)
                {
                    case "UNA":
                    case "UNB":
                        url = string.Format(Properties.Settings.Default.urlMessage, tmpNode.Text.ToUpper());
                        break;
                    case "UNH":
                    case "UNT":
                    case "UNZ":
                        this.populatePanelInfo(mess.Title);
                        url = string.Format(Properties.Settings.Default.urlMessage, tmpNode.Text.ToUpper());
                        break;
                    default:
                        url = string.Format(Properties.Settings.Default.urlSegment, string.Format("{0}{1}", mess.Version, mess.Release).ToUpper(), tmpNode.Text.ToUpper());
                        break;
                }


                ToolStripMenuItem tsmi = new ToolStripMenuItem(e.Node.Text, null, new System.EventHandler(this.navigateToolStripMenuItem_Click));
                tsmi.Tag = url;
                cmsTreeviewMenu.Items.Add(string.Format("Go to {0} Website", tsmi));
                WebBrowserEdifact.Navigate(url);

            }


        }

        private void tv_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNodeCollection nodes = tv.Nodes;
            string seg = tv.SelectedNode.Text;
            int compt = 0;

            foreach (TreeNode tn in nodes)
            {
                if (tn.Text == seg)
                    compt++;
                compt = RecursiveTreeviewCount(tn, seg, ref compt);
            }

            StatusLabelSegmtNumber.Text = string.Format("{0} segment {1}", compt.ToString(), seg);

        }

        private int RecursiveTreeviewCount(TreeNode treeNode, string seg, ref int compt)
        {
            foreach (TreeNode tn in treeNode.Nodes)
            {
                if (tn.Text == seg)
                    compt++;
                RecursiveTreeviewCount(tn, seg, ref compt);
            }
            return compt;
        }

        private void RecursiveTreeview(TreeNode treeNode, string search, bool isSearch)
        {
            foreach (TreeNode tn in treeNode.Nodes)
            {
                FormatTreeview(search, isSearch, tn);
                RecursiveTreeview(tn, search, isSearch);
            }
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

        private void expandAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tv.SelectedNode.ExpandAll();
        }

        private void navigateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = (ToolStripMenuItem)sender;
            System.Diagnostics.Process.Start(tsmi.Tag.ToString());
        }

        private void collapseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tv.CollapseAll();
        }

        private void WebBrowserEdifact_Navigating(object sender, WebBrowserNavigatingEventArgs e)
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

        private void CreateWebBrowser()
        {
            this.pnlInfosWebBrowser.SuspendLayout();
            if (WebBrowserEdifact != null)
                WebBrowserEdifact.Dispose();
            WebBrowserEdifact = new WebBrowser();
            WebBrowserEdifact.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.WebBrowserEdifact_Navigating);
            WebBrowserEdifact.Navigate("about:blank");
            pnlInfosWebBrowser.Controls.Add(WebBrowserEdifact);
            this.pnlInfosWebBrowser.ResumeLayout(false);
            WebBrowserEdifact.Dock = DockStyle.Fill;
        }

        private void cmsTreeviewMenu_VisibleChanged(object sender, EventArgs e)
        {
            if (cmsTreeviewMenu.Visible == true)
            {
                foreach (ToolStripMenuItem t in cmsTreeviewMenu.Items)
                    if (t.Text.StartsWith("Go"))
                        cmsTreeviewMenu.Items.Remove(t);
            }
        }

    }
}
