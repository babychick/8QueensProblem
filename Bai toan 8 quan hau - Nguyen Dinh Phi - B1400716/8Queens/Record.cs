using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _8Queens
{
    public partial class Record : Form
    {
        public Record()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;
            this.Icon = Properties.Resources.black_queen_2d;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Manual.player = textBox1.Text;
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Manual.player = "";
            this.Dispose();
        }
    }
}
