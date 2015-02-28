namespace Usertracking
{
    partial class FormOptions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormOptions));
            this.butClose = new System.Windows.Forms.Button();
            this.chkSeatedModeOn = new System.Windows.Forms.CheckBox();
            this.chkAutoHideMenu = new System.Windows.Forms.CheckBox();
            this.btRunCalibration = new System.Windows.Forms.Button();
            this.butShowCalibration = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // butClose
            // 
            this.butClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.butClose.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.butClose.FlatAppearance.BorderSize = 2;
            this.butClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Blue;
            this.butClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.butClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butClose.Font = new System.Drawing.Font("Quartz MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butClose.ForeColor = System.Drawing.Color.White;
            this.butClose.Location = new System.Drawing.Point(245, 318);
            this.butClose.Name = "butClose";
            this.butClose.Size = new System.Drawing.Size(108, 45);
            this.butClose.TabIndex = 1;
            this.butClose.Text = "Close";
            this.butClose.UseVisualStyleBackColor = true;
            this.butClose.Click += new System.EventHandler(this.butClose_Click);
            // 
            // chkSeatedModeOn
            // 
            this.chkSeatedModeOn.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkSeatedModeOn.BackColor = System.Drawing.Color.Coral;
            this.chkSeatedModeOn.Checked = global::Usertracking.Properties.Settings.Default.SeatedModeOn;
            this.chkSeatedModeOn.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.chkSeatedModeOn.FlatAppearance.BorderSize = 5;
            this.chkSeatedModeOn.FlatAppearance.CheckedBackColor = System.Drawing.Color.PaleGreen;
            this.chkSeatedModeOn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.chkSeatedModeOn.Font = new System.Drawing.Font("Quartz MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSeatedModeOn.Location = new System.Drawing.Point(12, 12);
            this.chkSeatedModeOn.Name = "chkSeatedModeOn";
            this.chkSeatedModeOn.Size = new System.Drawing.Size(208, 30);
            this.chkSeatedModeOn.TabIndex = 0;
            this.chkSeatedModeOn.Text = "Seated Mode [OFF]";
            this.chkSeatedModeOn.UseVisualStyleBackColor = false;
            this.chkSeatedModeOn.CheckedChanged += new System.EventHandler(this.chkSeatedModeOn_CheckedChanged);
            // 
            // chkAutoHideMenu
            // 
            this.chkAutoHideMenu.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkAutoHideMenu.BackColor = System.Drawing.Color.Coral;
            this.chkAutoHideMenu.Checked = global::Usertracking.Properties.Settings.Default.SeatedModeOn;
            this.chkAutoHideMenu.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.chkAutoHideMenu.FlatAppearance.BorderSize = 5;
            this.chkAutoHideMenu.FlatAppearance.CheckedBackColor = System.Drawing.Color.PaleGreen;
            this.chkAutoHideMenu.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.chkAutoHideMenu.Font = new System.Drawing.Font("Quartz MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAutoHideMenu.Location = new System.Drawing.Point(12, 48);
            this.chkAutoHideMenu.Name = "chkAutoHideMenu";
            this.chkAutoHideMenu.Size = new System.Drawing.Size(208, 30);
            this.chkAutoHideMenu.TabIndex = 3;
            this.chkAutoHideMenu.Text = "Auto Hide Menu [OFF]";
            this.chkAutoHideMenu.UseVisualStyleBackColor = false;
            this.chkAutoHideMenu.CheckedChanged += new System.EventHandler(this.chkAutoHideMenu_CheckedChanged);
            // 
            // btRunCalibration
            // 
            this.btRunCalibration.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Blue;
            this.btRunCalibration.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Cyan;
            this.btRunCalibration.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btRunCalibration.Font = new System.Drawing.Font("Quartz MS", 12F);
            this.btRunCalibration.ForeColor = System.Drawing.Color.White;
            this.btRunCalibration.Location = new System.Drawing.Point(226, 12);
            this.btRunCalibration.Name = "btRunCalibration";
            this.btRunCalibration.Size = new System.Drawing.Size(207, 30);
            this.btRunCalibration.TabIndex = 4;
            this.btRunCalibration.Text = "Run Calibration";
            this.btRunCalibration.UseVisualStyleBackColor = true;
            this.btRunCalibration.Click += new System.EventHandler(this.btRunCalibration_Click);
            // 
            // butShowCalibration
            // 
            this.butShowCalibration.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Blue;
            this.butShowCalibration.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Cyan;
            this.butShowCalibration.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butShowCalibration.Font = new System.Drawing.Font("Quartz MS", 12F);
            this.butShowCalibration.ForeColor = System.Drawing.Color.White;
            this.butShowCalibration.Location = new System.Drawing.Point(226, 48);
            this.butShowCalibration.Name = "butShowCalibration";
            this.butShowCalibration.Size = new System.Drawing.Size(207, 30);
            this.butShowCalibration.TabIndex = 5;
            this.butShowCalibration.Text = "Show Calibration";
            this.butShowCalibration.UseVisualStyleBackColor = true;
            this.butShowCalibration.Click += new System.EventHandler(this.butShowCalibration_Click);
            // 
            // FormOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(591, 385);
            this.ControlBox = false;
            this.Controls.Add(this.butShowCalibration);
            this.Controls.Add(this.btRunCalibration);
            this.Controls.Add(this.chkAutoHideMenu);
            this.Controls.Add(this.butClose);
            this.Controls.Add(this.chkSeatedModeOn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormOptions";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Options";
            this.Load += new System.EventHandler(this.FormOptions_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox chkSeatedModeOn;
        private System.Windows.Forms.Button butClose;
        private System.Windows.Forms.CheckBox chkAutoHideMenu;
        private System.Windows.Forms.Button btRunCalibration;
        private System.Windows.Forms.Button butShowCalibration;
    }
}