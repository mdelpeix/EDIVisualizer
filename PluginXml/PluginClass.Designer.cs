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
            this.gbXml = new System.Windows.Forms.GroupBox();
            this.pnWb = new System.Windows.Forms.Panel();
            this.wbXml = new System.Windows.Forms.WebBrowser();
            this.gbXml.SuspendLayout();
            this.pnWb.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbXml
            // 
            this.gbXml.Controls.Add(this.pnWb);
            this.gbXml.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbXml.Location = new System.Drawing.Point(0, 0);
            this.gbXml.Margin = new System.Windows.Forms.Padding(5);
            this.gbXml.Name = "gbXml";
            this.gbXml.Size = new System.Drawing.Size(461, 228);
            this.gbXml.TabIndex = 0;
            this.gbXml.TabStop = false;
            this.gbXml.Text = "Document";
            // 
            // pnWb
            // 
            this.pnWb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnWb.Controls.Add(this.wbXml);
            this.pnWb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnWb.Location = new System.Drawing.Point(3, 16);
            this.pnWb.Name = "pnWb";
            this.pnWb.Size = new System.Drawing.Size(455, 209);
            this.pnWb.TabIndex = 1;
            // 
            // wbXml
            // 
            this.wbXml.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbXml.Location = new System.Drawing.Point(0, 0);
            this.wbXml.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbXml.Name = "wbXml";
            this.wbXml.Size = new System.Drawing.Size(453, 207);
            this.wbXml.TabIndex = 1;
            // 
            // PluginClass
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbXml);
            this.Name = "PluginClass";
            this.Size = new System.Drawing.Size(461, 228);
            this.gbXml.ResumeLayout(false);
            this.pnWb.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbXml;
        private System.Windows.Forms.Panel pnWb;
        private System.Windows.Forms.WebBrowser wbXml;
    }
}
