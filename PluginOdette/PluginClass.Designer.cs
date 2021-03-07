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
            this.pnlInfosWebBrowser = new System.Windows.Forms.Panel();
            this.pnlInfosBottom = new System.Windows.Forms.Panel();
            this.StatusStrip = new System.Windows.Forms.StatusStrip();
            this.StatusLabelMessNumber = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusLabelSegmtNumber = new System.Windows.Forms.ToolStripStatusLabel();
            this.pnlInfos = new System.Windows.Forms.Panel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.tv = new System.Windows.Forms.TreeView();
            this.cmsTreeviewMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.expandToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.collapseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.expandAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.collapseAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlTreeview = new System.Windows.Forms.Panel();
            this.pnlInfosBottom.SuspendLayout();
            this.StatusStrip.SuspendLayout();
            this.pnlInfos.SuspendLayout();
            this.cmsTreeviewMenu.SuspendLayout();
            this.pnlTreeview.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlInfosWebBrowser
            // 
            this.pnlInfosWebBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlInfosWebBrowser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlInfosWebBrowser.Location = new System.Drawing.Point(6, 11);
            this.pnlInfosWebBrowser.Name = "pnlInfosWebBrowser";
            this.pnlInfosWebBrowser.Size = new System.Drawing.Size(519, 406);
            this.pnlInfosWebBrowser.TabIndex = 2;
            // 
            // pnlInfosBottom
            // 
            this.pnlInfosBottom.Controls.Add(this.StatusStrip);
            this.pnlInfosBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlInfosBottom.Location = new System.Drawing.Point(0, 423);
            this.pnlInfosBottom.Name = "pnlInfosBottom";
            this.pnlInfosBottom.Size = new System.Drawing.Size(528, 25);
            this.pnlInfosBottom.TabIndex = 1;
            // 
            // StatusStrip
            // 
            this.StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabelMessNumber,
            this.StatusLabelSegmtNumber});
            this.StatusStrip.Location = new System.Drawing.Point(0, 3);
            this.StatusStrip.Name = "StatusStrip";
            this.StatusStrip.Size = new System.Drawing.Size(528, 22);
            this.StatusStrip.TabIndex = 0;
            this.StatusStrip.Text = "statusStrip1";
            // 
            // StatusLabelMessNumber
            // 
            this.StatusLabelMessNumber.Name = "StatusLabelMessNumber";
            this.StatusLabelMessNumber.Size = new System.Drawing.Size(96, 17);
            this.StatusLabelMessNumber.Text = "[Message number]";
            // 
            // StatusLabelSegmtNumber
            // 
            this.StatusLabelSegmtNumber.Name = "StatusLabelSegmtNumber";
            this.StatusLabelSegmtNumber.Size = new System.Drawing.Size(97, 17);
            this.StatusLabelSegmtNumber.Text = "[Segment Number]";
            // 
            // pnlInfos
            // 
            this.pnlInfos.Controls.Add(this.pnlInfosWebBrowser);
            this.pnlInfos.Controls.Add(this.pnlInfosBottom);
            this.pnlInfos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlInfos.Location = new System.Drawing.Point(203, 0);
            this.pnlInfos.Name = "pnlInfos";
            this.pnlInfos.Size = new System.Drawing.Size(528, 448);
            this.pnlInfos.TabIndex = 5;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(200, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 448);
            this.splitter1.TabIndex = 4;
            this.splitter1.TabStop = false;
            // 
            // tv
            // 
            this.tv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tv.ContextMenuStrip = this.cmsTreeviewMenu;
            this.tv.Location = new System.Drawing.Point(8, 11);
            this.tv.Name = "tv";
            this.tv.Size = new System.Drawing.Size(185, 431);
            this.tv.TabIndex = 1;
            this.tv.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tv_NodeMouseClick);
            // 
            // cmsTreeviewMenu
            // 
            this.cmsTreeviewMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.expandToolStripMenuItem,
            this.collapseToolStripMenuItem,
            this.expandAllToolStripMenuItem,
            this.collapseAllToolStripMenuItem});
            this.cmsTreeviewMenu.Name = "cmsTreeviewMenu";
            this.cmsTreeviewMenu.Size = new System.Drawing.Size(129, 92);
            // 
            // expandToolStripMenuItem
            // 
            this.expandToolStripMenuItem.Name = "expandToolStripMenuItem";
            this.expandToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.expandToolStripMenuItem.Text = "&Expand";
            this.expandToolStripMenuItem.Click += new System.EventHandler(this.expandToolStripMenuItem_Click);
            // 
            // collapseToolStripMenuItem
            // 
            this.collapseToolStripMenuItem.Name = "collapseToolStripMenuItem";
            this.collapseToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.collapseToolStripMenuItem.Text = "&Collapse";
            this.collapseToolStripMenuItem.Click += new System.EventHandler(this.collapseToolStripMenuItem_Click);
            // 
            // expandAllToolStripMenuItem
            // 
            this.expandAllToolStripMenuItem.Name = "expandAllToolStripMenuItem";
            this.expandAllToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.expandAllToolStripMenuItem.Text = "E&xpand All";
            this.expandAllToolStripMenuItem.Click += new System.EventHandler(this.expandAllToolStripMenuItem_Click);
            // 
            // collapseAllToolStripMenuItem
            // 
            this.collapseAllToolStripMenuItem.Name = "collapseAllToolStripMenuItem";
            this.collapseAllToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.collapseAllToolStripMenuItem.Text = "C&ollapse All";
            this.collapseAllToolStripMenuItem.Click += new System.EventHandler(this.collapseAllToolStripMenuItem_Click);
            // 
            // pnlTreeview
            // 
            this.pnlTreeview.Controls.Add(this.tv);
            this.pnlTreeview.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlTreeview.Location = new System.Drawing.Point(0, 0);
            this.pnlTreeview.Name = "pnlTreeview";
            this.pnlTreeview.Size = new System.Drawing.Size(200, 448);
            this.pnlTreeview.TabIndex = 3;
            // 
            // PluginClass
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlInfos);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.pnlTreeview);
            this.Name = "PluginClass";
            this.Size = new System.Drawing.Size(731, 448);
            this.pnlInfosBottom.ResumeLayout(false);
            this.pnlInfosBottom.PerformLayout();
            this.StatusStrip.ResumeLayout(false);
            this.StatusStrip.PerformLayout();
            this.pnlInfos.ResumeLayout(false);
            this.cmsTreeviewMenu.ResumeLayout(false);
            this.pnlTreeview.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlInfosWebBrowser;
        private System.Windows.Forms.Panel pnlInfosBottom;
        private System.Windows.Forms.StatusStrip StatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabelMessNumber;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabelSegmtNumber;
        private System.Windows.Forms.Panel pnlInfos;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.TreeView tv;
        private System.Windows.Forms.ContextMenuStrip cmsTreeviewMenu;
        private System.Windows.Forms.ToolStripMenuItem expandToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem collapseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem expandAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem collapseAllToolStripMenuItem;
        private System.Windows.Forms.Panel pnlTreeview;
    }
}
