namespace Usertracking
{
    partial class FormCalibra
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCalibra));
            this.pbEye = new System.Windows.Forms.PictureBox();
            this.pbFollowMe = new System.Windows.Forms.PictureBox();
            this.butStartCalibration = new System.Windows.Forms.Button();
            this.tmrCalibrating = new System.Windows.Forms.Timer(this.components);
            this.lblCount = new System.Windows.Forms.Label();
            this.tmrPrepare = new System.Windows.Forms.Timer(this.components);
            this.lblUserFaceNotDetected = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbEye)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbFollowMe)).BeginInit();
            this.SuspendLayout();
            // 
            // pbEye
            // 
            this.pbEye.BackColor = System.Drawing.Color.Fuchsia;
            this.pbEye.ErrorImage = null;
            this.pbEye.Image = ((System.Drawing.Image)(resources.GetObject("pbEye.Image")));
            this.pbEye.Location = new System.Drawing.Point(402, 355);
            this.pbEye.Name = "pbEye";
            this.pbEye.Size = new System.Drawing.Size(101, 101);
            this.pbEye.TabIndex = 1;
            this.pbEye.TabStop = false;
            this.pbEye.Visible = false;
            // 
            // pbFollowMe
            // 
            this.pbFollowMe.BackColor = System.Drawing.Color.Black;
            this.pbFollowMe.ErrorImage = null;
            this.pbFollowMe.Image = ((System.Drawing.Image)(resources.GetObject("pbFollowMe.Image")));
            this.pbFollowMe.Location = new System.Drawing.Point(0, 0);
            this.pbFollowMe.Name = "pbFollowMe";
            this.pbFollowMe.Size = new System.Drawing.Size(101, 101);
            this.pbFollowMe.TabIndex = 2;
            this.pbFollowMe.TabStop = false;
            this.pbFollowMe.Visible = false;
            // 
            // butStartCalibration
            // 
            this.butStartCalibration.BackColor = System.Drawing.Color.Black;
            this.butStartCalibration.FlatAppearance.BorderSize = 5;
            this.butStartCalibration.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Blue;
            this.butStartCalibration.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Cyan;
            this.butStartCalibration.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butStartCalibration.Font = new System.Drawing.Font("Quartz MS", 14.25F, System.Drawing.FontStyle.Bold);
            this.butStartCalibration.ForeColor = System.Drawing.Color.White;
            this.butStartCalibration.Location = new System.Drawing.Point(336, 228);
            this.butStartCalibration.Name = "butStartCalibration";
            this.butStartCalibration.Size = new System.Drawing.Size(257, 112);
            this.butStartCalibration.TabIndex = 3;
            this.butStartCalibration.Text = "Click to start calibration!";
            this.butStartCalibration.UseVisualStyleBackColor = false;
            this.butStartCalibration.Click += new System.EventHandler(this.butStartCalibration_Click);
            // 
            // tmrCalibrating
            // 
            this.tmrCalibrating.Interval = 15;
            this.tmrCalibrating.Tick += new System.EventHandler(this.tmrCalibrating_Tick);
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 72F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCount.ForeColor = System.Drawing.Color.White;
            this.lblCount.Location = new System.Drawing.Point(1, 0);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(98, 108);
            this.lblCount.TabIndex = 4;
            this.lblCount.Text = "3";
            this.lblCount.Visible = false;
            // 
            // tmrPrepare
            // 
            this.tmrPrepare.Interval = 1000;
            this.tmrPrepare.Tick += new System.EventHandler(this.tmrPrepare_Tick);
            // 
            // lblUserFaceNotDetected
            // 
            this.lblUserFaceNotDetected.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUserFaceNotDetected.AutoSize = true;
            this.lblUserFaceNotDetected.Font = new System.Drawing.Font("Quartz MS", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserFaceNotDetected.ForeColor = System.Drawing.Color.Red;
            this.lblUserFaceNotDetected.Location = new System.Drawing.Point(30, 239);
            this.lblUserFaceNotDetected.Name = "lblUserFaceNotDetected";
            this.lblUserFaceNotDetected.Size = new System.Drawing.Size(870, 77);
            this.lblUserFaceNotDetected.TabIndex = 5;
            this.lblUserFaceNotDetected.Text = "User face not detected!";
            this.lblUserFaceNotDetected.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblUserFaceNotDetected.Visible = false;
            // 
            // FormCalibra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(939, 521);
            this.Controls.Add(this.lblUserFaceNotDetected);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.butStartCalibration);
            this.Controls.Add(this.pbFollowMe);
            this.Controls.Add(this.pbEye);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormCalibra";
            this.Text = "FormCalibra";
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormCalibra_FormClosed);
            this.Load += new System.EventHandler(this.FormCalibra_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbEye)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbFollowMe)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbEye;
        private System.Windows.Forms.PictureBox pbFollowMe;
        private System.Windows.Forms.Button butStartCalibration;
        private System.Windows.Forms.Timer tmrCalibrating;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Timer tmrPrepare;
        private System.Windows.Forms.Label lblUserFaceNotDetected;
    }
}