using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace _8Queens
{
    public partial class Auto : UserControl
    {
        private FlowLayoutPanel _flpLeft;
        private Panel _pnRight;
        private Button _btnStart;
        private Button _btnPause;
        private Button _btnResult;
        private Graphics g;
        private ChessBoardAI _cb;
        private Thread t;
        private TrackBar _trbSpeed;
        private Label _lbSpeed;
        private ManualResetEvent _pauseEvent = new ManualResetEvent(true);
        //private ProgressBar _prComplete;
        private bool flag = true;
        private int delayTime = 50;
        private bool pause = false;
        private bool _isFinish = false;
        private bool _isStart = false;
        private int count = 1;

        public Thread T { get => t; set => t = value; }
        public bool Pause { get => pause; set => pause = value; }
        public bool IsStart { get => _isStart; set => _isStart = value; }

        public Auto()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.Width = 890;
            this.Height = 740;
            add_Leftpn();
            add_Rightpn();
            //add_Progressbar();
        }

        public void add_Leftpn()
        {
            _flpLeft = new FlowLayoutPanel();
            _flpLeft.Width = 720;
            _flpLeft.Height = 720;
            _flpLeft.Location = new Point(5, 5);
            _flpLeft.BorderStyle = BorderStyle.FixedSingle;
            g = _flpLeft.CreateGraphics();
            _cb = new ChessBoardAI();
            _cb.create_ChessBoardAI(90, 90);
            this.Controls.Add(_flpLeft);
        }

        public void add_Rightpn()
        {
            _pnRight = new Panel();
            _pnRight.Width = 150;
            _pnRight.Height = 730;
            _pnRight.Location = new Point(740, 0);
            _pnRight.BorderStyle = BorderStyle.Fixed3D;
            _pnRight.BackColor = Color.LightGray;
            _btnStart = new Button();
            _btnPause = new Button();
            _btnResult = new Button();
            _lbSpeed = new Label();
            Label _1, _2, _3, _4, _5, _6;
            _1 = new Label();
            _2 = new Label();
            _3 = new Label();
            _4 = new Label();
            _5 = new Label();
            _6 = new Label();
            add_Button(_btnStart, "Bắt đầu", 13, 550, Start_ButtonClick);
            add_Button(_btnPause, "Tạm dừng", 13, 600, Pause_ButtonClick);
            add_Button(_btnResult, "Kết quả", 13, 650, Result_ButtonClick);
            _btnPause.Enabled = false;
            add_TrackBarSpeed();
            _trbSpeed.Enabled = false;
            add_Label(_lbSpeed, "Tốc độ", 30, 470);
            add_Label(_6, "6", 90, 65);
            add_Label(_5, "5", 90, 140);
            add_Label(_4, "4", 90, 212);
            add_Label(_3, "3", 90, 285);
            add_Label(_2, "2", 90, 358);
            add_Label(_1, "1", 90, 430);
            this.Controls.Add(_pnRight);
        }

        public void add_Button(Button btn, string text, int x, int y, EventHandler btnClick)
        {
            btn.Width = 120;
            btn.Height = 35;
            btn.Text = text;
            btn.Font = new Font("Arial", 10);
            btn.BackColor = Color.White;
            btn.Location = new Point(x, y);
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderColor = Color.Red;
            btn.FlatAppearance.BorderSize = 2;
            btn.Click += new EventHandler(btnClick);
            _pnRight.Controls.Add(btn);
        }

        public void add_Label(Label lb, string text, int x, int y)
        {
            lb.AutoSize = true;
            lb.Text = text;
            lb.Font = new Font("Arial", 11, FontStyle.Italic);
            lb.Location = new Point(x, y);
            _pnRight.Controls.Add(lb);
        }

        public void add_TrackBarSpeed()
        {
            _trbSpeed = new TrackBar();
            _trbSpeed.Width = 70;
            _trbSpeed.Height = 400;
            _trbSpeed.Minimum = 1;
            _trbSpeed.Maximum = 6;
            _trbSpeed.TickFrequency = 1;
            _trbSpeed.LargeChange = 1;
            _trbSpeed.Value = 2;
            _trbSpeed.Orientation = Orientation.Vertical;
            _trbSpeed.TickStyle = TickStyle.BottomRight;
            _trbSpeed.Location = new Point(40, 60);
            _trbSpeed.Scroll += new EventHandler(Speed_Scroll);
            _pnRight.Controls.Add(_trbSpeed);
        }

        //public void add_Progressbar()
        //{
        //    _prComplete = new ProgressBar();
        //    _prComplete.Width = 730;
        //    _prComplete.Height = 7;
        //    _prComplete.Location = new Point(0, 732);
        //    _prComplete.MarqueeAnimationSpeed = 50;
        //    _prComplete.Minimum = 0;
        //    _prComplete.Maximum = 100;
        //    this.Controls.Add(_prComplete);
        //}

        private void Auto_Paint(object sender, PaintEventArgs e)
        {
            _flpLeft.Refresh();
            Pen p = new Pen(Color.Black, 4);
            e.Graphics.DrawRectangle(p, 2, 2, 726, 726);
            p.Dispose();
            _cb.draw_ChessBoardAI(g);
        }

        private void Start_ButtonClick(object sender, EventArgs e)
        {
            if (IsStart == false)
            {
                IsStart = true;
                _btnStart.Text = "Dừng lại";
                _cb = new ChessBoardAI();
                _cb.create_ChessBoardAI(90, 90);
                _cb.draw_ChessBoardAI(g);
                T = new Thread(runThread);
                T.Start();
                _btnPause.Enabled = true;
                _trbSpeed.Enabled = true;
                _btnResult.Enabled = false;
            }
            else
            {
                IsStart = false;
                _btnStart.Text = "Bắt đầu";
                T.Abort();
                _btnPause.Enabled = false;
                _trbSpeed.Enabled = false;
                _btnResult.Enabled = true;
            }
        }

        private void runThread()
        {
            Solution();
            T.Abort();
            _cb.create_ChessBoardAI(90, 90);
        }

        private void Pause_ButtonClick(object sender, EventArgs e)
        {

            if (Pause == false)
            {
                Pause = true;
                _btnPause.Text = "Tiếp tục";
                _pauseEvent.Reset();
                //T.Suspend();
            }
            else
            {
                Pause = false;
                _btnPause.Text = "Tạm dừng";
                _pauseEvent.Set();
                //T.Resume();
            }
        }

        private void Result_ButtonClick(object sender, EventArgs e)
        {
            if (_cb.readFile() == true)
            {
                ResultForm rf = new ResultForm();
                rf.Show();
                for (int i = 0; i < _cb.ListResult.Length; i++)
                {
                    miniMap m = new miniMap();
                    m.L = _cb.ListResult[i];
                    m.New();
                    rf.Flp.Controls.Add(m);
                }
            }
            else
                MessageBox.Show("CHƯA GHI NHẬN ĐƯỢC KẾT QUẢ NÀO!", "Thông báo");
        }

        private void Speed_Scroll(object sender, EventArgs e)
        {
            switch (_trbSpeed.Value)
            {
                case 1:
                    delayTime = 0;
                    break;
                case 2:
                    delayTime = 35;
                    break;
                case 3:
                    delayTime = 100;
                    break;
                case 4:
                    delayTime = 200;
                    break;
                case 5:
                    delayTime = 350;
                    break;
                case 6:
                    delayTime = 450;
                    break;
            }
        }

        private void Solution()
        {
            #region backtracking with recursion
            //for (int i = 0; i < 8; i++)
            //{
            //    if (_cb.isSafe(x, i))
            //    {
            //        _cb.place_Queen(g, x, i);
            //        _cb.mark_Cell(x, i);
            //        Thread.Sleep(delayTime);
            //        if (x < 7)
            //            Solution(x + 1);
            //        else
            //        {
            //            // record
            //        }
            //        _cb.remove_Queen(g, x, i);
            //        _cb.unmark_Cell(x, i);
            //    }
            //}
            #endregion

            #region backtracking using stack
            File.WriteAllText(@"\Result.txt", string.Empty);
            int j = 0;
            count = 1;

            for (int i = 0; i < 8; i++)
            {
                _pauseEvent.WaitOne();
                Thread.Sleep(delayTime);
                if (flag)
                    j = 0;
                for (; j < 8; j++)
                {
                    if (_cb.isSafe(i, j))
                    {
                        _pauseEvent.WaitOne();
                        Thread.Sleep(delayTime);
                        _cb.place_Queen(g, i, j);
                        _cb.Stack.Push(i, j);
                        _cb.mark_Cell(i, j);
                        flag = true;
                        break;
                    }
                }

                if (j == 8)
                {
                    _pauseEvent.WaitOne();
                    Thread.Sleep(delayTime);
                    int a = _cb.Stack.TopX();
                    int b = _cb.Stack.TopY();

                    _cb.unmark_Cell(a, b);
                    _cb.Stack.Pop();
                    _cb.remove_Queen(g, a, b);
                    i -= 2;
                    j = b + 1;
                    flag = false;

                    if (a == 0 && b == 7)
                    {
                        _isFinish = true;
                    }
                    else
                    {
                        _isFinish = false;
                    }
                }

                //Console.WriteLine(_cb.Stack.Size);

                if (_cb.Stack.isFull())
                {
                    Console.WriteLine("result " + count + " " + i + " " + j);
                    _cb.writeFile(count);
                    count++;

                    int a = -1, b = -1;

                    for (int t = 0; t < 2; t++)
                    {
                        _pauseEvent.WaitOne();
                        Thread.Sleep(delayTime);
                        a = _cb.Stack.TopX();
                        b = _cb.Stack.TopY();

                        _cb.unmark_Cell(a, b);
                        _cb.Stack.Pop();
                        _cb.remove_Queen(g, a, b);
                    }

                    i -= 2;
                    j = b + 1;
                    flag = false;
                }

                if (_isFinish)
                    break;
            }

            if (_isFinish)
            {
                MessageBox.Show("HOÀN TẤT!", "Thông báo");
                _isFinish = false;
                _isStart = false;
                pause = false;
                // use this code to access controls in another thread
                this.Invoke(new MethodInvoker(delegate ()
                {
                    //Access your controls
                    _btnPause.Enabled = false;
                    _btnResult.Enabled = true;
                    _btnStart.Text = "Bắt đầu";
                    _btnPause.Text = "Tạm dừng";
                }));

                T.Abort();
            }

            _pauseEvent.Close();
            #endregion
        }
    }
}