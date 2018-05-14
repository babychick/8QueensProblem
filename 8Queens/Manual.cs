using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace _8Queens
{
    public partial class Manual : UserControl
    {
        public struct Score
        {
            public string[] name;
            public int[] time;
            public int count;
        }

        private Panel _pnRight;
        private FlowLayoutPanel _flpTop;
        private ChessBoard _cbGamePlay;
        private Graphics g;
        private Label _lbCount;
        private Label _lbTimer;
        private Label _lbLeft;
        private Label _lbRight;
        private Label _lbGuild;
        private Label _lbInfo;
        private Label _lbRecord;
        private Button _btnReplay;
        private Timer _ticker;
        private TableLayoutPanel _tlpRecord;
        private bool _isWin = false;
        private bool _isStart = false;
        private int tick = 0;
        public static string player;
        private Score save = new Score();

        public Manual()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.Width = 890;
            this.Height = 740;
            add_Leftpn();
            add_Rightpn();
            ReadFile();
            add_Table();
            player = "";
            _ticker = new Timer();
            _ticker.Interval = 1000;
            _ticker.Tick += new EventHandler(Timer_Tick);
        }

        public void add_Rightpn()
        {
            _pnRight = new Panel();
            _pnRight.Width = 150;
            _pnRight.Height = 730;
            _pnRight.BorderStyle = BorderStyle.Fixed3D;
            _pnRight.BackColor = Color.LightSteelBlue;
            _pnRight.Location = new Point(740, 0);

            _lbInfo = new Label();
            add_LabelText(_lbInfo, "Thông tin", 10, 50);
            //add timer label
            _lbTimer = new Label();
            add_Label(_lbTimer, tick + "", 25, 100, 32, 32, Properties.Resources.timer);
            //add countdown label
            _lbCount = new Label();
            add_Label(_lbCount, "" + _cbGamePlay.MaxQueen, 25, 150, 32, 32, Properties.Resources.black_queen_48px);

            _lbGuild = new Label();
            add_LabelText(_lbGuild, "Hướng dẫn", 10, 250);
            //add label leftclick
            _lbLeft = new Label();
            add_Label(_lbLeft, "Đặt hậu", 10, 300, 48, 48, Properties.Resources.leftclick);
            _lbLeft.Text = "     Đặt hậu";
            //add label rightclick
            _lbRight = new Label();
            add_Label(_lbRight, "Gỡ hậu", 15, 350, 48, 48, Properties.Resources.rightclick);
            _lbRight.Text = "   Gỡ hậu ";

            _lbRecord = new Label();
            add_LabelText(_lbRecord, "Xếp hạng", 10, 450);
            //add button replay
            _btnReplay = new Button();
            add_Button(_btnReplay, "Chơi lại", 25, 650, Replay_ButtonClick);
                        
            this.Controls.Add(_pnRight);
        }

        public void add_Label(Label lb, string text, int x, int y, int w, int h, Image img)
        {
            lb.AutoSize = false;
            lb.Text = text + "        ";
            lb.TextAlign = ContentAlignment.MiddleRight;
            lb.Width = 120;
            lb.Height = 50;
            lb.Image = new Bitmap(img, w, h);
            lb.ImageAlign = ContentAlignment.MiddleLeft;
            lb.Font = new Font("Arial", 11);
            lb.Location = new Point(x, y);
            _pnRight.Controls.Add(lb);
        }

        public void add_LabelText(Label lb, string text, int x, int y)
        {
            lb.AutoSize = false;
            lb.Width = 150;
            lb.Height = 35;
            lb.Text = text;
            lb.Font = new Font("Arial", 12);
            lb.TextAlign = ContentAlignment.MiddleLeft;
            lb.Location = new Point(x, y);
            _pnRight.Controls.Add(lb);
        }

        public void add_Button(Button btn, string text, int x, int y, EventHandler click)
        {
            btn.Text = text;
            btn.Font = new Font("Arial", 10);
            btn.TextAlign = ContentAlignment.MiddleCenter;
            btn.Height = 35;
            btn.Width = 100;
            btn.Location = new Point(x, y);
            btn.Padding = new Padding(2);
            btn.BackColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderColor = Color.Red;
            btn.FlatAppearance.BorderSize = 2;
            btn.Click += new EventHandler(click);
            _pnRight.Controls.Add(btn);
        }

        public void add_Leftpn()
        {
            _flpTop = new FlowLayoutPanel();
            _flpTop.Width = 720;
            _flpTop.Height = 720;
            _flpTop.Location = new Point(5, 5);
            _flpTop.BorderStyle = BorderStyle.FixedSingle;
            g = _flpTop.CreateGraphics();
            _cbGamePlay = new ChessBoard();
            _cbGamePlay.create_ChessBoard();
            _flpTop.MouseDown += new MouseEventHandler(Manual_MouseDown);
            this.Controls.Add(_flpTop);
        }

        public void add_Table()
        {
            _tlpRecord = new TableLayoutPanel();
            _tlpRecord.ColumnCount = 2;
            _tlpRecord.RowCount = 5;
            _tlpRecord.Width = 140;
            _tlpRecord.Height = 200;
            //_tlpRecord.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            //_tlpRecord.Controls.Add(new Label() { Text = "Xếp hạng", Font = new Font("Arial", 12) }, 1, 0);
            _tlpRecord.Location = new Point(10, 500);
            add_Score();
            _pnRight.Controls.Add(_tlpRecord);
        }
        private void Manual_Paint(object sender, PaintEventArgs e)
        {
            _flpTop.Refresh();
            Pen p = new Pen(Color.Black, 4);
            e.Graphics.DrawRectangle(p, 2, 2, 726, 726);
            p.Dispose();
            _cbGamePlay.draw_ChessBoard(g);

        }
        private void Manual_MouseDown(object sender, MouseEventArgs e)
        {
            int x = e.X / 90;
            int y = e.Y / 90;

            if ((x >= 0 && x <= 7) && (y >= 0 && y <= 7))
            {
                if (_isStart == false)
                {
                    _ticker.Start();
                    _isStart = true;
                }

                if (!_isWin)
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        _cbGamePlay.place_Queen(g, x, y);
                    }

                    if (e.Button == MouseButtons.Right)
                    {
                        _cbGamePlay.remove_Queen(g, x, y);
                    }
                }

                _lbCount.Text = _cbGamePlay.MaxQueen + "        ";

                if (_cbGamePlay.MaxQueen < 5)
                {
                    if (_cbGamePlay.check_Win() == true)
                    {
                        _isWin = true;
                        _ticker.Stop();
                        if (MessageBox.Show("BẠN ĐÃ CHIẾN THẮNG!", "Thông báo") == DialogResult.OK)
                        {
                            Record r = new Record();
                            r.ShowDialog();
                            if (player != "")
                            {
                                int i = save.count;
                                // save score
                                save.name[i] = player;
                                save.time[i] = tick;
                                save.count++;
                                add_Score();
                                WriteFile();
                            }                            
                            r.Dispose();
                        }
                    }
                }
            }
        }
        private void Replay_ButtonClick(object sender, EventArgs e)
        {
            tick = 0;
            _isStart = false;
            _ticker.Stop();
            _cbGamePlay = new ChessBoard();
            _cbGamePlay.create_ChessBoard();
            _cbGamePlay.draw_ChessBoard(g);
            _isWin = false;
            _lbCount.Text = _cbGamePlay.MaxQueen + "        ";
            _lbTimer.Text = tick + "        ";
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            tick++;
            _lbTimer.Text = tick + "        ";
        }
        public void add_Score()
        {
            // Sort
            int min_index;

            for (int a = 0; a < save.count - 1; a++)
            {
                min_index = a;

                for (int b = a + 1; b < save.count; b++)
                {
                    if (save.time[b] < save.time[min_index])
                        min_index = b;
                }

                if (min_index != a)
                {
                    int temp_time;
                    string temp_name;

                    temp_time = save.time[min_index];
                    temp_name = save.name[min_index];

                    save.time[min_index] = save.time[a];
                    save.name[min_index] = save.name[a];

                    save.time[a] = temp_time;
                    save.name[a] = temp_name;
                }
            }
            // add record into table
            _tlpRecord.Controls.Clear();
            for (int i = 0; i < save.count; i++)
            {
                _tlpRecord.Controls.Add(new Label() { Text = (i + 1) + ".  " + save.name[i], Width = 70}, 0, i);
                _tlpRecord.Controls.Add(new Label() { Text = save.time[i] + "s", Width = 70 }, 1, i);
            }
        }
        public void ReadFile()
        {
            save.name = new string[10];
            save.time = new int[10];
            StreamReader r = new StreamReader("..//..//Record.txt");

            if (r.Peek() > 0)
            {
                save.count = Int32.Parse(r.ReadLine());
                int i = 0;

                // read data from text file
                while (i < save.count)
                {
                    save.name[i] = r.ReadLine();
                    save.time[i] = Int32.Parse(r.ReadLine());
                    i++;
                }
            }
            r.Close();
        }

        public void WriteFile()
        {
            StreamWriter w = new StreamWriter("..//..//Record.txt");
            w.WriteLine(save.count);
            int i = 0;
            while (i < save.count)
            {
                w.WriteLine(save.name[i]);
                w.WriteLine(save.time[i]);
                i++;
            }
            w.Close();
        }
    }
}
