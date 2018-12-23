using System;
using System.Drawing;
using System.Drawing.Imaging;
namespace Barrage1
{
    unsafe public class Barrage
    {
        int width, height;
        int OffsetX, OffsetY;
        int Penstyle;
        int percent;
        int r0, g0, b0;
        int r, g, b;
        int newxy;
        int tempxy;
        byte* checkstart; byte* checkendd; Bitmap originalBitmap;
        byte* poriginbitmap;
        Boolean smear = false;

        public Bitmap MyNewBmp0;
        public BitmapData MyBmpData0;

        int penW, penH;
        int[] penWH = new int[1];

        double coordinate = 0;
        /*///////////////////////////////////*/
        /* int tra1, tra2, tra3, tra4, tra5, tra6;
         int bitmapwidth, bitmapheight;
         int tempxy = 0;*/
        Boolean[] enemybool = new Boolean[1000];
        double enemynumber = 0;
        double[] enemyX0 = new double[1000];
        double[] enemyY0 = new double[1000];
        double[] enemydistance = new double[1000];
        double[] enemycoordinate = new double[1000];

        public void Penset(int x, int y, int penstyle, int p, int cor, int cog, int cob, Bitmap nowbitmap, Boolean bs)
        {
            OffsetX = x;
            OffsetY = y;
            Penstyle = penstyle;
            percent = p;
            r = r0 = cor;
            g = g0 = cog;
            b = b0 = cob;
            smear = bs;
            if (smear == false)
                return;
            originalBitmap = new Bitmap(nowbitmap);
            originBitmap();
        }
        private void count(byte* num)
        {
            if (num < checkstart || num > checkendd)
                return;
            *num = (byte)(0);
            *(num + 1) = (byte)(0);
            *(num + 2) = (byte)(0);
            *(num + 3) = (byte)(150);
            //return;
            /* if (smear)
             {
                 *num = (byte)((b) * (percent) / 100 + (*(poriginbitmap + newxy)) * (100 - percent) / 100);
                 *(num + 1) = (byte)((g) * (percent) / 100 + (*(poriginbitmap + newxy + 1)) * (100 - percent) / 100);
                 *(num + 2) = (byte)((r) * (percent) / 100 + (*(poriginbitmap + newxy + 2)) * (100 - percent) / 100);
             }
             else
             {
                 *num = (byte)((b) * (percent) / 100 + (*((num)) * (100 - percent) / 100));
                 *(num + 1) = (byte)((g) * (percent) / 100 + (*((num + 1)) * (100 - percent) / 100));
                 *(num + 2) = (byte)((r) * (percent) / 100 + (*((num + 1)) * (100 - percent) / 100));
             }*/
        }
        private void point(byte* num)
        {
            int i;
            for (i = 0; i < penWH.Length; i++)
                count((num) + penWH[i]);
            // return;
            /* for (n1 = 0; n1 < 100; n1++)
             {
                 for (n2 = 0; n2 < 100; n2++)
                 {
                     nn = (int)Math.Sqrt((50 - n2) * (50 - n2) + (50 - n1) * (50 - n1));
                     if (nn == 55)
                     {
                         newxy = (n1 * width + n2);
                         newxy <<= 2;//X4
                         newxy += xyn;
                         count((num) + newxy );
                     }
                 }
             }*/
            ///////////////////////////////////

        }
        public void Startdraw01(Bitmap MyNewBmp, int startX1, int startY1, int endX1, int endY1)
        {
            Rectangle MyRec = new Rectangle(0, 0, MyNewBmp.Width, MyNewBmp.Height);
            BitmapData MyBmpData = MyNewBmp.LockBits(MyRec, ImageLockMode.ReadWrite, MyNewBmp.PixelFormat);
            byte* MyNewColor = (byte*)MyBmpData.Scan0;
            checkstart = MyNewColor;
            checkendd = checkstart + MyBmpData.Stride * MyNewBmp.Height;
            height = MyNewBmp.Height;
            width = MyNewBmp.Width;


            int xy;
            xy = (((startY1 + 0) * MyNewBmp.Width + (startX1 + 0)) + OffsetY * width + OffsetX) * 4;
            point((MyNewColor + xy));
            MyNewBmp.UnlockBits(MyBmpData);
        }

