using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Kinect.Toolkit.FaceTracking;

namespace Usertracking
{
    public class AVGStabilizerV3D
    {
        private int stacksize=10;

        public AVGStabilizer XStab;
        public AVGStabilizer YStab;
        public AVGStabilizer ZStab;

        public AVGStabilizerV3D()
        {
            this.XStab = new AVGStabilizer(stacksize);
            this.YStab = new AVGStabilizer(stacksize);
            this.ZStab = new AVGStabilizer(stacksize);
        }

        public AVGStabilizerV3D(int newstacksize)
        {
            this.XStab = new AVGStabilizer(newstacksize);
            this.YStab = new AVGStabilizer(newstacksize);
            this.ZStab = new AVGStabilizer(newstacksize);
        }

        public void stabilize(ref Vector3DF vec){
            float nx, ny, nz;
            nx = vec.X;
            ny = vec.Y;
            nz = vec.Z;
            this.XStab.stabilize(ref nx);
            this.YStab.stabilize(ref ny);
            this.ZStab.stabilize(ref nz);

            vec.X = nx;
            vec.Y = ny;
            vec.Z = nz;

        }

        ~AVGStabilizerV3D()
        {
            this.XStab=null;
            this.YStab=null;
            this.ZStab=null;
        }
    }
}
