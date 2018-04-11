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
    public partial class MainForm : Form
    {
        private MenuStrip _Menu;
        private ToolStripMenuItem[] _menuItems;
        private Panel _pnMain;
        private Panel _pnInfo;
        private Label _lbInfo;
        private Graphics g;
        private Manual _mn;
        private Auto _at;
        private bool _isAuto = false;

        public MainForm()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.Width = 850;
            this.Height = 300;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(500, 50);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.Icon = Properties.Resources.black_queen_2d;
            add_Menu();
            add_Label();
            add_MainPanel();
            add_InfoPanel();
        }

        public void add_Menu()
        {
            // Create menu
            _Menu = new MenuStrip();
            _Menu.BackColor = Color.LightSteelBlue;
            _menuItems = new ToolStripMenuItem[2];
            
            _menuItems[0] = new ToolStripMenuItem();
            _menuItems[0].Text = "Chế độ chơi";
            _menuItems[0].Font = new Font("Arial", 10);

            ToolStripMenuItem[] _subItems1 = new ToolStripMenuItem[2];
            _subItems1[0] = new ToolStripMenuItem("Người chơi");
            _subItems1[0].Click += new EventHandler(Manual_MenuClick);
            _subItems1[1] = new ToolStripMenuItem("Máy chơi");
            _subItems1[1].Click += new EventHandler(Auto_MenuClick);

            _menuItems[0].DropDownItems.Add(_subItems1[0]);
            _menuItems[0].DropDownItems.Add(_subItems1[1]);
            
            _menuItems[1] = new ToolStripMenuItem();
            _menuItems[1].Text = "Thông tin trò chơi";
            _menuItems[1].Font = new Font("Arial", 10);
            _menuItems[1].Click += new EventHandler(Info_MenuClick);

            //ToolStripMenuItem[] _subItems2 = new ToolStripMenuItem[2];
            //_subItems2[0] = new ToolStripMenuItem("Cách chơi");
            //_subItems2[1] = new ToolStripMenuItem("Thông tin");

            //_menuItems[1].DropDownItems.Add(_subItems2[0]);
            //_menuItems[1].DropDownItems.Add(_subItems2[1]);

            _Menu.Dock = DockStyle.Top;
            _Menu.AutoSize = true;
            _Menu.Items.Add(_menuItems[0]);
            _Menu.Items.Add(_menuItems[1]);
            
            this.Controls.Add(_Menu);
        }

        public void add_Label()
        {
            _lbInfo = new Label();
            _lbInfo.Text = "TRÒ CHƠI MÔ PHỎNG SẮP XẾP QUÂN HẬU";
            _lbInfo.Font = new Font(FontFamily.GenericMonospace, 19, FontStyle.Bold);
            _lbInfo.ForeColor = Color.MidnightBlue;
            _lbInfo.AutoSize = true;
            _lbInfo.TextAlign = ContentAlignment.MiddleCenter;
            _lbInfo.Location = new Point(90, 50);
            this.Controls.Add(_lbInfo);
        }

        public void add_MainPanel()
        {
            _pnMain = new Panel();
            _pnMain.AutoSize = true;
            _pnMain.Location = new Point(0, 100);
            this.AutoSize = true;
            this.Controls.Add(_pnMain);
            g = _pnMain.CreateGraphics();
        }

        public void add_InfoPanel()
        {
            _pnInfo = new Panel();
            _pnInfo.Width = 650;
            _pnInfo.Height = 500;
            _pnInfo.Location = new Point(100, 0);

            Label _lb = new Label();
            _lb.Width = 650;
            _lb.Height = 70;
            _lb.Text = "- - - Đề tài Niên luận Cơ sở KTPM - - -\n"
                       + "Nguyễn Đình Phi";
            _lb.Font = new Font("Times New Roman", 14);
            _lb.TextAlign = ContentAlignment.MiddleCenter;
            _lb.Location = new Point(0, 0);
            _lb.ForeColor = Color.Salmon;
            _pnInfo.Controls.Add(_lb);

            Label _lbGuild = new Label();
            _lbGuild.Width = 650;
            _lbGuild.Height = 400;
            _lbGuild.Font = new Font("Arial", 10);
            _lbGuild.Location = new Point(0, 70);
            _lbGuild.Text = "   Trong cờ vua, quân hậu được phép di chuyển theo hàng, theo cột, theo đường chéo "
                            + "và có thể “ăn” bất cứ quân nào nằm trên đường đi của nó.\n\n"
                            + "   Sắp xếp 8 quân hậu là bài toán đặt lần lượt 8 quân hậu trên bàn cờ vua "
                            + "có kích thước 8x8 (64 ô) sao cho các quân hậu từng đôi một không “ăn” được nhau. "
                            + "Có nghĩa là, trên cùng hàng, cùng cột hoặc cùng đường chéo không có nhiều hơn 1 quân hậu. "
                            + "Trong bài toán này, màu sắc các quân hậu được xem là như nhau.\n\n"
                            + "   Phần mềm trò chơi mô phỏng sắp xếp 8 quân hậu có 2 chế độ:\n\n"
                            + "     1. Chế độ Người chơi (thủ công): Người chơi dùng chuột trái để đặt quân hậu "
                            + "vào ô cờ, dùng chuột phải để gỡ bỏ quân hậu ra khỏi ô cờ.\n"
                            + "Cho đến khi được cả 8 quân hậu lên bàn cờ thì chiến thắng.\n\n"
                            + "     2. Chế độ Máy chơi (tự động): Khi bấm vào nút Bắt Đầu, phần mềm sẽ tự tìm và đặt quân hậu vào vị trí thích hợp "
                            + "cho đến khi tìm được hết tất cả các lời giải có thể có thì dừng giải thuật. Có thể điều chỉnh "
                            + "độ nhanh, chậm quá trình tìm kiếm bằng thanh Tốc Độ phía bên phải.";
            _lbGuild.TextAlign = ContentAlignment.MiddleLeft;
            _pnInfo.Controls.Add(_lbGuild);
            _pnMain.Controls.Add(_pnInfo);
        }
        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
        }

        private void Manual_MenuClick(object sender, EventArgs e)
        {
            _isAuto = false;
            _pnMain.Controls.Clear();
            this.Width = 1002;
            this.Height = 900;
            _mn = new Manual();
            _mn.Location = new Point(52, 0);
            _pnMain.Controls.Add(_mn);
        }

        private void Auto_MenuClick(object sender, EventArgs e)
        {
            _isAuto = true;
            _pnMain.Controls.Clear();
            this.Width = 1002;
            this.Height = 900;
            _at = new Auto();
            _at.Location = new Point(52, 0);
            _pnMain.Controls.Add(_at);
        }

        private void Info_MenuClick(object sender, EventArgs e)
        {
            _pnMain.Controls.Clear();
            add_InfoPanel();
            this.Width = 850;
            this.Height = 500;
            _isAuto = false;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {            
            if (_isAuto == true && _at.IsStart == true)
            {
                if (_at.Pause == true)
                {
                    _at.T.Abort();
                }

                _at.T.Abort();
            }
            g.Dispose();
        }
    }
}
