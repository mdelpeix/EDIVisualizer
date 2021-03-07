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
            this.splitContainerVDA = new System.Windows.Forms.SplitContainer();
            this.rtbVDAFile = new System.Windows.Forms.RichTextBox();
            this.cms = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyCms = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSegment = new System.Windows.Forms.Button();
            this.btnAnalyz = new System.Windows.Forms.Button();
            this.pnWebBrowser = new System.Windows.Forms.Panel();
            this.WebBrowser = new System.Windows.Forms.WebBrowser();
            this.lblInfos = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerVDA)).BeginInit();
            this.splitContainerVDA.Panel1.SuspendLayout();
            this.splitContainerVDA.Panel2.SuspendLayout();
            this.splitContainerVDA.SuspendLayout();
            this.cms.SuspendLayout();
            this.pnWebBrowser.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerVDA
            // 
            this.splitContainerVDA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerVDA.Location = new System.Drawing.Point(0, 0);
            this.splitContainerVDA.Name = "splitContainerVDA";
            this.splitContainerVDA.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerVDA.Panel1
            // 
            this.splitContainerVDA.Panel1.Controls.Add(this.rtbVDAFile);
            // 
            // splitContainerVDA.Panel2
            // 
            this.splitContainerVDA.Panel2.Controls.Add(this.btnSegment);
            this.splitContainerVDA.Panel2.Controls.Add(this.btnAnalyz);
            this.splitContainerVDA.Panel2.Controls.Add(this.pnWebBrowser);
            this.splitContainerVDA.Panel2.Controls.Add(this.lblInfos);
            this.splitContainerVDA.Size = new System.Drawing.Size(533, 375);
            this.splitContainerVDA.SplitterDistance = 109;
            this.splitContainerVDA.TabIndex = 0;
            // 
            // rtbVDAFile
            // 
            this.rtbVDAFile.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbVDAFile.BackColor = System.Drawing.SystemColors.Window;
            this.rtbVDAFile.ContextMenuStrip = this.cms;
            this.rtbVDAFile.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbVDAFile.Location = new System.Drawing.Point(3, 3);
            this.rtbVDAFile.Name = "rtbVDAFile";
            this.rtbVDAFile.ReadOnly = true;
            this.rtbVDAFile.Size = new System.Drawing.Size(525, 103);
            this.rtbVDAFile.TabIndex = 0;
            this.rtbVDAFile.Text = "";
            this.rtbVDAFile.WordWrap = false;
            this.rtbVDAFile.SelectionChanged += new System.EventHandler(this.rtbVDAFile_SelectionChanged);
            this.rtbVDAFile.MouseClick += new System.Windows.Forms.MouseEventHandler(this.rtbVDAFile_MouseClick);
            this.rtbVDAFile.KeyUp += new System.Windows.Forms.KeyEventHandler(this.rtbVDAFile_KeyUp);
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
            // btnSegment
            // 
            this.btnSegment.Location = new System.Drawing.Point(101, 3);
            this.btnSegment.Name = "btnSegment";
            this.btnSegment.Size = new System.Drawing.Size(57, 35);
            this.btnSegment.TabIndex = 6;
            this.btnSegment.Text = "Infos Segment";
            this.btnSegment.UseVisualStyleBackColor = true;
            this.btnSegment.Visible = false;
            this.btnSegment.Click += new System.EventHandler(this.btnSegment_Click);
            // 
            // btnAnalyz
            // 
            this.btnAnalyz.Location = new System.Drawing.Point(164, 2);
            this.btnAnalyz.Name = "btnAnalyz";
            this.btnAnalyz.Size = new System.Drawing.Size(57, 35);
            this.btnAnalyz.TabIndex = 5;
            this.btnAnalyz.Text = "Analyze";
            this.btnAnalyz.UseVisualStyleBackColor = true;
            this.btnAnalyz.Visible = false;
            this.btnAnalyz.Click += new System.EventHandler(this.btnAnalyz_Click);
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
            this.pnWebBrowser.Size = new System.Drawing.Size(525, 214);
            this.pnWebBrowser.TabIndex = 3;
            // 
            // WebBrowser
            // 
            this.WebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WebBrowser.Location = new System.Drawing.Point(0, 0);
            this.WebBrowser.Margin = new System.Windows.Forms.Padding(5);
            this.WebBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.WebBrowser.Name = "WebBrowser";
            this.WebBrowser.Size = new System.Drawing.Size(523, 212);
            this.WebBrowser.TabIndex = 1;
            this.WebBrowser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.WebBrowser_DocumentCompleted);
            this.WebBrowser.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.WebBrowser_Navigating);
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
            this.Controls.Add(this.splitContainerVDA);
            this.Name = "PluginClass";
            this.Size = new System.Drawing.Size(533, 375);
            this.splitContainerVDA.Panel1.ResumeLayout(false);
            this.splitContainerVDA.Panel2.ResumeLayout(false);
            this.splitContainerVDA.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerVDA)).EndInit();
            this.splitContainerVDA.ResumeLayout(false);
            this.cms.ResumeLayout(false);
            this.pnWebBrowser.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainerVDA;
        private System.Windows.Forms.RichTextBox rtbVDAFile;
        private System.Windows.Forms.WebBrowser WebBrowser;
        private System.Windows.Forms.Label lblInfos;
        private System.Windows.Forms.Panel pnWebBrowser;
        private System.Windows.Forms.ContextMenuStrip cms;
        private System.Windows.Forms.ToolStripMenuItem copyCms;
        private System.Windows.Forms.Button btnAnalyz;
        private System.Windows.Forms.Button btnSegment;
    }
}
