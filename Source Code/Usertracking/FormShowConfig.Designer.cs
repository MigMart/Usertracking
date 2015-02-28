namespace Usertracking
{
    partial class FormShowConfig
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.butRecal = new System.Windows.Forms.Button();
            this.lblRecalc = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1149, 536);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // 
            // butRecal
            // 
            this.butRecal.Location = new System.Drawing.Point(1015, 13);
            this.butRecal.Name = "butRecal";
            this.butRecal.Size = new System.Drawing.Size(103, 37);
            this.butRecal.TabIndex = 1;
            this.butRecal.Text = "Recalc full error matrix";
            this.butRecal.UseVisualStyleBackColor = true;
            this.butRecal.Click += new System.EventHandler(this.butRecal_Click);
            // 
            // lblRecalc
            // 
            this.lblRecalc.AutoSize = true;
            this.lblRecalc.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecalc.Location = new System.Drawing.Point(415, 86);
            this.lblRecalc.Name = "lblRecalc";
            this.lblRecalc.Size = new System.Drawing.Size(275, 25);
            this.lblRecalc.TabIndex = 2;
            this.lblRecalc.Text = "Recalculating error matrix...";
            this.lblRecalc.Visible = false;
            // 
            // FormShowConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1149, 536);
            this.Controls.Add(this.lblRecalc);
            this.Controls.Add(this.butRecal);
            this.Controls.Add(this.pictureBox1);
            this.Name = "FormShowConfig";
            this.Text = "FormShowConfig";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormShowConfig_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button butRecal;
        private System.Windows.Forms.Label lblRecalc;
    }
}