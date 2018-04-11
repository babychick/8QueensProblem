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
    public partial class ResultForm : Form
    {
        private FlowLayoutPanel flp;

        public ResultForm()
        {
            InitializeComponent();
            this.Width = 1330;
            this.Height = 920;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(300, 30);
            this.Icon = Properties.Resources.black_queen_2d;
            add_Panel();
        }

        public FlowLayoutPanel Flp { get => flp; set => flp = value; }

        public void add_Panel()
        {
            Flp = new FlowLayoutPanel();
            Flp.Width = 1295;
            Flp.Height = 850;
            Flp.BorderStyle = BorderStyle.Fixed3D;
            Flp.AutoScroll = true;
            Flp.FlowDirection = FlowDirection.LeftToRight;
            Flp.Location = new Point(10, 10);
            this.Controls.Add(Flp);
        }
    }
}
