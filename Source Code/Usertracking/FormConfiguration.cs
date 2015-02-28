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


    public partial class FormConfiguration : Form
    {
        private MyApp App;

        private byte[] colorImageData;
        private ColorImageFormat currentColorImageFormat = ColorImageFormat.Undefined;
        IntPtr colorPtr;


        public FormConfiguration()
        {
            InitializeComponent();
        }

        public FormConfiguration(MyApp prmApp)
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
        private void UpdateSeatedModeOn(CheckBox chk)
        {


            if (chk.Checked != Properties.Settings.Default.SeatedModeOn)
            {
                Properties.Settings.Default.SeatedModeOn = chk.Checked;
                Properties.Settings.Default.Save();

                if ((bool)Properties.Settings.Default.SeatedModeOn)
                    this.App.kinect.SkeletonStream.TrackingMode = SkeletonTrackingMode.Seated;
                else
                    this.App.kinect.SkeletonStream.TrackingMode = SkeletonTrackingMode.Default;
            }
        }

        private void UpdateAutoHideMenu(CheckBox chk)
        {

            if (chk.Checked != Properties.Settings.Default.AutoHideMenu)
            {
                Properties.Settings.Default.AutoHideMenu = chk.Checked;
                Properties.Settings.Default.Save();
            }
        }

        private void LoadSettings(){
            this.chkSeatedModeOn.Checked = Properties.Settings.Default.SeatedModeOn;
            this.chkAutoHideMenu.Checked = Properties.Settings.Default.AutoHideMenu;
            this.chkEnableMouseControl.Checked = Properties.Settings.Default.EnableMouseControl;

            this.numCorrZ.Value = (decimal)Properties.Settings.Default.CorrectFacePositionZ;
            this.numCorrY.Value = (decimal)Properties.Settings.Default.CorrectFacePositionY;
            this.numCorrX.Value = (decimal)Properties.Settings.Default.CorrectFacePositionX;

            this.numCorrCY.Value = (decimal)Properties.Settings.Default.CorrectCursorY;
            this.numCorrCX.Value = (decimal)Properties.Settings.Default.CorrectCursorX;

            this.numElevationAngle.Minimum = this.App.kinect.MinElevationAngle;
            this.numElevationAngle.Maximum = this.App.kinect.MaxElevationAngle;
            this.numElevationAngle.Value = (decimal)Properties.Settings.Default.SetKinectElevationAngle;

            UpdateSeatedModeOn(this.chkSeatedModeOn);
            UpdateAutoHideMenu(this.chkAutoHideMenu);

            if (this.App.kinect != null)
                this.App.kinect.AllFramesReady += KinectSensorOnAllFramesReady;

            this.Width = 952;
            this.Height = 560;

            if (this.App.ScreenInfo.DeviceName != null)
                this.lblMonitor.Text = this.App.ScreenInfo.DeviceName.ToString();

            this.lblCamPosX.Text = Properties.Settings.Default.CamPosX.ToString() + " cm";
            this.lblCamPosY.Text = Properties.Settings.Default.CamPosY.ToString() + " cm";

            this.lblMonitorSize.Text = this.App.ScreenInfo.WidthCm.ToString() + " x " + this.App.ScreenInfo.HeightCm.ToString();
            this.lblMonitorWidth.Text = this.App.ScreenInfo.WidthCm.ToString() + " cm";
            this.lblMonitorHeight.Text = this.App.ScreenInfo.HeightCm.ToString() + " cm";
            this.lblMonitorResolution.Text = this.App.ScreenInfo.WidthPix.ToString() + " x " + this.App.ScreenInfo.HeightPix.ToString();


            if (Properties.Settings.Default.UseMouthClick)
                tabControl2.SelectedIndex=0;
            else
                tabControl2.SelectedIndex=1;
           
            this.numMinMouthOpening.Value = (decimal)Properties.Settings.Default.minMouth4MouseClick;
            this.numMinEyebrowRaising.Value = (decimal)Properties.Settings.Default.minEyebrow4MouseClick;

            numMindX.Value = (decimal)this.App.MouseXStabilizer.min_error_threshold;
            numMaxdX.Value = (decimal)this.App.MouseXStabilizer.max_error_threshold;
            chkeX.Checked = this.App.MouseXStabilizer.use_error_thresholding;
            numMindY.Value = (decimal)this.App.MouseYStabilizer.min_error_threshold;
            numMaxdY.Value = (decimal)this.App.MouseYStabilizer.max_error_threshold;
            chkeY.Checked = this.App.MouseYStabilizer.use_error_thresholding;
            
            this.lineHoriz.X1 = this.picCalResult.Left;
            this.lineHoriz.Y1 = (this.picCalResult.Top + this.picCalResult.Bottom) /2;
            this.lineHoriz.X2 = this.picCalResult.Right;
            this.lineHoriz.Y2 = this.lineHoriz.Y1;


            this.lineVert.Y1 = this.picCalResult.Top;
            this.lineVert.X1 = (this.picCalResult.Left + this.picCalResult.Right) / 2;
            this.lineVert.Y2 = this.picCalResult.Right;
            this.lineVert.X2 = this.lineVert.X1;

            /*
            this.lblLookHere.Left = this.picCalResult.Left + this.picCalResult.Width / 2 - this.lblLookHere.Width / 2;
            this.lblLookHere.Top = this.picCalResult.Top + this.picCalResult.Height / 2 - this.lblLookHere.Height / 2;
            this.lblLookHere.Visible = true;
             * */
        }

        private void FormConfiguration_Load(object sender, EventArgs e)
        {
            LoadSettings();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void lblImageSize_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int x, y, width, height; //used only to represent error correction rectangles

            if (this.App.kinect != null)
                if (this.App.kinect.IsRunning)
                    this.lblNoCamAvailable.Visible = false;
                else
                    this.lblNoCamAvailable.Visible = true;
            else           
                this.lblNoCamAvailable.Visible = true;

            this.SuspendLayout();
            this.rectMaxErrThrs.Visible = this.rectMinErrThrs.Visible = this.App.MouseXStabilizer.use_error_thresholding || this.App.MouseYStabilizer.use_error_thresholding;

            if (this.rectMinErrThrs.Visible)
            {
                // Draw rectangle representing minimum error thresholding
                if (this.App.MouseXStabilizer.use_error_thresholding)
                {
                    x = (int)(this.picCalResult.Left + this.picCalResult.Width / 2 - this.App.MouseXStabilizer.min_error_threshold);
                    width = (int)(this.App.MouseXStabilizer.min_error_threshold * 2);
                }
                else
                {
                    x = (int)(this.picCalResult.Left + this.picCalResult.Width / 2);
                    width = 0;
                }

                if (this.App.MouseYStabilizer.use_error_thresholding)
                {
                    y = (int)(this.picCalResult.Top + this.picCalResult.Height / 2 - this.App.MouseYStabilizer.min_error_threshold);
                    height = (int)(this.App.MouseYStabilizer.min_error_threshold * 2);
                }
                else
                {
                    y = (int)(this.picCalResult.Top + this.picCalResult.Height / 2);
                    height = 0;
                }

                this.rectMinErrThrs.Left = x;
                this.rectMinErrThrs.Top = y;
                this.rectMinErrThrs.Width = width;
                this.rectMinErrThrs.Height = height;
            }

            if (this.rectMaxErrThrs.Visible)
            {
                // Draw rectangle representing minimum error thresholding
                if (this.App.MouseXStabilizer.use_error_thresholding)
                {
                    x = (int)(this.picCalResult.Left + this.picCalResult.Width / 2 - this.App.MouseXStabilizer.max_error_threshold);
                    width = (int)(this.App.MouseXStabilizer.max_error_threshold * 2);
                }
                else
                {
                    x = this.picCalResult.Left;
                    width = this.picCalResult.Width;
                }

                if (this.App.MouseYStabilizer.use_error_thresholding)
                {
                    y = (int)(this.picCalResult.Top + this.picCalResult.Height / 2 - this.App.MouseYStabilizer.max_error_threshold);
                    height = (int)(this.App.MouseYStabilizer.max_error_threshold * 2);
                }
                else
                {
                    y = this.picCalResult.Top;
                    height = this.picCalResult.Height;
                }

                this.rectMaxErrThrs.Left = x;
                this.rectMaxErrThrs.Top = y;
                this.rectMaxErrThrs.Width = width;
                this.rectMaxErrThrs.Height = height;
            }



            if (this.App.kinect != null)
            {
                if (this.App.kinect.IsRunning)
                {
                    this.lblElevationAngle.Text = this.App.kinect.ElevationAngle.ToString();


                    this.lblFPS.Text = System.DateTime.Now.ToString() + " " + this.App.ScreenInfo.CameraFPS.ToString() + " FPS";
                    

                    if (this.App.UserInfo.UserFaceDetected)
                    {
                        //this.lblFacePosition.Text = "X: " + this.App.UserInfo.FacePosition.X.ToString() + " Y: " + this.App.UserInfo.FacePosition.Y.ToString() + " Z: " + this.App.UserInfo.FacePosition.Z.ToString();
                        this.lblX.Text = this.App.UserInfo.FacePosition.X.ToString(); //Math.Round(this.App.UserInfo.FacePosition.X, 2).ToString();
                        this.lblY.Text = this.App.UserInfo.FacePosition.Y.ToString();//Math.Round(this.App.UserInfo.FacePosition.Y, 2).ToString();
                        this.lblZ.Text = this.App.UserInfo.FacePosition.Z.ToString();//Math.Round(this.App.UserInfo.FacePosition.Z, 2).ToString();

                        //this.lblHeadRotation.Text = "RX: " + this.App.UserInfo.FaceRotation.X.ToString() + " RY: " + this.App.UserInfo.FaceRotation.Y.ToString() + " Z: " + this.App.UserInfo.FaceRotation.Z.ToString();
                        this.lblPitchAngle.Text = this.App.UserInfo.FaceRotation.X.ToString();//Math.Round(this.App.UserInfo.FaceRotation.X,2).ToString();
                        this.lblYawAngle.Text = this.App.UserInfo.FaceRotation.Y.ToString(); //Math.Round(this.App.UserInfo.FaceRotation.Y, 2).ToString();
                        this.lblRollAngle.Text = this.App.UserInfo.FaceRotation.Z.ToString(); //Math.Round(this.App.UserInfo.FaceRotation.Z, 2).ToString();

                        this.lblCursorPosition.Text = "X: " + this.App.cursorx.ToString() + " , Y: " + this.App.cursory.ToString();
                        this.lblMouthOpening.Text = this.App.UserInfo.currentMouthOpening.ToString();
                        this.lblEyebrowRaising.Text = this.App.UserInfo.currentEyebrowOpening.ToString();
                    }
                    else
                    {
                        //this.lblFacePosition.Text = "(not detected)";

                        this.lblX.Text = "(not detected)";
                        this.lblY.Text = "(not detected)";
                        this.lblZ.Text = "(not detected)";

                        this.lblPitchAngle.Text = "(not detected)";
                        this.lblYawAngle.Text = "(not detected)";
                        this.lblRollAngle.Text = "(not detected)";

                        this.lblCursorPosition.Text = "X: " + Math.Round(this.App.cursorx, 2).ToString() + " , Y: " + Math.Round(this.App.cursory, 2).ToString();
                        this.lblMouthOpening.Text = "(not detected)";
                        this.lblEyebrowRaising.Text = "(not detected)";
                    }
                }
                else
                {
                    this.lblElevationAngle.Text = "(not detected)";
                    this.lblMouthOpening.Text = "(not detected)";
                    this.lblEyebrowRaising.Text = "(not detected)";
                    this.lblCursorPosition.Text = "X: " + Math.Round(this.App.cursorx, 2).ToString() + " , Y: " + Math.Round(this.App.cursory, 2).ToString();
                    this.lblFPS.Text = "0 FPS";
                }
            }
            else
            {
                this.lblElevationAngle.Text = "(not detected)";
                this.lblMouthOpening.Text = "(not detected)";
                this.lblEyebrowRaising.Text = "(not detected)";
                this.lblCursorPosition.Text = "X: " + Math.Round(this.App.cursorx, 2).ToString() + " , Y: " + Math.Round(this.App.cursory, 2).ToString();
                this.lblFPS.Text = "0 FPS";
            }

            this.ResumeLayout();

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
                        Properties.Settings.Default.SetKinectElevationAngle = (int)this.numElevationAngle.Value;
                        Properties.Settings.Default.Save();
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

        private void tmrHideElevationAngle_Tick(object sender, EventArgs e)
        {
            this.tmrHideElevationAngle.Stop();
            this.tmrHideElevationAngle.Enabled = false;
            this.numElevationAngle.Visible = false;
        }

        private void numMinMouthOpening_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.minMouth4MouseClick = (float)this.numMinMouthOpening.Value;
            Properties.Settings.Default.minMouth4MouseClick = Properties.Settings.Default.minMouth4MouseClick;
            Properties.Settings.Default.Save();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void numCorrCX_ValueChanged(object sender, EventArgs e)
        {
            this.App.CalibrationValues.eCursorX = (float) this.numCorrCX.Value;
            Properties.Settings.Default.CorrectCursorX = (float)this.numCorrCX.Value;
            Properties.Settings.Default.Save();
        }

        private void numCorrCY_ValueChanged(object sender, EventArgs e)
        {
            this.App.CalibrationValues.eCursorY = (float) this.numCorrCY.Value;
            Properties.Settings.Default.CorrectCursorY = (float)this.numCorrCY.Value;
            Properties.Settings.Default.Save();
        }

        private void numCorrX_ValueChanged(object sender, EventArgs e)
        {
            this.App.CalibrationValues.eX = (float)this.numCorrX.Value;
            Properties.Settings.Default.CorrectFacePositionX = (float)this.numCorrX.Value;
            Properties.Settings.Default.Save();
        }
        private void numCorrY_ValueChanged(object sender, EventArgs e)
        {
            this.App.CalibrationValues.eY = (float)this.numCorrY.Value;
            Properties.Settings.Default.CorrectFacePositionY = (float)this.numCorrY.Value;
            Properties.Settings.Default.Save();
        }
        private void numCorrZ_ValueChanged(object sender, EventArgs e)
        {
            this.App.CalibrationValues.eZ = (float)this.numCorrZ.Value;
            Properties.Settings.Default.CorrectFacePositionZ = (float)this.numCorrZ.Value;
            Properties.Settings.Default.Save();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void butCalibrate_Click(object sender, EventArgs e)
        {
            if (tmrCalibrate.Enabled == false)
            {
                this.App.Calibrating = true;
                this.butCalibrate.Text = "Stop";
                

                picCalResult.Image = new Bitmap(this.picCalResult.Width, this.picCalResult.Height);
                picCalResult.Refresh();

                tmrCalibrate.Enabled = true;
                tmrCalibrate.Start();
            }
            else
            {
                this.butCalibrate.Text = "Calibrate";
                tmrCalibrate.Stop();
                tmrCalibrate.Enabled = false;
                this.App.Calibrating = false;
            }
        }

        private void ClearGraph()
        {
            //if (picCalResult.Image == null)
            picCalResult.Image = new Bitmap(this.picCalResult.Width, this.picCalResult.Height);

            Graphics graf = Graphics.FromImage(picCalResult.Image);

            //graf.Clear(System.Drawing.Color.White);

            graf.Save();
        }

        private void DrawRect(System.Drawing.Color bordercolor,float x, float y, float rectwidth, float rectheight)
        {
            if (picCalResult.Image == null)
                picCalResult.Image = new Bitmap(this.picCalResult.Width, this.picCalResult.Height);

            Graphics graf = Graphics.FromImage(picCalResult.Image);
            System.Drawing.Pen pen = new System.Drawing.Pen(bordercolor, 2.0f);

            graf.DrawRectangle(pen, x, y, rectwidth, rectheight);

            graf.Save();
        }

        float old_cx=0, old_cy=0;

        private void tmrCalibrate_Tick(object sender, EventArgs e)
        {


            this.SuspendLayout();
            if (this.App.UserInfo.UserFaceDetected)
            {
                float x=0, y=0;
                float dx=0, dy = 0;

                x = picCalResult.Left + picCalResult.Width / 2;
                y = picCalResult.Top + picCalResult.Height / 2;

                if (old_cx != 0)
                {
                    dx = (this.App.cursorx - old_cx);
                }
               
                if (old_cx != 0)
                    dy = (this.App.cursory - old_cy);

  
                if ((Math.Abs(dx) >= 500)||(Math.Abs(dy)>=500))
                {
                    dx = 0;
                    dy = 0;
                    old_cx = 0;
                    old_cy = 0;
                    ClearGraph();
                }
                else
                {
                    x += dx;
                    y += dy;

                    old_cx = this.App.cursorx;
                    old_cy = this.App.cursory;

                    if (x < this.picCalResult.Left)
                        x = this.picCalResult.Left;
                    if (x > this.picCalResult.Right)
                        x = this.picCalResult.Right;

                    if (y < this.picCalResult.Top)
                        y = this.picCalResult.Top;
                    if (y > this.picCalResult.Bottom)
                        y = this.picCalResult.Bottom;

                    // Draw point representing cursor position
                    /*float x = Cursor.Position.X - this.picCalResult.Left - this.tabControl1.Left - this.Left;
                    float y =  Cursor.Position.Y - this.picCalResult.Top - this.tabControl1.Top - this.Top;
                    */

                    DrawRect(System.Drawing.Color.Red, x, y, 2.0f, 2.0f);
                }
            }


            refreshShapes();

            this.ResumeLayout();
        }

        private void picCalResult_Paint(object sender, PaintEventArgs e)
        {

        }

        private void picCalResult_MouseHover(object sender, EventArgs e)
        {
            

        }

        private void refreshShapes(){
            picCalResult.SuspendLayout();
            picCalResult.Refresh();
            picCalResult.ResumeLayout();
            this.rectMaxErrThrs.ResumePaint(true);
            this.rectMinErrThrs.ResumePaint(true);
            this.lineHoriz.ResumePaint(true);
            this.lineVert.ResumePaint(true);
          
            
        }
        private void numMindX_ValueChanged(object sender, EventArgs e)
        {

            this.App.MouseXStabilizer.min_error_threshold = (float) numMindX.Value;
            Properties.Settings.Default.MinHorizErrorThrsh = this.App.MouseXStabilizer.min_error_threshold;
            Properties.Settings.Default.Save();
            this.SuspendLayout();
            refreshShapes();
            this.ResumeLayout();
        }

        private void numMaxdX_ValueChanged(object sender, EventArgs e)
        {
            this.App.MouseXStabilizer.max_error_threshold = (float) numMaxdX.Value;
            Properties.Settings.Default.MaxHorizErrorThrsh = this.App.MouseXStabilizer.max_error_threshold;
            Properties.Settings.Default.Save();
            this.SuspendLayout();
            refreshShapes();
            this.ResumeLayout();
        }

        private void chkeX_CheckedChanged(object sender, EventArgs e)
        {
            this.App.MouseXStabilizer.use_error_thresholding = chkeX.Checked;
            Properties.Settings.Default.UseHorizErrorCorrection = this.App.MouseXStabilizer.use_error_thresholding;
            Properties.Settings.Default.Save();
            this.SuspendLayout();
            refreshShapes();
            this.ResumeLayout();

        }

        private void numMindY_ValueChanged(object sender, EventArgs e)
        {

            this.App.MouseYStabilizer.min_error_threshold = (float)numMindY.Value;
            Properties.Settings.Default.MinVertErrorThrsh = this.App.MouseYStabilizer.min_error_threshold;
            Properties.Settings.Default.Save();
            this.SuspendLayout();
            refreshShapes();
            this.ResumeLayout();

        }

        private void numMaxdY_ValueChanged(object sender, EventArgs e)
        {
            
            this.App.MouseYStabilizer.max_error_threshold = (float) numMaxdY.Value;
            Properties.Settings.Default.MaxVertErrorThrsh = this.App.MouseYStabilizer.max_error_threshold;
            Properties.Settings.Default.Save();
            this.SuspendLayout();
            refreshShapes();
            this.ResumeLayout();
        }

        private void chkeY_CheckedChanged(object sender, EventArgs e)
        {
            this.App.MouseYStabilizer.use_error_thresholding = chkeY.Checked;
            Properties.Settings.Default.UseVertErrorCorrection = this.App.MouseYStabilizer.use_error_thresholding;
            Properties.Settings.Default.Save();
            this.SuspendLayout();
            refreshShapes();
            this.ResumeLayout();
            
        }

        private void chkSeatedModeOn_CheckedChanged(object sender, EventArgs e)
        {
            UpdateSeatedModeOn((CheckBox)sender);
        }

        private void chkAutoHideMenu_CheckedChanged(object sender, EventArgs e)
        {
            UpdateAutoHideMenu((CheckBox)sender);
        }

        private void butAutoDetectMonitor_Click(object sender, EventArgs e)
        {
            this.App.AutoDetectMonitorInfo();
            LoadSettings();
        }

        private void lblMonitorWidth_Click(object sender, EventArgs e)
        {
            this.numMonitorWidth.Text = this.App.ScreenInfo.WidthCm.ToString();
            this.numMonitorWidth.Location = lblMonitorWidth.Location;
            //this.numMonitorWidth.Size = lblMonitorWidth.Size;
            this.numMonitorWidth.Visible = true;
            this.numMonitorWidth.Focus();
        }

        private void lblMonitorHeight_Click(object sender, EventArgs e)
        {
            this.numMonitorHeight.Text = this.App.ScreenInfo.HeightCm.ToString();
            this.numMonitorHeight.Location = lblMonitorHeight.Location;
            //this.numMonitorHeight.Size = lblMonitorHeight.Size;
            this.numMonitorHeight.Visible = true;
            this.numMonitorHeight.Focus();
        }

        private void numMonitorWidth_ValueChanged(object sender, EventArgs e)
        {
            this.App.ScreenInfo.WidthCm = (float)this.numMonitorWidth.Value;
        }

        private void numMonitorHeight_ValueChanged(object sender, EventArgs e)
        {
            this.App.ScreenInfo.HeightCm = (float)this.numMonitorHeight.Value;
        }

        private void numMonitorHeight_Leave(object sender, EventArgs e)
        {
            this.lblMonitorHeight.Text = this.App.ScreenInfo.HeightCm.ToString() + " cm";
            Properties.Settings.Default.MonitorHeight = this.App.ScreenInfo.HeightCm;
            Properties.Settings.Default.Save();
            this.numMonitorHeight.Visible = false;
            
        }

        private void numMonitorWidth_Leave(object sender, EventArgs e)
        {
            this.lblMonitorWidth.Text = this.App.ScreenInfo.WidthCm.ToString() + " cm";
            Properties.Settings.Default.MonitorWidth = this.App.ScreenInfo.WidthCm;
            Properties.Settings.Default.Save();
            this.numMonitorWidth.Visible = false;
           
        }

        private void picCam_Click(object sender, EventArgs e)
        {

        }

        bool mousebuttonpressed = false;
        System.Drawing.Point ini;
        private void picCam_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mousebuttonpressed = true;
                ini = e.Location;
            }
        }

        private void picCam_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Properties.Settings.Default.Save();
                mousebuttonpressed = false;
            }
        }

        private void picCam_MouseMove(object sender, MouseEventArgs e)
        {

            if (mousebuttonpressed)
            {
                this.picCam.Left += e.X - ini.X;
                this.picCam.Top += e.Y - ini.Y;

                //Adjust vertical line
                this.lineShape2.X1 = (int) (rectangleShape11.Location.X + rectangleShape11.Size.Width / 2);
                this.lineShape2.Y1 = (int)(rectangleShape11.Location.Y + rectangleShape11.Size.Height / 2);
                this.lineShape2.X2 = this.lineShape2.X1;
                this.lineShape2.Y2 = (int) (this.picCam.Bottom-3);

                //Adjust horizontal line
                this.lineShape4.X1 = this.lineShape2.X2;
                this.lineShape4.Y1 = this.lineShape2.Y2;
                this.lineShape4.X2 = (int)(this.picCam.Left + this.picCam.Width / 2);
                this.lineShape4.Y2 = lineShape4.Y1;

                //Calculate camera position in centimeters
                Properties.Settings.Default.CamPosX = (float) Math.Round((float)(this.lineShape4.X2 - this.lineShape4.X1) / (float)rectangleShape10.Width * this.App.ScreenInfo.WidthCm,1);
                Properties.Settings.Default.CamPosY = (float)Math.Round((float) (this.lineShape2.Y1 - this.lineShape2.Y2) / (float)rectangleShape10.Height * this.App.ScreenInfo.HeightCm,1);
                

                //Update labels on screen
                this.lblCamPosX.Text = Properties.Settings.Default.CamPosX.ToString() + " cm";
                this.lblCamPosY.Text = Properties.Settings.Default.CamPosY.ToString() + " cm";

                this.lblCamPosY.Left = (int)(this.lineShape2.X1 - this.lblCamPosY.Width / 2);
                this.lblCamPosY.Top = (int)((this.lineShape2.Y1 + this.lineShape2.Y2) / 2 - this.lblCamPosY.Height / 2); 


                this.lblCamPosX.Left = (int)((this.lineShape4.X1 + this.lineShape4.X2) /2 - this.lblCamPosX.Width / 2);
                this.lblCamPosX.Top = (int)(this.lineShape4.Y1 - this.lblCamPosX.Height / 2);
            }
        }

        private void chkEnableMouseControl_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.EnableMouseControl = chkEnableMouseControl.Checked;
            Properties.Settings.Default.Save();
        }

        private void butMoreinfo_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormGraph x = new FormGraph(this.App);
            x.ShowDialog();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void picCam_Click_1(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void lblFPS_Click(object sender, EventArgs e)
        {

        }

        private void butlowleveldata_Click(object sender, EventArgs e)
        {
            this.App.RunCalibration();
        }

        private void label35_Click(object sender, EventArgs e)
        {

        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl2.SelectedIndex)
            {
                case 0: Properties.Settings.Default.UseMouthClick = true;
                        Properties.Settings.Default.UseEyeBrowClick = false;
                        break;
                case 1: Properties.Settings.Default.UseMouthClick = false;
                        Properties.Settings.Default.UseEyeBrowClick = true;
                        break;
            }

            Properties.Settings.Default.Save();
        }

        private void numMinEyebrow4Click_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.minEyebrow4MouseClick = (float) numMinEyebrowRaising.Value;
            Properties.Settings.Default.Save();
        }

        private void label26_Click(object sender, EventArgs e)
        {

        }


    }
}
