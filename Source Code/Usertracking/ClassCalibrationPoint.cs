using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Kinect.Toolkit.FaceTracking;
using Microsoft.VisualBasic.PowerPacks;

namespace Usertracking
{
    public class CalibrationPoint
    {
        //private Form form;
        public int X;
        public int Y;
        public Label Label;
        public int StepNumber;
        public System.Drawing.Color Color;
        public ResultPoint[] ResultPoints;
        public int ResultPointsCount=0;

        public CalibrationPoint(Control parent, int x, int y, int step_number, System.Drawing.Color color){
            //form = parent;

            this.X = x;
            this.Y = y;
            this.Color = color;
            this.StepNumber = step_number;
            this.Label = new Label();
            this.Label.Top = y;
            this.Label.Left = x;
            this.Label.Text = step_number.ToString();
            this.Label.Visible = false;
            this.Label.ForeColor = color;
            this.Label.BackColor = Color.Transparent;
            this.Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 72F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label.Width = 98;
            this.Label.Height = 108;
            this.Label.Parent = parent;
            //parent.Controls.Add(this.Label);
        }

        public CalibrationPoint(ShapeContainer parent, int x, int y, int step_number, System.Drawing.Color color)
        {
            //form = parent;

            this.X = x;
            this.Y = y;
            this.Color = color;
            this.StepNumber = step_number;
            this.Label = new Label();
            this.Label.Top = y;
            this.Label.Left = x;
            this.Label.Text = step_number.ToString();
            this.Label.Visible = false;
            this.Label.ForeColor = color;
            this.Label.BackColor = Color.Transparent;
            this.Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 72F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label.Width = 98;
            this.Label.Height = 108;
            this.Label.Parent = parent;
        }

        public ResultPoint AddResultPoint(int x, int y){

           ResultPoint resultpoint = new ResultPoint(x, y);
           resultpoint.CalibrationPoint = this;

           if (ResultPoints == null)
               ResultPoints = new ResultPoint[10];
           else
           {
               if (ResultPointsCount==10 || ResultPointsCount==100 || ResultPointsCount==1000){
                    ResultPoint[] temp;
                    temp = new ResultPoint[ResultPointsCount*10];
                    for(int i=0;i<ResultPointsCount;i++)
                    {
                        temp.SetValue(ResultPoints[i], i);
                    }
                    ResultPoints = temp;
               }
           }

           ResultPoints.SetValue(resultpoint, ResultPointsCount);
           ResultPointsCount++;
           
           return resultpoint;
        }

        public ResultPoint AddResultPoint(int x, int y, Vector3DF faceposition, Vector3DF facerotation, float kinectelevationangle)
        {

           ResultPoint resultpoint = new ResultPoint(x, y, DateTime.Now, this, faceposition, facerotation, kinectelevationangle);

           if (ResultPoints == null)
               ResultPoints = new ResultPoint[10];
           else
           {
               if (ResultPointsCount==10 || ResultPointsCount==100 || ResultPointsCount==1000){
                    ResultPoint[] temp;
                    temp = new ResultPoint[ResultPointsCount*10];
                    for(int i=0;i<ResultPointsCount;i++)
                    {
                        temp.SetValue(ResultPoints[i], i);
                    }
                    ResultPoints = temp;
               }
           }

           ResultPoints.SetValue(resultpoint, ResultPointsCount);
           ResultPointsCount++;
           
           return resultpoint;
        }
        

        ~CalibrationPoint(){
            this.Label.Dispose();

        }

    }
}
