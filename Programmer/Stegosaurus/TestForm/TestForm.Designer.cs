namespace TestForm {
    partial class TestForm {
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
            this.getFileCover = new System.Windows.Forms.OpenFileDialog();
            this.getFileMessage = new System.Windows.Forms.OpenFileDialog();
            this.picCover = new System.Windows.Forms.PictureBox();
            this.picMessage = new System.Windows.Forms.PictureBox();
            this.loadCover = new System.Windows.Forms.Button();
            this.loadMessage = new System.Windows.Forms.Button();
            this.btnEncode = new System.Windows.Forms.Button();
            this.btnDecode = new System.Windows.Forms.Button();
            this.picStego = new System.Windows.Forms.PictureBox();
            this.loadStego = new System.Windows.Forms.Button();
            this.getFileStego = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.picCover)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMessage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picStego)).BeginInit();
            this.SuspendLayout();
            // 
            // getFileCover
            // 
            this.getFileCover.FileName = "Select an image to be the Cover";
            this.getFileCover.FileOk += new System.ComponentModel.CancelEventHandler(this.getFileCover_FileOk);
            // 
            // getFileMessage
            // 
            this.getFileMessage.FileName = "Select an image with half the width and height and width of the Cover image";
            this.getFileMessage.FileOk += new System.ComponentModel.CancelEventHandler(this.getFileMessage_FileOk);
            // 
            // picCover
            // 
            this.picCover.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picCover.Location = new System.Drawing.Point(12, 12);
            this.picCover.Name = "picCover";
            this.picCover.Size = new System.Drawing.Size(216, 216);
            this.picCover.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picCover.TabIndex = 0;
            this.picCover.TabStop = false;
            // 
            // picMessage
            // 
            this.picMessage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picMessage.Location = new System.Drawing.Point(234, 33);
            this.picMessage.Name = "picMessage";
            this.picMessage.Size = new System.Drawing.Size(108, 108);
            this.picMessage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picMessage.TabIndex = 1;
            this.picMessage.TabStop = false;
            // 
            // loadCover
            // 
            this.loadCover.Location = new System.Drawing.Point(77, 234);
            this.loadCover.Name = "loadCover";
            this.loadCover.Size = new System.Drawing.Size(86, 23);
            this.loadCover.TabIndex = 2;
            this.loadCover.Text = "Load image";
            this.loadCover.UseVisualStyleBackColor = true;
            this.loadCover.Click += new System.EventHandler(this.loadCover_Click);
            // 
            // loadMessage
            // 
            this.loadMessage.Location = new System.Drawing.Point(245, 147);
            this.loadMessage.Name = "loadMessage";
            this.loadMessage.Size = new System.Drawing.Size(86, 23);
            this.loadMessage.TabIndex = 3;
            this.loadMessage.Text = "Load image";
            this.loadMessage.UseVisualStyleBackColor = true;
            this.loadMessage.Click += new System.EventHandler(this.loadMessage_Click);
            // 
            // btnEncode
            // 
            this.btnEncode.Enabled = false;
            this.btnEncode.Location = new System.Drawing.Point(245, 176);
            this.btnEncode.Name = "btnEncode";
            this.btnEncode.Size = new System.Drawing.Size(86, 23);
            this.btnEncode.TabIndex = 4;
            this.btnEncode.Text = "Encode";
            this.btnEncode.UseVisualStyleBackColor = true;
            this.btnEncode.Click += new System.EventHandler(this.Encode_Click);
            // 
            // btnDecode
            // 
            this.btnDecode.Enabled = false;
            this.btnDecode.Location = new System.Drawing.Point(245, 205);
            this.btnDecode.Name = "btnDecode";
            this.btnDecode.Size = new System.Drawing.Size(86, 23);
            this.btnDecode.TabIndex = 5;
            this.btnDecode.Text = "Decode";
            this.btnDecode.UseVisualStyleBackColor = true;
            this.btnDecode.Click += new System.EventHandler(this.Decode_Click);
            // 
            // picStego
            // 
            this.picStego.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picStego.Location = new System.Drawing.Point(348, 12);
            this.picStego.Name = "picStego";
            this.picStego.Size = new System.Drawing.Size(216, 216);
            this.picStego.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picStego.TabIndex = 6;
            this.picStego.TabStop = false;
            // 
            // loadStego
            // 
            this.loadStego.Location = new System.Drawing.Point(414, 234);
            this.loadStego.Name = "loadStego";
            this.loadStego.Size = new System.Drawing.Size(86, 23);
            this.loadStego.TabIndex = 7;
            this.loadStego.Text = "Load image";
            this.loadStego.UseVisualStyleBackColor = true;
            this.loadStego.Click += new System.EventHandler(this.loadStego_Click);
            // 
            // getFileStego
            // 
            this.getFileStego.FileName = "Select an Encodeed photo to extract a photo from";
            this.getFileStego.FileOk += new System.ComponentModel.CancelEventHandler(this.getFileStego_FileOk);
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(576, 262);
            this.Controls.Add(this.loadStego);
            this.Controls.Add(this.picStego);
            this.Controls.Add(this.btnDecode);
            this.Controls.Add(this.btnEncode);
            this.Controls.Add(this.loadMessage);
            this.Controls.Add(this.loadCover);
            this.Controls.Add(this.picMessage);
            this.Controls.Add(this.picCover);
            this.Name = "TestForm";
            this.Text = "TestForm";
            ((System.ComponentModel.ISupportInitialize)(this.picCover)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMessage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picStego)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog getFileCover;
        private System.Windows.Forms.OpenFileDialog getFileMessage;
        private System.Windows.Forms.PictureBox picCover;
        private System.Windows.Forms.PictureBox picMessage;
        private System.Windows.Forms.Button loadCover;
        private System.Windows.Forms.Button loadMessage;
        private System.Windows.Forms.Button btnEncode;
        private System.Windows.Forms.Button btnDecode;
        private System.Windows.Forms.PictureBox picStego;
        private System.Windows.Forms.Button loadStego;
        private System.Windows.Forms.OpenFileDialog getFileStego;
    }
}

