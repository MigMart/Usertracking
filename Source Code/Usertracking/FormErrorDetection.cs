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
    public partial class FormErrorDetection : Form
    {
        private MyApp App;

        public FormErrorDetection()
        {
            InitializeComponent();
        }

        public FormErrorDetection(MyApp prmApp)
        {
            App = prmApp;
            InitializeComponent();
        }

        private void butClose_Click(object sender, EventArgs e)
        {

        }

        private void FormErrorDetection_Load(object sender, EventArgs e)
        {
            this.Top = 0;
            this.Left = 0;
            this.Width = App.ScreenInfo.WidthPix;
            this.Width = App.ScreenInfo.HeightPix;

            App.Calibrating = true;

        }

        private void FormErrorDetection_FormClosing(object sender, FormClosingEventArgs e)
        {
            App.Calibrating = false;
        }
    }
}
