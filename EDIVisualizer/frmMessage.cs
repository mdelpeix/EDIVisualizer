using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EDIVisualizer
{
    public partial class frmMessage : Form
    {

        private string _message;

        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                lblMess.Text = _message;
                this.Refresh();
            }
        }

        public frmMessage()
        {
            InitializeComponent();
        }

        private void frmMessage_Load(object sender, EventArgs e)
        {
            lblMess.Size = this.Size;
        }

        private void frmMessage_Layout(object sender, LayoutEventArgs e)
        {
            this.Location = new Point(this.Owner.Top + (this.Owner.Width / 2), this.Owner.Top + (this.Owner.Height / 2));

        }
    }
}
