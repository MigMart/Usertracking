using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Usertracking
{
    public partial class FormCalibra : Form
    {

        private MyApp App;

        public FormCalibra()
        {
            InitializeComponent();
        }

        public FormCalibra(MyApp prmApp)
        {
            this.App = prmApp;
            InitializeComponent();
        }

        private void FormCalibra_Load(object sender, EventArgs e)
        {

            this.Left = 0;
            this.Top = 0;

            this.Width = (int) this.App.ScreenInfo.WidthPix;
            this.Height = (int) this.App.ScreenInfo.HeightPix;

            this.TopMost = true;
            this.BringToFront();

            this.butStartCalibration.Top = this.Height / 2 - this.butStartCalibration.Height / 2;
            this.butStartCalibration.Left = this.Width / 2 - this.butStartCalibration.Width / 2;
        }

        private void butStartCalibration_Click(object sender, EventArgs e)
        {


            this.pbFollowMe.Top = this.Top;
            this.pbFollowMe.Left = this.Left;

            this.pbFollowMe.Visible = true;
            this.butStartCalibration.Visible = false;

            this.lblCount.Visible = true;

            tmrPrepare.Enabled = true;
            tmrPrepare.Start();

            this.pbEye.Visible = true;

            CalibrationError.Initialize();
        }

        int calibration_step=0;
        int horizontal_increment=0;
        int vertical_increment=0;

        private void tmrCalibrating_Tick(object sender, EventArgs e)
        {
            this.lblUserFaceNotDetected.Visible = !this.App.UserInfo.UserFaceDetected;

            if (this.App.UserInfo.UserFaceDetected == true)
            {

                /*
                this.TopMost = true;
                this.BringToFront();
                */

                //Check if calibrating cross is in top left corner
                if ((this.pbFollowMe.Left <= this.Left) && (this.pbFollowMe.Top <= this.Top) && (calibration_step == 0))
                {

                    this.pbFollowMe.Left = this.Left;
                    this.pbFollowMe.Top = this.Top;

                    calibration_step = 1;
                }
                //Check if calibrating cross is in top left corner
                if ((this.pbFollowMe.Left <= this.Left) && (this.pbFollowMe.Top <= this.Top) && (calibration_step == 4))
                {

                    this.pbFollowMe.Left = this.Left;
                    this.pbFollowMe.Top = this.Top;

                    calibration_step = 0;
                    this.tmrCalibrating.Stop();
                    this.tmrCalibrating.Enabled = false;

                    this.pbFollowMe.Visible = false;
                    this.butStartCalibration.Visible = true;

                    this.Close();
                }

                //Check if calibrating cross is in top right corner
                if ((this.pbFollowMe.Right >= this.Right) && (this.pbFollowMe.Top <= this.Top))
                {
                    this.pbFollowMe.Left = this.Right - this.pbFollowMe.Width;
                    this.pbFollowMe.Top = this.Top;
                    calibration_step = 2;
                }
                //Check if calibrating cross is in bottom right corner
                if ((this.pbFollowMe.Right >= this.Right) && (this.pbFollowMe.Bottom >= this.Bottom))
                {
                    this.pbFollowMe.Left = this.Right - this.pbFollowMe.Width;
                    this.pbFollowMe.Top = this.Bottom - this.pbFollowMe.Height;

                    calibration_step = 3;
                }
                //Check if calibrating cross is in bottom left corner
                if ((this.pbFollowMe.Left <= this.Left) && (this.pbFollowMe.Bottom >= this.Bottom))
                {

                    this.pbFollowMe.Left = this.Left;
                    this.pbFollowMe.Top = this.Bottom - this.pbFollowMe.Height;

                    calibration_step = 4;
                }

                switch (calibration_step)
                {
                    case 1:
                        horizontal_increment = 1;
                        vertical_increment = 0;
                        break;
                    case 2:
                        horizontal_increment = 0;
                        vertical_increment = 1;
                        break;
                    case 3:
                        horizontal_increment = -1;
                        vertical_increment = 0;
                        break;
                    case 4:
                        horizontal_increment = 0;
                        vertical_increment = -1;
                        break;
                }


                this.pbFollowMe.Top += vertical_increment * 5;
                this.pbFollowMe.Left += horizontal_increment * 5;

                CalibrationError.SetPoint(this.pbFollowMe.Left, this.pbFollowMe.Top, (int)this.App.cursorx, (int)this.App.cursory);

                this.pbEye.Top = (int)this.App.cursory - this.pbEye.Height;
                this.pbEye.Left = (int)this.App.cursorx - this.pbEye.Width;
            }
        }

        private void FormCalibra_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.App.Calibrating = false;
        }

        int countdown = 3;
        private void tmrPrepare_Tick(object sender, EventArgs e)
        {
            countdown--;

            if (countdown > 0)
            {
                lblCount.Text = countdown.ToString();
                tmrPrepare.Stop();
                tmrPrepare.Start();
            }
            else
            {
                this.lblCount.Visible = false;

                tmrPrepare.Enabled = false;
                countdown = 3;
                tmrCalibrating.Enabled = true;
                tmrCalibrating.Start();
            }

        }
    }
}
