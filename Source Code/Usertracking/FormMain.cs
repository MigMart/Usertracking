using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Kinect;
using Microsoft.Kinect.Toolkit;
using Microsoft.Kinect.Toolkit.FaceTracking;

using VirtualKeyboard;

namespace Usertracking
{
    public partial class FormMain : Form
    {
        private MyApp App;  //Main application object
        private int frameCounter = 0; //Camera frame counter

        public FormMain()
        {
            InitializeComponent();
        }

        public FormMain(MyApp prmApp)
        {
            this.App = prmApp;
            InitializeComponent();
        }

        private void toolStripContainer1_RightToolStripPanel_Click(object sender, EventArgs e)
        {
            if ((bool)Properties.Settings.Default.AutoHideMenu)
            {
                this.toolStripContainer1.RightToolStripPanelVisible = false;
                this.toolStripContainer1.TopToolStripPanelVisible = false;
                this.toolStripContainer1.LeftToolStripPanelVisible = false;
                this.toolStripContainer1.BottomToolStripPanelVisible = false;
            }
        }
        private void toolStripContainer1_TopToolStripPanel_Click(object sender, EventArgs e)
        {
            if ((bool)Properties.Settings.Default.AutoHideMenu)
            {
                this.toolStripContainer1.RightToolStripPanelVisible = false;
                this.toolStripContainer1.TopToolStripPanelVisible = false;
                this.toolStripContainer1.LeftToolStripPanelVisible = false;
                this.toolStripContainer1.BottomToolStripPanelVisible = false;
            }
        }

        private void toolStripContainer1_LeftToolStripPanel_Click(object sender, EventArgs e)
        {
            if ((bool)Properties.Settings.Default.AutoHideMenu)
            {
                this.toolStripContainer1.RightToolStripPanelVisible = false;
                this.toolStripContainer1.TopToolStripPanelVisible = false;
                this.toolStripContainer1.LeftToolStripPanelVisible = false;
                this.toolStripContainer1.BottomToolStripPanelVisible = false;
            }
        }

