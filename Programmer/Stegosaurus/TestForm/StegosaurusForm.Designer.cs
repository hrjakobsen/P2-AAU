namespace TestForm {
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
            this.getFileInputLSB = new System.Windows.Forms.OpenFileDialog();
            this.getFileMessageLSB = new System.Windows.Forms.OpenFileDialog();
            this.getFileStego = new System.Windows.Forms.OpenFileDialog();
            this.line = new System.Windows.Forms.Label();
            this.picResult = new System.Windows.Forms.PictureBox();
            this.Decode = new System.Windows.Forms.Button();
            this.Encode = new System.Windows.Forms.Button();
            this.btnLoadMessage = new System.Windows.Forms.Button();
            this.btnLoadInput = new System.Windows.Forms.Button();
            this.picMessage = new System.Windows.Forms.PictureBox();
            this.picInput = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.showHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rdioDecode = new System.Windows.Forms.RadioButton();
            this.rdioEncode = new System.Windows.Forms.RadioButton();
            this.btnProceed = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ttStegosaurus = new System.Windows.Forms.ToolTip(this.components);
            this.picGTInput = new System.Windows.Forms.PictureBox();
            this.btnGTLoadMessageFile = new System.Windows.Forms.Button();
            this.btnGTLoadInput = new System.Windows.Forms.Button();
            this.picGTResult = new System.Windows.Forms.PictureBox();
            this.tbarGTEncodingQuality = new System.Windows.Forms.TrackBar();
            this.tcMethod = new System.Windows.Forms.TabControl();
            this.tpMethodGT = new System.Windows.Forms.TabPage();
            this.tbGTMessage = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.tbGTMessageFilePath = new System.Windows.Forms.TextBox();
            this.lblGTEncodingQuality = new System.Windows.Forms.Label();
            this.lblGTEncodingQualityValue = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnGTProceed = new System.Windows.Forms.Button();
            this.rdioGTEncode = new System.Windows.Forms.RadioButton();
            this.rdioGTDecode = new System.Windows.Forms.RadioButton();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.tpMethodLSB = new System.Windows.Forms.TabPage();
            this.getFileInputGT = new System.Windows.Forms.OpenFileDialog();
            this.GetFileMessageGT = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.picResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMessage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picInput)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picGTInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picGTResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbarGTEncodingQuality)).BeginInit();
            this.tcMethod.SuspendLayout();
            this.tpMethodGT.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tpMethodLSB.SuspendLayout();
            this.SuspendLayout();
            // 
            // getFileInputLSB
            // 
            this.getFileInputLSB.FileName = "Select an image to be the Cover";
            this.getFileInputLSB.FileOk += new System.ComponentModel.CancelEventHandler(this.getFileInput_FileOk);
            // 
            // getFileMessageLSB
            // 
            this.getFileMessageLSB.FileName = "Select an image with half the width and height and width of the Cover image";
            this.getFileMessageLSB.FileOk += new System.ComponentModel.CancelEventHandler(this.getFileMessage_FileOk);
            // 
            // getFileStego
            // 
            this.getFileStego.FileName = "Select an Encodeed photo to extract a photo from";
            // 
            // line
            // 
            this.line.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.line.Location = new System.Drawing.Point(4, 25);
            this.line.Name = "line";
            this.line.Size = new System.Drawing.Size(615, 2);
            this.line.TabIndex = 24;
            // 
            // picResult
            // 
            this.picResult.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picResult.Location = new System.Drawing.Point(385, 6);
            this.picResult.Name = "picResult";
            this.picResult.Size = new System.Drawing.Size(216, 216);
            this.picResult.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picResult.TabIndex = 22;
            this.picResult.TabStop = false;
            this.ttStegosaurus.SetToolTip(this.picResult, "Result-image");
            // 
            // Decode
            // 
            this.Decode.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.Decode.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Decode.Location = new System.Drawing.Point(239, 199);
            this.Decode.Name = "Decode";
            this.Decode.Size = new System.Drawing.Size(86, 23);
            this.Decode.TabIndex = 21;
            this.Decode.Text = "Decode";
            this.Decode.UseVisualStyleBackColor = false;
            // 
            // Encode
            // 
            this.Encode.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.Encode.Location = new System.Drawing.Point(239, 170);
            this.Encode.Name = "Encode";
            this.Encode.Size = new System.Drawing.Size(86, 23);
            this.Encode.TabIndex = 20;
            this.Encode.Text = "Encode";
            this.Encode.UseVisualStyleBackColor = false;
            // 
            // btnLoadMessage
            // 
            this.btnLoadMessage.Enabled = false;
            this.btnLoadMessage.Location = new System.Drawing.Point(260, 130);
            this.btnLoadMessage.Name = "btnLoadMessage";
            this.btnLoadMessage.Size = new System.Drawing.Size(86, 23);
            this.btnLoadMessage.TabIndex = 19;
            this.btnLoadMessage.Text = "Load image";
            this.ttStegosaurus.SetToolTip(this.btnLoadMessage, "Choose the data you would like to hide\r\ninside your cover-image");
            this.btnLoadMessage.UseVisualStyleBackColor = true;
            this.btnLoadMessage.Click += new System.EventHandler(this.loadMessage_Click_1);
            // 
            // btnLoadInput
            // 
            this.btnLoadInput.Location = new System.Drawing.Point(71, 228);
            this.btnLoadInput.Name = "btnLoadInput";
            this.btnLoadInput.Size = new System.Drawing.Size(86, 23);
            this.btnLoadInput.TabIndex = 18;
            this.btnLoadInput.Text = "Load image";
            this.ttStegosaurus.SetToolTip(this.btnLoadInput, "Choose the image you would like to hide\r\ndata in (cover-image) or retrieve data \r" +
        "\nfrom (decode)");
            this.btnLoadInput.UseVisualStyleBackColor = true;
            this.btnLoadInput.Click += new System.EventHandler(this.loadInputImage_Click_1);
            // 
            // picMessage
            // 
            this.picMessage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picMessage.Location = new System.Drawing.Point(249, 16);
            this.picMessage.Name = "picMessage";
            this.picMessage.Size = new System.Drawing.Size(108, 108);
            this.picMessage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picMessage.TabIndex = 17;
            this.picMessage.TabStop = false;
            this.ttStegosaurus.SetToolTip(this.picMessage, "Message-image");
            // 
            // picInput
            // 
            this.picInput.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picInput.Location = new System.Drawing.Point(6, 6);
            this.picInput.Name = "picInput";
            this.picInput.Size = new System.Drawing.Size(216, 216);
            this.picInput.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picInput.TabIndex = 16;
            this.picInput.TabStop = false;
            this.ttStegosaurus.SetToolTip(this.picInput, "Input-image");
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem,
            this.helpToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(624, 24);
            this.menuStrip1.TabIndex = 23;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewOptions});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "View";
            // 
            // viewOptions
            // 
            this.viewOptions.Name = "viewOptions";
            this.viewOptions.Size = new System.Drawing.Size(116, 22);
            this.viewOptions.Text = "Options";
            this.viewOptions.Click += new System.EventHandler(this.viewOptionsToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem1
            // 
            this.helpToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showHelpToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem1.Name = "helpToolStripMenuItem1";
            this.helpToolStripMenuItem1.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem1.Text = "Help";
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
            this.rdioDecode.CheckedChanged += new System.EventHandler(this.DisplayLoadMessage);
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
            this.rdioEncode.CheckedChanged += new System.EventHandler(this.DisplayLoadMessage);
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
            this.btnProceed.Click += new System.EventHandler(this.btProceed_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnProceed);
            this.groupBox1.Controls.Add(this.rdioEncode);
            this.groupBox1.Controls.Add(this.rdioDecode);
            this.groupBox1.Location = new System.Drawing.Point(230, 150);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(142, 72);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // picGTInput
            // 
            this.picGTInput.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picGTInput.Location = new System.Drawing.Point(6, 6);
            this.picGTInput.Name = "picGTInput";
            this.picGTInput.Size = new System.Drawing.Size(216, 216);
            this.picGTInput.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picGTInput.TabIndex = 29;
            this.picGTInput.TabStop = false;
            this.ttStegosaurus.SetToolTip(this.picGTInput, "Input-image");
            // 
            // btnGTLoadMessageFile
            // 
            this.btnGTLoadMessageFile.Enabled = false;
            this.btnGTLoadMessageFile.Location = new System.Drawing.Point(250, 125);
            this.btnGTLoadMessageFile.Name = "btnGTLoadMessageFile";
            this.btnGTLoadMessageFile.Size = new System.Drawing.Size(86, 23);
            this.btnGTLoadMessageFile.TabIndex = 32;
            this.btnGTLoadMessageFile.Text = "Load file";
            this.ttStegosaurus.SetToolTip(this.btnGTLoadMessageFile, "Choose the data you would like to hide\r\ninside your cover-image");
            this.btnGTLoadMessageFile.UseVisualStyleBackColor = true;
            this.btnGTLoadMessageFile.Click += new System.EventHandler(this.btnGTLoadMessageFile_Click);
            // 
            // btnGTLoadInput
            // 
            this.btnGTLoadInput.Location = new System.Drawing.Point(71, 228);
            this.btnGTLoadInput.Name = "btnGTLoadInput";
            this.btnGTLoadInput.Size = new System.Drawing.Size(86, 23);
            this.btnGTLoadInput.TabIndex = 31;
            this.btnGTLoadInput.Text = "Load image";
            this.ttStegosaurus.SetToolTip(this.btnGTLoadInput, "Choose the image you would like to hide\r\ndata in (cover-image) or retrieve data \r" +
        "\nfrom (decode)");
            this.btnGTLoadInput.UseVisualStyleBackColor = true;
            this.btnGTLoadInput.Click += new System.EventHandler(this.btnGTLoadInput_Click);
            // 
            // picGTResult
            // 
            this.picGTResult.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picGTResult.Location = new System.Drawing.Point(380, 6);
            this.picGTResult.Name = "picGTResult";
            this.picGTResult.Size = new System.Drawing.Size(216, 216);
            this.picGTResult.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picGTResult.TabIndex = 35;
            this.picGTResult.TabStop = false;
            this.ttStegosaurus.SetToolTip(this.picGTResult, "Result-image");
            // 
            // tbarGTEncodingQuality
            // 
            this.tbarGTEncodingQuality.Location = new System.Drawing.Point(248, 239);
            this.tbarGTEncodingQuality.Maximum = 100;
            this.tbarGTEncodingQuality.Name = "tbarGTEncodingQuality";
            this.tbarGTEncodingQuality.Size = new System.Drawing.Size(104, 45);
            this.tbarGTEncodingQuality.TabIndex = 36;
            this.tbarGTEncodingQuality.TickStyle = System.Windows.Forms.TickStyle.None;
            this.ttStegosaurus.SetToolTip(this.tbarGTEncodingQuality, "Set the encoding quality from which\r\nthe amount of data you can hide varries");
            this.tbarGTEncodingQuality.ValueChanged += new System.EventHandler(this.tbarGTEncodingQuality_ValueChanged);
            // 
            // tcMethod
            // 
            this.tcMethod.Controls.Add(this.tpMethodGT);
            this.tcMethod.Controls.Add(this.tpMethodLSB);
            this.tcMethod.Location = new System.Drawing.Point(4, 30);
            this.tcMethod.Name = "tcMethod";
            this.tcMethod.SelectedIndex = 0;
            this.tcMethod.Size = new System.Drawing.Size(615, 291);
            this.tcMethod.TabIndex = 26;
            // 
            // tpMethodGT
            // 
            this.tpMethodGT.BackColor = System.Drawing.SystemColors.Menu;
            this.tpMethodGT.Controls.Add(this.tbGTMessage);
            this.tpMethodGT.Controls.Add(this.checkBox1);
            this.tpMethodGT.Controls.Add(this.tbGTMessageFilePath);
            this.tpMethodGT.Controls.Add(this.lblGTEncodingQuality);
            this.tpMethodGT.Controls.Add(this.picGTInput);
            this.tpMethodGT.Controls.Add(this.lblGTEncodingQualityValue);
            this.tpMethodGT.Controls.Add(this.btnGTLoadMessageFile);
            this.tpMethodGT.Controls.Add(this.btnGTLoadInput);
            this.tpMethodGT.Controls.Add(this.groupBox2);
            this.tpMethodGT.Controls.Add(this.button4);
            this.tpMethodGT.Controls.Add(this.button5);
            this.tpMethodGT.Controls.Add(this.picGTResult);
            this.tpMethodGT.Controls.Add(this.tbarGTEncodingQuality);
            this.tpMethodGT.Location = new System.Drawing.Point(4, 22);
            this.tpMethodGT.Name = "tpMethodGT";
            this.tpMethodGT.Padding = new System.Windows.Forms.Padding(3);
            this.tpMethodGT.Size = new System.Drawing.Size(607, 265);
            this.tpMethodGT.TabIndex = 1;
            this.tpMethodGT.Text = "GT";
            // 
            // tbGTMessage
            // 
            this.tbGTMessage.Enabled = false;
            this.tbGTMessage.ForeColor = System.Drawing.SystemColors.MenuText;
            this.tbGTMessage.Location = new System.Drawing.Point(232, 16);
            this.tbGTMessage.Multiline = true;
            this.tbGTMessage.Name = "tbGTMessage";
            this.tbGTMessage.Size = new System.Drawing.Size(142, 72);
            this.tbGTMessage.TabIndex = 40;
            this.tbGTMessage.Text = "Enter the message you would like to encode into your image.";
            this.tbGTMessage.TextChanged += new System.EventHandler(this.tbGTMessage_TextChanged);
            this.tbGTMessage.Leave += new System.EventHandler(this.tbGTMessage_Leave);
            this.tbGTMessage.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tbGTMessage_MouseDoubleClick);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Enabled = false;
            this.checkBox1.Location = new System.Drawing.Point(343, 130);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(15, 14);
            this.checkBox1.TabIndex = 39;
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // tbGTMessageFilePath
            // 
            this.tbGTMessageFilePath.Enabled = false;
            this.tbGTMessageFilePath.Location = new System.Drawing.Point(251, 99);
            this.tbGTMessageFilePath.Name = "tbGTMessageFilePath";
            this.tbGTMessageFilePath.Size = new System.Drawing.Size(102, 20);
            this.tbGTMessageFilePath.TabIndex = 38;
            this.tbGTMessageFilePath.Text = "Your filepath";
            // 
            // lblGTEncodingQuality
            // 
            this.lblGTEncodingQuality.AutoSize = true;
            this.lblGTEncodingQuality.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblGTEncodingQuality.Location = new System.Drawing.Point(268, 225);
            this.lblGTEncodingQuality.Name = "lblGTEncodingQuality";
            this.lblGTEncodingQuality.Size = new System.Drawing.Size(42, 13);
            this.lblGTEncodingQuality.TabIndex = 37;
            this.lblGTEncodingQuality.Text = "Quality:";
            // 
            // lblGTEncodingQualityValue
            // 
            this.lblGTEncodingQualityValue.AutoSize = true;
            this.lblGTEncodingQualityValue.Location = new System.Drawing.Point(312, 225);
            this.lblGTEncodingQualityValue.Name = "lblGTEncodingQualityValue";
            this.lblGTEncodingQualityValue.Size = new System.Drawing.Size(13, 13);
            this.lblGTEncodingQualityValue.TabIndex = 28;
            this.lblGTEncodingQualityValue.Text = "0";
            this.lblGTEncodingQualityValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnGTProceed);
            this.groupBox2.Controls.Add(this.rdioGTEncode);
            this.groupBox2.Controls.Add(this.rdioGTDecode);
            this.groupBox2.Location = new System.Drawing.Point(232, 150);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(142, 72);
            this.groupBox2.TabIndex = 27;
            this.groupBox2.TabStop = false;
            // 
            // btnGTProceed
            // 
            this.btnGTProceed.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnGTProceed.Enabled = false;
            this.btnGTProceed.Location = new System.Drawing.Point(28, 41);
            this.btnGTProceed.Name = "btnGTProceed";
            this.btnGTProceed.Size = new System.Drawing.Size(86, 22);
            this.btnGTProceed.TabIndex = 2;
            this.btnGTProceed.Text = "Procceed";
            this.btnGTProceed.UseVisualStyleBackColor = false;
            this.btnGTProceed.Click += new System.EventHandler(this.btnGTProceed_Click);
            // 
            // rdioGTEncode
            // 
            this.rdioGTEncode.AutoSize = true;
            this.rdioGTEncode.Location = new System.Drawing.Point(8, 14);
            this.rdioGTEncode.Name = "rdioGTEncode";
            this.rdioGTEncode.Size = new System.Drawing.Size(62, 17);
            this.rdioGTEncode.TabIndex = 0;
            this.rdioGTEncode.TabStop = true;
            this.rdioGTEncode.Text = "Encode";
            this.rdioGTEncode.UseVisualStyleBackColor = true;
            this.rdioGTEncode.CheckedChanged += new System.EventHandler(this.rdioGTEncode_CheckedChanged);
            // 
            // rdioGTDecode
            // 
            this.rdioGTDecode.AutoSize = true;
            this.rdioGTDecode.Location = new System.Drawing.Point(76, 14);
            this.rdioGTDecode.Name = "rdioGTDecode";
            this.rdioGTDecode.Size = new System.Drawing.Size(63, 17);
            this.rdioGTDecode.TabIndex = 1;
            this.rdioGTDecode.TabStop = true;
            this.rdioGTDecode.Text = "Decode";
            this.rdioGTDecode.UseVisualStyleBackColor = true;
            this.rdioGTDecode.CheckedChanged += new System.EventHandler(this.rdioGTEncode_CheckedChanged);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.button4.Location = new System.Drawing.Point(239, 170);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(86, 23);
            this.button4.TabIndex = 33;
            this.button4.Text = "button4";
            this.button4.UseVisualStyleBackColor = false;
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.button5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button5.Location = new System.Drawing.Point(239, 199);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(86, 23);
            this.button5.TabIndex = 34;
            this.button5.Text = "button5";
            this.button5.UseVisualStyleBackColor = false;
            // 
            // tpMethodLSB
            // 
            this.tpMethodLSB.BackColor = System.Drawing.SystemColors.Menu;
            this.tpMethodLSB.Controls.Add(this.picInput);
            this.tpMethodLSB.Controls.Add(this.picMessage);
            this.tpMethodLSB.Controls.Add(this.btnLoadMessage);
            this.tpMethodLSB.Controls.Add(this.btnLoadInput);
            this.tpMethodLSB.Controls.Add(this.groupBox1);
            this.tpMethodLSB.Controls.Add(this.Encode);
            this.tpMethodLSB.Controls.Add(this.Decode);
            this.tpMethodLSB.Controls.Add(this.picResult);
            this.tpMethodLSB.Location = new System.Drawing.Point(4, 22);
            this.tpMethodLSB.Name = "tpMethodLSB";
            this.tpMethodLSB.Padding = new System.Windows.Forms.Padding(3);
            this.tpMethodLSB.Size = new System.Drawing.Size(607, 265);
            this.tpMethodLSB.TabIndex = 0;
            this.tpMethodLSB.Text = "LSB";
            // 
            // getFileInputGT
            // 
            this.getFileInputGT.FileName = "Select an image to be the Cover";
            this.getFileInputGT.FileOk += new System.ComponentModel.CancelEventHandler(this.getFileInputGT_FileOk);
            // 
            // GetFileMessageGT
            // 
            this.GetFileMessageGT.FileName = "Select a file to be the message";
            this.GetFileMessageGT.FileOk += new System.ComponentModel.CancelEventHandler(this.GetFileMessageGT_FileOk);
            // 
            // StegosaurusForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 322);
            this.Controls.Add(this.tcMethod);
            this.Controls.Add(this.line);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "StegosaurusForm";
            this.Text = "Stegosaurus";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.StegosaurusForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.picResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMessage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picInput)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picGTInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picGTResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbarGTEncodingQuality)).EndInit();
            this.tcMethod.ResumeLayout(false);
            this.tpMethodGT.ResumeLayout(false);
            this.tpMethodGT.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tpMethodLSB.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog getFileInputLSB;
        private System.Windows.Forms.OpenFileDialog getFileMessageLSB;
        private System.Windows.Forms.OpenFileDialog getFileStego;
        private System.Windows.Forms.Label line;
        private System.Windows.Forms.PictureBox picResult;
        private System.Windows.Forms.Button Decode;
        private System.Windows.Forms.Button Encode;
        private System.Windows.Forms.Button btnLoadMessage;
        private System.Windows.Forms.Button btnLoadInput;
        private System.Windows.Forms.PictureBox picMessage;
        private System.Windows.Forms.PictureBox picInput;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewOptions;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem showHelpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.RadioButton rdioDecode;
        private System.Windows.Forms.RadioButton rdioEncode;
        private System.Windows.Forms.Button btnProceed;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolTip ttStegosaurus;
        private System.Windows.Forms.TabControl tcMethod;
        private System.Windows.Forms.TabPage tpMethodLSB;
        private System.Windows.Forms.TabPage tpMethodGT;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox tbGTMessageFilePath;
        private System.Windows.Forms.Label lblGTEncodingQuality;
        private System.Windows.Forms.PictureBox picGTInput;
        private System.Windows.Forms.Label lblGTEncodingQualityValue;
        private System.Windows.Forms.Button btnGTLoadMessageFile;
        private System.Windows.Forms.Button btnGTLoadInput;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnGTProceed;
        private System.Windows.Forms.RadioButton rdioGTEncode;
        private System.Windows.Forms.RadioButton rdioGTDecode;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.PictureBox picGTResult;
        private System.Windows.Forms.TrackBar tbarGTEncodingQuality;
        private System.Windows.Forms.OpenFileDialog getFileInputGT;
        private System.Windows.Forms.OpenFileDialog GetFileMessageGT;
        private System.Windows.Forms.TextBox tbGTMessage;
    }
}

