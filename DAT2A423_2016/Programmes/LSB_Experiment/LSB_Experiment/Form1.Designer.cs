namespace WindowsFormsApplication1 {
    partial class Form1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.getFileVessel = new System.Windows.Forms.OpenFileDialog();
            this.getFilePlain = new System.Windows.Forms.OpenFileDialog();
            this.picVessel = new System.Windows.Forms.PictureBox();
            this.picPlain = new System.Windows.Forms.PictureBox();
            this.loadVessel = new System.Windows.Forms.Button();
            this.loadPlain = new System.Windows.Forms.Button();
            this.btnEncrypt = new System.Windows.Forms.Button();
            this.btnDecrypt = new System.Windows.Forms.Button();
            this.picCrypto = new System.Windows.Forms.PictureBox();
            this.loadCrypto = new System.Windows.Forms.Button();
            this.getFileCrypto = new System.Windows.Forms.OpenFileDialog();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.picVessel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPlain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCrypto)).BeginInit();
            this.SuspendLayout();
            // 
            // getFileVessel
            // 
            this.getFileVessel.FileName = "Select an image to be the vessel";
            this.getFileVessel.FileOk += new System.ComponentModel.CancelEventHandler(this.getFileVessel_FileOk);
            // 
            // getFilePlain
            // 
            this.getFilePlain.FileName = "Select an image with half the width and height and width of the vessel image";
            this.getFilePlain.FileOk += new System.ComponentModel.CancelEventHandler(this.getFilePlain_FileOk);
            // 
            // picVessel
            // 
            this.picVessel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picVessel.Location = new System.Drawing.Point(12, 12);
            this.picVessel.Name = "picVessel";
            this.picVessel.Size = new System.Drawing.Size(216, 216);
            this.picVessel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picVessel.TabIndex = 0;
            this.picVessel.TabStop = false;
            // 
            // picPlain
            // 
            this.picPlain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picPlain.Location = new System.Drawing.Point(234, 33);
            this.picPlain.Name = "picPlain";
            this.picPlain.Size = new System.Drawing.Size(108, 108);
            this.picPlain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picPlain.TabIndex = 1;
            this.picPlain.TabStop = false;
            // 
            // loadVessel
            // 
            this.loadVessel.Location = new System.Drawing.Point(77, 234);
            this.loadVessel.Name = "loadVessel";
            this.loadVessel.Size = new System.Drawing.Size(86, 23);
            this.loadVessel.TabIndex = 2;
            this.loadVessel.Text = "Load image";
            this.loadVessel.UseVisualStyleBackColor = true;
            this.loadVessel.Click += new System.EventHandler(this.loadVessel_Click);
            // 
            // loadPlain
            // 
            this.loadPlain.Location = new System.Drawing.Point(245, 147);
            this.loadPlain.Name = "loadPlain";
            this.loadPlain.Size = new System.Drawing.Size(86, 23);
            this.loadPlain.TabIndex = 3;
            this.loadPlain.Text = "Load image";
            this.loadPlain.UseVisualStyleBackColor = true;
            this.loadPlain.Click += new System.EventHandler(this.loadPlain_Click);
            // 
            // btnEncrypt
            // 
            this.btnEncrypt.Enabled = false;
            this.btnEncrypt.Location = new System.Drawing.Point(245, 176);
            this.btnEncrypt.Name = "btnEncrypt";
            this.btnEncrypt.Size = new System.Drawing.Size(86, 23);
            this.btnEncrypt.TabIndex = 4;
            this.btnEncrypt.Text = "Encrypt";
            this.btnEncrypt.UseVisualStyleBackColor = true;
            this.btnEncrypt.Click += new System.EventHandler(this.encrypt_Click);
            // 
            // btnDecrypt
            // 
            this.btnDecrypt.Enabled = false;
            this.btnDecrypt.Location = new System.Drawing.Point(245, 205);
            this.btnDecrypt.Name = "btnDecrypt";
            this.btnDecrypt.Size = new System.Drawing.Size(86, 23);
            this.btnDecrypt.TabIndex = 5;
            this.btnDecrypt.Text = "Decrypt";
            this.btnDecrypt.UseVisualStyleBackColor = true;
            this.btnDecrypt.Click += new System.EventHandler(this.decrypt_Click);
            // 
            // picCrypto
            // 
            this.picCrypto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picCrypto.Location = new System.Drawing.Point(348, 12);
            this.picCrypto.Name = "picCrypto";
            this.picCrypto.Size = new System.Drawing.Size(216, 216);
            this.picCrypto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picCrypto.TabIndex = 6;
            this.picCrypto.TabStop = false;
            // 
            // loadCrypto
            // 
            this.loadCrypto.Location = new System.Drawing.Point(414, 234);
            this.loadCrypto.Name = "loadCrypto";
            this.loadCrypto.Size = new System.Drawing.Size(86, 23);
            this.loadCrypto.TabIndex = 7;
            this.loadCrypto.Text = "Load image";
            this.loadCrypto.UseVisualStyleBackColor = true;
            this.loadCrypto.Click += new System.EventHandler(this.loadCrypto_Click);
            // 
            // getFileCrypto
            // 
            this.getFileCrypto.FileName = "Select an encrypted photo to extract a photo from";
            this.getFileCrypto.FileOk += new System.ComponentModel.CancelEventHandler(this.getFileCrypto_FileOk);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(169, 234);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(239, 23);
            this.progressBar.Step = 1;
            this.progressBar.TabIndex = 8;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(576, 262);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.loadCrypto);
            this.Controls.Add(this.picCrypto);
            this.Controls.Add(this.btnDecrypt);
            this.Controls.Add(this.btnEncrypt);
            this.Controls.Add(this.loadPlain);
            this.Controls.Add(this.loadVessel);
            this.Controls.Add(this.picPlain);
            this.Controls.Add(this.picVessel);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.picVessel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPlain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCrypto)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog getFileVessel;
        private System.Windows.Forms.OpenFileDialog getFilePlain;
        private System.Windows.Forms.PictureBox picVessel;
        private System.Windows.Forms.PictureBox picPlain;
        private System.Windows.Forms.Button loadVessel;
        private System.Windows.Forms.Button loadPlain;
        private System.Windows.Forms.Button btnEncrypt;
        private System.Windows.Forms.Button btnDecrypt;
        private System.Windows.Forms.PictureBox picCrypto;
        private System.Windows.Forms.Button loadCrypto;
        private System.Windows.Forms.OpenFileDialog getFileCrypto;
        private System.Windows.Forms.ProgressBar progressBar;
    }
}