        private void toolStripContainer1_BottomToolStripPanel_Click(object sender, EventArgs e)
        {
            if ((bool)Properties.Settings.Default.AutoHideMenu)
            {
                this.toolStripContainer1.RightToolStripPanelVisible = false;
                this.toolStripContainer1.TopToolStripPanelVisible = false;
                this.toolStripContainer1.LeftToolStripPanelVisible = false;
                this.toolStripContainer1.BottomToolStripPanelVisible = false;
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (Cursor.Position.X >= this.Right - 20)
                this.toolStripContainer1.RightToolStripPanelVisible = true;
            if (Cursor.Position.Y <= 20)
                this.toolStripContainer1.TopToolStripPanelVisible = true;
            if (Cursor.Position.X <= 20)
                this.toolStripContainer1.LeftToolStripPanelVisible = true;
            if (Cursor.Position.Y >= this.Bottom - 20)
                this.toolStripContainer1.BottomToolStripPanelVisible = true;

            timer1.Enabled = true;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.App.Calibrating == true)
            {
                if (this.Visible == true)
                    this.Hide();
            }else
            {
                if (this.Visible==false)
                    this.Show();

               

                
                if (this.App.VK != null)             
                    if (this.App.VK.isVisible()){
                        this.TopMost = false;
                    }else
                        this.TopMost = true;
                else
                    this.TopMost = true;

                if (this.App.ConfigurationForm().Visible){
                    this.TopMost = false;
                    this.App.ConfigurationForm().BringToFront();
                }

                /*
                if (this.TopMost)
                    this.BringToFront();
                */
                if ((bool)Properties.Settings.Default.AutoHideMenu)
                {
                    timer1.Stop();
                    timer1.Enabled = false;


                    if ((Cursor.Position.X >= this.Right - 20) || (Cursor.Position.X <= 20) || (Cursor.Position.Y >= this.Bottom - 20) || (Cursor.Position.Y <= 20))
                    {
                        timer2.Enabled = true;
                        timer2.Start();
                    }
                    else
                    {
                        if (this.toolStripContainer1.RightToolStripPanelVisible && Cursor.Position.X < this.Right - this.toolStripContainer1.RightToolStripPanel.Width)
                            this.toolStripContainer1.RightToolStripPanelVisible = false;

                        if (this.toolStripContainer1.TopToolStripPanelVisible && Cursor.Position.Y > this.Top + this.toolStripContainer1.TopToolStripPanel.Height)
                            this.toolStripContainer1.TopToolStripPanelVisible = false;

                        if (this.toolStripContainer1.LeftToolStripPanelVisible && Cursor.Position.X > this.Left + this.toolStripContainer1.LeftToolStripPanel.Width)
                            this.toolStripContainer1.LeftToolStripPanelVisible = false;

                        if (this.toolStripContainer1.BottomToolStripPanelVisible && Cursor.Position.Y < this.Bottom - this.toolStripContainer1.BottomToolStripPanel.Height)
                            this.toolStripContainer1.BottomToolStripPanelVisible = false;

                        timer2.Stop();
                        timer2.Enabled = false;
                        timer1.Enabled = true;
                        timer1.Start();
                    }
                }
                else
                {
                    this.toolStripContainer1.RightToolStripPanelVisible = true;
                    this.toolStripContainer1.TopToolStripPanelVisible = true;
                    this.toolStripContainer1.LeftToolStripPanelVisible = true;
                    this.toolStripContainer1.BottomToolStripPanelVisible = true;
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if ((bool)Properties.Settings.Default.AutoHideMenu)
            {
                this.toolStripContainer1.RightToolStripPanelVisible = false;
                this.toolStripContainer1.TopToolStripPanelVisible = false;
                this.toolStripContainer1.LeftToolStripPanelVisible = false;
                this.toolStripContainer1.BottomToolStripPanelVisible = false;
            }

            if (this.App.ConfigurationForm().ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {

            }
        }
        static bool visiblevk = false;
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if ((bool)Properties.Settings.Default.AutoHideMenu)
            {
                this.toolStripContainer1.RightToolStripPanelVisible = false;
                this.toolStripContainer1.TopToolStripPanelVisible = false;
                this.toolStripContainer1.LeftToolStripPanelVisible = false;
                this.toolStripContainer1.BottomToolStripPanelVisible = false;
            }

            if (this.App.VK == null)
                this.App.VK = new VirtualKeyboard.VKeyboard();

            visiblevk = !visiblevk;
            if (visiblevk)
            {
                this.App.VK.Show();
                
                this.toolStripButton2.BackColor = Color.Blue;
            }
            else
            {
                this.App.VK.Hide();
                this.toolStripButton2.BackColor = Color.Black;
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if ((bool)Properties.Settings.Default.AutoHideMenu)
            {
                this.toolStripContainer1.RightToolStripPanelVisible = false;
                this.toolStripContainer1.TopToolStripPanelVisible = false;
                this.toolStripContainer1.LeftToolStripPanelVisible = false;
                this.toolStripContainer1.BottomToolStripPanelVisible = false;
            }
            if (this.App.kinect != null)
                if (this.App.kinect.IsRunning)
                    this.App.kinect.Stop();

            Application.Exit();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.AutoHideMenu == false)
                this.toolStrip1.Visible = true;

            System.Drawing.Point center = new System.Drawing.Point((int)(Screen.PrimaryScreen.Bounds.Width * 0.5), (int)(Screen.PrimaryScreen.Bounds.Height * 0.5));
            Rectangle workingarea = Screen.GetWorkingArea(center);

            this.Top = workingarea.Top;
            this.Width = workingarea.Width;

            this.Left = workingarea.Left;
            this.Height = workingarea.Height;

            DialogResult rettype;
            rettype = DialogResult.Cancel;
            
            do
            {
                //toolStripStatusLabel1.Text = "No Kinect Sensors detected.";
                if (KinectSensor.KinectSensors.Count > 0)
                {
                    //toolStripStatusLabel1.Text = "Kinect Sensors detected:" + KinectSensor.KinectSensors.Count;
                    this.App.kinect = KinectSensor.KinectSensors[0];
                    if (this.App.kinect.IsRunning)
                        this.App.kinect.Stop();

                    this.App.kinect.Start();
                    if (this.App.kinect.IsRunning)
                    {

                        this.App.speech.kinectsensor = this.App.kinect;


                        try
                        {
                            this.App.kinect.ElevationAngle = (int)Properties.Settings.Default.SetKinectElevationAngle;
                        }
                        catch (Exception exc){
                        }
                        this.App.kinect.ColorStream.Enable(ColorImageFormat.RgbResolution640x480Fps30);
                        this.App.kinect.DepthStream.Enable(DepthImageFormat.Resolution320x240Fps30);
                        try
                        {
                            // This will throw on non Kinect For Windows devices.
                            this.App.kinect.DepthStream.Range = DepthRange.Near;
                            this.App.kinect.SkeletonStream.EnableTrackingInNearRange = true;
                        }
                        catch (InvalidOperationException)
                        {
                            this.App.kinect.DepthStream.Range = DepthRange.Default;
                            this.App.kinect.SkeletonStream.EnableTrackingInNearRange = false;
                        }

                        if ((bool)Properties.Settings.Default.SeatedModeOn)
                            this.App.kinect.SkeletonStream.TrackingMode = SkeletonTrackingMode.Seated;
                        else
                            this.App.kinect.SkeletonStream.TrackingMode = SkeletonTrackingMode.Default;

                        this.App.kinect.SkeletonStream.Enable();
                        this.App.speech.Start();
                        this.App.kinect.AllFramesReady += KinectSensorOnAllFramesReady;
                        //this.App.kinect.SkeletonFrameReady += KinectSensorOnSkeletonFrameReady;


                        this.App.FT = new FaceTracker(this.App.kinect);
                    }
                    else
                    {
                        rettype = MessageBox.Show("Couldn't start Kinect sensor. Check cable connection and click\"Retry\". Click \"Cancel\" to stop trying.", "Error", MessageBoxButtons.RetryCancel);
                    }
                }
                else
                {
                    rettype = MessageBox.Show("Couldn't detect the Kinect sensor. Check cable connection and click\"Retry\". Click \"Cancel\" to stop trying.", "Error", MessageBoxButtons.RetryCancel);
                }
            } while (rettype == DialogResult.Retry);
             

        }

        DateTime previousTime;
        
        private void KinectSensorOnAllFramesReady(object sender, AllFramesReadyEventArgs allFramesReadyEventArgs)
        {
            Skeleton[] skeletonData;
            short[] depthPixelData;
            byte[] colorPixelData;

     

            using (ColorImageFrame colorImageFrame = allFramesReadyEventArgs.OpenColorImageFrame())
            {
                if (colorImageFrame == null)
                    return;

                colorPixelData = new byte[colorImageFrame.PixelDataLength];

                colorImageFrame.CopyPixelDataTo(colorPixelData);
                
            }

            using (DepthImageFrame depthImageFrame = allFramesReadyEventArgs.OpenDepthImageFrame())
            {
                if (depthImageFrame == null)
                    return;

                depthPixelData = new short[depthImageFrame.PixelDataLength];

                depthImageFrame.CopyPixelDataTo(depthPixelData);
            }

           
            using (SkeletonFrame skeletonFrame = allFramesReadyEventArgs.OpenSkeletonFrame())
            {
                if (skeletonFrame == null)
                    return;

                skeletonData = new Skeleton[skeletonFrame.SkeletonArrayLength];

                skeletonFrame.CopySkeletonDataTo(skeletonData);
            }
            
            App.UserInfo.UserDetected = false;

            var skeleton = skeletonData.FirstOrDefault(s => s.TrackingState == SkeletonTrackingState.Tracked);
            if (skeleton == null)
                return;


            SkeletonPoint pos = skeleton.Position;
            App.UserInfo.UserDetected = true;
            App.UserInfo.Position = pos;

            /*
            SkeletonFrame skeleton = allFramesReadyEventArgs.OpenSkeletonFrame();
            if (skeleton != null)
            {
                SkeletonData = new Skeleton[skeleton.SkeletonArrayLength];
                int index;

                skeleton.CopySkeletonDataTo(SkeletonData);

                if (skeleton.SkeletonArrayLength > 0)
                {
                    for (index = 0; index < skeleton.SkeletonArrayLength; index++)
                    {
                        if (SkeletonData[index].TrackingState == SkeletonTrackingState.Tracked)
                        {
                            SkeletonPoint pos = SkeletonData[index].Position;
                            App.UserInfo.Detected = true;
                            App.UserInfo.Position = pos;
                        }
                    }
                }
            }
            else
            {
                App.UserInfo.Detected = false;
            }

            */

           

            if (this.App.FT!= null)
            {
                this.App.faceFrame = this.App.FT.Track(this.App.kinect.ColorStream.Format, colorPixelData,this.App.kinect.DepthStream.Format, depthPixelData, skeleton);
                this.App.UserInfo.UserFaceDetected = this.App.faceFrame.TrackSuccessful;

                if (this.App.faceFrame.TrackSuccessful)
                {

                    this.frameCounter += 1; //Calculate framerate
                    
                    DateTime dt = System.DateTime.Now;
                    
                    if (previousTime.Year == 1)
                        previousTime = dt;
                    else
                        if (dt.Subtract(previousTime).TotalSeconds >= 1.0)
                        {
                            this.App.ScreenInfo.CameraFPS = frameCounter;
                            this.frameCounter = 0;
                            previousTime = dt;
                        }
                    /*
                     this.App.UserInfo.FaceRotation.X = (float)Math.Round(faceFrame.Rotation.X);
                    this.App.UserInfo.FaceRotation.Y = (float)Math.Round(faceFrame.Rotation.Y);
                    this.App.UserInfo.FaceRotation.Z = (float)Math.Round(faceFrame.Rotation.Z);

                    this.App.UserInfo.FacePosition.X = (float)Math.Round(faceFrame.Translation.X, 2);
                    this.App.UserInfo.FacePosition.Y = (float)Math.Round(faceFrame.Translation.Y, 2);
                    this.App.UserInfo.FacePosition.Z = (float)Math.Round(faceFrame.Translation.Z, 2);
                    */
                    this.App.UserInfo.FaceRotation = this.App.faceFrame.Rotation;
                    this.App.UserInfo.FacePosition = this.App.faceFrame.Translation;

                    
                    this.App.CalcCursorCoordinates();


                    //Console.WriteLine(this.frameCounter.ToString() + " " + dt.Minute.ToString() + ":" + dt.Second.ToString() + "." + dt.Millisecond.ToString() + " " + Math.Round(this.App.faceFrame.Translation.X, 2).ToString() + " " + this.App.UserInfo.FacePosition.X.ToString() + " " + Math.Round(this.App.faceFrame.Translation.Y, 2).ToString() + " " + this.App.UserInfo.FacePosition.Y.ToString() + " " + Math.Round(this.App.faceFrame.Translation.Z, 2).ToString() + " " + this.App.UserInfo.FacePosition.Z.ToString() + " " + Math.Round(this.App.faceFrame.Rotation.X).ToString() + " " + this.App.UserInfo.FaceRotation.X.ToString() + " " + Math.Round(this.App.faceFrame.Rotation.Y).ToString() + " " + this.App.UserInfo.FaceRotation.Y.ToString() + " " + Math.Round(this.App.faceFrame.Rotation.Z).ToString() + " " + this.App.UserInfo.FaceRotation.Z.ToString() + " " + this.App.cursorx.ToString() + " " + this.App.cursory.ToString());
                   /*
                    FormConfiguration frm = (FormConfiguration) this.App.ConfigurationForm();
                    Pen pen = new Pen(Color.Red);
                    Graphics g = frm.pictureBox1.CreateGraphics();
                    float propx, propy;
                    propx=0.5f;
                    propy=0.5f;
                    //for (int p = 95; p <= 95; p++)
                    //{
                    g.DrawLine(pen, faceFrame.GetProjected3DShape()[7].X * propx + frm.pictureBox1.Left, faceFrame.GetProjected3DShape()[7].Y * propy + frm.pictureBox1.Top, faceFrame.GetProjected3DShape()[57].X * propx + frm.pictureBox1.Left, faceFrame.GetProjected3DShape()[57].Y * propy + frm.pictureBox1.Top);
                    g.DrawLine(pen, faceFrame.GetProjected3DShape()[57].X * propx + frm.pictureBox1.Left, faceFrame.GetProjected3DShape()[57].Y * propy + frm.pictureBox1.Top, faceFrame.GetProjected3DShape()[24].X * propx + frm.pictureBox1.Left, faceFrame.GetProjected3DShape()[24].Y * propy + frm.pictureBox1.Top);
                    g.DrawLine(pen, faceFrame.GetProjected3DShape()[24].X * propx + frm.pictureBox1.Left, faceFrame.GetProjected3DShape()[24].Y * propy + frm.pictureBox1.Top, faceFrame.GetProjected3DShape()[7].X * propx + frm.pictureBox1.Left, faceFrame.GetProjected3DShape()[7].Y * propy + frm.pictureBox1.Top);

                    g.DrawRectangle(pen, faceFrame.GetProjected3DShape()[7].X * propx + frm.pictureBox1.Left, faceFrame.GetProjected3DShape()[7].Y * propy + frm.pictureBox1.Top, 1, 1);
                    g.DrawRectangle(pen, faceFrame.GetProjected3DShape()[57].X * propx + frm.pictureBox1.Left, faceFrame.GetProjected3DShape()[57].Y * propy + frm.pictureBox1.Top, 1, 1);
                    g.DrawRectangle(pen, faceFrame.GetProjected3DShape()[24].X * propx + frm.pictureBox1.Left, faceFrame.GetProjected3DShape()[24].Y * propy + frm.pictureBox1.Top, 1, 1);
                    //}
                    */
                    if (this.App.Calibrating == false)
                    {
                        if (this.App.Parking)
                        {
                            Mouse.ChangeArrowCursor(Application.CommonAppDataPath + @"\Dot.cur");
                        }

                        if (this.App.Parking == false || Mouse.isOver((int)this.App.cursorx, (int)this.App.cursory, this.tsParkingZone.Bounds))
                        {
                            if (this.App.UserInfo.MouthClickDown == false)
                                Mouse.ChangeArrowCursor(Application.CommonAppDataPath + @"\PointUp.cur");
                        }
                        if (Properties.Settings.Default.EnableMouseControl)
                            this.App.MoveMouseToXY((int)this.App.cursorx, (int)this.App.cursory);

                        var AUCoeff = this.App.faceFrame.GetAnimationUnitCoefficients();

                        bool useJaw = Properties.Settings.Default.UseMouthClick;
                        bool clickdown = false;

                        var jawLower = AUCoeff[AnimationUnit.JawLower];
                        var BrowRaiser = AUCoeff[AnimationUnit.BrowRaiser];
                        //var BrowLower = AUCoeff[AnimationUnit.BrowLower];
                        
                        this.App.UserInfo.currentMouthOpening = jawLower;
                        this.App.UserInfo.currentEyebrowOpening = BrowRaiser;

                        if (Properties.Settings.Default.UseMouthClick)
                        {
                            if (0 < jawLower && jawLower <= 1)
                                if (jawLower >= Properties.Settings.Default.minMouth4MouseClick)
                                    clickdown = true;
                        }
                        else if (Properties.Settings.Default.UseEyeBrowClick)
                        {
                            if (0 < BrowRaiser && BrowRaiser <= 1)
                                if (BrowRaiser >= Properties.Settings.Default.minEyebrow4MouseClick)
                                    clickdown = true;
                        }

                        if (this.App.UserInfo.ForceClickDown)
                            clickdown = true;

                        if (clickdown)
                        {
                            if (this.App.Parking == false || Mouse.isOver((int)this.App.cursorx, (int)this.App.cursory, this.tsParkingZone.Bounds))
                            {
                                if (this.App.UserInfo.MouthClickDown == false && Properties.Settings.Default.EnableMouseControl)
                                {
                                    if (this.App.UseMouseButton == MouseButtons.Left)
                                    {
                                        Mouse.MouseEvent(Mouse.MouseEventFlags.LeftDown);
                                    }
                                    else
                                        Mouse.MouseEvent(Mouse.MouseEventFlags.RightDown);

                                    Mouse.ChangeArrowCursor(Application.CommonAppDataPath + @"\Hand.cur");
                                }
                            }
                            this.App.UserInfo.MouthClickDown = true;
                        }
                        else
                        {

                            if (this.App.UserInfo.MouthClickDown && Properties.Settings.Default.EnableMouseControl)
                            {
                                if (this.App.Parking == false || Mouse.isOver((int)this.App.cursorx, (int) this.App.cursory,  this.tsParkingZone.Bounds))
                                {

                                    if (this.App.UseMouseButton==MouseButtons.Left)
                                        Mouse.MouseEvent(Mouse.MouseEventFlags.LeftUp);
                                    else
                                        Mouse.MouseEvent(Mouse.MouseEventFlags.RightUp);

                                    Mouse.RestoreArrowCursor();
                                }
                            }

                            this.App.UserInfo.MouthClickDown = false;
                        }
                    }

                    if (this.App.UserInfo.MouthClickDown)
                        this.tsbButtonClick.BackColor = Color.ForestGreen;
                    else
                        this.tsbButtonClick.BackColor = Color.Black;
                }
                else
                {
                    Mouse.ChangeArrowCursor(this.App.DefaultSystemMouseCursor);
                }
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if ((bool)Properties.Settings.Default.AutoHideMenu)
            {
                this.toolStripContainer1.RightToolStripPanelVisible = false;
                this.toolStripContainer1.TopToolStripPanelVisible = false;
                this.toolStripContainer1.LeftToolStripPanelVisible = false;
                this.toolStripContainer1.BottomToolStripPanelVisible = false;
            }


            if (this.App.ConfigurationForm().ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {

            }
        }

        private void KinectSensorOnSkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs SkeletonReadyEventArgs)
        {
            /*
            SkeletonFrame skeleton = SkeletonReadyEventArgs.OpenSkeletonFrame();
            if (skeleton != null)
            {
                Skeleton[] SkeletonData = new Skeleton[skeleton.SkeletonArrayLength];
                int index;

                skeleton.CopySkeletonDataTo(SkeletonData);

                if (skeleton.SkeletonArrayLength > 0)
                {
                    for (index = 0; index < skeleton.SkeletonArrayLength; index++)
                    {
                        if (SkeletonData[index].TrackingState == SkeletonTrackingState.Tracked)
                        {
                            SkeletonPoint pos = SkeletonData[index].Position;
                            App.UserInfo.UserDetected = true;
                            App.UserInfo.Position = pos;
                        }
                    }
                }
            }
            else
            {
                App.UserInfo.UserDetected = false;
            }
             * */
        }

       
        private void tmrCamFramerate_Tick(object sender, EventArgs e)
        {
            /*
            DateTime dt = System.DateTime.Now;
            this.App.ScreenInfo.CameraFPS = frameCounter;

            Console.WriteLine(dt.Second.ToString() + "."+ dt.Millisecond.ToString() + " " + this.frameCounter.ToString() + " FPS");
            frameCounter = 0;
             * */
        }

        private void tsParkingZone_Click(object sender, EventArgs e)
        {
            this.App.Parking = ! this.App.Parking;
            //this.pbEye.Visible = this.App.Parking;
            if (this.App.Parking)
            {
            //    this.Opacity = 0.5;
                this.tsParkingZone.BackColor = Color.Blue;
            }
            else
            {
              //  this.Opacity = 1;
                this.tsParkingZone.BackColor = Color.Black;
            }
        }

        public void UpdateButtonClickText(string value)
        {
            this.tsbButtonClick.Text = value;
        }

        private void tsbButtonClick_Click(object sender, EventArgs e)
        {

            if (this.App.UseMouseButton == MouseButtons.Left)
            {
                this.App.UseMouseButton = MouseButtons.Right;
                UpdateButtonClickText("Right Click");
            }
            else
            {
                this.App.UseMouseButton = MouseButtons.Left;
                UpdateButtonClickText("Left Click");
            }

        }



    }
}

