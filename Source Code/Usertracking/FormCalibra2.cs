using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic.PowerPacks;

namespace Usertracking
{
    public partial class FormCalibra2 : Form
    {

        private MyApp App;
        private AVGStabilizer exstab, eystab;

        private CalibrationProcess calibration;
        

        public FormCalibra2()
        {
            InitializeComponent();
        }

        public FormCalibra2(MyApp prmApp)
        {
            this.App = prmApp;
            InitializeComponent();
            
            calibration = new CalibrationProcess(this.shapeContainer1);
        }

        private void FormCalibra2_Load(object sender, EventArgs e)
        {
            this.App.Calibrating = true;

            this.Left = 0;
            this.Top = 0;

            this.Width = (int) this.App.ScreenInfo.WidthPix;
            this.Height = (int) this.App.ScreenInfo.HeightPix;

            //this.TopMost = true;
            this.BringToFront();



            /*
            calibration.CalibrationPoints.SetValue(new CalibrationPoint((Form)this, this.Left, this.Top * 1 / 6, (int)1, Color.Red), 1);
            calibration.CalibrationPoints.SetValue(new CalibrationPoint((Form)this, this.Left + this.Width / 2 - this.pbFollowMe.Width / 2, this.Top, (int)2, Color.White), 2);
            calibration.CalibrationPoints.SetValue(new CalibrationPoint((Form)this, this.Right - this.pbFollowMe.Width, this.Top, (int)3, Color.DarkBlue), 3);
            calibration.CalibrationPoints.SetValue(new CalibrationPoint((Form)this, this.Right - this.pbFollowMe.Width, this.Top + this.Height / 2 - this.pbFollowMe.Height / 2, (int)4, Color.Yellow), 4);
            calibration.CalibrationPoints.SetValue(new CalibrationPoint((Form)this, this.Right - this.pbFollowMe.Width, this.Bottom - this.pbFollowMe.Height, (int)5, Color.Fuchsia), 5);
            calibration.CalibrationPoints.SetValue(new CalibrationPoint((Form)this, this.Left + this.Width / 2 - this.pbFollowMe.Width / 2, this.Bottom - this.pbFollowMe.Height, (int)6, Color.Cyan), 6);
            calibration.CalibrationPoints.SetValue(new CalibrationPoint((Form)this, this.Left, this.Bottom - this.pbFollowMe.Height, (int)7, Color.Brown), 7);
            calibration.CalibrationPoints.SetValue(new CalibrationPoint((Form)this, this.Left, this.Top + this.Height / 2 - this.pbFollowMe.Height / 2, (int)8, Color.Green), 8);
            calibration.CalibrationPoints.SetValue(new CalibrationPoint((Form)this, this.Left + this.Width / 2 - this.pbFollowMe.Width / 2, this.Top + this.Height / 2 - this.pbFollowMe.Height / 2, (int)9, Color.Orange), 9);

            */

            calibration.AddNewCalibrationPoint(this.Left + this.Width * 1 / 6 - this.pbFollowMe.Width / 2, this.Top + this.Height * 1 / 6 - this.pbFollowMe.Height / 2, (int)1, Color.Red);
            calibration.AddNewCalibrationPoint(this.Left + this.Width / 2 - this.pbFollowMe.Width / 2, this.Top + this.Height * 1 / 6 - this.pbFollowMe.Height / 2, (int)2, Color.White);
            calibration.AddNewCalibrationPoint(this.Left + this.Width * 5 / 6 - this.pbFollowMe.Width / 2, this.Top + this.Height * 1 / 6 - this.pbFollowMe.Height / 2, (int)3, Color.DarkBlue);
            calibration.AddNewCalibrationPoint(this.Left + this.Width * 5 / 6 - this.pbFollowMe.Width / 2, this.Top + this.Height / 2 - this.pbFollowMe.Height / 2, (int)4, Color.Yellow);
            calibration.AddNewCalibrationPoint(this.Left + this.Width * 5 / 6 - this.pbFollowMe.Width / 2, this.Top + this.Height * 5 / 6 - this.pbFollowMe.Height / 2, (int)5, Color.Pink);

            calibration.AddNewCalibrationPoint(this.Left + this.Width / 2 - this.pbFollowMe.Width / 2, this.Top + this.Height * 5 / 6 - this.pbFollowMe.Height / 2, (int)6, Color.Cyan);
            calibration.AddNewCalibrationPoint(this.Left + this.Width * 1 / 6 - this.pbFollowMe.Width / 2, this.Top + this.Height * 5 / 6 - this.pbFollowMe.Height / 2, (int)7, Color.Brown);
            calibration.AddNewCalibrationPoint(this.Left + this.Width * 1 / 6 - this.pbFollowMe.Width / 2, this.Top + this.Height / 2 - this.pbFollowMe.Height / 2, (int)8, Color.Green);
            calibration.AddNewCalibrationPoint(this.Left + this.Width / 2 - this.pbFollowMe.Width / 2, this.Top + this.Height / 2 - this.pbFollowMe.Height / 2, (int)9, Color.Orange);

            this.shapeContainer1.Top = 0;
            this.shapeContainer1.Left = 0;
            this.shapeContainer1.Size = new System.Drawing.Size(this.Width , this.Height);

            this.butStartCalibration.Top = this.Height / 2 - this.butStartCalibration.Height / 2;
            this.butStartCalibration.Left = this.Width / 2 - this.butStartCalibration.Width / 2;


            this.lblUserFaceNotDetected.Top = this.butStartCalibration.Top - (int)(this.lblUserFaceNotDetected.Height * 1.5f);
            this.lblUserFaceNotDetected.Left = this.Width / 2 - this.lblUserFaceNotDetected.Width / 2;

            if (exstab == null)
                exstab = new AVGStabilizer();
            
            if (eystab == null)
                eystab = new AVGStabilizer();

            exstab.clearstack();
            eystab.clearstack();
        }

