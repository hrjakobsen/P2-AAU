namespace SteGUI {
    partial class StegosaurusForm {
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StegosaurusForm));
            this.getFileStego = new System.Windows.Forms.OpenFileDialog();
            this.line = new System.Windows.Forms.Label();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.viewOptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ttStegosaurus = new System.Windows.Forms.ToolTip(this.components);
            this.picInput = new System.Windows.Forms.PictureBox();
            this.btnLoadMessageFile = new System.Windows.Forms.Button();
            this.btnLoadInputImage = new System.Windows.Forms.Button();
            this.picResult = new System.Windows.Forms.PictureBox();
            this.tbarEncodingQuality = new System.Windows.Forms.TrackBar();
            this.btnRemoveMsgFile = new System.Windows.Forms.Button();
            this.tbMessage = new System.Windows.Forms.TextBox();
            this.tbMessageFilePath = new System.Windows.Forms.TextBox();
            this.lblEncodingQuality = new System.Windows.Forms.Label();
            this.lblEncodingQualityValue = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnProceed = new System.Windows.Forms.Button();
            this.rdioEncode = new System.Windows.Forms.RadioButton();
            this.rdioDecode = new System.Windows.Forms.RadioButton();
            this.getFileInputImage = new System.Windows.Forms.OpenFileDialog();
            this.GetFileMessage = new System.Windows.Forms.OpenFileDialog();
            this.lblProcessing = new System.Windows.Forms.Label();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbarEncodingQuality)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // line
            // 
            this.line.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.line.Location = new System.Drawing.Point(4, 25);
            this.line.Name = "line";
            this.line.Size = new System.Drawing.Size(608, 2);
            this.line.TabIndex = 24;
            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewOptionsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(616, 24);
            this.menuStrip.TabIndex = 23;
            this.menuStrip.Text = "menuStrip";
            // 
            // viewOptionsToolStripMenuItem
            // 
            this.viewOptionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewOptions});
            this.viewOptionsToolStripMenuItem.Name = "viewOptionsToolStripMenuItem";
            this.viewOptionsToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewOptionsToolStripMenuItem.Text = "View";
            // 
            // viewOptions
            // 
            this.viewOptions.Name = "viewOptions";
            this.viewOptions.Size = new System.Drawing.Size(116, 22);
            this.viewOptions.Text = "Options";
            this.viewOptions.Click += new System.EventHandler(this.viewOptionsToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showHelpToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // showHelpToolStripMenuItem
            // 
            this.showHelpToolStripMenuItem.Name = "showHelpToolStripMenuItem";
            this.showHelpToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.showHelpToolStripMenuItem.Text = "Show help";
            this.showHelpToolStripMenuItem.Click += new System.EventHandler(this.showHelpToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // picInput
            // 
            this.picInput.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picInput.Location = new System.Drawing.Point(12, 32);
            this.picInput.Name = "picInput";
            this.picInput.Size = new System.Drawing.Size(216, 216);
            this.picInput.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picInput.TabIndex = 29;
            this.picInput.TabStop = false;
            this.ttStegosaurus.SetToolTip(this.picInput, "Input-image");
            // 
            // btnLoadMessageFile
            // 
            this.btnLoadMessageFile.Enabled = false;
            this.btnLoadMessageFile.Location = new System.Drawing.Point(251, 151);
            this.btnLoadMessageFile.Name = "btnLoadMessageFile";
            this.btnLoadMessageFile.Size = new System.Drawing.Size(86, 23);
            this.btnLoadMessageFile.TabIndex = 32;
            this.btnLoadMessageFile.Text = "Load file";
            this.ttStegosaurus.SetToolTip(this.btnLoadMessageFile, "Choose the data you would like to hide\r\ninside your cover-image");
            this.btnLoadMessageFile.UseVisualStyleBackColor = true;
            this.btnLoadMessageFile.Click += new System.EventHandler(this.btnLoadMessageFile_Click);
            // 
            // btnLoadInputImage
            // 
            this.btnLoadInputImage.Location = new System.Drawing.Point(77, 255);
            this.btnLoadInputImage.Name = "btnLoadInputImage";
            this.btnLoadInputImage.Size = new System.Drawing.Size(86, 23);
            this.btnLoadInputImage.TabIndex = 31;
            this.btnLoadInputImage.Text = "Load image";
            this.ttStegosaurus.SetToolTip(this.btnLoadInputImage, "Choose the image you would like to hide\r\ndata in (cover-image) or retrieve data \r" +
        "\nfrom (decode)");
            this.btnLoadInputImage.UseVisualStyleBackColor = true;
            this.btnLoadInputImage.Click += new System.EventHandler(this.btnLoadInputImage_Click);
            // 
            // picResult
            // 
            this.picResult.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picResult.Location = new System.Drawing.Point(386, 32);
            this.picResult.Name = "picResult";
            this.picResult.Size = new System.Drawing.Size(216, 216);
            this.picResult.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picResult.TabIndex = 35;
            this.picResult.TabStop = false;
            this.ttStegosaurus.SetToolTip(this.picResult, "Result-image");
            // 
            // tbarEncodingQuality
            // 
            this.tbarEncodingQuality.Location = new System.Drawing.Point(249, 265);
            this.tbarEncodingQuality.Maximum = 100;
            this.tbarEncodingQuality.Name = "tbarEncodingQuality";
            this.tbarEncodingQuality.Size = new System.Drawing.Size(114, 45);
            this.tbarEncodingQuality.TabIndex = 36;
            this.tbarEncodingQuality.TickStyle = System.Windows.Forms.TickStyle.None;
            this.ttStegosaurus.SetToolTip(this.tbarEncodingQuality, "Set the encoding quality from which\r\nthe amount of data you can hide varries");
            this.tbarEncodingQuality.ValueChanged += new System.EventHandler(this.tbarEncodingQuality_ValueChanged);
            // 
            // btnRemoveMsgFile
            // 
            this.btnRemoveMsgFile.Location = new System.Drawing.Point(343, 151);
            this.btnRemoveMsgFile.Name = "btnRemoveMsgFile";
            this.btnRemoveMsgFile.Size = new System.Drawing.Size(26, 23);
            this.btnRemoveMsgFile.TabIndex = 41;
            this.btnRemoveMsgFile.Text = "X";
            this.ttStegosaurus.SetToolTip(this.btnRemoveMsgFile, "Remove message file");
            this.btnRemoveMsgFile.UseVisualStyleBackColor = true;
            this.btnRemoveMsgFile.Click += new System.EventHandler(this.btnRemoveMsgFile_Click);
            // 
            // tbMessage
            // 
            this.tbMessage.Enabled = false;
            this.tbMessage.ForeColor = System.Drawing.SystemColors.MenuText;
            this.tbMessage.Location = new System.Drawing.Point(238, 42);
            this.tbMessage.Multiline = true;
            this.tbMessage.Name = "tbMessage";
            this.tbMessage.Size = new System.Drawing.Size(142, 72);
            this.tbMessage.TabIndex = 40;
            this.tbMessage.Text = "Enter the message you would like to encode into your image.";
            this.tbMessage.TextChanged += new System.EventHandler(this.tbMessage_TextChanged);
            this.tbMessage.Leave += new System.EventHandler(this.tbMessage_Leave);
            this.tbMessage.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tbMessage_MouseDoubleClick);
            // 
            // tbMessageFilePath
            // 
            this.tbMessageFilePath.Enabled = false;
            this.tbMessageFilePath.Location = new System.Drawing.Point(252, 125);
            this.tbMessageFilePath.Name = "tbMessageFilePath";
            this.tbMessageFilePath.Size = new System.Drawing.Size(117, 20);
            this.tbMessageFilePath.TabIndex = 38;
            this.tbMessageFilePath.Text = "Message file";
            // 
            // lblEncodingQuality
            // 
            this.lblEncodingQuality.AutoSize = true;
            this.lblEncodingQuality.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblEncodingQuality.Location = new System.Drawing.Point(274, 251);
            this.lblEncodingQuality.Name = "lblEncodingQuality";
            this.lblEncodingQuality.Size = new System.Drawing.Size(42, 13);
            this.lblEncodingQuality.TabIndex = 37;
            this.lblEncodingQuality.Text = "Quality:";
            // 
            // lblEncodingQualityValue
            // 
            this.lblEncodingQualityValue.AutoSize = true;
            this.lblEncodingQualityValue.Location = new System.Drawing.Point(318, 251);
            this.lblEncodingQualityValue.Name = "lblEncodingQualityValue";
            this.lblEncodingQualityValue.Size = new System.Drawing.Size(13, 13);
            this.lblEncodingQualityValue.TabIndex = 28;
            this.lblEncodingQualityValue.Text = "0";
            this.lblEncodingQualityValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnProceed);
            this.groupBox2.Controls.Add(this.rdioEncode);
            this.groupBox2.Controls.Add(this.rdioDecode);
            this.groupBox2.Location = new System.Drawing.Point(238, 176);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(142, 72);
            this.groupBox2.TabIndex = 27;
            this.groupBox2.TabStop = false;
            // 
            // btnProceed
            // 
            this.btnProceed.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnProceed.Enabled = false;
            this.btnProceed.Location = new System.Drawing.Point(28, 41);
            this.btnProceed.Name = "btnProceed";
            this.btnProceed.Size = new System.Drawing.Size(86, 22);
            this.btnProceed.TabIndex = 2;
            this.btnProceed.Text = "Procceed";
            this.btnProceed.UseVisualStyleBackColor = false;
            this.btnProceed.Click += new System.EventHandler(this.btnProceed_Click);
            // 
            // rdioEncode
            // 
            this.rdioEncode.AutoSize = true;
            this.rdioEncode.Location = new System.Drawing.Point(8, 14);
            this.rdioEncode.Name = "rdioEncode";
            this.rdioEncode.Size = new System.Drawing.Size(62, 17);
            this.rdioEncode.TabIndex = 0;
            this.rdioEncode.TabStop = true;
            this.rdioEncode.Text = "Encode";
            this.rdioEncode.UseVisualStyleBackColor = true;
            this.rdioEncode.CheckedChanged += new System.EventHandler(this.rdioEncode_CheckedChanged);
            // 
            // rdioDecode
            // 
            this.rdioDecode.AutoSize = true;
            this.rdioDecode.Location = new System.Drawing.Point(76, 14);
            this.rdioDecode.Name = "rdioDecode";
            this.rdioDecode.Size = new System.Drawing.Size(63, 17);
            this.rdioDecode.TabIndex = 1;
            this.rdioDecode.TabStop = true;
            this.rdioDecode.Text = "Decode";
            this.rdioDecode.UseVisualStyleBackColor = true;
            this.rdioDecode.CheckedChanged += new System.EventHandler(this.rdioEncode_CheckedChanged);
            // 
            // getFileInputImage
            // 
            this.getFileInputImage.FileName = "Select an image to be the Cover";
            this.getFileInputImage.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png, *.PNG, *.BMP, *.DIB, *.RLE, *.G" +
    "IF, *.TIF, *.TIFF) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png; *.PNG; *.BMP; *.DIB; *" +
    ".RLE; *.GIF; *.TIF; *.TIFF;";
            this.getFileInputImage.Title = "Select an image to be the Cover";
            this.getFileInputImage.FileOk += new System.ComponentModel.CancelEventHandler(this.getFileInputImage_FileOk);
            // 
            // GetFileMessage
            // 
            this.GetFileMessage.FileName = "Select a file to be the message";
            this.GetFileMessage.FileOk += new System.ComponentModel.CancelEventHandler(this.GetFileMessage_FileOk);
            // 
            // lblProcessing
            // 
            this.lblProcessing.AutoSize = true;
            this.lblProcessing.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblProcessing.Location = new System.Drawing.Point(455, 269);
            this.lblProcessing.Name = "lblProcessing";
            this.lblProcessing.Size = new System.Drawing.Size(0, 13);
            this.lblProcessing.TabIndex = 42;
            // 
            // StegosaurusForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(616, 295);
            this.Controls.Add(this.lblProcessing);
            this.Controls.Add(this.btnRemoveMsgFile);
            this.Controls.Add(this.tbMessage);
            this.Controls.Add(this.line);
            this.Controls.Add(this.tbMessageFilePath);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.lblEncodingQuality);
            this.Controls.Add(this.picInput);
            this.Controls.Add(this.tbarEncodingQuality);
            this.Controls.Add(this.lblEncodingQualityValue);
            this.Controls.Add(this.picResult);
            this.Controls.Add(this.btnLoadMessageFile);
            this.Controls.Add(this.btnLoadInputImage);
            this.Controls.Add(this.groupBox2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "StegosaurusForm";
            this.Text = "Stegosaurus";
            this.Load += new System.EventHandler(this.StegosaurusForm_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbarEncodingQuality)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog getFileStego;
        private System.Windows.Forms.Label line;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem viewOptionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewOptions;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showHelpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolTip ttStegosaurus;
        private System.Windows.Forms.TextBox tbMessageFilePath;
        private System.Windows.Forms.Label lblEncodingQuality;
        private System.Windows.Forms.PictureBox picInput;
        private System.Windows.Forms.Label lblEncodingQualityValue;
        private System.Windows.Forms.Button btnLoadMessageFile;
        private System.Windows.Forms.Button btnLoadInputImage;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnProceed;
        private System.Windows.Forms.RadioButton rdioEncode;
        private System.Windows.Forms.RadioButton rdioDecode;
        private System.Windows.Forms.PictureBox picResult;
        private System.Windows.Forms.OpenFileDialog getFileInputImage;
        private System.Windows.Forms.OpenFileDialog GetFileMessage;
        private System.Windows.Forms.TextBox tbMessage;
        private System.Windows.Forms.TrackBar tbarEncodingQuality;
        private System.Windows.Forms.Button btnRemoveMsgFile;
        private System.Windows.Forms.Label lblProcessing;
    }
}

