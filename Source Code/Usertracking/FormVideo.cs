using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Kinect;
using Microsoft.Kinect.Toolkit.FaceTracking;
using System.Runtime.InteropServices;

namespace Usertracking
{


    public partial class FormVideo : Form
    {
        private MyApp App;

        private byte[] colorImageData;
        private ColorImageFormat currentColorImageFormat = ColorImageFormat.Undefined;
        IntPtr colorPtr;


        public FormVideo()
        {
            InitializeComponent();
        }

        public FormVideo(MyApp prmApp)
        {
            this.App = prmApp;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void KinectSensorOnAllFramesReady(object sender, AllFramesReadyEventArgs allFramesReadyEventArgs)
        {
            if (this.Visible)
            {

                using (var colorImageFrame = allFramesReadyEventArgs.OpenColorImageFrame())
                {
                    if (colorImageFrame == null)
                    {
                        return;
                    }

                    // Make a copy of the color frame for displaying.
                    var haveNewFormat = this.currentColorImageFormat != colorImageFrame.Format;
                    if (haveNewFormat)
                    {
                        this.Width = colorImageFrame.Width;
                        this.Height = colorImageFrame.Height;
                        this.lblImageSize.Text = colorImageFrame.Format.ToString();
                        this.currentColorImageFormat = colorImageFrame.Format;
                        this.colorImageData = new byte[colorImageFrame.PixelDataLength];
                    }

                    colorImageFrame.CopyPixelDataTo(this.colorImageData);

                    Marshal.FreeHGlobal(colorPtr);
                    colorPtr = Marshal.AllocHGlobal(colorImageData.Length);
                    Marshal.Copy(colorImageData, 0, colorPtr, colorImageData.Length);

                    Bitmap kinectVideoBitmap = new Bitmap(
                        colorImageFrame.Width,
                        colorImageFrame.Height,
                        colorImageFrame.Width * colorImageFrame.BytesPerPixel,
                        System.Drawing.Imaging.PixelFormat.Format32bppRgb,
                        colorPtr);

                    this.pictureBox1.Image = kinectVideoBitmap;
                }
            }
        }

        private void FormVideo_Load(object sender, EventArgs e)
        {
            this.App.kinect.AllFramesReady += KinectSensorOnAllFramesReady;
            this.Width = 952;
            this.Height = 588;
            this.lblMonitor.Text = this.App.ScreenInfo.DeviceName.ToString();


            this.lblMonitorSize.Text = this.App.ScreenInfo.WidthCm.ToString() + " x " + this.App.ScreenInfo.HeightCm.ToString();
            this.lblMonitorResolution.Text = this.App.ScreenInfo.WidthPix.ToString() + " x " + this.App.ScreenInfo.HeightPix.ToString();
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void lblImageSize_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.App.kinect != null)
            {
                if (this.App.kinect.IsRunning)
                {
                    this.lblElevationAngle.Text = this.App.kinect.ElevationAngle.ToString();

                    if (this.App.UserInfo.UserDetected)
                    {
                        this.lblUserPosition.Text = "X: " + ((int)(this.App.UserInfo.Position.X * 100)).ToString() + " Y: " + this.App.UserInfo.Position.Y.ToString() + " Z: " + this.App.UserInfo.Position.Z.ToString();
                    }
                    else
                    {
                        this.lblUserPosition.Text = "(not detected)";
                    }

                    if (this.App.UserInfo.UserFaceDetected)
                    {
                        this.lblFacePosition.Text = "X: " + this.App.UserInfo.FacePosition.X.ToString() + " Y: " + this.App.UserInfo.FacePosition.Y.ToString() + " Z: " + this.App.UserInfo.FacePosition.Z.ToString();
                        this.lblHeadRotation.Text = "RX: " + this.App.UserInfo.FaceRotation.X.ToString() + " RY: " + this.App.UserInfo.FaceRotation.Y.ToString() + " Z: " + this.App.UserInfo.FaceRotation.Z.ToString();

                        this.lblCursorPosition.Text = "X: " + this.App.cursorx.ToString() + " , Y: " + this.App.cursorx.ToString();
                        this.lblMouthOpening.Text = this.App.UserInfo.currentMouthOpening.ToString();
                    }
                    else
                    {
                        this.lblFacePosition.Text = "(not detected)";
                        this.lblHeadRotation.Text = "(not detected)";
                        this.lblCursorPosition.Text = "X: " + this.App.cursorx.ToString() + " , Y: " + this.App.cursorx.ToString();
                        this.lblMouthOpening.Text = "(not detected)";
                    }
                }
                else
                {
                    this.lblElevationAngle.Text = "(not detected)";
                    this.lblMouthOpening.Text = "(not detected)";
                    this.lblCursorPosition.Text = "X: " + this.App.cursorx.ToString() + " , Y: " + this.App.cursorx.ToString();
                }
            }
            else
            {
                this.lblElevationAngle.Text = "(not detected)";
                this.lblMouthOpening.Text = "(not detected)";
                this.lblCursorPosition.Text = "X: " + this.App.cursorx.ToString() + " , Y: " + this.App.cursorx.ToString();
            }

        }


        private void lblElevationAngle_Click(object sender, EventArgs e)
        {
            if (this.lblElevationAngle.Text != "(not detected)")
            {
                if (this.App.kinect != null)
                {
                    if (this.App.kinect.IsRunning)
                    {
                        this.numElevationAngle.Visible = true;

                        this.numElevationAngle.Minimum = this.App.kinect.MinElevationAngle;
                        this.numElevationAngle.Maximum = this.App.kinect.MaxElevationAngle;
                        this.numElevationAngle.Value = this.App.kinect.ElevationAngle;
                    }
                }
            }
        }

        private void numElevationAngle_ValueChanged(object sender, EventArgs e)
        {
            if (this.App.kinect != null)
            {
                if (this.App.kinect.IsRunning)
                {
                    try
                    {
                        this.App.kinect.ElevationAngle = (int)this.numElevationAngle.Value;
                    }
                    catch (Exception exc)
                    {
                    }
                }
            }

            if (this.tmrHideElevationAngle.Enabled)
                this.tmrHideElevationAngle.Stop();
            else
                this.tmrHideElevationAngle.Enabled = true;

            this.tmrHideElevationAngle.Start();
        }

        private void numElevationAngle_Leave(object sender, EventArgs e)
        {
            this.numElevationAngle.Visible = false;
        }

        private void FormVideo_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void tmrHideElevationAngle_Tick(object sender, EventArgs e)
        {
            this.tmrHideElevationAngle.Stop();
            this.tmrHideElevationAngle.Enabled = false;
            this.numElevationAngle.Visible = false;
        }

        private void numCorrAngle_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }


       

    }
}
