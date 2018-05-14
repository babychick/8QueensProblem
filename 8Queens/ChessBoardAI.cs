using System;
using System.Drawing;
using System.IO;

namespace _8Queens
{
    class ChessBoardAI
    {
        private Cell[,] _c;
        private int[] _row;
        private int[] _col;
        private int[] _mainDiagonal;
        private int[] _subDiagonal;
        private Stack _stack;
        private List[] _listResult;

        public int[] Row { get => _row; set => _row = value; }
        public int[] Col { get => _col; set => _col = value; }
        public int[] MainDiagonal { get => _mainDiagonal; set => _mainDiagonal = value; }
        public int[] SubDiagonal { get => _subDiagonal; set => _subDiagonal = value; }
        internal Cell[,] C { get => _c; set => _c = value; }
        internal Stack Stack { get => _stack; set => _stack = value; }
        internal List[] ListResult { get => _listResult; set => _listResult = value; }

        public ChessBoardAI()
        {
            C = new Cell[8, 8];
            Row = new int[8];
            Col = new int[8];
            MainDiagonal = new int[16];
            SubDiagonal = new int[16];

            for (int i = 0; i < 8; i++)
            {
                Row[i] = -1;
                Col[i] = -1;
            }

            for (int i = 0; i < 16; i++)
            {
                MainDiagonal[i] = -1;
                SubDiagonal[i] = -1;
            }

            Stack = new Stack();
        }

        public void create_ChessBoardAI(int width, int height)
        {
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    if ((i % 2 == 0 && j % 2 == 0) || (i % 2 != 0 && j % 2 != 0))
                    {
                        C[i, j] = new Cell(i * width, j * height, -1, Color.Brown);
                    }
                    else
                    {
                        C[i, j] = new Cell(i * width, j * height, -1, Color.AntiqueWhite);
                    }
                }
        }

        public void draw_ChessBoardAI(Graphics g)
        {
            // Draw cells
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    C[x, y].draw_Cell(g);
                    if (C[x, y].Queen == true)
                        C[x, y].draw_Queen(g);
                    if (C[x, y].Queen == false && C[x, y].Mark.isEmpty() == false)
                        C[x, y].mark_Cell(g);
                }
            }
        }

        public void place_Queen(Graphics g, int x, int y)
        {
            C[x, y].draw_Queen(g);
            C[x, y].Queen = true;
        }

        public void mark_Cell(int x, int y)
        {
            Row[x] = y;
            Col[y] = 1;
            MainDiagonal[x - y + 8] = 1;
            SubDiagonal[x + y] = 1;
        }

        public void remove_Queen(Graphics g, int x, int y)
        {
            C[x, y].draw_Cell(g);
            C[x, y].Queen = false;
        }

        public void unmark_Cell(int x, int y)
        {
            Row[x] = -1;
            Col[y] = -1;
            MainDiagonal[x - y + 8] = -1;
            SubDiagonal[x + y] = -1;
        }

        public bool isSafe(int x, int y)
        {
            return (Row[x] == -1 && Col[y] == -1 && MainDiagonal[x - y + 8] == -1 && SubDiagonal[x + y] == -1) ? true : false;
        }

        public void writeFile(int i)
        {
            StreamWriter w = new StreamWriter(@"\Result.txt", true);
            
            w.WriteLine("Result: " + i);
            for (int j = 0; j < 8; j++)
            {
                string s = Row[j] +  1 + "";
                w.WriteLine(s);
            }
            w.Close();
        }

        public bool readFile()
        {
            _listResult = new List[92];
            int i = 0;
            StreamReader r = new StreamReader(@"\Result.txt");
            string line;
            if (r.Peek() > 0)
            {
                while ((line = r.ReadLine()) != null)
                {
                    _listResult[i] = new List();
                    int num;
                    for (int j = 0; j < 8; j++)
                    {
                        num = Int32.Parse(r.ReadLine());
                        _listResult[i].addElement(num);
                    }
                    i++;
                }
            }
            else
                return false;
            r.Close();
            return true;
        }
    }
}
