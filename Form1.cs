using System;
using System.Drawing;
using System.Windows.Forms;
using System.Timers;
using System.Threading;
using System.Collections.Generic;
using System.Linq;

namespace TestMoving
{
   
    public partial class Form1 : Form
    {
       private int x ;
        public static bool loop = true;
        private int y;
        private bool val;
        Block[] b = new Block[7];
        int[][] changes = new int[100][];
        char[] block = new char[100]; 
        int c = 5;
        char[] moved=new char[100];
        int[][] stepsmoved = new int[100][];
        int steps;
        public Form1(int count, char[] m, int[][] st, Dictionary<char, int[]> init,int ste)
        {
            steps = ste;
            int i = 0;
            foreach (KeyValuePair<char, int[]> kvp in init)
            {
                int[] t = kvp.Value;
                b[i] = new Block(t[0], t[1]);
                block[i] = kvp.Key;
                    i++;
            }
            moved = m;
            stepsmoved = st;
                InitializeComponent();
            c = count;
            x = 50;
            y = 400;
            val = true;
            label1.Text = "Number of intermediate state Changes - " + steps;

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {

            DrawIt(c);

        }
        private void DrawIt(int count)
        {

            System.Drawing.Graphics graphics = this.CreateGraphics();
            if (count >= 1)
            {
                graphics.DrawRectangle(new Pen(Color.White, 5), b[0].x, b[0].y, 100, 50);
                graphics.FillRectangle(Brushes.Black, b[0].x, b[0].y, 100, 50);
                graphics.DrawString(""+block[0], new Font("Arial", 16), new SolidBrush(Color.White), b[0].x + 40, b[0].y + 17);
            }
            if (count >= 2) {
                graphics.DrawRectangle(new Pen(Color.White, 5), b[1].x, b[1].y, 100, 50);
                graphics.FillRectangle(Brushes.Black, b[1].x, b[1].y, 100, 50);
            graphics.DrawString("" + block[1], new Font("Arial", 16), new SolidBrush(Color.White), b[1].x + 40, b[1].y + 17);
            }
            if (count >= 3) {
                graphics.DrawRectangle(new Pen(Color.White, 5), b[2].x, b[2].y, 100, 50);
                graphics.FillRectangle(Brushes.Black, b[2].x, b[2].y, 100, 50);
            graphics.DrawString("" + block[2], new Font("Arial", 16), new SolidBrush(Color.White), b[2].x + 40, b[2].y + 17);
            }
            if (count >= 4) {
                graphics.DrawRectangle(new Pen(Color.White, 5), b[3].x, b[3].y, 100, 50);
                graphics.FillRectangle(Brushes.Black, b[3].x, b[3].y, 100, 50);
            graphics.DrawString("" + block[3], new Font("Arial", 16), new SolidBrush(Color.White), b[3].x + 40, b[3].y + 17);
            }
            if (count >= 5) {
                graphics.DrawRectangle(new Pen(Color.White, 5), b[4].x, b[4].y, 100, 50);
                graphics.FillRectangle(Brushes.Black, b[4].x, b[4].y, 100, 50);
            graphics.DrawString("" + block[4], new Font("Arial", 16), new SolidBrush(Color.White), b[4].x + 40, b[4].y + 17);
            }
            if (count >= 6) {
                graphics.DrawRectangle(new Pen(Color.White, 5), b[5].x, b[5].y, 100, 50);
                graphics.FillRectangle(Brushes.Black, b[5].x, b[5].y, 100, 50);
            graphics.DrawString("" + block[5], new Font("Arial", 16), new SolidBrush(Color.White), b[5].x + 40, b[5].y + 17);
            }
            if (count == 7) {
                graphics.DrawRectangle(new Pen(Color.White, 5), b[6].x, b[6].y, 100, 50);
                graphics.FillRectangle(Brushes.Black, b[6].x, b[6].y, 100, 50);
            graphics.DrawString("" + block[6], new Font("Arial", 16), new SolidBrush(Color.White), b[6].x+40, b[6].y+17);
            }

            StartThread();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            
            Invalidate();
        }
        int test(int v)
        {
            return v + 50;


        }
        public void CallToChildThread()
        {
            if(loop == true)
            { 

            // the thread is paused for 5000 milliseconds

            int sleepfor = 1000;
            for (int i = 0; i < steps; i++)
            {
                Thread.Sleep(sleepfor);
                int index = getIndex(moved[i]);
                b[index].Reinistiate(b[index].posx, 6);
                b[index].Reinistiate(stepsmoved[i][0], 6);
                b[index].Reinistiate(stepsmoved[i][0], stepsmoved[i][1]);
                Thread.Sleep(sleepfor);
            }
                loop = false;
            }


        }
        public int getIndex(char ch)
        {
            for(int i=0;i<c;i++)
            {
                if(ch==block[i])
                {
                    return i;
                }
            }
            return -1;
        }
       
        public void StartThread()
        {
            ThreadStart childref = new ThreadStart(CallToChildThread);
            Thread childThread = new Thread(childref);
            childThread.Start();
        }
    }
}
