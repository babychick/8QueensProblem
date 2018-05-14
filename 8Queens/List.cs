using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8Queens
{
    class List
    {
        private int[] list;
        private int size;

        public List()
        {
            list = new int[8];
            for (int i = 0; i < 8; i++)
            {
                list[i] = -1;
            }
            size = 0;
        }
        
        public bool isEmpty()
        {
            int count = 0;
            for (int i = 0; i < list.Length; i++)
            {
                if (list[i] == -1)
                    count++;
            }

            return (count == list.Length) ? true : false;
        }

        public void addElement (int x)
        {
            list[size] = x;
            size++;
        }

        public void removeElement (int x)
        {
            for (int i = 0; i < size; i++)
            {
                if (list[i] == x)
                {
                    for (int j = i; j < size; j++)
                        list[j] = list[j + 1];
                }
                list[size - 1] = -1;
                size--;
                i = size;
            }
        }

        public int getElement (int i)
        {
            return list[i];
        }
    }
}
