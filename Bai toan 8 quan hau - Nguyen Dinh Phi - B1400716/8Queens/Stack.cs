using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8Queens
{
    class Stack
    {
        private int size;
        private int[] x;
        private int[] y;

        public int Size { get => size; set => size = value; }

        public Stack()
        {
            size = 0;
            x = new int[8];
            y = new int[8];
        }

        public void make_Null()
        {
            size = 0;
        }

        public void Pop()
        {
            size--;
        }

        public int TopX()
        {
            if (size > 0)
                return x[size - 1];
            return -1;
        }

        public int TopY()
        {
            if (size > 0)
                return y[size - 1];
            return -1;
        }

        public void Push(int a, int b)
        {
            x[size] = a;
            y[size] = b;
            size++;
        }

        public bool isFull()
        {
            return (size == 8) ? true : false;
        }
    }
}
