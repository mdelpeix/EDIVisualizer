namespace PluginApp
{
    partial class PluginClass
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.rtbFile = new System.Windows.Forms.RichTextBox();
            this.cms = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyCms = new System.Windows.Forms.ToolStripMenuItem();
            this.WebBrowser = new System.Windows.Forms.WebBrowser();
            this.pnWebBrowser = new System.Windows.Forms.Panel();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.btnSegment = new System.Windows.Forms.Button();
            this.lblInfos = new System.Windows.Forms.Label();
            this.cms.SuspendLayout();
            this.pnWebBrowser.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtbFile
            // 
            this.rtbFile.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbFile.BackColor = System.Drawing.SystemColors.Window;
            this.rtbFile.ContextMenuStrip = this.cms;
            this.rtbFile.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbFile.Location = new System.Drawing.Point(3, 3);
            this.rtbFile.Name = "rtbFile";
            this.rtbFile.ReadOnly = true;
            this.rtbFile.Size = new System.Drawing.Size(477, 126);
            this.rtbFile.TabIndex = 0;
            this.rtbFile.Text = "";
            this.rtbFile.WordWrap = false;
            this.rtbFile.SelectionChanged += new System.EventHandler(this.rtbFile_SelectionChanged);
            this.rtbFile.MouseClick += new System.Windows.Forms.MouseEventHandler(this.rtbFile_MouseClick);
            this.rtbFile.KeyUp += new System.Windows.Forms.KeyEventHandler(this.rtbFile_KeyUp);
            // 
            // cms
            // 
            this.cms.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyCms});
            this.cms.Name = "cms";
            this.cms.Size = new System.Drawing.Size(100, 26);
            // 
            // copyCms
            // 
            this.copyCms.Name = "copyCms";
            this.copyCms.Size = new System.Drawing.Size(99, 22);
            this.copyCms.Text = "&Copy";
            this.copyCms.Click += new System.EventHandler(this.copyCms_Click);
            // 
            // WebBrowser
            // 
            this.WebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WebBrowser.Location = new System.Drawing.Point(0, 0);
            this.WebBrowser.Margin = new System.Windows.Forms.Padding(5);
            this.WebBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.WebBrowser.Name = "WebBrowser";
            this.WebBrowser.Size = new System.Drawing.Size(475, 269);
            this.WebBrowser.TabIndex = 1;
            this.WebBrowser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.WebBrowser_DocumentCompleted);
            this.WebBrowser.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.WebBrowser_Navigating);
            // 
            // pnWebBrowser
            // 
            this.pnWebBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnWebBrowser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnWebBrowser.Controls.Add(this.WebBrowser);
            this.pnWebBrowser.Location = new System.Drawing.Point(3, 45);
            this.pnWebBrowser.Name = "pnWebBrowser";
            this.pnWebBrowser.Size = new System.Drawing.Size(477, 271);
            this.pnWebBrowser.TabIndex = 3;
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.rtbFile);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.btnSegment);
            this.splitContainer.Panel2.Controls.Add(this.pnWebBrowser);
            this.splitContainer.Panel2.Controls.Add(this.lblInfos);
            this.splitContainer.Size = new System.Drawing.Size(485, 455);
            this.splitContainer.SplitterDistance = 132;
            this.splitContainer.TabIndex = 1;
            // 
            // btnSegment
            // 
            this.btnSegment.Location = new System.Drawing.Point(74, 4);
            this.btnSegment.Name = "btnSegment";
            this.btnSegment.Size = new System.Drawing.Size(57, 35);
            this.btnSegment.TabIndex = 6;
            this.btnSegment.Text = "Infos Segment";
            this.btnSegment.UseVisualStyleBackColor = false;
            this.btnSegment.Visible = false;
            this.btnSegment.Click += new System.EventHandler(this.btnSegment_Click);
            // 
            // lblInfos
            // 
            this.lblInfos.AutoSize = true;
            this.lblInfos.Location = new System.Drawing.Point(8, 4);
            this.lblInfos.Name = "lblInfos";
            this.lblInfos.Size = new System.Drawing.Size(0, 13);
            this.lblInfos.TabIndex = 2;
            // 
            // PluginClass
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer);
            this.Name = "PluginClass";
            this.Size = new System.Drawing.Size(485, 455);
            this.cms.ResumeLayout(false);
            this.pnWebBrowser.ResumeLayout(false);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbFile;
        private System.Windows.Forms.ContextMenuStrip cms;
        private System.Windows.Forms.ToolStripMenuItem copyCms;
        private System.Windows.Forms.WebBrowser WebBrowser;
        private System.Windows.Forms.Panel pnWebBrowser;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Label lblInfos;
        private System.Windows.Forms.Button btnSegment;
    }
}
