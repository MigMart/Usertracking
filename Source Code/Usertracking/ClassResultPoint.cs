using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Kinect.Toolkit.FaceTracking;

namespace Usertracking
{
    public class ResultPoint
    {
        public DateTime TimeStamp;
        public int X;
        public int Y;
        public CalibrationPoint CalibrationPoint;
        public Vector3DF FacePosition;
        public Vector3DF FaceRotation;
        public float ElevationAngle;

        public ResultPoint()
        {
            this.TimeStamp = DateTime.Now;
        }

        public ResultPoint(DateTime timestamp)
        {
            this.TimeStamp = timestamp;
        }


        public ResultPoint(int x, int y)
        {
            this.TimeStamp = DateTime.Now;
            this.X = x;
            this.Y = y;
        }

        public ResultPoint(int x, int y, DateTime timestamp)
        {
            this.TimeStamp = DateTime.Now;
            this.X = x;
            this.Y = y;
        }

        public ResultPoint(int x, int y, DateTime timestamp, CalibrationPoint calibrationpoint)
        {
            this.TimeStamp = DateTime.Now;
            this.X = x;
            this.Y = y;
            this.CalibrationPoint = calibrationpoint;
        }

        public ResultPoint(int x, int y, DateTime timestamp, CalibrationPoint calibrationpoint,Vector3DF faceposition, Vector3DF facerotation, float kinectelevationangle )
        {
            this.TimeStamp = DateTime.Now;
            this.X = x;
            this.Y = y;
            this.CalibrationPoint = calibrationpoint;
            this.FacePosition = faceposition;
            this.FaceRotation = facerotation;
            this.ElevationAngle = kinectelevationangle;
        }

        ~ResultPoint()
        {
        }
     }

}
