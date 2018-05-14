using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace _8Queens
{
    public partial class miniMap : UserControl
    {
        private Graphics g;
        private List l;
        private Cell[,] c;

        internal List L { get => l; set => l = value; }
        internal Cell[,] C { get => c; set => c = value; }

        public miniMap()
        {
            InitializeComponent();
            this.Width = 417;
            this.Height = 417;
            g = this.CreateGraphics();
            l = new List();
            C = new Cell[8, 8];
        }

        public void New()
        {
            int x = -1;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if ((i % 2 == 0 && j % 2 == 0) || (i % 2 != 0 && j % 2 != 0))
                    {
                        C[i, j] = new Cell(i * 50 + 7, j * 50 + 7, Color.Brown);
                    }
                    else
                    {
                        C[i, j] = new Cell(i * 50 + 7, j * 50 + 7, Color.AntiqueWhite);
                    }
                }
                x = l.getElement(i) - 1;
                if (x >= 0)
                    C[i, x].Queen = true;
            }
        }

        private void miniMap_Paint(object sender, PaintEventArgs e)
        {
            Pen p = new Pen(Color.Black, 4);
            g.DrawRectangle(p, 5, 5, 404, 404);

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    C[x, y].draw_Cell(g, 50, 50);
                    if (C[x, y].Queen == true)
                        C[x, y].draw_Queen(g, 46, 46);
                }
            }
            p.Dispose();
        }
    }
}
