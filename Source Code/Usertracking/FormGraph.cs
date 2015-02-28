using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GraphLib;

namespace Usertracking
{
    public partial class FormGraph : Form
    {
        private MyApp App;  //Main application object
        
        DataSource CursorX = new DataSource();
        int captureindex=0;
        int stacksize=10000;
        DataSource CursorY = new DataSource();

        DataSource FaceX = new DataSource();
        DataSource FaceY = new DataSource();
        DataSource FaceZ = new DataSource();

        DataSource FaceRX = new DataSource();
        DataSource FaceRY = new DataSource();
        DataSource FaceRZ = new DataSource();
        
        public FormGraph()
        {
            InitializeComponent();
        }

        public FormGraph(MyApp prmApp)
        {
            this.App = prmApp;
            InitializeComponent();
            
            InitGraph();

        }

        
        private void InitGraph(){
            captureindex=0;
            this.Text = "Camera values";
            display.Smoothing = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
            display.PanelLayout = PlotterGraphPaneEx.LayoutMode.VERTICAL_ARRANGED;
            display.SetDisplayRangeX(0, 100);
            display.SetGridDistanceX(10);
            display.DataSources.Clear();

            //Display Cursor X values
            //DataSource CursorX = new DataSource();
            CursorX.Name = "CursorX";
            CursorX.OnRenderXAxisLabel += RenderXLabel;

            CursorX.SetDisplayRangeY(-250, 250);
            CursorX.SetGridDistanceY(100);
            CursorX.AutoScaleY = true;
            CursorX.GraphColor = Color.FromArgb(0, 0, 255);
            CursorX.OnRenderYAxisLabel = RenderYLabel;
            CursorX.Length = stacksize;


            //}

            //Display Cursor Y values
            //DataSource CursorY = new DataSource();
            CursorY.Name = "Cursor Y";
            CursorY.OnRenderXAxisLabel += RenderXLabel;
            CursorY.Length = stacksize;
            CursorY.SetDisplayRangeY(-250, 250);
            CursorY.SetGridDistanceY(100);
            CursorY.AutoScaleY = true;
            CursorY.GraphColor = Color.FromArgb(0, 255, 0);
            CursorY.OnRenderYAxisLabel = RenderYLabel;
            
            //Display Face X values
            //DataSource FaceX = new DataSource();
            FaceX.Name = "X [cm]";
            FaceX.OnRenderXAxisLabel += RenderXLabel;
            FaceX.Length = stacksize;
            FaceX.SetDisplayRangeY(-250, 250);
            FaceX.SetGridDistanceY(100);
            FaceX.AutoScaleY = true;
            FaceX.GraphColor = Color.FromArgb(0, 255, 255);
            FaceX.OnRenderYAxisLabel = RenderYLabel;


            //Display Face Y values
            //DataSource FaceY = new DataSource();
            FaceY.Name = "Y [cm]";
            FaceY.OnRenderXAxisLabel += RenderXLabel;
            FaceY.Length = stacksize;
            FaceY.SetDisplayRangeY(-250, 250);
            FaceY.SetGridDistanceY(100);
            FaceY.AutoScaleY = true;
            FaceY.GraphColor = Color.FromArgb(255, 0, 0);
            FaceY.OnRenderYAxisLabel = RenderYLabel;
            
            //Display Face X values
            //DataSource FaceZ = new DataSource();
            FaceZ.Name = "Z [cm]";
            FaceZ.OnRenderXAxisLabel += RenderXLabel;
            FaceZ.Length = stacksize;
            FaceZ.SetDisplayRangeY(-250, 250);
            FaceZ.SetGridDistanceY(100);
            FaceZ.AutoScaleY = true;
            FaceZ.GraphColor = Color.FromArgb(255, 0, 255);
            FaceZ.OnRenderYAxisLabel = RenderYLabel;

            //Display Face rotation X values
            //DataSource FaceRX = new DataSource();
            FaceRX.Name = "RX [º]";
            FaceRX.OnRenderXAxisLabel += RenderXLabel;
            FaceRX.Length = stacksize;
            FaceRX.SetDisplayRangeY(-90, 90);
            FaceRX.SetGridDistanceY(10);
            FaceRX.AutoScaleY = true;
            FaceRX.GraphColor = Color.FromArgb(255, 255, 0);
            FaceRX.OnRenderYAxisLabel = RenderYLabel;

            //Display Face rotation Y values
            //DataSource FaceRY = new DataSource();
            FaceRY.Name = "RY [º]";
            FaceRY.OnRenderXAxisLabel += RenderXLabel;
            FaceRY.Length = stacksize;
            FaceRY.SetDisplayRangeY(-90, 90);
            FaceRY.SetGridDistanceY(10);
            FaceRY.AutoScaleY = true;
            FaceRY.GraphColor = Color.FromArgb(255, 255, 255);
            FaceRY.OnRenderYAxisLabel = RenderYLabel;

            //Display Face X values
            //DataSource FaceRZ = new DataSource();
            FaceRZ.Name = "RZ [º]";
            FaceRZ.OnRenderXAxisLabel += RenderXLabel;
            FaceRZ.Length = stacksize;
            FaceRZ.SetDisplayRangeY(-90, 90);
            FaceRZ.SetGridDistanceY(10);
            FaceRZ.AutoScaleY = true;
            FaceRZ.GraphColor = Color.FromArgb(255, 128, 0);
            FaceRZ.OnRenderYAxisLabel = RenderYLabel;         


            display.DataSources.Add(CursorX);
            display.DataSources.Add(CursorY);

            display.DataSources.Add(FaceX);
            display.DataSources.Add(FaceY);
            display.DataSources.Add(FaceZ);

            display.DataSources.Add(FaceRX);
            display.DataSources.Add(FaceRY);
            display.DataSources.Add(FaceRZ);

            RefreshGraph();
        }

