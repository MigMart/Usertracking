using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Usertracking
{
    class CalibrationError
    {
        private static int wx, wy;
        private static int[,] errorx;
        private static int[,] errory;
        private static int min_cx, max_cx;
        private static int min_cy, max_cy;

        /* http://stackoverflow.com/questions/6539571/how-to-resize-multidimensional-2d-array-in-c */

        public static void ResizeArray<T>(ref T[,] array, int padLeft, int padRight, int padTop, int padBottom)
        {
            int ow = array.GetLength(0);
            int oh = array.GetLength(1);
            int nw = ow + padLeft + padRight;
            int nh = oh + padTop + padBottom;

            int x0 = padLeft;
            int y0 = padTop;
            int x1 = x0 + ow - 1;
            int y1 = y0 + oh - 1;
            int u0 = -x0;
            int v0 = -y0;

            if (x0 < 0) x0 = 0;
            if (y0 < 0) y0 = 0;
            if (x1 >= nw) x1 = nw - 1;
            if (y1 >= nh) y1 = nh - 1;

            T[,] nArr = new T[nw, nh];
            for (int y = y0; y <= y1; y++)
            {
                for (int x = x0; x <= x1; x++)
                {
                    nArr[x, y] = array[u0 + x, v0 + y];
                }
            }
            array = nArr;
        }

        protected T[,] ResizeArray<T>(T[,] original, int x, int y)
        {
            T[,] newArray = new T[x, y];
            int minX = Math.Min(original.GetLength(0), newArray.GetLength(0));
            int minY = Math.Min(original.GetLength(1), newArray.GetLength(1));

            for (int i = 0; i < minY; ++i)
                Array.Copy(original, i * original.GetLength(0), newArray, i * newArray.GetLength(0), minX);

            return newArray;
        }

        static public void Initialize()
        {
            System.Drawing.Rectangle b=System.Windows.Forms.Screen.PrimaryScreen.Bounds;
            wx = b.Width;
            wy= b.Height;

            min_cx = 0; 
            max_cx= wx - 1;
            
            min_cy = 0;
            max_cy = wy - 1;

            errorx = new int[wx, wy];
            errory = new int[wx, wy];
        }
        static public void SetPoint(int wanted_x, int wanted_y, int got_x, int got_y)
        {
            int ex, ey;
            ex = wanted_x - got_x;
            ey = wanted_y - got_y;

            int padleft=0, padright=0, padtop=0, padbottom=0;

            if (min_cx > got_x)
            {
                padleft = min_cx - got_x;
                min_cx = got_x;
            }

            if (max_cx < got_x)
            {
                padright = got_x - max_cx;
                max_cx = got_x;
            }
            if (min_cy > got_y)
            {
                padtop = min_cy - got_y;
                min_cy = got_y;
            }
            if (max_cy < got_y)
            {
                padbottom = got_y - max_cy;
                max_cy = got_y;
            }

            if (padleft > 0 || padright > 0 || padbottom > 0 || padtop > 0)
            {
                
                ResizeArray(ref errorx, padleft, padright, padtop, padbottom);
                ResizeArray(ref errory, padleft, padright, padtop, padbottom);
            }

            int ix = (int) (max_cx - min_cx) / (wx-1) * got_x;
            int iy = (int) (max_cy - min_cy) / (wy-1) * got_y;

            if (errorx[ix, iy] != 0)
                errorx[ix, iy] = (int)(ex * 0.5 + errorx[ix, iy] * 0.5);
            else
                errorx[ix, iy] = ex;

            if (errory[ix, iy] != 0)
                errory[ix, iy] = (int)(ey * 0.5 + errory[ix, iy] * 0.5);
            else
                errory[ix, iy] = ey;

            Console.WriteLine("Wanted: {0}, {1} Got: {2}, {3} Error: {4}, {5}", wanted_x, wanted_y, got_x, got_y, errorx[ix, iy], errory[ix, iy]);
        }

        static public void RecalcAllPoints()
        {
            int j, i1, i2, ey1, ey2, ey_inc;
            int i, j1, j2, ex1, ex2, ex_inc;

            for (i = 0; i < wx; i++)
            {
                j1=0;

                while (j1 < wy)
                {
                    while (j1 < wy)
                    {
                        if (errory[i, j1] != 0) break;

                        j1++;
                    }

                    if (j1 < wy)
                    {
                        ey1 = errory[i, j1];
                        j2 = j1 + 1;
                        while (j2 < wy)
                        {
                            if (errory[i, j2] != 0) break;

                            j2++;
                        }

                        if (j2 >= wy)
                            j2 = wy - 1;
                        
                        ey2 = errory[i, j2];

                        if (j2 != j1)
                            ey_inc = (ey2 - ey1) / (j2 - j1);
                        else
                            ey_inc = (ey2 - ey1);

                        while (j1 <= j2)
                        {

                            if (j1 <= 0)
                                j1++;

                            errory[i, j1] = errory[i, j1-1]  + ey_inc;
                            j1++;
                        }
                        
                    }
                }
            }


            for (j = 0; j < wy; j++)
            {
                i1 = 0;

                while (i1 < wx)
                {
                    while (i1 < wx)
                    {
                        if (errorx[i1, j] != 0) break;
                        i1++;
                    }

                    if (i1 < wx)
                    {
                        ex1 = errorx[i1, j];
                        i2 = i1 + 1;
                        while (i2 < wy)
                        {
                            if (errorx[i2, j] != 0) break;
                            i2++;
                        }
                        if (i2 >= wx)
                            i2 = wx - 1;
                        
                        ex2 = errorx[i2, j];

                        if (i2 != i1)
                            ex_inc = (ex2 - ex1) / (i2 - i1);
                        else
                            ex_inc = (ex2 - ex1);

                        while (i1 <= i2)
                        {

                            if (i1 <= 0)
                                i1++;

                            errorx[i1, j] = errorx[i1 - 1, j] + ex_inc;

                            i1++;
                        }
                        
                    }
                }
            }
        }

        static public int getErrorX(int got_x, int got_y)
        {
            if ((min_cx <= got_x) && (got_x <= max_cx) && (min_cy <= got_y) && (got_y <= max_cy))
            {
                int ix = (int)(max_cx - min_cx) / (wx - 1) * got_x;
                int iy = (int)(max_cy - min_cy) / (wy - 1) * got_y;

                int err=0;
                    
                try{
                    err=errorx[ix, iy];
                }catch(Exception e){

                }
                return err;
            }
            else
            {
                return 0;
            }

        }

        static public int getErrorY(int got_x, int got_y)
        {
            if ((min_cx <= got_x) && (got_x <= max_cx) && (min_cy <= got_y) && (got_y <= max_cy))
            {
                int ix = (int)(max_cx - min_cx) / (wx - 1) * got_x;
                int iy = (int)(max_cy - min_cy) / (wy - 1) * got_y;



                int err = 0;

                try
                {
                    err = errory[ix, iy];
                }
                catch (Exception e)
                {

                }
                return err;
            }
            else
            {
                return 0;
            }

        }
    }
}