        private void butStartCalibration_Click(object sender, EventArgs e)
        {
            if (!tmrPrepare.Enabled && !tmrCalibrating.Enabled)
            {
                butStartCalibration.Text = "Click to cancel";

                butStartCalibration.Visible = false;

                this.pbFollowMe.Top = this.Top;
                this.pbFollowMe.Left = this.Left;

                //this.pbFollowMe.Visible = true;
                this.butStartCalibration.Visible = true;

                this.lblCount.Visible = true;

                tmrPrepare.Enabled = true;
                tmrPrepare.Start();

                tmrCalibrating.Enabled = true;
                tmrCalibrating.Start();

                //this.pbEye.Visible = true;

                CalibrationError.Initialize();
            }
            else
            {
                this.shapeContainer1.BringToFront();
                this.shapeContainer1.Focus();
                this.calibration.SaveData();

                tmrPrepare.Stop();
                tmrPrepare.Enabled = false;

                tmrCalibrating.Stop();
                tmrCalibrating.Enabled = false;

                this.Close();
            }
         }

        int calibration_step=1;
        int calibration_counter=0;

        RectangleShape rsPreviousDot = null;
        private void AddNewDot(int x, int y, Color cor)
        {
            RectangleShape rsNewDot=new RectangleShape(x,y,10,10);
            // 
            // rsNewDot
            // 
            rsNewDot.BackColor = cor;
            rsNewDot.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque;
            rsNewDot.BorderColor = cor;
            rsNewDot.FillColor = System.Drawing.Color.WhiteSmoke;
            rsNewDot.FillGradientStyle = Microsoft.VisualBasic.PowerPacks.FillGradientStyle.Horizontal;
            //rsNewDot.Location = new System.Drawing.Point(265, 169);
            rsNewDot.Name = "rsNewDot" + (calibration_step + 1) * calibration_counter;
            //rsNewDot.Size = new System.Drawing.Size(5, 5);
            

            this.shapeContainer1.Shapes.Add(rsNewDot);

            if (rsPreviousDot!=null){

                LineShape rsLine = new LineShape(rsPreviousDot.Left, rsPreviousDot.Top, x, y);
                rsLine.BorderColor = cor;
                rsLine.BorderWidth=2;
                this.shapeContainer1.Shapes.Add(rsLine);
            }

            rsPreviousDot = rsNewDot;

            this.SuspendLayout();
            
            
            this.shapeContainer1.BringToFront();
            this.ResumeLayout(true);
        }

        

