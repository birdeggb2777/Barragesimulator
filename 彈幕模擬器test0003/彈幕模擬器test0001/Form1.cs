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
        Barrage gs;//控制類別   
        int tra1, tra2, tra3, tra4, tra5, tra6;
        int bitmapwidth, bitmapheight;
        Image image0;
        Bitmap nowimage;
        Bitmap tempbitmap;
        int time = 0;
        int time2;
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
            bitmapwidth = 700;
            this.WindowState = FormWindowState.Maximized;
            Bitmap MyNewBmp = new Bitmap(bitmapwidth, bitmapheight);
            Rectangle MyRec = new Rectangle(0, 0, MyNewBmp.Width, MyNewBmp.Height);
            BitmapData MyBmpData = MyNewBmp.LockBits(MyRec, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            IntPtr MyPtr = MyBmpData.Scan0;
            int MyByteCount = MyBmpData.Stride * MyNewBmp.Height;
            byte[] MyNewColor = new byte[MyByteCount];
            Marshal.Copy(MyPtr, MyNewColor, 0, MyByteCount);
            for (int n = 0; n < MyByteCount; n++)
                MyNewColor[n] = 255;
            Marshal.Copy(MyNewColor, 0, MyPtr, MyByteCount);
            MyNewBmp.UnlockBits(MyBmpData);
            pictureBox1.Image = MyNewBmp;
            image0 = MyNewBmp;
            pictureBox1.Image = MyNewBmp;
            nowimage = MyNewBmp ;        
            tempbitmap = new Bitmap(nowimage, nowimage.Width, nowimage.Height);
            gs.Barragestart(bitmapwidth);
            gs.penarray();
            timer1.Enabled = true;
        }



        unsafe private void timer1_Tick(object sender, EventArgs e)
        {
            tempbitmap = nowimage;
            time ++;
           // time2++;
            timer1.Enabled = false;
            gs.test01(tempbitmap, time, tra1, tra2, tra3, tra4, tra5, tra6);
            pictureBox1.Image = tempbitmap;
            time = 0;
           /* if (time2 < 10)
                gs.test01(tempbitmap, time, tra1, tra2, tra3, tra4, tra5, tra6);
            else
            {
                gs.test02( time, tra1, tra2, tra3, tra4, tra5, tra6);
                pictureBox1.Image = tempbitmap;
                time2 = 0;
            }*/
            timer1.Enabled = true;      
        }
        private void pictureBox1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
           /* endX1 = e.X;
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
            startX1 = e.X;*/
        }

        private void pictureBox1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
          /*  pencheck = false;
            nowimage = tempbitmap;
            pictureBox1.Image = nowimage;
            if (smear == true)
                gs.MyNewBmp0.UnlockBits(gs.MyBmpData0);*/
        }

        private void pictureBox1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
         /*   startX0 = e.X;
            startY0 = e.Y;
           // gs.Penset(OffsetX, OffsetY, penstyle, percent, colorR, colorG, colorB, (Bitmap)nowimage, smear);
            pencheck = true;*/
        }
    }
}
