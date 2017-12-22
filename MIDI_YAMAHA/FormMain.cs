using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MIDI_YAMAHA
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void btnMIDI2PC_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            FormMIDI2PC midi2pcFrom = new FormMIDI2PC();
            if (midi2pcFrom.ShowDialog(this) == DialogResult.OK)
            {
                //We would apply changes here since the user accepted them
            }
            midi2pcFrom.Dispose();
            this.Visible = true;
        }

        private void btnText2Message_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            FormTextMessage TextMessageFrom = new FormTextMessage();
            if (TextMessageFrom.ShowDialog(this) == DialogResult.OK)
            {
                //We would apply changes here since the user accepted them
            }
            TextMessageFrom.Dispose();
            this.Visible = true;
        }
    }
}
