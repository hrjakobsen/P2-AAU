namespace TestOfImageEncoder {
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.EncodedImagePicker = new System.Windows.Forms.OpenFileDialog();
            this.EncodedToPicker = new System.Windows.Forms.OpenFileDialog();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.ExtractPicker = new System.Windows.Forms.OpenFileDialog();
            this.SaveEncoded = new System.Windows.Forms.SaveFileDialog();
            this.SaveExtracted = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(178, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Choose image to be encoded";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(13, 42);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(178, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Choose image to be encoded in";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(12, 71);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(179, 23);
            this.button3.TabIndex = 2;
            this.button3.Text = "Start encoding";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // EncodedImagePicker
            // 
            this.EncodedImagePicker.FileOk += new System.ComponentModel.CancelEventHandler(this.EncodedImagePicker_FileOk);
            // 
            // EncodedToPicker
            // 
            this.EncodedToPicker.FileOk += new System.ComponentModel.CancelEventHandler(this.EncodedToPicker_FileOk);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(13, 142);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(178, 23);
            this.button4.TabIndex = 3;
            this.button4.Text = "Choose File to extract from";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(12, 171);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(179, 23);
            this.button5.TabIndex = 4;
            this.button5.Text = "Start Extracting";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // ExtractPicker
            // 
            this.ExtractPicker.FileOk += new System.ComponentModel.CancelEventHandler(this.ExtractPicker_FileOk);
            // 
            // SaveEncoded
            // 
            this.SaveEncoded.DefaultExt = "bmp";
            this.SaveEncoded.FileOk += new System.ComponentModel.CancelEventHandler(this.SaveEncoded_FileOk);
            // 
            // SaveExtracted
            // 
            this.SaveExtracted.DefaultExt = "bmp";
            this.SaveExtracted.FileOk += new System.ComponentModel.CancelEventHandler(this.SaveExtracted_FileOk);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Encoder and decoder of images";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.OpenFileDialog EncodedImagePicker;
        private System.Windows.Forms.OpenFileDialog EncodedToPicker;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.OpenFileDialog ExtractPicker;
        private System.Windows.Forms.SaveFileDialog SaveEncoded;
        private System.Windows.Forms.SaveFileDialog SaveExtracted;
    }
}

