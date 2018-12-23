using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using Barrage1;
namespace 彈幕模擬器test0001
{
    public partial class Form1 : Form
    {

        Boolean pencheck = false;//繪製事件判定
        Boolean smear = false;//是否開啟連續塗抹
        Barrage gs;//控制類別   
        int startX1, startY1, endX1, endY1, startX0, startY0;//游標座標
                                                             // int bitmapwidth = 1800, bitmapheight = 1400;//圖片大小
        int penstyle = 1;//樣式
        int OffsetX = 0, OffsetY = 0;//偏移值  
        int percent = 50;//筆刷濃度
        int colorR = 50, colorG = 80, colorB = 60;//顏色值

 
        int tra1, tra2, tra3, tra4, tra5, tra6;
        int bitmapwidth, bitmapheight;
        //int tempxy = 0;
        Image image0;
        Bitmap nowimage;
        Bitmap tempbitmap;
        int time = 0;
        Boolean[] enemybool = new Boolean[1000];
        double[] enemyX0 = new double[1000];
        double[] enemyY0 = new double[1000];
        double[] enemydistance = new double[1000];
        double[] enemycoordinate = new double[1000];
        double coordinate = 0;

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            tra1 = trackBar1.Value;//角度
        }
        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            tra2 = trackBar2.Value;//速度
        }
        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            tra4 = trackBar4.Value;//X
        }
        private void trackBar5_Scroll(object sender, EventArgs e)
        {
            tra5 = trackBar5.Value;//Y
        }
        private void trackBar6_Scroll(object sender, EventArgs e)
        {
            tra6 = trackBar6.Value;//間隔
        }
        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            tra3 = trackBar3.Value;//大小
        }
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            gs = new Barrage();
            tra1 = trackBar1.Value;
            tra2 = trackBar2.Value;
            tra3 = trackBar3.Value;
            tra4 = trackBar4.Value;
            tra5 = trackBar5.Value;
            tra6 = trackBar6.Value;
            bitmapheight = 900;
            bitmapwidth = 1000;
            this.WindowState = FormWindowState.Maximized;
            Bitmap MyNewBmp = new Bitmap(bitmapwidth, bitmapheight);
            Rectangle MyRec = new Rectangle(0, 0, MyNewBmp.Width, MyNewBmp.Height);
            BitmapData MyBmpData = MyNewBmp.LockBits(MyRec, ImageLockMode.ReadWrite, MyNewBmp.PixelFormat);
            IntPtr MyPtr = MyBmpData.Scan0;
            int MyByteCount = MyBmpData.Stride * MyNewBmp.Height;
            byte[] MyNewColor = new byte[MyByteCount];
            Marshal.Copy(MyPtr, MyNewColor, 0, MyByteCount);
            for (int n = 0; n < MyByteCount; n++)
            {
                MyNewColor[n] = 255;
            }
            Marshal.Copy(MyNewColor, 0, MyPtr, MyByteCount);
            MyNewBmp.UnlockBits(MyBmpData);
            pictureBox1.Image = MyNewBmp;
            image0 = MyNewBmp;
            pictureBox1.Image = MyNewBmp;
            nowimage = MyNewBmp ;
            tempbitmap = new Bitmap(nowimage, nowimage.Width, nowimage.Height);
            gs.penarray();
            timer1.Enabled = true;
        }



        unsafe private void timer1_Tick(object sender, EventArgs e)
        {
            tempbitmap = nowimage;
            time += 1;
            timer1.Enabled = false;
            time=gs.test01(tempbitmap, time, tra1, tra2, tra3, tra4, tra5, tra6);
            //if (time % 2 == 0) 
            pictureBox1.Image = tempbitmap;
            timer1.Enabled = true;
            return;
           /* time += 1;
            timer1.Enabled = false;


            Graphics g = Graphics.FromImage(tempbitmap);
            g.Clear(Color.Black);

            coordinate += tra1;
            if (time >= tra6)
            {
                enemynumber += 1;
                tempxy += 1;

                time = 0;

                if (enemynumber >= 990)
                    enemynumber = 990;
                if (coordinate >= 360)
                { //coordinate -= 360; 
                }
                if (tempxy >= 990)
                    tempxy = 0;
                enemyX0[tempxy] = tra4;
                enemyY0[tempxy] = tra5;
                enemybool[tempxy] = true;
                enemydistance[tempxy] = 0;
                enemycoordinate[tempxy] = 0;
                enemycoordinate[tempxy] += coordinate;
            }

            Pen myPen = new Pen(Color.FromArgb(255, 100, 0, 0), tra3);
            Pen myPen2 = new Pen(Color.FromArgb(255, 0, 100, 0), tra3);
            Pen mePen = new Pen(Color.FromArgb(255, 100, 100, 100), 50);
            g.DrawEllipse(mePen, meX - 25, meY - 25, 50, 50);
            for (int i = 0; i <= 990; i++)
            {
                enemydistance[i] += tra2;



                double x = 0, y = enemydistance[i];
                double x2 = (x * Math.Cos(enemycoordinate[i]) - y * Math.Sin(enemycoordinate[i]));
                double y2 = (x * Math.Sin(enemycoordinate[i]) + y * Math.Cos(enemycoordinate[i]));
                x2 = enemyX0[i] - x2; y2 = enemyY0[i] - y2;
                if (x2 > 0 && x2 < bitmapwidth && y2 > 0 && y2 < bitmapheight)
                {


                    if ((i % 2) == 0)
                    { g.DrawEllipse(myPen, (float)x2, (float)y2, tra3, tra3); }
                    else
                    { g.DrawEllipse(myPen2, (float)x2, (float)y2, tra3, tra3); }

                }


                if (tempxy >= 990)
                {
                    for (int i2 = 0; i2 <= 989; i2++)
                    {
                        enemydistance[i2] = enemydistance[i2 + 1];
                        enemycoordinate[i2] = enemycoordinate[i2 + 1];
                        enemyX0[i2] = enemyX0[i2 + 1];
                        enemyY0[i2] = enemyY0[i2 + 1];
                        enemydistance[990] = 0;
                        enemycoordinate[990] = 0;
                        enemyX0[990] = tra4;
                        enemyY0[990] = tra5;
                    }

                    break;
                }
            }

            g.Dispose();
            pictureBox1.Image = tempbitmap;
            timer1.Enabled = true;
            */

        }
        private void pictureBox1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            endX1 = e.X;
            endY1 = e.Y;
            if (pencheck == true)
            {
                pencheck = false;
                if (penstyle == 1)
                    ;//  gs.Startdraw01(tempbitmap, startX1, startY1, endX1, endY1);
                pencheck = true;
                // pictureBox1.Image = tempbitmap;
            }
            startY1 = e.Y;
            startX1 = e.X;
        }

        private void pictureBox1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            pencheck = false;
            nowimage = tempbitmap;
            pictureBox1.Image = nowimage;
            if (smear == true)
                gs.MyNewBmp0.UnlockBits(gs.MyBmpData0);
        }

        private void pictureBox1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            startX0 = e.X;
            startY0 = e.Y;
            gs.Penset(OffsetX, OffsetY, penstyle, percent, colorR, colorG, colorB, (Bitmap)nowimage, smear);
            pencheck = true;
        }
    }
}
