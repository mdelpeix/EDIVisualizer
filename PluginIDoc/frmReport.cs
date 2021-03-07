using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Text.RegularExpressions;

namespace PluginApp
{
    public partial class frmReport : Form
    {

        public string PivotFile { get; set; }
        public string MessageLine { get; set; }

        public frmReport()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape) this.Close();
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void frmReport_Load(object sender, EventArgs e)
        {


            try
            {

                Cursor.Current = Cursors.WaitCursor;

                StringBuilder sbHtml = new StringBuilder();


                string segment = string.Empty;
                Match match = Regex.Match(MessageLine, @"^\s{3}?[A-Z]{3}\d");
                if (match.Success)
                    segment = match.Value.Trim();


                if (segment != string.Empty)
                {

                    XmlDocument doc = new XmlDocument();
                    doc.Load(PivotFile);

                    XmlNodeList nodeList = doc.SelectNodes(string.Format("/Ci/Enregistrements/Enreg[Label='{0}']/Zones/Zone", segment));

                    sbHtml.AppendLine(@"<html><head>");
                    sbHtml.AppendLine(@"<style type=""text/css"">.tableau{border: 1px solid #000000; width: 100%; border-spacing:0; border-collapse:collapse; text-align: right;}.tableau td{border: 1px solid #000000; padding: 5px;}.tableau th{background-color: #B0C4DE; color:#000000; border: 0px solid #000000; padding: 5px;}.tableau tr:hover{background-color: #FF6600;color:#FFFFFF !important;}.important{background-color: #FF6600;color:#FFFFFF}.important a{color:#FFFFFF}.normal{background-color: #FFFFFF; color: #000000}.mandatory{color:#8B0000; font-weight:bold; text-align: center}.alignleft{text-align: left;}.alignCenter{text-align: center;}</style>");
                    sbHtml.AppendLine(@"</head><body><table class=""tableau"">");
                    sbHtml.AppendLine(@"<tr class=""alignCenter""><th>Name</th><th>Value</th><th>Mandatory</th><th>Position</th><th>Length</th><th>Type</th><th>Format</th></tr>");

                    foreach (XmlNode node in nodeList)
                        sbHtml.AppendLine(string.Format(@"<tr class=""alignleft""><td>{0}</td><td><b>{1}</b></td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td></tr>",
                            node["ZoneLabel"].InnerText,
                            this.getValue(MessageLine, int.Parse(node["ZonePos"].InnerText), int.Parse(node["ZoneLg"].InnerText)),
                            (node["Zonestatut"]) != null ? node["Zonestatut"].InnerText : string.Empty,
                            node["ZonePos"].InnerText,
                            node["ZoneLg"].InnerText,
                            node["ZoneType"].InnerText,
                            node["ZoneFormat"].InnerText));

                    sbHtml.AppendLine(@"</table></body></html>");

                    wb.Navigate("about:blank");
                    HtmlDocument hDoc = wb.Document;
                    hDoc.Write(String.Empty);
                    wb.DocumentText = sbHtml.ToString();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("{0} : {1}", ex.Message, ex.InnerException.Message), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }

        }

        private string getValue(string MessageLine, int position, int length)
        {
            return MessageLine.Substring(position-1, length).Trim();
        }

    }
}