        private void originBitmap()
        {
            MyNewBmp0 = originalBitmap;
            Rectangle MyRec0 = new Rectangle(0, 0, MyNewBmp0.Width, MyNewBmp0.Height);
            MyBmpData0 = MyNewBmp0.LockBits(MyRec0, ImageLockMode.ReadWrite, MyNewBmp0.PixelFormat);
            poriginbitmap = (byte*)MyBmpData0.Scan0;
        }
        public void test02(Bitmap MyNewBmp, int startX1, int startY1, int endX1, int endY1)
        {


        }

        /*/////////////////////////////////////////////////////*/
        public int test01(Bitmap MyNewBmp, int time, int tra1, int tra2, int tra3, int tra4, int tra5, int tra6)
        {
            Rectangle MyRec = new Rectangle(0, 0, MyNewBmp.Width, MyNewBmp.Height);
            BitmapData MyBmpData = MyNewBmp.LockBits(MyRec, ImageLockMode.ReadWrite, MyNewBmp.PixelFormat);
            byte* MyNewColor = (byte*)MyBmpData.Scan0;
            checkstart = MyNewColor;
            int MyByteCount = MyBmpData.Stride * MyNewBmp.Height;
            checkendd = checkstart + MyByteCount;
            height = MyNewBmp.Height;
            width = MyNewBmp.Width;
            ///////////////////
            for (int i = 0; i < MyByteCount; i++)
                *(MyNewColor+i) = 255;


            // Graphics g = Graphics.FromImage(MyNewBmp);
            // g.Clear(Color.Black);

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

            //Pen myPen = new Pen(Color.FromArgb(255, 100, 0, 0), tra3);
            // Pen myPen2 = new Pen(Color.FromArgb(255, 0, 100, 0), tra3);
            // Pen mePen = new Pen(Color.FromArgb(255, 100, 100, 100), 50);
            //g.DrawEllipse(mePen, meX - 25, meY - 25, 50, 50);
            for (int i = 0; i <= 990; i++)
            {
                enemydistance[i] += tra2;



                double x = 0, y = enemydistance[i];
                double x2 = (x * Math.Cos(enemycoordinate[i]) - y * Math.Sin(enemycoordinate[i]));
                double y2 = (x * Math.Sin(enemycoordinate[i]) + y * Math.Cos(enemycoordinate[i]));
                x2 = enemyX0[i] - x2; y2 = enemyY0[i] - y2;
                if (x2 > 0 && x2 < width && y2 > 0 && y2 < height)
                {


                    if ((i % 2) == 0)
                    {// g.DrawEllipse(myPen, (float)x2, (float)y2, tra3, tra3);
                        point((MyNewColor + (int)(y2 * width + x2) * 4));
                    }
                    else
                    { //g.DrawEllipse(myPen2, (float)x2, (float)y2, tra3, tra3); 
                        point((MyNewColor + (int)(y2 * width + x2) * 4));
                    }

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
                    //  Array.Reverse(enemycoordinate);
                    //  enemydistance[i] = 0;
                    // enemycoordinate[i] = 0;
                    break;
                }
            }
            // g.Dispose();
            //  Bitmap b = new Bitmap(MyNewBmp);
            //MyNewBmp.UnlockBits(MyBmpData);
            MyNewBmp.UnlockBits(MyBmpData);
            return time;

        }





        public void penarray()
        {
            penWH = new int[1];
            int nn = 0;
            int xyn = newxy;
            int numcount = 0;
            int h, w;
            penH = 100;
            penW = 100;
            for (h = 0; h < penH; h++)
            {
                for (w = 0; w < penW; w++)
                {
                    nn = (int)Math.Sqrt((50 - w) * (50 - w) + (50 - h) * (50 - h));
                    if (nn == 55)
                        numcount++;
                }
            }
            if (numcount <= 0)
            {
                penWH = new int[1];
            }
            penWH = new int[numcount];
            numcount = 0;
            for (h = 0; h < penH; h++)
            {
                for (w = 0; w < penW; w++)
                {
                    nn = (int)Math.Sqrt((50 - w) * (50 - w) + (50 - h) * (50 - h));
                    if (nn == 55)
                    {
                        newxy = (h * 1000 + w);
                        newxy <<= 2;
                        penWH[numcount] = newxy;
                        numcount++;
                    }
                }
            }
        }
    }
}