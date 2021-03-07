namespace EDIVisualizer
{
    partial class frmMessage
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblMess = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblMess
            // 
            this.lblMess.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMess.CausesValidation = false;
            this.lblMess.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMess.Location = new System.Drawing.Point(0, 0);
            this.lblMess.Name = "lblMess";
            this.lblMess.Size = new System.Drawing.Size(146, 48);
            this.lblMess.TabIndex = 0;
            this.lblMess.Text = "text";
            this.lblMess.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(146, 48);
            this.Controls.Add(this.lblMess);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmMessage";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Load += new System.EventHandler(this.frmMessage_Load);
            this.Layout += new System.Windows.Forms.LayoutEventHandler(this.frmMessage_Layout);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblMess;
    }
}