using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace PluginApp
{
    public partial class frm4913 : Form
    {

        private StringBuilder sb;

        public string VDA4913File { get; set; }

        public frm4913()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmEAVI_Load(object sender, EventArgs e)
        {

            try
            {

                Cursor.Current = Cursors.WaitCursor;
                this.Enabled = false;


                if (File.Exists(VDA4913File))
                {

                    this.Text += " " + VDA4913File;

                    using (StreamReader r = new StreamReader(VDA4913File))
                    {

                        string line;
                        TreeNode tnMessages = null;
                        TreeNode tnArticle = null;
                        TreeNode tnPalette = null;
                        TreeNode tnColis = null;
                        TreeNode tnDest = null;

                        while ((line = r.ReadLine()) != null)
                        {

                            string seg = line.Substring(0, 3);
                            switch (seg)
                            {
                                case "713":
                                    tnMessages = new TreeNode(string.Format("MESSAGE : {0}", line.Substring(5, 8).Trim()));
                                    tnDest = new TreeNode(string.Format("DESTINATAIRE : {0}", line.Substring(48, 3).Trim()));
                                    tnMessages.Nodes.Add(tnDest);
                                    tv.Nodes.Add(tnMessages);
                                    break;
                                case "714":
                                    tnArticle = new TreeNode(string.Format("ARTICLE : {0}", line.Substring(5, 22).Trim()));
                                    tnMessages.Nodes.Add(tnArticle);
                                    break;
                                case "715":

                                    string VDA71508 = line.Substring(78, 9).Trim();
                                    string VDA71509 = line.Substring(87, 9).Trim();

                                    if (VDA71508 != string.Empty && VDA71509 == string.Empty)
                                    {
                                        tnPalette = new TreeNode(string.Format("PALETTE : {0} (TYPE {1})", line.Substring(78, 9).Trim(), line.Substring(124, 1).Trim()));
                                        tnArticle.Nodes.Add(tnPalette);
                                    }
                                    else if (VDA71508 != string.Empty && VDA71509 != string.Empty)
                                    {
                                        tnColis = new TreeNode(string.Format("COLIS : {0} - {1}", line.Substring(78, 9).Trim(), line.Substring(87, 9).Trim()));
                                        tnPalette.Nodes.Add(tnColis);
                                    }
                                    else
                                    {
                                        //tnColis = new TreeNode(string.Format("ACCESSOIRES : {0}", line.Substring(5, 22).Trim()));
                                    }

                                    break;
                                default:
                                    break;
                            }

                        }

                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("{0}", ex.InnerException.Message), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Enabled = true;
                this.Focus();
                Cursor.Current = Cursors.Default;
            }


        }

        private void btnCopy_Click(object sender, EventArgs e)
        {

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.Enabled = false;

                sb = new StringBuilder();

                foreach (TreeNode tn in tv.Nodes)
                {
                    sb.AppendLine(tn.Text);
                    RecursiveTreeviewCount(tn);
                }

                try
                { Clipboard.SetText(sb.ToString()); }
                catch (Exception) { }


            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("{0}", ex.InnerException.Message), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Enabled = true;
                this.Focus();
                Cursor.Current = Cursors.Default;
            }



        }


        private void RecursiveTreeviewCount(TreeNode treeNode)
        {

            foreach (TreeNode tn in treeNode.Nodes)
            {
                sb.AppendLine(tn.Text);
                RecursiveTreeviewCount(tn);
            }

        }

    }
}
