using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Barrage1
{
    unsafe public class Barrage
    {
        public int newtra3 = 80;
        public Bitmap MyNewBmp0;
        public BitmapData MyBmpData0;
        byte r, g, b;
        int tempxy;
        int width, height;
        int CircleLenge = 15;
        byte* checkstart;
        byte* checkendd;
        int penW, penH;
        int[] penWH = new int[1];
        int[] penA1 = new int[1];
        int[] penA2 = new int[1];
        int[] penA3 = new int[1];
        double coordinate = 0;
        Boolean[] enemybool = new Boolean[1000];
        double enemynumber = 0;
        double[] enemyX0 = new double[1000];
        double[] enemyY0 = new double[1000];
        double[] enemydistance = new double[1000];
        double[] enemycoordinate = new double[1000];
        public void Barragestart(int w)
        {
            width = w;
        }
        private void countA2(byte* num)
        {
            if (num < checkstart || num > checkendd)
                return;
            *num = 255;
            *(num + 1) = 255;
            *(num + 2) = 255;
        }
        private void countA3(byte* num)
        {
            if (num < checkstart || num > checkendd)
                return;
            *num =200;
            *(num + 1) = 200;
            *(num + 2) = 200;
        }
        private void countA1(byte* num)
        {
            if (num < checkstart || num > checkendd)
                return;
            *num = 127;
            *(num + 1) = 127;
            *(num + 2) = 127;
        }
        private void count(byte* num)
        {
            if (num < checkstart || num > checkendd)
                return;/*
            *num = (byte)((*num)/2+b/2);
            *(num + 1) = (byte)((*num+1) / 2 + g / 2);
            *(num + 2) = (byte)((*num+2) / 2 + r / 2);
            */
            *num = b;
            *(num + 1) = g;
            *(num + 2) = r;
        }
        private void point(byte* num)
        {
            int i;
            for (i = 0; i < penWH.Length; i++)
                count((num) + penWH[i]);
            for (i = 0; i < penA1.Length; i++)
               countA1((num) + penA1[i]);
            for (i = 0; i < penA2.Length; i++)
                countA2((num) + penA2[i]);
            for (i = 0; i < penA3.Length; i++)
                countA3((num) + penA3[i]);
        }



        public void test01(Bitmap MyNewBmp, int time, int tra1, int tra2, int tra3, int tra4, int tra5, int tra6)
        {
            Rectangle MyRec = new Rectangle(0, 0, MyNewBmp.Width, MyNewBmp.Height);
            BitmapData MyBmpData = MyNewBmp.LockBits(MyRec, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            byte* MyNewColor = (byte*)MyBmpData.Scan0;
            checkstart = MyNewColor;
            int MyByteCount = MyBmpData.Stride * MyNewBmp.Height;
            checkendd = checkstart + MyByteCount;
            height = MyNewBmp.Height;
            width = MyNewBmp.Width;
            for (int i = 0; i < MyByteCount; i += 3)
                *(MyNewColor + i) = *(MyNewColor + i + 1) = *(MyNewColor + i + 2) = 0;
            coordinate += tra1;
            if (time >= tra6)
            {
                enemynumber += 1;
                tempxy += 1;
                time = 0;
                if (enemynumber >= 990)
                    enemynumber = 990;
                if (tempxy >= 990)
                    tempxy = 0;
                enemyX0[tempxy] = tra4;
                enemyY0[tempxy] = tra5;
                enemybool[tempxy] = true;
                enemydistance[tempxy] = 0;
                enemycoordinate[tempxy] = 0;
                enemycoordinate[tempxy] += coordinate;
            }
            for (int i = 0; i <= 990; i++)
            {
                enemydistance[i] += tra2;
                double x = 0, y = enemydistance[i];
                double x2 = (x * Math.Cos(enemycoordinate[i]) - y * Math.Sin(enemycoordinate[i]));
                double y2 = (x * Math.Sin(enemycoordinate[i]) + y * Math.Cos(enemycoordinate[i]));
                x2 = enemyX0[i] - x2;
                y2 = enemyY0[i] - y2;
                if (x2 > 0 && x2 < width && y2 > 0 && y2 < height)
                {
                    if ((i % 2) == 0)
                    {
                        r = 255; g = 0; b = 0;
                        point((MyNewColor + ((int)y2 * width + (int)x2) * 3));
                    }
                    else
                    {
                        r = 0; g = 255; b = 0;
                        point((MyNewColor + ((int)y2 * width + (int)x2) * 3));
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
                    break;
                }
            }
            MyNewBmp.UnlockBits(MyBmpData);
        }
        public void test02(int time, int tra1, int tra2, int tra3, int tra4, int tra5, int tra6)
        {           
            coordinate += tra1;
            if (time >= tra6)
            {
                enemynumber += 1;
                tempxy += 1;
                time = 0;
                if (enemynumber >= 990)
                    enemynumber = 990;
                if (tempxy >= 990)
                    tempxy = 0;
                enemyX0[tempxy] = tra4;
                enemyY0[tempxy] = tra5;
                enemybool[tempxy] = true;
                enemydistance[tempxy] = 0;
                enemycoordinate[tempxy] = 0;
                enemycoordinate[tempxy] += coordinate;
            }
            for (int i = 0; i <= 990; i++)
            {
                enemydistance[i] += tra2;
                double x = 0, y = enemydistance[i];
                double x2 = (x * Math.Cos(enemycoordinate[i]) - y * Math.Sin(enemycoordinate[i]));
                double y2 = (x * Math.Sin(enemycoordinate[i]) + y * Math.Cos(enemycoordinate[i]));
                x2 = enemyX0[i] - x2;
                y2 = enemyY0[i] - y2;
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
        }
        public void penarray()
        {
            penWH = new int[1];
            penA1 = new int[1];
            penA2 = new int[1];
            penA3 = new int[1];
            int nn = 0;
            int numcount = 0;
            int numcountA1 = 0;
            int numcountA2 = 0;
            int numcountA3 = 0;
            int h, w;
            penH = newtra3;
            penW = newtra3;
            int tra35 = 50;
            for (h = 0; h < penH; h++)
            {
                for (w = 0; w < penW; w++)
                {
                    nn = (int)Math.Sqrt((tra35 - w) * (tra35 - w) + (tra35 - h) * (tra35 - h));
                    if (nn <= CircleLenge)
                        numcount++;
                    else if (nn <= CircleLenge + 1)
                        numcountA1++;
                    else if (nn <= CircleLenge + 2)
                        numcountA2++;
                    else if (nn <= CircleLenge + 3)
                        numcountA3++;
                }
            }
            if (numcount <= 0)
                penWH = new int[1];
            if (numcountA1 <= 0)
                penA1 = new int[1];
            if (numcountA2 <= 0)
                penA2 = new int[1];
            if (numcountA3 <= 0)
                penA3 = new int[1];
            penWH = new int[numcount];
            penA1 = new int[numcountA1];
            penA2 = new int[numcountA2];
            penA3 = new int[numcountA3];
            numcount = 0;
            numcountA1 = 0;
            numcountA2 = 0;
            numcountA3 = 0;
            for (h = 0; h < penH; h++)
            {
                for (w = 0; w < penW; w++)
                {
                    nn = (int)Math.Sqrt((tra35 - w) * (tra35 - w) + (tra35 - h) * (tra35 - h));
                    if (nn <= CircleLenge)
                    {
                        penWH[numcount] = (h * width + w) * 3;
                        numcount++;
                    }
                    else if (nn <= CircleLenge + 1)
                    {
                        penA1[numcountA1] = (h * width + w) * 3;
                        numcountA1++;
                    }
                    else if (nn <= CircleLenge + 2)
                    {
                        penA2[numcountA2] = (h * width + w) * 3;
                        numcountA2++;
                    }
                    else if (nn <= CircleLenge + 3)
                    {
                        penA3[numcountA3] = (h * width + w) * 3;
                        numcountA3++;
                    }
                }
            }
        }
    }
}
