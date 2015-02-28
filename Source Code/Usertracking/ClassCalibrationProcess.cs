using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualBasic.PowerPacks;

namespace Usertracking
{
    public class CalibrationProcess
    {
        public CalibrationPoint[] CalibrationPoints;
        public int CalibrationPointsCount = 0;
        public Control Parent;

        public CalibrationProcess(Form parent)
        {
            this.CalibrationPoints = new CalibrationPoint[10];
            this.Parent = parent;
        }

        public CalibrationProcess(ShapeContainer parent)
        {
            this.CalibrationPoints = new CalibrationPoint[10];
            this.Parent = parent;
        }

        ~CalibrationProcess()
        {
        }

        public CalibrationPoint AddNewCalibrationPoint(int x, int y, int step_number, Color color)
        {

            CalibrationPoint calibrationPoint = new CalibrationPoint(this.Parent, x, y, step_number, color);


            if (CalibrationPoints == null)
            {
                CalibrationPoints = new CalibrationPoint[10];
            }
            else
            {
                if (CalibrationPointsCount == 10 || CalibrationPointsCount == 100 || CalibrationPointsCount == 1000)
                {
                    CalibrationPoint[] temp = new CalibrationPoint[CalibrationPointsCount * 10];
                    
                    for (int i = 0; i < CalibrationPointsCount; i++)
                    {
                        temp.SetValue(CalibrationPoints[i], i);
                    }
                    CalibrationPoints = temp;

                }
            }

           CalibrationPoints.SetValue(calibrationPoint, step_number-1);

           CalibrationPointsCount++;

           return calibrationPoint;
        }

        public CalibrationPoint getCalibrationPointForStep(int step_number)
        {

            if (step_number < 0)
                return null;

            if (this.CalibrationPoints[step_number - 1].StepNumber == step_number)
                return this.CalibrationPoints[step_number - 1];
            else
               return null;
        }

        public void SaveData(){
            string filename = DateTime.Now.ToString();
            filename=filename.Replace(":", "");
            filename=filename.Replace(" ", "");
            filename=filename.Replace("-", "");
            filename=filename.Replace("/", "");
            filename=@"c:\CalibrationProcess_" + filename;
            Bitmap screenshotBMP = new Bitmap(this.Parent.Width, this.Parent.Height);
            this.Parent.DrawToBitmap(screenshotBMP, this.Parent.Bounds);
            screenshotBMP.Save(filename + ".bmp");
            Clipboard.SetImage((Image) screenshotBMP);

            
            System.IO.StreamWriter file = System.IO.File.CreateText(filename + ".csv");
            file.WriteLine("Step;TimeStamp;StepX;StepY;CursorX;CursorY;FacePositionX;FacePositionY;FacePositionZ;FaceRotationX;FaceRotationY;FaceRotationZ;ElevationAngle");
            for (int i = 0; i < this.CalibrationPointsCount; i++)
                for (int j = 0; j < this.CalibrationPoints[i].ResultPointsCount;j++)
                    file.WriteLine( this.CalibrationPoints[i].StepNumber + ";" + 
                                    this.CalibrationPoints[i].ResultPoints[j].TimeStamp.ToLongTimeString() +"."+ 
                                    this.CalibrationPoints[i].ResultPoints[j].TimeStamp.Millisecond.ToString() + ";" + 
                                    this.CalibrationPoints[i].X + ";" + 
                                    this.CalibrationPoints[i].Y + ";" + 
                                    this.CalibrationPoints[i].ResultPoints[j].X + ";" + 
                                    this.CalibrationPoints[i].ResultPoints[j].Y + ";" +
                                    this.CalibrationPoints[i].ResultPoints[j].FacePosition.X + ";" +
                                    this.CalibrationPoints[i].ResultPoints[j].FacePosition.Y + ";" +
                                    this.CalibrationPoints[i].ResultPoints[j].FacePosition.Z + ";" +
                                    this.CalibrationPoints[i].ResultPoints[j].FaceRotation.X + ";" +
                                    this.CalibrationPoints[i].ResultPoints[j].FaceRotation.Y + ";" +
                                    this.CalibrationPoints[i].ResultPoints[j].FaceRotation.Z + ";" +
                                    this.CalibrationPoints[i].ResultPoints[j].ElevationAngle);
            file.Flush();
            file.Close();
        }

        
    }
}
