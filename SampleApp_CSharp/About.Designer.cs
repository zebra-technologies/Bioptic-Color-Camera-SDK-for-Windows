namespace CameraSDKSampleApp
{
    partial class About
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(About));
            this.Button_ok = new System.Windows.Forms.Button();
            this.iconZebra = new System.Windows.Forms.PictureBox();
            this.fistLine = new System.Windows.Forms.Label();
            this.secondLine = new System.Windows.Forms.Label();
            this.thirdLine = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.iconZebra)).BeginInit();
            this.SuspendLayout();
            // 
            // Button_ok
            // 
            this.Button_ok.Location = new System.Drawing.Point(371, 109);
            this.Button_ok.Name = "Button_ok";
            this.Button_ok.Size = new System.Drawing.Size(75, 23);
            this.Button_ok.TabIndex = 0;
            this.Button_ok.Text = "OK";
            this.Button_ok.UseVisualStyleBackColor = true;
            this.Button_ok.Click += new System.EventHandler(this.Button_ok_Click);
            // 
            // iconZebra
            // 
            this.iconZebra.Image = ((System.Drawing.Image)(resources.GetObject("iconZebra.Image")));
            this.iconZebra.Location = new System.Drawing.Point(12, 22);
            this.iconZebra.Name = "iconZebra";
            this.iconZebra.Size = new System.Drawing.Size(52, 51);
            this.iconZebra.TabIndex = 1;
            this.iconZebra.TabStop = false;
            // 
            // fistLine
            // 
            this.fistLine.AutoSize = true;
            this.fistLine.Location = new System.Drawing.Point(95, 22);
            this.fistLine.Name = "fistLine";
            this.fistLine.Size = new System.Drawing.Size(0, 13);
            this.fistLine.TabIndex = 2;
            // 
            // secondLine
            // 
            this.secondLine.AutoSize = true;
            this.secondLine.Location = new System.Drawing.Point(95, 48);
            this.secondLine.Name = "secondLine";
            this.secondLine.Size = new System.Drawing.Size(0, 13);
            this.secondLine.TabIndex = 3;
            // 
            // thirdLine
            // 
            this.thirdLine.AutoSize = true;
            this.thirdLine.Location = new System.Drawing.Point(95, 76);
            this.thirdLine.Name = "thirdLine";
            this.thirdLine.Size = new System.Drawing.Size(0, 13);
            this.thirdLine.TabIndex = 4;
            // 
            // About
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(468, 144);
            this.Controls.Add(this.thirdLine);
            this.Controls.Add(this.secondLine);
            this.Controls.Add(this.fistLine);
            this.Controls.Add(this.iconZebra);
            this.Controls.Add(this.Button_ok);
            this.Name = "About";
            this.Text = "About";
            this.Load += new System.EventHandler(this.About_Load);
            ((System.ComponentModel.ISupportInitialize)(this.iconZebra)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
            this.Text = "About Zebra Camera SDK Sample Application";

        }

        #endregion

        private System.Windows.Forms.Button Button_ok;
        private System.Windows.Forms.PictureBox iconZebra;
        private System.Windows.Forms.Label fistLine;
        private System.Windows.Forms.Label secondLine;
        private System.Windows.Forms.Label thirdLine;
    }
}