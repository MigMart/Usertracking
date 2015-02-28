namespace Usertracking
{
    partial class FormGraph
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGraph));
            this.display = new GraphLib.PlotterDisplayEx();
            this.tmrRefresh = new System.Windows.Forms.Timer(this.components);
            this.tmrCapture = new System.Windows.Forms.Timer(this.components);
            this.chkCaptureOn = new System.Windows.Forms.CheckBox();
            this.chkAutoRefresh = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // display
            // 
            this.display.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.display.AutoScroll = true;
            this.display.BackColor = System.Drawing.Color.Transparent;
            this.display.BackgroundColorBot = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.display.BackgroundColorTop = System.Drawing.Color.Black;
            this.display.DashedGridColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.display.DoubleBuffering = true;
            this.display.Location = new System.Drawing.Point(0, 32);
            this.display.Name = "display";
            this.display.PlaySpeed = 0.5F;
            this.display.Size = new System.Drawing.Size(833, 528);
            this.display.SolidGridColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.display.TabIndex = 0;
            // 
            // tmrRefresh
            // 
            this.tmrRefresh.Interval = 5000;
            this.tmrRefresh.Tick += new System.EventHandler(this.tmrRefresh_Tick);
            // 
            // tmrCapture
            // 
            this.tmrCapture.Tick += new System.EventHandler(this.tmrCapture_Tick);
            // 
            // chkCaptureOn
            // 
            this.chkCaptureOn.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkCaptureOn.AutoSize = true;
            this.chkCaptureOn.FlatAppearance.CheckedBackColor = System.Drawing.Color.Lime;
            this.chkCaptureOn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkCaptureOn.Location = new System.Drawing.Point(1, 1);
            this.chkCaptureOn.Name = "chkCaptureOn";
            this.chkCaptureOn.Size = new System.Drawing.Size(54, 23);
            this.chkCaptureOn.TabIndex = 1;
            this.chkCaptureOn.Text = "Capture";
            this.chkCaptureOn.UseVisualStyleBackColor = true;
            this.chkCaptureOn.CheckedChanged += new System.EventHandler(this.chkCaptureOn_CheckedChanged);
            // 
            // chkAutoRefresh
            // 
            this.chkAutoRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkAutoRefresh.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkAutoRefresh.AutoSize = true;
            this.chkAutoRefresh.FlatAppearance.CheckedBackColor = System.Drawing.Color.Lime;
            this.chkAutoRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkAutoRefresh.Location = new System.Drawing.Point(754, 1);
            this.chkAutoRefresh.Name = "chkAutoRefresh";
            this.chkAutoRefresh.Size = new System.Drawing.Size(79, 23);
            this.chkAutoRefresh.TabIndex = 2;
            this.chkAutoRefresh.Text = "Auto Refresh";
            this.chkAutoRefresh.UseVisualStyleBackColor = true;
            this.chkAutoRefresh.CheckedChanged += new System.EventHandler(this.chkAutoRefresh_CheckedChanged);
            // 
            // FormGraph
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(833, 560);
            this.Controls.Add(this.chkAutoRefresh);
            this.Controls.Add(this.chkCaptureOn);
            this.Controls.Add(this.display);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormGraph";
            this.Text = "FormGrpah";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GraphLib.PlotterDisplayEx display;
        private System.Windows.Forms.Timer tmrRefresh;
        private System.Windows.Forms.Timer tmrCapture;
        private System.Windows.Forms.CheckBox chkCaptureOn;
        private System.Windows.Forms.CheckBox chkAutoRefresh;
    }
}