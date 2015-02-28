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

namespace Usertracking
{
    public partial class FormOptions : Form
    {
        private MyApp App;

        public FormOptions(Usertracking.MyApp prmApp)
        {
            InitializeComponent();

            App = prmApp;
        }

        private void UpdateSeatedModeOn(CheckBox chk)
        {

            switch (chk.CheckState)
            {
                case CheckState.Checked:
                    chk.BackColor = Color.Lime;
                    chk.Text = "Seated Mode [ON]";
                    break;
                case CheckState.Indeterminate:
                case CheckState.Unchecked:
                    chk.BackColor = Color.Coral;
                    chk.Text = "Seated Mode [OFF]";
                    break;
            }

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

        private void chkSeatedModeOn_CheckedChanged(object sender, EventArgs e)
        {
            UpdateSeatedModeOn((CheckBox)sender);
        }

        private void FormOptions_Load(object sender, EventArgs e)
        {
            this.chkSeatedModeOn.Checked = Properties.Settings.Default.SeatedModeOn;
            this.chkAutoHideMenu.Checked = Properties.Settings.Default.AutoHideMenu;

            UpdateSeatedModeOn(this.chkSeatedModeOn);
            UpdateAutoHideMenu(this.chkAutoHideMenu);
            
        }

        private void UpdateAutoHideMenu(CheckBox chk)
        {

            switch (chk.CheckState)
            {
                case CheckState.Checked:
                    chk.BackColor = Color.Lime;
                    chk.Text = "Auto Hide Menu [ON]";
                    break;
                case CheckState.Indeterminate:
                case CheckState.Unchecked:
                    chk.BackColor = Color.Coral;
                    chk.Text = "Auto Hide Menu [OFF]";
                    break;
            }

            Properties.Settings.Default.AutoHideMenu = chk.Checked;
            Properties.Settings.Default.Save();
        }

        private void chkAutoHideMenu_CheckedChanged(object sender, EventArgs e)
        {
            UpdateAutoHideMenu((CheckBox)sender);
        }

        private void btRunCalibration_Click(object sender, EventArgs e)
        {
            this.Close();

            this.App.RunCalibration();


        }

        private void butShowCalibration_Click(object sender, EventArgs e)
        {
            FormShowConfig frm = new FormShowConfig(this.App);

            frm.Show();
            frm.BringToFront();
        }

        private void butClose_Click(object sender, EventArgs e)
        {
            
        }

    }
}
