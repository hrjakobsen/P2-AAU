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
            this.Okay = new System.Windows.Forms.Button();
            this.OptionsBox = new System.Windows.Forms.ListBox();
            this.pnlOptionsHuffman = new System.Windows.Forms.Panel();
            this.btnHuffmanAddRow = new System.Windows.Forms.Button();
            this.grpCustormHuffman = new System.Windows.Forms.GroupBox();
            this.pnlHuffmanY_DC = new System.Windows.Forms.Panel();
            this.pnlHuffmanY_AC = new System.Windows.Forms.Panel();
            this.pnlHuffmanChr_DC = new System.Windows.Forms.Panel();
            this.rdioHuffmanY_DC = new System.Windows.Forms.RadioButton();
            this.rdioHuffmanY_AC = new System.Windows.Forms.RadioButton();
            this.rdioHuffmanChr_DC = new System.Windows.Forms.RadioButton();
            this.pnlHuffmanChr_AC = new System.Windows.Forms.Panel();
            this.rdioHuffmanChr_AC = new System.Windows.Forms.RadioButton();
            this.pnlOptionsQuality = new System.Windows.Forms.Panel();
            this.tbarQualitySlider = new System.Windows.Forms.TrackBar();
            this.grpQualityDesc = new System.Windows.Forms.GroupBox();
            this.grpQuality = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlOptionsQuantization = new System.Windows.Forms.Panel();
            this.grpQuantizationY = new System.Windows.Forms.GroupBox();
            this.lblQuantizationDescription = new System.Windows.Forms.Label();
            this.rdioQuantizationChrChannel = new System.Windows.Forms.RadioButton();
            this.rdioQuantizationYChannel = new System.Windows.Forms.RadioButton();
            this.pnlQuantizationY = new System.Windows.Forms.Panel();
            this.pnlQuantizationChr = new System.Windows.Forms.Panel();
            this.pnlOptionsGeneral = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.tbSaveLocation = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pnlOptionsHuffman.SuspendLayout();
            this.grpCustormHuffman.SuspendLayout();
            this.pnlOptionsQuality.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbarQualitySlider)).BeginInit();
            this.grpQuality.SuspendLayout();
            this.pnlOptionsQuantization.SuspendLayout();
            this.grpQuantizationY.SuspendLayout();
            this.pnlOptionsGeneral.SuspendLayout();
            this.groupBox3.SuspendLayout();
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
            // Okay
            // 
            this.Okay.Location = new System.Drawing.Point(554, 342);
            this.Okay.Margin = new System.Windows.Forms.Padding(2);
            this.Okay.Name = "Okay";
            this.Okay.Size = new System.Drawing.Size(58, 24);
            this.Okay.TabIndex = 25;
            this.Okay.Text = "Okay";
            this.Okay.UseVisualStyleBackColor = true;
            this.Okay.Click += new System.EventHandler(this.Okay_Click);
            // 
            // OptionsBox
            // 
            this.OptionsBox.FormattingEnabled = true;
            this.OptionsBox.Items.AddRange(new object[] {
            "General",
            "Huffman table (custom)",
            "Encoding options",
            "Quality setting",
            "Quantization table (custom)"});
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
            this.pnlOptionsHuffman.Controls.Add(this.grpCustormHuffman);
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
            // grpCustormHuffman
            // 
            this.grpCustormHuffman.Controls.Add(this.pnlHuffmanY_DC);
            this.grpCustormHuffman.Controls.Add(this.pnlHuffmanY_AC);
            this.grpCustormHuffman.Controls.Add(this.pnlHuffmanChr_DC);
            this.grpCustormHuffman.Controls.Add(this.rdioHuffmanY_DC);
            this.grpCustormHuffman.Controls.Add(this.rdioHuffmanY_AC);
            this.grpCustormHuffman.Controls.Add(this.rdioHuffmanChr_DC);
            this.grpCustormHuffman.Controls.Add(this.pnlHuffmanChr_AC);
            this.grpCustormHuffman.Controls.Add(this.rdioHuffmanChr_AC);
            this.grpCustormHuffman.Location = new System.Drawing.Point(9, 0);
            this.grpCustormHuffman.Margin = new System.Windows.Forms.Padding(2);
            this.grpCustormHuffman.Name = "grpCustormHuffman";
            this.grpCustormHuffman.Padding = new System.Windows.Forms.Padding(2);
            this.grpCustormHuffman.Size = new System.Drawing.Size(421, 277);
            this.grpCustormHuffman.TabIndex = 23;
            this.grpCustormHuffman.TabStop = false;
            this.grpCustormHuffman.Text = "Huffman Tables:";
            // 
            // pnlHuffmanY_DC
            // 
            this.pnlHuffmanY_DC.AutoScroll = true;
            this.pnlHuffmanY_DC.Location = new System.Drawing.Point(4, 30);
            this.pnlHuffmanY_DC.Name = "pnlHuffmanY_DC";
            this.pnlHuffmanY_DC.Size = new System.Drawing.Size(410, 244);
            this.pnlHuffmanY_DC.TabIndex = 137;
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
            this.rdioHuffmanY_DC.Location = new System.Drawing.Point(355, 10);
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
            this.rdioHuffmanY_AC.Location = new System.Drawing.Point(297, 10);
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
            this.rdioHuffmanChr_DC.Location = new System.Drawing.Point(229, 10);
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
            this.rdioHuffmanChr_AC.Location = new System.Drawing.Point(162, 10);
            this.rdioHuffmanChr_AC.Name = "rdioHuffmanChr_AC";
            this.rdioHuffmanChr_AC.Size = new System.Drawing.Size(61, 17);
            this.rdioHuffmanChr_AC.TabIndex = 132;
            this.rdioHuffmanChr_AC.Text = "Chr_AC";
            this.rdioHuffmanChr_AC.UseVisualStyleBackColor = true;
            this.rdioHuffmanChr_AC.CheckedChanged += new System.EventHandler(this.HuffmannChannelCheckedChanged_DisplayCorrectTable);
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
            this.tbarQualitySlider.Location = new System.Drawing.Point(20, 58);
            this.tbarQualitySlider.Maximum = 100;
            this.tbarQualitySlider.Name = "tbarQualitySlider";
            this.tbarQualitySlider.Size = new System.Drawing.Size(219, 30);
            this.tbarQualitySlider.TabIndex = 26;
            this.tbarQualitySlider.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbarQualitySlider.ValueChanged += new System.EventHandler(this.tbarQualitySlider2_ValueChanged);
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
            this.pnlOptionsQuantization.Controls.Add(this.grpQuantizationY);
            this.pnlOptionsQuantization.Enabled = false;
            this.pnlOptionsQuantization.Location = new System.Drawing.Point(169, 20);
            this.pnlOptionsQuantization.Name = "pnlOptionsQuantization";
            this.pnlOptionsQuantization.Size = new System.Drawing.Size(442, 271);
            this.pnlOptionsQuantization.TabIndex = 26;
            this.pnlOptionsQuantization.Visible = false;
            // 
            // grpQuantizationY
            // 
            this.grpQuantizationY.Controls.Add(this.lblQuantizationDescription);
            this.grpQuantizationY.Controls.Add(this.rdioQuantizationChrChannel);
            this.grpQuantizationY.Controls.Add(this.rdioQuantizationYChannel);
            this.grpQuantizationY.Controls.Add(this.pnlQuantizationY);
            this.grpQuantizationY.Controls.Add(this.pnlQuantizationChr);
            this.grpQuantizationY.Location = new System.Drawing.Point(9, 0);
            this.grpQuantizationY.Margin = new System.Windows.Forms.Padding(2);
            this.grpQuantizationY.Name = "grpQuantizationY";
            this.grpQuantizationY.Padding = new System.Windows.Forms.Padding(2);
            this.grpQuantizationY.Size = new System.Drawing.Size(413, 269);
            this.grpQuantizationY.TabIndex = 8;
            this.grpQuantizationY.TabStop = false;
            this.grpQuantizationY.Text = "Custom Quantization";
            // 
            // lblQuantizationDescription
            // 
            this.lblQuantizationDescription.AutoSize = true;
            this.lblQuantizationDescription.Location = new System.Drawing.Point(6, 236);
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
            // pnlQuantizationY
            // 
            this.pnlQuantizationY.Location = new System.Drawing.Point(4, 30);
            this.pnlQuantizationY.Name = "pnlQuantizationY";
            this.pnlQuantizationY.Size = new System.Drawing.Size(399, 206);
            this.pnlQuantizationY.TabIndex = 74;
            this.pnlQuantizationY.Visible = false;
            // 
            // pnlQuantizationChr
            // 
            this.pnlQuantizationChr.Location = new System.Drawing.Point(4, 30);
            this.pnlQuantizationChr.Name = "pnlQuantizationChr";
            this.pnlQuantizationChr.Size = new System.Drawing.Size(399, 206);
            this.pnlQuantizationChr.TabIndex = 129;
            this.pnlQuantizationChr.Visible = false;
            // 
            // pnlOptionsGeneral
            // 
            this.pnlOptionsGeneral.Controls.Add(this.groupBox3);
            this.pnlOptionsGeneral.Enabled = false;
            this.pnlOptionsGeneral.Location = new System.Drawing.Point(169, 20);
            this.pnlOptionsGeneral.Name = "pnlOptionsGeneral";
            this.pnlOptionsGeneral.Size = new System.Drawing.Size(442, 269);
            this.pnlOptionsGeneral.TabIndex = 29;
            this.pnlOptionsGeneral.Visible = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Controls.Add(this.tbSaveLocation);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Location = new System.Drawing.Point(9, 0);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(413, 256);
            this.groupBox3.TabIndex = 18;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "General settings";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(354, 43);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(53, 21);
            this.button1.TabIndex = 18;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // tbSaveLocation
            // 
            this.tbSaveLocation.Location = new System.Drawing.Point(19, 43);
            this.tbSaveLocation.Name = "tbSaveLocation";
            this.tbSaveLocation.Size = new System.Drawing.Size(329, 20);
            this.tbSaveLocation.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 24);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Choose save location:";
            // 
            // OptionsForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(623, 377);
            this.Controls.Add(this.line);
            this.Controls.Add(this.Okay);
            this.Controls.Add(this.OptionsBox);
            this.Controls.Add(this.pnlOptionsHuffman);
            this.Controls.Add(this.pnlOptionsQuantization);
            this.Controls.Add(this.pnlOptionsGeneral);
            this.Controls.Add(this.pnlOptionsQuality);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "OptionsForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Stegosaurus - Options";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormClosing_SaveQuantization);
            this.Load += new System.EventHandler(this.OptionsForm_Load);
            this.pnlOptionsHuffman.ResumeLayout(false);
            this.grpCustormHuffman.ResumeLayout(false);
            this.grpCustormHuffman.PerformLayout();
            this.pnlOptionsQuality.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tbarQualitySlider)).EndInit();
            this.grpQuality.ResumeLayout(false);
            this.grpQuality.PerformLayout();
            this.pnlOptionsQuantization.ResumeLayout(false);
            this.grpQuantizationY.ResumeLayout(false);
            this.grpQuantizationY.PerformLayout();
            this.pnlOptionsGeneral.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label line;
        private System.Windows.Forms.Button Okay;
        private System.Windows.Forms.ListBox OptionsBox;
        private System.Windows.Forms.Panel pnlOptionsHuffman;
        private System.Windows.Forms.GroupBox grpCustormHuffman;
        private System.Windows.Forms.Panel pnlOptionsQuality;
        private System.Windows.Forms.GroupBox grpQualityDesc;
        private System.Windows.Forms.GroupBox grpQuality;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlOptionsQuantization;
        private System.Windows.Forms.GroupBox grpQuantizationY;
        private System.Windows.Forms.TrackBar tbarQualitySlider;
        private System.Windows.Forms.Panel pnlQuantizationY;
        private System.Windows.Forms.Panel pnlQuantizationChr;
        private System.Windows.Forms.Panel pnlOptionsGeneral;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rdioQuantizationChrChannel;
        private System.Windows.Forms.RadioButton rdioQuantizationYChannel;
        private System.Windows.Forms.Panel pnlHuffmanChr_AC;
        private System.Windows.Forms.TextBox tbSaveLocation;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblQuantizationDescription;
        private System.Windows.Forms.Button btnHuffmanAddRow;
        private System.Windows.Forms.RadioButton rdioHuffmanChr_DC;
        private System.Windows.Forms.RadioButton rdioHuffmanChr_AC;
        private System.Windows.Forms.RadioButton rdioHuffmanY_DC;
        private System.Windows.Forms.RadioButton rdioHuffmanY_AC;
        private System.Windows.Forms.Panel pnlHuffmanY_DC;
        private System.Windows.Forms.Panel pnlHuffmanY_AC;
        private System.Windows.Forms.Panel pnlHuffmanChr_DC;
    }
}