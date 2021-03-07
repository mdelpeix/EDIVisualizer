namespace PluginApp
{
    partial class PluginClass
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pnlTreeview = new System.Windows.Forms.Panel();
            this.tv = new System.Windows.Forms.TreeView();
            this.cmsTreeviewMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.expandToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.collapseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.expandAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.collapseAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.pnlInfos = new System.Windows.Forms.Panel();
            this.pnlInfosWebBrowser = new System.Windows.Forms.Panel();
            this.pnlInfosBottom = new System.Windows.Forms.Panel();
            this.StatusStrip = new System.Windows.Forms.StatusStrip();
            this.StatusLabelMessNumber = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusLabelSegmtNumber = new System.Windows.Forms.ToolStripStatusLabel();
            this.pnlInfosTop = new System.Windows.Forms.Panel();
            this.lblMessage = new System.Windows.Forms.Label();
            this.pnlTreeview.SuspendLayout();
            this.cmsTreeviewMenu.SuspendLayout();
            this.pnlInfos.SuspendLayout();
            this.pnlInfosBottom.SuspendLayout();
            this.StatusStrip.SuspendLayout();
            this.pnlInfosTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTreeview
            // 
            this.pnlTreeview.Controls.Add(this.tv);
            this.pnlTreeview.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlTreeview.Location = new System.Drawing.Point(0, 0);
            this.pnlTreeview.Name = "pnlTreeview";
            this.pnlTreeview.Size = new System.Drawing.Size(200, 366);
            this.pnlTreeview.TabIndex = 0;
            // 
            // tv
            // 
            this.tv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tv.ContextMenuStrip = this.cmsTreeviewMenu;
            this.tv.Location = new System.Drawing.Point(8, 11);
            this.tv.Name = "tv";
            this.tv.Size = new System.Drawing.Size(185, 349);
            this.tv.TabIndex = 1;
            this.tv.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tv_AfterSelect);
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
            this.cmsTreeviewMenu.Size = new System.Drawing.Size(153, 114);
            this.cmsTreeviewMenu.VisibleChanged += new System.EventHandler(this.cmsTreeviewMenu_VisibleChanged);
            // 
            // expandToolStripMenuItem
            // 
            this.expandToolStripMenuItem.Name = "expandToolStripMenuItem";
            this.expandToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.expandToolStripMenuItem.Text = "&Expand";
            this.expandToolStripMenuItem.Click += new System.EventHandler(this.expandToolStripMenuItem_Click);
            // 
            // collapseToolStripMenuItem
            // 
            this.collapseToolStripMenuItem.Name = "collapseToolStripMenuItem";
            this.collapseToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.collapseToolStripMenuItem.Text = "&Collapse";
            this.collapseToolStripMenuItem.Click += new System.EventHandler(this.collapseToolStripMenuItem_Click);
            // 
            // expandAllToolStripMenuItem
            // 
            this.expandAllToolStripMenuItem.Name = "expandAllToolStripMenuItem";
            this.expandAllToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.expandAllToolStripMenuItem.Text = "E&xpand All";
            this.expandAllToolStripMenuItem.Click += new System.EventHandler(this.expandAllToolStripMenuItem_Click);
            // 
            // collapseAllToolStripMenuItem
            // 
            this.collapseAllToolStripMenuItem.Name = "collapseAllToolStripMenuItem";
            this.collapseAllToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.collapseAllToolStripMenuItem.Text = "C&ollapse All";
            this.collapseAllToolStripMenuItem.Click += new System.EventHandler(this.collapseAllToolStripMenuItem_Click);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(200, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 366);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // pnlInfos
            // 
            this.pnlInfos.Controls.Add(this.pnlInfosWebBrowser);
            this.pnlInfos.Controls.Add(this.pnlInfosBottom);
            this.pnlInfos.Controls.Add(this.pnlInfosTop);
            this.pnlInfos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlInfos.Location = new System.Drawing.Point(203, 0);
            this.pnlInfos.Name = "pnlInfos";
            this.pnlInfos.Size = new System.Drawing.Size(473, 366);
            this.pnlInfos.TabIndex = 2;
            // 
            // pnlInfosWebBrowser
            // 
            this.pnlInfosWebBrowser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlInfosWebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlInfosWebBrowser.Location = new System.Drawing.Point(0, 52);
            this.pnlInfosWebBrowser.Name = "pnlInfosWebBrowser";
            this.pnlInfosWebBrowser.Size = new System.Drawing.Size(473, 289);
            this.pnlInfosWebBrowser.TabIndex = 2;
            // 
            // pnlInfosBottom
            // 
            this.pnlInfosBottom.Controls.Add(this.StatusStrip);
            this.pnlInfosBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlInfosBottom.Location = new System.Drawing.Point(0, 341);
            this.pnlInfosBottom.Name = "pnlInfosBottom";
            this.pnlInfosBottom.Size = new System.Drawing.Size(473, 25);
            this.pnlInfosBottom.TabIndex = 1;
            // 
            // StatusStrip
            // 
            this.StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabelMessNumber,
            this.StatusLabelSegmtNumber});
            this.StatusStrip.Location = new System.Drawing.Point(0, 3);
            this.StatusStrip.Name = "StatusStrip";
            this.StatusStrip.Size = new System.Drawing.Size(473, 22);
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
            // pnlInfosTop
            // 
            this.pnlInfosTop.Controls.Add(this.lblMessage);
            this.pnlInfosTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlInfosTop.Location = new System.Drawing.Point(0, 0);
            this.pnlInfosTop.Name = "pnlInfosTop";
            this.pnlInfosTop.Size = new System.Drawing.Size(473, 52);
            this.pnlInfosTop.TabIndex = 0;
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.Location = new System.Drawing.Point(6, 11);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(145, 29);
            this.lblMessage.TabIndex = 0;
            this.lblMessage.Text = "MESSAGE";
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PluginClass
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlInfos);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.pnlTreeview);
            this.Name = "PluginClass";
            this.Size = new System.Drawing.Size(676, 366);
            this.pnlTreeview.ResumeLayout(false);
            this.cmsTreeviewMenu.ResumeLayout(false);
            this.pnlInfos.ResumeLayout(false);
            this.pnlInfosBottom.ResumeLayout(false);
            this.pnlInfosBottom.PerformLayout();
            this.StatusStrip.ResumeLayout(false);
            this.StatusStrip.PerformLayout();
            this.pnlInfosTop.ResumeLayout(false);
            this.pnlInfosTop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTreeview;
        private System.Windows.Forms.TreeView tv;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel pnlInfos;
        private System.Windows.Forms.ContextMenuStrip cmsTreeviewMenu;
        private System.Windows.Forms.ToolStripMenuItem expandToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem collapseToolStripMenuItem;
        private System.Windows.Forms.Panel pnlInfosWebBrowser;
        private System.Windows.Forms.Panel pnlInfosBottom;
        private System.Windows.Forms.Panel pnlInfosTop;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.StatusStrip StatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabelMessNumber;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabelSegmtNumber;
        private System.Windows.Forms.ToolStripMenuItem expandAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem collapseAllToolStripMenuItem;
    }
}
