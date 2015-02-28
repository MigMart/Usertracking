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
    public partial class FormShowConfig : Form
    {
        MyApp App;

        public FormShowConfig()
        {
            InitializeComponent();

        }

        public FormShowConfig(MyApp prmApp)
        {
            App = prmApp;

            InitializeComponent();

        }

        private void FormShowConfig_Load(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            
            Pen blackPen=new Pen(Color.Black, 2f);
            Pen redPen=new Pen(Color.Red, 1f);
            Pen greenPen = new Pen(Color.Green, 1f);
            int sx, sy, wx, wy;

            sx = (int)(this.Width * 0.25);
            sy = (int)(this.Height * 0.25);
            wx = (int)(this.Width * 0.5);
            wy = (int)(this.Height * 0.5);

            e.Graphics.DrawRectangle(blackPen, new Rectangle(sx, sy, wx, wy));

            int i, j;

            for (j=0; j<= this.Height; j++)
                for (i = 0; i <= this.Width; i++)
                {
                    int px= sx + (int)(i * 0.5);
                    int py= sy + (int)(j * 0.5);
                    
                    int ex=(int)(CalibrationError.getErrorX(i, j) * 0.5);
                    int ey=(int)(CalibrationError.getErrorY(i, j) * 0.5);

                    if (ex != 0 || ey != 0)
                    {
                        e.Graphics.DrawLine(redPen, px, py, px + ex, py + ey);
                        e.Graphics.DrawRectangle(greenPen, px + ex, py + ey, 2, 2);
                    }
                }
            /*
            e.Graphics.DrawEllipse(
                new Pen(Color.Red, 2f),
                0, 0, pictureBox1.Size.Width, pictureBox1.Size.Height);
             * 
             * */
        }

        private void butRecal_Click(object sender, EventArgs e)
        {
            
            this.lblRecalc.Visible = true;
            
            CalibrationError.RecalcAllPoints();
            
            this.lblRecalc.Visible = false;

            this.pictureBox1.Refresh();
        }
    }
}
