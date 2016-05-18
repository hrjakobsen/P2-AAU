namespace TestForm
{
    partial class OptionsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionsForm));
            this.line = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.OptionsBox = new System.Windows.Forms.ListBox();
            this.pnlOptionsHuffman = new System.Windows.Forms.Panel();
            this.btnHuffmanAddRow = new System.Windows.Forms.Button();
            this.grpCustomHuffman = new System.Windows.Forms.GroupBox();
            this.pnlHuffmanY_AC = new System.Windows.Forms.Panel();
            this.pnlHuffmanChr_DC = new System.Windows.Forms.Panel();
            this.rdioHuffmanY_DC = new System.Windows.Forms.RadioButton();
            this.rdioHuffmanY_AC = new System.Windows.Forms.RadioButton();
            this.rdioHuffmanChr_DC = new System.Windows.Forms.RadioButton();
            this.pnlHuffmanChr_AC = new System.Windows.Forms.Panel();
            this.rdioHuffmanChr_AC = new System.Windows.Forms.RadioButton();
            this.pnlHuffmanY_DC = new System.Windows.Forms.Panel();
            this.pnlOptionsQuality = new System.Windows.Forms.Panel();
            this.tbarQualitySlider = new System.Windows.Forms.TrackBar();
            this.grpQualityDesc = new System.Windows.Forms.GroupBox();
            this.grpQuality = new System.Windows.Forms.GroupBox();
            this.lblEncodingQuality = new System.Windows.Forms.Label();
            this.lblEncodingQualityValue = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlOptionsQuantization = new System.Windows.Forms.Panel();
            this.grpQuantization = new System.Windows.Forms.GroupBox();
            this.lblQuantizationDescription = new System.Windows.Forms.Label();
            this.rdioQuantizationChrChannel = new System.Windows.Forms.RadioButton();
            this.rdioQuantizationYChannel = new System.Windows.Forms.RadioButton();
            this.pnlQuantization = new System.Windows.Forms.Panel();
            this.selectOutputFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.btnClose = new System.Windows.Forms.Button();
            this.pnlOptionsEncodingMethod = new System.Windows.Forms.Panel();
            this.grpEncodingMethod = new System.Windows.Forms.GroupBox();
            this.rdioLSBMethod = new System.Windows.Forms.RadioButton();
            this.rdioGTMethod = new System.Windows.Forms.RadioButton();
            this.lblEncodingMethod = new System.Windows.Forms.Label();
            this.btnDefault = new System.Windows.Forms.Button();
            this.pnlOptionsHuffman.SuspendLayout();
            this.grpCustomHuffman.SuspendLayout();
            this.pnlOptionsQuality.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbarQualitySlider)).BeginInit();
            this.grpQuality.SuspendLayout();
            this.pnlOptionsQuantization.SuspendLayout();
            this.grpQuantization.SuspendLayout();
            this.pnlOptionsEncodingMethod.SuspendLayout();
            this.grpEncodingMethod.SuspendLayout();
            this.SuspendLayout();
            // 
            // line
            // 
            this.line.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.line.Location = new System.Drawing.Point(166, 334);
            this.line.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.line.Name = "line";
            this.line.Size = new System.Drawing.Size(446, 2);
            this.line.TabIndex = 27;
            // 
            // btnSave
            // 
            this.btnSave.AutoSize = true;
            this.btnSave.Location = new System.Drawing.Point(459, 342);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(91, 24);
            this.btnSave.TabIndex = 25;
            this.btnSave.Text = "Save and close";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // OptionsBox
            // 
            this.OptionsBox.FormattingEnabled = true;
            this.OptionsBox.Items.AddRange(new object[] {
            "Huffman table (custom)",
            "Quantization table (custom)",
            "Quality setting",
            "Encoding method"});
            this.OptionsBox.Location = new System.Drawing.Point(14, 20);
            this.OptionsBox.Margin = new System.Windows.Forms.Padding(2);
            this.OptionsBox.Name = "OptionsBox";
            this.OptionsBox.Size = new System.Drawing.Size(144, 316);
            this.OptionsBox.TabIndex = 24;
            this.OptionsBox.SelectedIndexChanged += new System.EventHandler(this.OptionsBox_SelectedIndexChanged);
            // 
            // pnlOptionsHuffman
            // 
            this.pnlOptionsHuffman.Controls.Add(this.btnHuffmanAddRow);
            this.pnlOptionsHuffman.Controls.Add(this.grpCustomHuffman);
            this.pnlOptionsHuffman.Location = new System.Drawing.Point(169, 20);
            this.pnlOptionsHuffman.Name = "pnlOptionsHuffman";
            this.pnlOptionsHuffman.Size = new System.Drawing.Size(442, 311);
            this.pnlOptionsHuffman.TabIndex = 2;
            // 
            // btnHuffmanAddRow
            // 
            this.btnHuffmanAddRow.Location = new System.Drawing.Point(10, 280);
            this.btnHuffmanAddRow.Name = "btnHuffmanAddRow";
            this.btnHuffmanAddRow.Size = new System.Drawing.Size(75, 23);
            this.btnHuffmanAddRow.TabIndex = 24;
            this.btnHuffmanAddRow.Text = "Add row";
            this.btnHuffmanAddRow.UseVisualStyleBackColor = true;
            this.btnHuffmanAddRow.Click += new System.EventHandler(this.btnHuffmanAddRow_Click);
            // 
            // grpCustomHuffman
            // 
            this.grpCustomHuffman.Controls.Add(this.pnlHuffmanY_AC);
            this.grpCustomHuffman.Controls.Add(this.pnlHuffmanChr_DC);
            this.grpCustomHuffman.Controls.Add(this.rdioHuffmanY_DC);
            this.grpCustomHuffman.Controls.Add(this.rdioHuffmanY_AC);
            this.grpCustomHuffman.Controls.Add(this.rdioHuffmanChr_DC);
            this.grpCustomHuffman.Controls.Add(this.pnlHuffmanChr_AC);
            this.grpCustomHuffman.Controls.Add(this.rdioHuffmanChr_AC);
            this.grpCustomHuffman.Controls.Add(this.pnlHuffmanY_DC);
            this.grpCustomHuffman.Location = new System.Drawing.Point(9, 0);
            this.grpCustomHuffman.Margin = new System.Windows.Forms.Padding(2);
            this.grpCustomHuffman.Name = "grpCustomHuffman";
            this.grpCustomHuffman.Padding = new System.Windows.Forms.Padding(2);
            this.grpCustomHuffman.Size = new System.Drawing.Size(421, 277);
            this.grpCustomHuffman.TabIndex = 23;
            this.grpCustomHuffman.TabStop = false;
            this.grpCustomHuffman.Text = "Huffman Tables:";
            // 
            // pnlHuffmanY_AC
            // 
            this.pnlHuffmanY_AC.AutoScroll = true;
            this.pnlHuffmanY_AC.Location = new System.Drawing.Point(4, 30);
            this.pnlHuffmanY_AC.Name = "pnlHuffmanY_AC";
            this.pnlHuffmanY_AC.Size = new System.Drawing.Size(410, 244);
            this.pnlHuffmanY_AC.TabIndex = 136;
            // 
            // pnlHuffmanChr_DC
            // 
            this.pnlHuffmanChr_DC.AutoScroll = true;
            this.pnlHuffmanChr_DC.Location = new System.Drawing.Point(4, 30);
            this.pnlHuffmanChr_DC.Name = "pnlHuffmanChr_DC";
            this.pnlHuffmanChr_DC.Size = new System.Drawing.Size(410, 244);
            this.pnlHuffmanChr_DC.TabIndex = 1;
            // 
            // rdioHuffmanY_DC
            // 
            this.rdioHuffmanY_DC.AutoSize = true;
            this.rdioHuffmanY_DC.Location = new System.Drawing.Point(223, 10);
            this.rdioHuffmanY_DC.Name = "rdioHuffmanY_DC";
            this.rdioHuffmanY_DC.Size = new System.Drawing.Size(53, 17);
            this.rdioHuffmanY_DC.TabIndex = 135;
            this.rdioHuffmanY_DC.Text = "Y_DC";
            this.rdioHuffmanY_DC.UseVisualStyleBackColor = true;
            this.rdioHuffmanY_DC.CheckedChanged += new System.EventHandler(this.HuffmannChannelCheckedChanged_DisplayCorrectTable);
            // 
            // rdioHuffmanY_AC
            // 
            this.rdioHuffmanY_AC.AutoSize = true;
            this.rdioHuffmanY_AC.Location = new System.Drawing.Point(165, 10);
            this.rdioHuffmanY_AC.Name = "rdioHuffmanY_AC";
            this.rdioHuffmanY_AC.Size = new System.Drawing.Size(52, 17);
            this.rdioHuffmanY_AC.TabIndex = 134;
            this.rdioHuffmanY_AC.Text = "Y_AC";
            this.rdioHuffmanY_AC.UseVisualStyleBackColor = true;
            this.rdioHuffmanY_AC.CheckedChanged += new System.EventHandler(this.HuffmannChannelCheckedChanged_DisplayCorrectTable);
            // 
            // rdioHuffmanChr_DC
            // 
            this.rdioHuffmanChr_DC.AutoSize = true;
            this.rdioHuffmanChr_DC.Location = new System.Drawing.Point(347, 10);
            this.rdioHuffmanChr_DC.Name = "rdioHuffmanChr_DC";
            this.rdioHuffmanChr_DC.Size = new System.Drawing.Size(62, 17);
            this.rdioHuffmanChr_DC.TabIndex = 133;
            this.rdioHuffmanChr_DC.Text = "Chr_DC";
            this.rdioHuffmanChr_DC.UseVisualStyleBackColor = true;
            this.rdioHuffmanChr_DC.CheckedChanged += new System.EventHandler(this.HuffmannChannelCheckedChanged_DisplayCorrectTable);
            // 
            // pnlHuffmanChr_AC
            // 
            this.pnlHuffmanChr_AC.AutoScroll = true;
            this.pnlHuffmanChr_AC.Location = new System.Drawing.Point(4, 30);
            this.pnlHuffmanChr_AC.Name = "pnlHuffmanChr_AC";
            this.pnlHuffmanChr_AC.Size = new System.Drawing.Size(410, 244);
            this.pnlHuffmanChr_AC.TabIndex = 0;
            // 
            // rdioHuffmanChr_AC
            // 
            this.rdioHuffmanChr_AC.AutoSize = true;
            this.rdioHuffmanChr_AC.Location = new System.Drawing.Point(281, 10);
            this.rdioHuffmanChr_AC.Name = "rdioHuffmanChr_AC";
            this.rdioHuffmanChr_AC.Size = new System.Drawing.Size(61, 17);
            this.rdioHuffmanChr_AC.TabIndex = 132;
            this.rdioHuffmanChr_AC.Text = "Chr_AC";
            this.rdioHuffmanChr_AC.UseVisualStyleBackColor = true;
            this.rdioHuffmanChr_AC.CheckedChanged += new System.EventHandler(this.HuffmannChannelCheckedChanged_DisplayCorrectTable);
            // 
            // pnlHuffmanY_DC
            // 
            this.pnlHuffmanY_DC.AutoScroll = true;
            this.pnlHuffmanY_DC.Location = new System.Drawing.Point(4, 30);
            this.pnlHuffmanY_DC.Name = "pnlHuffmanY_DC";
            this.pnlHuffmanY_DC.Size = new System.Drawing.Size(410, 244);
            this.pnlHuffmanY_DC.TabIndex = 137;
            // 
            // pnlOptionsQuality
            // 
            this.pnlOptionsQuality.Controls.Add(this.tbarQualitySlider);
            this.pnlOptionsQuality.Controls.Add(this.grpQualityDesc);
            this.pnlOptionsQuality.Controls.Add(this.grpQuality);
            this.pnlOptionsQuality.Enabled = false;
            this.pnlOptionsQuality.Location = new System.Drawing.Point(169, 20);
            this.pnlOptionsQuality.Name = "pnlOptionsQuality";
            this.pnlOptionsQuality.Size = new System.Drawing.Size(442, 269);
            this.pnlOptionsQuality.TabIndex = 28;
            this.pnlOptionsQuality.Visible = false;
            // 
            // tbarQualitySlider
            // 
            this.tbarQualitySlider.AutoSize = false;
            this.tbarQualitySlider.Location = new System.Drawing.Point(20, 73);
            this.tbarQualitySlider.Maximum = 100;
            this.tbarQualitySlider.Name = "tbarQualitySlider";
            this.tbarQualitySlider.Size = new System.Drawing.Size(219, 30);
            this.tbarQualitySlider.TabIndex = 26;
            this.tbarQualitySlider.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbarQualitySlider.ValueChanged += new System.EventHandler(this.tbarQualitySlider_ValueChanged);
            // 
            // grpQualityDesc
            // 
            this.grpQualityDesc.Location = new System.Drawing.Point(9, 118);
            this.grpQualityDesc.Margin = new System.Windows.Forms.Padding(2);
            this.grpQualityDesc.Name = "grpQualityDesc";
            this.grpQualityDesc.Padding = new System.Windows.Forms.Padding(2);
            this.grpQualityDesc.Size = new System.Drawing.Size(413, 138);
            this.grpQualityDesc.TabIndex = 23;
            this.grpQualityDesc.TabStop = false;
            this.grpQualityDesc.Text = "Description";
            // 
            // grpQuality
            // 
            this.grpQuality.Controls.Add(this.lblEncodingQuality);
            this.grpQuality.Controls.Add(this.lblEncodingQualityValue);
            this.grpQuality.Controls.Add(this.label1);
            this.grpQuality.Location = new System.Drawing.Point(9, 0);
            this.grpQuality.Margin = new System.Windows.Forms.Padding(2);
            this.grpQuality.Name = "grpQuality";
            this.grpQuality.Padding = new System.Windows.Forms.Padding(2);
            this.grpQuality.Size = new System.Drawing.Size(413, 109);
            this.grpQuality.TabIndex = 18;
            this.grpQuality.TabStop = false;
            this.grpQuality.Text = "Encoding quality";
            // 
            // lblEncodingQuality
            // 
            this.lblEncodingQuality.AutoSize = true;
            this.lblEncodingQuality.Location = new System.Drawing.Point(21, 48);
            this.lblEncodingQuality.Name = "lblEncodingQuality";
            this.lblEncodingQuality.Size = new System.Drawing.Size(42, 13);
            this.lblEncodingQuality.TabIndex = 18;
            this.lblEncodingQuality.Text = "Quality:";
            // 
            // lblEncodingQualityValue
            // 
            this.lblEncodingQualityValue.AutoSize = true;
            this.lblEncodingQualityValue.Location = new System.Drawing.Point(65, 48);
            this.lblEncodingQualityValue.Name = "lblEncodingQualityValue";
            this.lblEncodingQualityValue.Size = new System.Drawing.Size(13, 13);
            this.lblEncodingQualityValue.TabIndex = 17;
            this.lblEncodingQualityValue.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 24);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Choose quality setting:";
            // 
            // pnlOptionsQuantization
            // 
            this.pnlOptionsQuantization.Controls.Add(this.grpQuantization);
            this.pnlOptionsQuantization.Enabled = false;
            this.pnlOptionsQuantization.Location = new System.Drawing.Point(169, 20);
            this.pnlOptionsQuantization.Name = "pnlOptionsQuantization";
            this.pnlOptionsQuantization.Size = new System.Drawing.Size(442, 271);
            this.pnlOptionsQuantization.TabIndex = 26;
            this.pnlOptionsQuantization.Visible = false;
            // 
            // grpQuantization
            // 
            this.grpQuantization.Controls.Add(this.lblQuantizationDescription);
            this.grpQuantization.Controls.Add(this.rdioQuantizationChrChannel);
            this.grpQuantization.Controls.Add(this.rdioQuantizationYChannel);
            this.grpQuantization.Controls.Add(this.pnlQuantization);
            this.grpQuantization.Location = new System.Drawing.Point(9, 2);
            this.grpQuantization.Margin = new System.Windows.Forms.Padding(2);
            this.grpQuantization.Name = "grpQuantization";
            this.grpQuantization.Padding = new System.Windows.Forms.Padding(2);
            this.grpQuantization.Size = new System.Drawing.Size(421, 275);
            this.grpQuantization.TabIndex = 8;
            this.grpQuantization.TabStop = false;
            this.grpQuantization.Text = "Custom Quantization";
            // 
            // lblQuantizationDescription
            // 
            this.lblQuantizationDescription.AutoSize = true;
            this.lblQuantizationDescription.Location = new System.Drawing.Point(5, 239);
            this.lblQuantizationDescription.Name = "lblQuantizationDescription";
            this.lblQuantizationDescription.Size = new System.Drawing.Size(383, 26);
            this.lblQuantizationDescription.TabIndex = 132;
            this.lblQuantizationDescription.Text = "Changes the quantization-table used when encoding with the Graph Theoretical\r\nmet" +
    "hod.\r\n";
            // 
            // rdioQuantizationChrChannel
            // 
            this.rdioQuantizationChrChannel.AutoSize = true;
            this.rdioQuantizationChrChannel.Location = new System.Drawing.Point(313, 10);
            this.rdioQuantizationChrChannel.Name = "rdioQuantizationChrChannel";
            this.rdioQuantizationChrChannel.Size = new System.Drawing.Size(82, 17);
            this.rdioQuantizationChrChannel.TabIndex = 131;
            this.rdioQuantizationChrChannel.Text = "Chr-channel";
            this.rdioQuantizationChrChannel.UseVisualStyleBackColor = true;
            // 
            // rdioQuantizationYChannel
            // 
            this.rdioQuantizationYChannel.AutoSize = true;
            this.rdioQuantizationYChannel.Location = new System.Drawing.Point(234, 10);
            this.rdioQuantizationYChannel.Name = "rdioQuantizationYChannel";
            this.rdioQuantizationYChannel.Size = new System.Drawing.Size(73, 17);
            this.rdioQuantizationYChannel.TabIndex = 8;
            this.rdioQuantizationYChannel.Text = "Y-channel";
            this.rdioQuantizationYChannel.UseVisualStyleBackColor = true;
            this.rdioQuantizationYChannel.CheckedChanged += new System.EventHandler(this.yQuantizationChannelChecked_DisplayYOrChrTable);
            // 
            // pnlQuantization
            // 
            this.pnlQuantization.Location = new System.Drawing.Point(1, 15);
            this.pnlQuantization.Name = "pnlQuantization";
            this.pnlQuantization.Size = new System.Drawing.Size(412, 262);
            this.pnlQuantization.TabIndex = 129;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(554, 342);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(58, 24);
            this.btnClose.TabIndex = 30;
            this.btnClose.Text = "Cancel";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // pnlOptionsEncodingMethod
            // 
            this.pnlOptionsEncodingMethod.Controls.Add(this.grpEncodingMethod);
            this.pnlOptionsEncodingMethod.Location = new System.Drawing.Point(169, 20);
            this.pnlOptionsEncodingMethod.Name = "pnlOptionsEncodingMethod";
            this.pnlOptionsEncodingMethod.Size = new System.Drawing.Size(442, 311);
            this.pnlOptionsEncodingMethod.TabIndex = 25;
            // 
            // grpEncodingMethod
            // 
            this.grpEncodingMethod.Controls.Add(this.rdioLSBMethod);
            this.grpEncodingMethod.Controls.Add(this.rdioGTMethod);
            this.grpEncodingMethod.Controls.Add(this.lblEncodingMethod);
            this.grpEncodingMethod.Location = new System.Drawing.Point(9, 0);
            this.grpEncodingMethod.Margin = new System.Windows.Forms.Padding(2);
            this.grpEncodingMethod.Name = "grpEncodingMethod";
            this.grpEncodingMethod.Padding = new System.Windows.Forms.Padding(2);
            this.grpEncodingMethod.Size = new System.Drawing.Size(421, 303);
            this.grpEncodingMethod.TabIndex = 23;
            this.grpEncodingMethod.TabStop = false;
            this.grpEncodingMethod.Text = "Encoding Method";
            // 
            // rdioLSBMethod
            // 
            this.rdioLSBMethod.AutoSize = true;
            this.rdioLSBMethod.Location = new System.Drawing.Point(135, 44);
            this.rdioLSBMethod.Name = "rdioLSBMethod";
            this.rdioLSBMethod.Size = new System.Drawing.Size(129, 17);
            this.rdioLSBMethod.TabIndex = 19;
            this.rdioLSBMethod.Text = "Least Significant Bit(s)";
            this.rdioLSBMethod.UseVisualStyleBackColor = true;
            this.rdioLSBMethod.CheckedChanged += new System.EventHandler(this.rdioGT_CheckedChangedSetMethod);
            // 
            // rdioGTMethod
            // 
            this.rdioGTMethod.AutoSize = true;
            this.rdioGTMethod.Checked = true;
            this.rdioGTMethod.Location = new System.Drawing.Point(27, 44);
            this.rdioGTMethod.Name = "rdioGTMethod";
            this.rdioGTMethod.Size = new System.Drawing.Size(102, 17);
            this.rdioGTMethod.TabIndex = 18;
            this.rdioGTMethod.TabStop = true;
            this.rdioGTMethod.Text = "Graph Theoretic";
            this.rdioGTMethod.UseVisualStyleBackColor = true;
            this.rdioGTMethod.CheckedChanged += new System.EventHandler(this.rdioGT_CheckedChangedSetMethod);
            // 
            // lblEncodingMethod
            // 
            this.lblEncodingMethod.AutoSize = true;
            this.lblEncodingMethod.Location = new System.Drawing.Point(21, 27);
            this.lblEncodingMethod.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblEncodingMethod.Name = "lblEncodingMethod";
            this.lblEncodingMethod.Size = new System.Drawing.Size(125, 13);
            this.lblEncodingMethod.TabIndex = 17;
            this.lblEncodingMethod.Text = "Select encoding method:";
            // 
            // btnDefault
            // 
            this.btnDefault.Location = new System.Drawing.Point(169, 342);
            this.btnDefault.Name = "btnDefault";
            this.btnDefault.Size = new System.Drawing.Size(75, 23);
            this.btnDefault.TabIndex = 31;
            this.btnDefault.Text = "Default";
            this.btnDefault.UseVisualStyleBackColor = true;
            this.btnDefault.Click += new System.EventHandler(this.btnDefault_Click);
            // 
            // OptionsForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(623, 377);
            this.Controls.Add(this.btnDefault);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.line);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.OptionsBox);
            this.Controls.Add(this.pnlOptionsQuality);
            this.Controls.Add(this.pnlOptionsEncodingMethod);
            this.Controls.Add(this.pnlOptionsHuffman);
            this.Controls.Add(this.pnlOptionsQuantization);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "OptionsForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Stegosaurus - Options";
            this.pnlOptionsHuffman.ResumeLayout(false);
            this.grpCustomHuffman.ResumeLayout(false);
            this.grpCustomHuffman.PerformLayout();
            this.pnlOptionsQuality.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tbarQualitySlider)).EndInit();
            this.grpQuality.ResumeLayout(false);
            this.grpQuality.PerformLayout();
            this.pnlOptionsQuantization.ResumeLayout(false);
            this.grpQuantization.ResumeLayout(false);
            this.grpQuantization.PerformLayout();
            this.pnlOptionsEncodingMethod.ResumeLayout(false);
            this.grpEncodingMethod.ResumeLayout(false);
            this.grpEncodingMethod.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label line;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ListBox OptionsBox;
        private System.Windows.Forms.Panel pnlOptionsHuffman;
        private System.Windows.Forms.Panel pnlOptionsQuality;
        private System.Windows.Forms.GroupBox grpQualityDesc;
        private System.Windows.Forms.GroupBox grpQuality;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlOptionsQuantization;
        private System.Windows.Forms.GroupBox grpQuantization;
        private System.Windows.Forms.TrackBar tbarQualitySlider;
        private System.Windows.Forms.Panel pnlQuantization;
        private System.Windows.Forms.RadioButton rdioQuantizationChrChannel;
        private System.Windows.Forms.RadioButton rdioQuantizationYChannel;
        private System.Windows.Forms.Label lblQuantizationDescription;
        private System.Windows.Forms.Button btnHuffmanAddRow;
        private System.Windows.Forms.GroupBox grpCustomHuffman;
        private System.Windows.Forms.Panel pnlHuffmanY_AC;
        private System.Windows.Forms.Panel pnlHuffmanChr_DC;
        private System.Windows.Forms.RadioButton rdioHuffmanY_DC;
        private System.Windows.Forms.RadioButton rdioHuffmanY_AC;
        private System.Windows.Forms.RadioButton rdioHuffmanChr_DC;
        private System.Windows.Forms.Panel pnlHuffmanChr_AC;
        private System.Windows.Forms.RadioButton rdioHuffmanChr_AC;
        private System.Windows.Forms.Panel pnlHuffmanY_DC;
        private System.Windows.Forms.FolderBrowserDialog selectOutputFolder;
        private System.Windows.Forms.Label lblEncodingQualityValue;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblEncodingQuality;
        private System.Windows.Forms.Panel pnlOptionsEncodingMethod;
        private System.Windows.Forms.GroupBox grpEncodingMethod;
        private System.Windows.Forms.RadioButton rdioLSBMethod;
        private System.Windows.Forms.RadioButton rdioGTMethod;
        private System.Windows.Forms.Label lblEncodingMethod;
        private System.Windows.Forms.Button btnDefault;
    }
}