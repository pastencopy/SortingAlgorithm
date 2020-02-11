using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SortingAlgorithm
{
    public partial class Form1 : Form
    {
        Random rnd = new Random();
        
        Data [] datas;

        const int BOX_SIZE = 8;
        const int TIME_SLEEP = 10;

        int trim_size = 1;

        Bitmap drawImage;

        public Form1()
        {
            InitializeComponent();

            drawImage = new Bitmap(picCanvas.Width, picCanvas.Height);

        }

        private void btnQuickSort_Click(object sender, EventArgs e)
        {
            this.Text = "QuickSort";
            datas = new Data[picCanvas.Width / BOX_SIZE];

            //Generate Random datas
            for (int i = 0; i < datas.Length; i++)
            {
                datas[i] = new Data(picCanvas.Height);
            }

            Draw();

            QuickSort(0, datas.Length - 1 );

            Draw();
            MessageBox.Show("Done!");
        }

        private void swap(int a, int b)
        {
            Data tmp = datas[b];
            datas[b] = datas[a];
            datas[a] = tmp;
        }
        

        /*
         * 
         * QuickSort
         * 
         * 
         * */
        private int Partition(int lo, int hi)
        {
            Data pivot = datas[(lo + hi) / 2];

            int i = lo - 1;
            int j = hi + 1;

            while (true)
            {
                do
                {
                    i++;

                } while (datas[i].CompareTo(pivot) > 0);
                
                do
                {
                    j--;

                } while (datas[j].CompareTo(pivot) < 0);

                if (i >= j)
                    return j;

                //Drawing
                datas[i].dataState = Data.STATE.SELECTED;
                datas[j].dataState = Data.STATE.SELECTED;
                Thread.Sleep(TIME_SLEEP);
                Draw();
                datas[i].dataState = Data.STATE.NONE;
                datas[j].dataState = Data.STATE.NONE;
                //Drawing

                swap(i, j);
            }
        }
        private void QuickSort(int lo, int hi)
        {
            if (lo < hi)
            {
                int p = Partition(lo, hi);
                QuickSort(lo, p);
                QuickSort(p + 1, hi);
            }
        }

        private void Draw()
        {
            if (datas == null || datas.Length == 0) return;

            Graphics g = Graphics.FromImage(drawImage);
            g.Clear(Color.White);
            int line_size = ((picCanvas.Width - (datas.Length * trim_size)) / datas.Length);

            for (int i = 0; i < datas.Length; i++)
            {
                Brush brush = Brushes.Black;
                if (datas[i].dataState == Data.STATE.NONE)
                {
                    brush = Brushes.Black;
                }
                else if (datas[i].dataState == Data.STATE.SELECTED)
                {
                    brush = Brushes.GreenYellow;
                }
                g.FillRectangle(brush, i * (trim_size + line_size), datas[i].Value, line_size, picCanvas.Height - datas[i].Value);
            }

            picCanvas.CreateGraphics().DrawImageUnscaled(drawImage, 0, 0);
        }


        /*
         * 
         * HeapSort
         * 
         * 
         * */
        void HeapSort(int count)
        {
            Heapify(count);

            int end = count - 1;
            while(end > 0)
            {
                swap(end, 0);
                end--;
                SiftDown(0, end);
            }
        }

        void Heapify(int count)
        {
            int start = (count - 1) / 2;

            while(start >= 0)
            {
                SiftDown(start, count - 1);
                start--;
            }
        }
        void SiftDown(int start, int end)
        {
            /**
             *         0              left = (current * 2) +1
             *      1    2            right = (current * 2) + 2
             *     3 4  5 6
             * 
             * */
            int root = start;
            int leftChild = (root * 2) + 1;

            while (leftChild <= end)
            {
                int curr = root;

                if (datas[curr].CompareTo(datas[leftChild]) > 0)
                    curr = leftChild;
                if (leftChild + 1 <= end && datas[curr].CompareTo(datas[leftChild + 1]) > 0)
                    curr = leftChild + 1;
                if (curr == root)
                    return;
                else
                {

                    //Drawing
                    datas[curr].dataState = Data.STATE.SELECTED;
                    datas[root].dataState = Data.STATE.SELECTED;
                    Thread.Sleep(TIME_SLEEP);
                    Draw();
                    datas[root].dataState = Data.STATE.NONE;
                    datas[curr].dataState = Data.STATE.NONE;
                    //Drawing

                    swap(root, curr);
                    root = curr;
                }
                leftChild = (root * 2) + 1;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Text = "HeapSort";
            datas = new Data[picCanvas.Width / BOX_SIZE];

            //Generate Random datas
            for (int i = 0; i < datas.Length; i++)
            {
                datas[i] = new Data(picCanvas.Height);
            }

            Draw();

            HeapSort(datas.Length);

            Draw();


            MessageBox.Show("Done!");
        }

        private void picCanvas_Paint(object sender, PaintEventArgs e)
        {
            Draw();
        }
    }
}