        private void tmrCalibrating_Tick(object sender, EventArgs e)
        {
            Color cor=Color.Red;

            this.lblUserFaceNotDetected.Visible = !this.App.UserInfo.UserFaceDetected;
            this.butStartCalibration.Visible = !this.App.UserInfo.UserFaceDetected;

            if (this.App.UserInfo.UserFaceDetected == true)
            {
                CalibrationPoint calibrationPoint=this.calibration.getCalibrationPointForStep(calibration_step);

                this.pbFollowMe.Left = calibrationPoint.X;
                this.pbFollowMe.Top = calibrationPoint.Y;

                lblCount.ForeColor = calibrationPoint.Color; 
                lblCount.SendToBack();

                AddNewDot((int)this.App.cursorx, (int)this.App.cursory, calibrationPoint.Color);

                calibrationPoint.AddResultPoint((int)this.App.cursorx, (int)this.App.cursory, this.App.UserInfo.FacePosition,this.App.UserInfo.FaceRotation, this.App.kinect.ElevationAngle);
                //this.calibration.CalibrationPoints[calibration_step].AddResultPoint((int)this.App.cursorx, (int)this.App.cursory);
                
                //lblStep.Text = "Step " + calibration_step.ToString();

                /*
                this.App.CalibrationValues.eCursorX = this.pbFollowMe.Left - (int)this.App.cursorx;
                this.App.CalibrationValues.eCursorY = this.pbFollowMe.Top - (int)this.App.cursory;

                exstab.stabilize(ref this.App.CalibrationValues.eCursorX);
                eystab.stabilize(ref this.App.CalibrationValues.eCursorY);
                */
                /*this.pbEye.Top = (int)(this.App.cursory - this.pbEye.Height + this.App.CalibrationValues.eCursorY);
                this.pbEye.Left = (int)(this.App.cursorx - this.pbEye.Width + this.App.CalibrationValues.eCursorX);
                */
                //CalibrationError.SetPoint(this.pbFollowMe.Left, this.pbFollowMe.Top, (int)this.App.cursorx, (int)this.App.cursory);
            }
        }

        private void FormCalibra2_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.App.Calibrating = false;
        }

        private void AddNewLabel(int x, int y, String text){
            Label lbl = new Label();
            lbl.Left = x;
            lbl.Top = y;
            lbl.Text = text;
            this.Controls.Add(lbl);
        }

        int countdown = 5;
        private void tmrPrepare_Tick(object sender, EventArgs e)
        {
            CalibrationPoint currentCP=null;
            
            if (calibration_step > 0)
            {
                currentCP = this.calibration.getCalibrationPointForStep(calibration_step);

                if (currentCP != null)
                {
                    lblCount.Top = currentCP.Y;
                    lblCount.Left = currentCP.X;
                }
            }

            if (this.App.UserInfo.UserFaceDetected)
            {
                

                countdown--;

                tmrPrepare.Stop();
                tmrPrepare.Start();

                 if (countdown == 0)
                {
                    

                    if (currentCP!=null)
                          currentCP.Label.Visible = true;

                    calibration_step++;

                    if (calibration_step > 9)
                    {
                        this.pbFollowMe.Left = this.Left;
                        this.pbFollowMe.Top = this.Top;

                        calibration_step = 0;
                        this.tmrCalibrating.Stop();
                        this.tmrCalibrating.Enabled = false;

                        this.pbFollowMe.Visible = false;
                        //this.butStartCalibration.Visible = true;
                        lblStep.Visible = false;
                        lblCount.Visible = false;

                        this.Refresh();
                        

                        
                        
                        //this.Close();
                    }
                    else
                    {
                        countdown = 5;
                    }

                }



                lblCount.Text = countdown.ToString();
  
            }
        }

        private void FormCalibra2_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
    }
}