        private void CaptureData()
        {

            captureindex++;

            CursorX.Samples[captureindex - 1].x = captureindex;
            CursorX.Samples[captureindex - 1].y = (float)this.App.cursorx;

            CursorY.Samples[captureindex - 1].x = captureindex;
            CursorY.Samples[captureindex - 1].y = (float)this.App.cursory;

            FaceX.Samples[captureindex - 1].x = captureindex;
            FaceX.Samples[captureindex - 1].y = (float)this.App.UserInfo.FacePosition.X *100.0f;

            FaceY.Samples[captureindex - 1].x = captureindex;
            FaceY.Samples[captureindex - 1].y = (float)this.App.UserInfo.FacePosition.Y*100.0f;

            FaceZ.Samples[captureindex - 1].x = captureindex;
            FaceZ.Samples[captureindex - 1].y = (float)this.App.UserInfo.FacePosition.Z*100.0f;

            FaceRX.Samples[captureindex - 1].x = captureindex;
            FaceRX.Samples[captureindex - 1].y = (float)this.App.UserInfo.FaceRotation.X;

            FaceRY.Samples[captureindex - 1].x = captureindex;
            FaceRY.Samples[captureindex - 1].y = (float)this.App.UserInfo.FaceRotation.Y;

            FaceRZ.Samples[captureindex - 1].x = captureindex;
            FaceRZ.Samples[captureindex - 1].y = (float)this.App.UserInfo.FaceRotation.Z;

        }

        private void RefreshGraph()
        {
            //this.SuspendLayout();
 
            //this.ResumeLayout();
            display.Refresh();
        }

        private String RenderXLabel(DataSource s, int idx)
        {
           int Value = (int)(s.Samples[idx].x);
           String Label = "" + Value;
           return Label;
        }

        private String RenderYLabel(DataSource s, float value)
        {
            return String.Format("{0:0.0}", value);
        }

        private void tmrRefresh_Tick(object sender, EventArgs e)
        {
           
           RefreshGraph();         
        }

        private void tmrCapture_Tick(object sender, EventArgs e)
        {
            CaptureData();
        }

        private void chkCaptureOn_CheckedChanged(object sender, EventArgs e)
        {
            tmrCapture.Enabled = chkCaptureOn.Checked;
            if (tmrCapture.Enabled)
            {
                InitGraph();
                this.App.Calibrating = true;
                tmrCapture.Start();
            }
            else
            {
                tmrCapture.Stop();
                this.App.Calibrating = false;
                RefreshGraph();
            }
        }

        private void chkAutoRefresh_CheckedChanged(object sender, EventArgs e)
        {
            tmrRefresh.Enabled = chkAutoRefresh.Checked;
            if (tmrRefresh.Enabled)
                tmrRefresh.Start();
            else
                tmrRefresh.Stop();
        }
    }
}
