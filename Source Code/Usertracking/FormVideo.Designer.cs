namespace Usertracking
{
    partial class FormVideo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormVideo));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblImageSize = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.label2 = new System.Windows.Forms.Label();
            this.lblUserPosition = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblFacePosition = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblHeadRotation = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lblElevationAngle = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.numElevationAngle = new System.Windows.Forms.NumericUpDown();
            this.tmrHideElevationAngle = new System.Windows.Forms.Timer(this.components);
            this.lblMonitor = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblMonitorSize = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblMonitorResolution = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblCursorPosition = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblMouthOpening = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numElevationAngle)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(640, 480);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.button1.FlatAppearance.BorderSize = 2;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Blue;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Quartz MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(849, 540);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(96, 39);
            this.button1.TabIndex = 1;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 540);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Image Size:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // lblImageSize
            // 
            this.lblImageSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblImageSize.AutoSize = true;
            this.lblImageSize.ForeColor = System.Drawing.Color.White;
            this.lblImageSize.Location = new System.Drawing.Point(93, 540);
            this.lblImageSize.Name = "lblImageSize";
            this.lblImageSize.Size = new System.Drawing.Size(73, 13);
            this.lblImageSize.TabIndex = 3;
            this.lblImageSize.Text = "(not detected)";
            this.lblImageSize.Click += new System.EventHandler(this.lblImageSize_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(12, 556);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "User Position:";
            // 
            // lblUserPosition
            // 
            this.lblUserPosition.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblUserPosition.AutoSize = true;
            this.lblUserPosition.ForeColor = System.Drawing.Color.White;
            this.lblUserPosition.Location = new System.Drawing.Point(93, 556);
            this.lblUserPosition.Name = "lblUserPosition";
            this.lblUserPosition.Size = new System.Drawing.Size(73, 13);
            this.lblUserPosition.TabIndex = 5;
            this.lblUserPosition.Text = "(not detected)";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 200;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblFacePosition
            // 
            this.lblFacePosition.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblFacePosition.AutoSize = true;
            this.lblFacePosition.ForeColor = System.Drawing.Color.White;
            this.lblFacePosition.Location = new System.Drawing.Point(387, 523);
            this.lblFacePosition.Name = "lblFacePosition";
            this.lblFacePosition.Size = new System.Drawing.Size(73, 13);
            this.lblFacePosition.TabIndex = 7;
            this.lblFacePosition.Text = "(not detected)";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(312, 523);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Head Position:";
            // 
            // lblHeadRotation
            // 
            this.lblHeadRotation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblHeadRotation.AutoSize = true;
            this.lblHeadRotation.ForeColor = System.Drawing.Color.White;
            this.lblHeadRotation.Location = new System.Drawing.Point(387, 540);
            this.lblHeadRotation.Name = "lblHeadRotation";
            this.lblHeadRotation.Size = new System.Drawing.Size(73, 13);
            this.lblHeadRotation.TabIndex = 9;
            this.lblHeadRotation.Text = "(not detected)";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(312, 540);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Head Rotation:";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(649, 2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(296, 481);
            this.pictureBox2.TabIndex = 10;
            this.pictureBox2.TabStop = false;
            // 
            // lblElevationAngle
            // 
            this.lblElevationAngle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblElevationAngle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblElevationAngle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblElevationAngle.ForeColor = System.Drawing.Color.White;
            this.lblElevationAngle.Location = new System.Drawing.Point(93, 523);
            this.lblElevationAngle.Name = "lblElevationAngle";
            this.lblElevationAngle.Size = new System.Drawing.Size(75, 15);
            this.lblElevationAngle.TabIndex = 12;
            this.lblElevationAngle.Text = "(not detected)";
            this.lblElevationAngle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblElevationAngle.Click += new System.EventHandler(this.lblElevationAngle_Click);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(12, 523);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Elevation Angle:";
            // 
            // numElevationAngle
            // 
            this.numElevationAngle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numElevationAngle.BackColor = System.Drawing.Color.Black;
            this.numElevationAngle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numElevationAngle.ForeColor = System.Drawing.Color.White;
            this.numElevationAngle.Location = new System.Drawing.Point(94, 521);
            this.numElevationAngle.Name = "numElevationAngle";
            this.numElevationAngle.Size = new System.Drawing.Size(85, 20);
            this.numElevationAngle.TabIndex = 13;
            this.numElevationAngle.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numElevationAngle.Visible = false;
            this.numElevationAngle.ValueChanged += new System.EventHandler(this.numElevationAngle_ValueChanged);
            this.numElevationAngle.Leave += new System.EventHandler(this.numElevationAngle_Leave);
            // 
            // tmrHideElevationAngle
            // 
            this.tmrHideElevationAngle.Interval = 5000;
            this.tmrHideElevationAngle.Tick += new System.EventHandler(this.tmrHideElevationAngle_Tick);
            // 
            // lblMonitor
            // 
            this.lblMonitor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblMonitor.AutoSize = true;
            this.lblMonitor.ForeColor = System.Drawing.Color.White;
            this.lblMonitor.Location = new System.Drawing.Point(707, 523);
            this.lblMonitor.Name = "lblMonitor";
            this.lblMonitor.Size = new System.Drawing.Size(73, 13);
            this.lblMonitor.TabIndex = 15;
            this.lblMonitor.Text = "(not detected)";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(610, 523);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(45, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Monitor:";
            // 
            // lblMonitorSize
            // 
            this.lblMonitorSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblMonitorSize.AutoSize = true;
            this.lblMonitorSize.ForeColor = System.Drawing.Color.White;
            this.lblMonitorSize.Location = new System.Drawing.Point(707, 540);
            this.lblMonitorSize.Name = "lblMonitorSize";
            this.lblMonitorSize.Size = new System.Drawing.Size(73, 13);
            this.lblMonitorSize.TabIndex = 17;
            this.lblMonitorSize.Text = "(not detected)";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(610, 540);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "Size [cm]:";
            // 
            // lblMonitorResolution
            // 
            this.lblMonitorResolution.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblMonitorResolution.AutoSize = true;
            this.lblMonitorResolution.ForeColor = System.Drawing.Color.White;
            this.lblMonitorResolution.Location = new System.Drawing.Point(707, 556);
            this.lblMonitorResolution.Name = "lblMonitorResolution";
            this.lblMonitorResolution.Size = new System.Drawing.Size(73, 13);
            this.lblMonitorResolution.TabIndex = 19;
            this.lblMonitorResolution.Text = "(not detected)";
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(610, 556);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(95, 13);
            this.label9.TabIndex = 18;
            this.label9.Text = "Resolution (pixels):";
            // 
            // lblCursorPosition
            // 
            this.lblCursorPosition.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCursorPosition.AutoSize = true;
            this.lblCursorPosition.ForeColor = System.Drawing.Color.White;
            this.lblCursorPosition.Location = new System.Drawing.Point(387, 566);
            this.lblCursorPosition.Name = "lblCursorPosition";
            this.lblCursorPosition.Size = new System.Drawing.Size(73, 13);
            this.lblCursorPosition.TabIndex = 21;
            this.lblCursorPosition.Text = "(not detected)";
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(312, 566);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(80, 13);
            this.label10.TabIndex = 20;
            this.label10.Text = "Cursor Position:";
            // 
            // lblMouthOpening
            // 
            this.lblMouthOpening.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblMouthOpening.AutoSize = true;
            this.lblMouthOpening.ForeColor = System.Drawing.Color.White;
            this.lblMouthOpening.Location = new System.Drawing.Point(387, 553);
            this.lblMouthOpening.Name = "lblMouthOpening";
            this.lblMouthOpening.Size = new System.Drawing.Size(73, 13);
            this.lblMouthOpening.TabIndex = 23;
            this.lblMouthOpening.Text = "(not detected)";
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(312, 553);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(83, 13);
            this.label11.TabIndex = 22;
            this.label11.Text = "Mouth Opening:";
            // 
            // FormVideo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(964, 618);
            this.ControlBox = false;
            this.Controls.Add(this.lblMouthOpening);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.lblCursorPosition);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.lblMonitorResolution);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.lblMonitorSize);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lblMonitor);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.numElevationAngle);
            this.Controls.Add(this.lblElevationAngle);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.lblHeadRotation);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblFacePosition);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblUserPosition);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblImageSize);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(966, 620);
            this.Name = "FormVideo";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Video";
            this.Load += new System.EventHandler(this.FormVideo_Load);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.FormVideo_MouseClick);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numElevationAngle)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblImageSize;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblUserPosition;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblFacePosition;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblHeadRotation;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label lblElevationAngle;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numElevationAngle;
        private System.Windows.Forms.Timer tmrHideElevationAngle;
        private System.Windows.Forms.Label lblMonitor;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblMonitorSize;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblMonitorResolution;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblCursorPosition;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblMouthOpening;
        private System.Windows.Forms.Label label11;
    }
}