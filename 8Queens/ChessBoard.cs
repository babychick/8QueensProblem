using System.Drawing;

namespace _8Queens
{
    class ChessBoard
    {
        private Cell[,] _c;
        private int _col;
        private int _row;
        private int _maxQueen;
        private int[] _markRow;
        private int[] _markCol;
        private int[] _mainD;
        private int[] _subD;

        public ChessBoard()
        {
            C = new Cell[8, 8];
            _col = 8;
            _row = 8;
            _maxQueen = 8;
            _markRow = new int[8];
            _markCol = new int[8];

            for (int i = 0; i < 8; i++)
            {
                _markRow[i] = -1;
                _markCol[i] = -1;
            }
        }

        public int MaxQueen { get => _maxQueen; set => _maxQueen = value; }
        internal Cell[,] C { get => _c; set => _c = value; }
        public int[] MainD { get => _mainD; set => _mainD = value; }
        public int[] SubD { get => _subD; set => _subD = value; }
        public int[] MarkRow { get => _markRow; set => _markRow = value; }
        public int[] MarkCol { get => _markCol; set => _markCol = value; }

        public void create_ChessBoard()
        {
            for (int i = 0; i < _col; i++)
                for (int j = 0; j < _row; j++)
                {
                    if ((i % 2 == 0 && j % 2 == 0) || (i % 2 != 0 && j % 2 != 0))
                    {
                        C[i, j] = new Cell(i * 90, j * 90, -1, Color.Brown);
                    }
                    else
                    {
                        C[i, j] = new Cell(i * 90, j * 90, -1, Color.AntiqueWhite);
                    }
                }
        }

        public void draw_ChessBoard(Graphics g)
        {
            // Draw cells
            for (int x = 0; x < _col; x++)
            {
                for (int y = 0; y < _row; y++)
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
            if (C[x, y].Queen == false && C[x, y].Mark.isEmpty() == true)
            {
                C[x, y].draw_Queen(g);
                C[x, y].State = _maxQueen;
                C[x, y].Queen = true;
                C[x, y].Mark.addElement(_maxQueen);
                _markCol[x] = _maxQueen;
                _markRow[y] = _maxQueen;

                #region ****Mark Column and Row*** 
                for (int i = 0; i < 8; i++)
                {
                    if (i != x)
                    {
                        C[i, y].mark_Cell(g);
                        C[i, y].Mark.addElement(_maxQueen);
                    }

                    if (i != y)
                    {
                        C[x, i].mark_Cell(g);
                        C[x, i].Mark.addElement(_maxQueen);
                    }
                }
                #endregion

                #region ***Mark MainDiagonal***
                // Mark the main diagonal
                int mainDiagonal = x - y + 8;
                int d = 0;

                if (x > y)
                {
                    d = x - y;
                    for (int i = d; i < 8; i++)
                    {
                        for (int j = 0; j < 8 - d; j++)
                        {
                            if ((i - j + 8) == mainDiagonal && i != x && j != y)
                            {
                                C[i, j].mark_Cell(g);
                                C[i, j].Mark.addElement(_maxQueen);
                            }
                        }
                    }
                }
                else
                {
                    d = y - x;
                    for (int i = 0; i < 8 - d; i++)
                    {
                        for (int j = d; j < 8; j++)
                        {
                            if ((i - j + 8) == mainDiagonal && i != x && j != y)
                            {
                                C[i, j].mark_Cell(g);
                                C[i, j].Mark.addElement(_maxQueen);
                            }
                        }
                    }
                }
                #endregion

                #region ***Mark SubDiagonal***
                // Mark the sub diagonal
                int subDiagonal = x + y;
                if (subDiagonal < 8)
                {
                    for (int i = subDiagonal; i >= 0; i--)
                    {
                        for (int j = 0; j <= subDiagonal; j++)
                        {
                            if (i != x && j != y && (i + j) == subDiagonal)
                            {
                                C[i, j].mark_Cell(g);
                                C[i, j].Mark.addElement(_maxQueen);
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 7; i >= 0; i--)
                    {
                        for (int j = subDiagonal - 7; j < 8; j++)
                        {
                            if (i != x && j != y && (i + j) == subDiagonal)
                            {
                                C[i, j].mark_Cell(g);
                                C[i, j].Mark.addElement(_maxQueen);
                            }
                        }
                    }
                }
                #endregion
                _maxQueen--;
            }
        }

        public void remove_Queen(Graphics g, int x, int y)
        {
            if (C[x, y].Queen == true)
            {
                int a = C[x, y].State;
                C[x, y].draw_Cell(g);

                #region UnMark Column and Row
                for (int i = 0; i < 8; i++)
                {
                    C[i, y].Mark.removeElement(a);
                    if (i != x && C[i, y].Mark.isEmpty() == true)
                    {
                        C[i, y].draw_Cell(g);
                    }

                    C[x, i].Mark.removeElement(a);
                    if (i != y && C[x, i].Mark.isEmpty() == true)
                    {
                        C[x, i].draw_Cell(g);
                    }
                }
                #endregion

                #region ***UnMark MainDiagonal***
                // UnMark the main diagonal
                int mainDiagonal = x - y + 8;
                int d = 0;

                if (x > y)
                {
                    d = x - y;
                    for (int i = d; i < 8; i++)
                    {
                        for (int j = 0; j < 8 - d; j++)
                        {
                            if ((i - j + 8) == mainDiagonal && i != x && j != y)
                            {
                                C[i, j].Mark.removeElement(a);
                                if (C[i, j].Mark.isEmpty() == true)
                                {
                                    C[i, j].draw_Cell(g);
                                }
                            }
                        }
                    }
                }
                else
                {
                    d = y - x;
                    for (int i = 0; i < 8 - d; i++)
                    {
                        for (int j = d; j < 8; j++)
                        {
                            if ((i - j + 8) == mainDiagonal && i != x && j != y)
                            {
                                C[i, j].Mark.removeElement(a);
                                if (C[i, j].Mark.isEmpty() == true)
                                {
                                    C[i, j].draw_Cell(g);
                                }
                            }
                        }
                    }
                }
                #endregion

                #region ***UnMark mainDiagonal***
                // UnMark the sub diagonal
                int subDiagonal = x + y;
                if (subDiagonal < 8)
                {
                    for (int i = subDiagonal; i >= 0; i--)
                    {
                        for (int j = 0; j <= subDiagonal; j++)
                        {
                            if (i != x && j != y && (i + j) == subDiagonal)
                            {
                                C[i, j].Mark.removeElement(a);
                                if (C[i, j].Mark.isEmpty() == true)
                                {
                                    C[i, j].draw_Cell(g);
                                }
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 7; i >= 0; i--)
                    {
                        for (int j = subDiagonal - 7; j < 8; j++)
                        {
                            if (i != x && j != y && (i + j) == subDiagonal)
                            {
                                C[i, j].Mark.removeElement(a);
                                if (C[i, j].Mark.isEmpty() == true)
                                {
                                    C[i, j].draw_Cell(g);
                                }
                            }
                        }
                    }
                }
                #endregion
                C[x, y].State = -1;
                C[x, y].Queen = false;
                C[x, y].Mark.removeElement(a);
                _markCol[x] = -1;
                _markRow[y] = -1;
                _maxQueen++;
            }
        }

        public bool check_Win()
        {
            int checkCol = 0;
            int checkRow = 0;
            for (int i = 0; i < 8; i++)
            {
                if (_markCol[i] != -1)
                    checkCol++;
                if (_markRow[i] != -1)
                    checkRow++;
            }

            return (checkCol == 8 && checkRow == 8) ? true : false;
        }
    }
}
