namespace LastCard
{
    partial class Form1
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
            this.tablePictureBox = new System.Windows.Forms.PictureBox();
            this.startButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.tablePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // tablePictureBox
            // 
            this.tablePictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tablePictureBox.BackColor = System.Drawing.Color.Green;
            this.tablePictureBox.Location = new System.Drawing.Point(12, 14);
            this.tablePictureBox.Name = "tablePictureBox";
            this.tablePictureBox.Padding = new System.Windows.Forms.Padding(20);
            this.tablePictureBox.Size = new System.Drawing.Size(988, 519);
            this.tablePictureBox.TabIndex = 0;
            this.tablePictureBox.TabStop = false;
            this.tablePictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.tablePictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.tablePictureBox_MouseMove);
            this.tablePictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tablePictureBox_MouseUp);
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(12, 553);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.TabIndex = 1;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(1014, 619);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.tablePictureBox);
            this.MinimumSize = new System.Drawing.Size(1030, 658);
            this.Name = "Form1";
            this.Text = "Last Card";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tablePictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox tablePictureBox;
        private System.Windows.Forms.Button startButton;
    }
}

