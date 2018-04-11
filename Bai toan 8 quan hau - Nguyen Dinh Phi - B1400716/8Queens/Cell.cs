using System.Drawing;

namespace _8Queens
{
    class Cell
    {
        private const int sizeof_Cell = 90;
        private int _x, _y;
        private int _state;
        private Image _img;
        private Color _clr;
        private bool _queen;
        private List _mark;

        public int X { get => _x; set => _x = value; }
        public int Y { get => _y; set => _y = value; }
        public int State { get => _state; set => _state = value; }
        public Image Img { get => _img; set => _img = value; }
        public Color Clr { get => _clr; set => _clr = value; }
        public bool Queen { get => _queen; set => _queen = value; }
        internal List Mark { get => _mark; set => _mark = value; }

        public Cell()
        {
            _x = _y = 0;
            _state = -1;
            _img = new Bitmap(Properties.Resources.black_queen_128px);
            _clr = Color.White;
            _queen = false;
            _mark = new List();
        }

        public Cell(int x, int y, int state, Color clr)
        {
            _x = x;
            _y = y;
            _state = state;
            _img = new Bitmap(Properties.Resources.black_queen_128px);
            _clr = clr;
            _queen = false;
            _mark = new List();
        }

        public Cell(int x, int y, Color clr)
        {
            _x = x;
            _y = y;
            _img = new Bitmap(Properties.Resources.black_queen_48px);
            _clr = clr;
            _queen = false;
        }

        public void draw_Queen(Graphics g)
        {
            _img = Properties.Resources.black_queen_128px;
            g.DrawImage(_img, _x + 2, _y, 86, 86);
            _img.Dispose();
        }

        public void draw_Queen(Graphics g, int width, int height)
        {
            _img = Properties.Resources.black_queen_128px;
            g.DrawImage(_img, _x + 1, _y, width, height);
            _img.Dispose();
        }

        public void draw_Cell(Graphics g)
        {
            SolidBrush sb = new SolidBrush(_clr);
            g.FillRectangle(sb, _x, _y, sizeof_Cell, sizeof_Cell);
            sb.Dispose();
        }

        public void draw_Cell(Graphics g, int width, int height)
        {
            SolidBrush sb = new SolidBrush(_clr);
            g.FillRectangle(sb, _x, _y, width, height);
            sb.Dispose();
        }

        public void mark_Cell(Graphics g)
        {
            _img = Properties.Resources.mark;
            g.DrawImage(_img, _x + 5, _y + 5, 80, 80);
            _img.Dispose();
        }
    }
}
