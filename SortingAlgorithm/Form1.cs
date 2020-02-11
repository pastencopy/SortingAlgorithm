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

        const int BOX_SIZE = 5;
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
            quicksort(0, datas.Length - 1 );

            MessageBox.Show("Done!");
        }

        private void swap(int a, int b)
        {
            Data tmp = datas[b];
            datas[b] = datas[a];
            datas[a] = tmp;
        }

        private int partition(int lo, int hi)
        {
            Data pivot = datas[(lo + hi) / 2];

            int i = lo - 1;
            int j = hi + 1;

         

            
            while (true)
            {
                do
                {
                    i++;

                    datas[i].dataState = Data.STATE.SELECTED;
                    Thread.Sleep(TIME_SLEEP);
                    Draw();

                } while (datas[i].CompareTo(pivot) > 0);
                
                do
                {
                    j--;

                    datas[j].dataState = Data.STATE.SELECTED;
                    Thread.Sleep(TIME_SLEEP);
                    Draw();
                } while (datas[j].CompareTo(pivot) < 0);
                if (i >= j)
                    return j;
                swap(i, j);
            }
        }


        private void quicksort(int lo, int hi)
        {
            if (lo < hi)
            {
                int p = partition(lo, hi);
                quicksort(lo, p);
                quicksort(p + 1, hi);
            }
        }

        private void Draw()
        {
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

    }
}
