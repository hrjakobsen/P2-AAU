using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Stegosaurus;

namespace TestForm
{
    partial class OptionsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(OptionsForm));
            this.line = new Label();
            this.btnSave = new Button();
            this.OptionsBox = new ListBox();
            this.pnlOptionsHuffman = new Panel();
            this.btnHuffmanAddRow = new Button();
            this.grpCustomHuffman = new GroupBox();
            this.pnlHuffmanY_AC = new Panel();
            this.pnlHuffmanChr_DC = new Panel();
            this.rdioHuffmanY_DC = new RadioButton();
            this.rdioHuffmanY_AC = new RadioButton();
            this.rdioHuffmanChr_DC = new RadioButton();
            this.pnlHuffmanChr_AC = new Panel();
            this.rdioHuffmanChr_AC = new RadioButton();
            this.pnlHuffmanY_DC = new Panel();
            this.pnlOptionsQuality = new Panel();
            this.tbarQualitySlider = new TrackBar();
            this.grpQuality = new GroupBox();
            this.cbMValue = new ComboBox();
            this.lblChooseMValue = new Label();
            this.lblEncodingQuality = new Label();
            this.lblEncodingQualityValue = new Label();
            this.lblChooseQuality = new Label();
            this.pnlOptionsQuantization = new Panel();
            this.grpQuantization = new GroupBox();
            this.lblQuantizationDescription = new Label();
            this.rdioQuantizationChrChannel = new RadioButton();
            this.rdioQuantizationYChannel = new RadioButton();
            this.pnlQuantization = new Panel();
            this.selectOutputFolder = new FolderBrowserDialog();
            this.btnClose = new Button();
            this.pnlOptionsEncodingMethod = new Panel();
            this.grpEncodingMethod = new GroupBox();
            this.rdioLSBMethod = new RadioButton();
            this.rdioGTMethod = new RadioButton();
            this.lblEncodingMethod = new Label();
            this.btnDefault = new Button();
            this.pnlOptionsHuffman.SuspendLayout();
            this.grpCustomHuffman.SuspendLayout();
            this.pnlOptionsQuality.SuspendLayout();
            ((ISupportInitialize)(this.tbarQualitySlider)).BeginInit();
            this.grpQuality.SuspendLayout();
            this.pnlOptionsQuantization.SuspendLayout();
            this.grpQuantization.SuspendLayout();
            this.pnlOptionsEncodingMethod.SuspendLayout();
            this.grpEncodingMethod.SuspendLayout();
            this.SuspendLayout();
            // 
            // line
            // 
            this.line.BorderStyle = BorderStyle.Fixed3D;
            this.line.Location = new Point(166, 334);
            this.line.Margin = new Padding(2, 0, 2, 0);
            this.line.Name = "line";
            this.line.Size = new Size(446, 2);
            this.line.TabIndex = 27;
            // 
            // btnSave
            // 
            this.btnSave.AutoSize = true;
            this.btnSave.Location = new Point(459, 342);
            this.btnSave.Margin = new Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new Size(91, 24);
            this.btnSave.TabIndex = 25;
            this.btnSave.Text = "Save and close";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new EventHandler(this.btnSave_Click);
            // 
            // OptionsBox
            // 
            this.OptionsBox.FormattingEnabled = true;
            this.OptionsBox.Items.AddRange(new object[] {
            "Huffman table",
            "Quantization table",
            "Quality setting",
            "Encoding method"});
            this.OptionsBox.Location = new Point(14, 20);
            this.OptionsBox.Margin = new Padding(2);
            this.OptionsBox.Name = "OptionsBox";
            this.OptionsBox.Size = new Size(144, 316);
            this.OptionsBox.TabIndex = 24;
            this.OptionsBox.SelectedIndexChanged += new EventHandler(this.OptionsBox_SelectedIndexChanged);
            // 
            // pnlOptionsHuffman
            // 
            this.pnlOptionsHuffman.Controls.Add(this.btnHuffmanAddRow);
            this.pnlOptionsHuffman.Controls.Add(this.grpCustomHuffman);
            this.pnlOptionsHuffman.Location = new Point(169, 20);
            this.pnlOptionsHuffman.Name = "pnlOptionsHuffman";
            this.pnlOptionsHuffman.Size = new Size(442, 311);
            this.pnlOptionsHuffman.TabIndex = 2;
            // 
            // btnHuffmanAddRow
            // 
            this.btnHuffmanAddRow.Location = new Point(10, 280);
            this.btnHuffmanAddRow.Name = "btnHuffmanAddRow";
            this.btnHuffmanAddRow.Size = new Size(75, 23);
            this.btnHuffmanAddRow.TabIndex = 24;
            this.btnHuffmanAddRow.Text = "Add row";
            this.btnHuffmanAddRow.UseVisualStyleBackColor = true;
            this.btnHuffmanAddRow.Click += new EventHandler(this.btnHuffmanAddRow_Click);
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
            this.grpCustomHuffman.Location = new Point(9, 0);
            this.grpCustomHuffman.Margin = new Padding(2);
            this.grpCustomHuffman.Name = "grpCustomHuffman";
            this.grpCustomHuffman.Padding = new Padding(2);
            this.grpCustomHuffman.Size = new Size(421, 277);
            this.grpCustomHuffman.TabIndex = 23;
            this.grpCustomHuffman.TabStop = false;
            this.grpCustomHuffman.Text = "Huffman Tables:";
            // 
            // pnlHuffmanY_AC
            // 
            this.pnlHuffmanY_AC.AutoScroll = true;
            this.pnlHuffmanY_AC.Location = new Point(4, 30);
            this.pnlHuffmanY_AC.Name = "pnlHuffmanY_AC";
            this.pnlHuffmanY_AC.Size = new Size(410, 244);
            this.pnlHuffmanY_AC.TabIndex = 136;
            // 
            // pnlHuffmanChr_DC
            // 
            this.pnlHuffmanChr_DC.AutoScroll = true;
            this.pnlHuffmanChr_DC.Location = new Point(4, 30);
            this.pnlHuffmanChr_DC.Name = "pnlHuffmanChr_DC";
            this.pnlHuffmanChr_DC.Size = new Size(410, 244);
            this.pnlHuffmanChr_DC.TabIndex = 1;
            // 
            // rdioHuffmanY_DC
            // 
            this.rdioHuffmanY_DC.AutoSize = true;
            this.rdioHuffmanY_DC.Location = new Point(223, 10);
            this.rdioHuffmanY_DC.Name = "rdioHuffmanY_DC";
            this.rdioHuffmanY_DC.Size = new Size(53, 17);
            this.rdioHuffmanY_DC.TabIndex = 135;
            this.rdioHuffmanY_DC.Text = "Y_DC";
            this.rdioHuffmanY_DC.UseVisualStyleBackColor = true;
            this.rdioHuffmanY_DC.CheckedChanged += new EventHandler(this.HuffmannChannelCheckedChanged_DisplayCorrectTable);
            // 
            // rdioHuffmanY_AC
            // 
            this.rdioHuffmanY_AC.AutoSize = true;
            this.rdioHuffmanY_AC.Location = new Point(165, 10);
            this.rdioHuffmanY_AC.Name = "rdioHuffmanY_AC";
            this.rdioHuffmanY_AC.Size = new Size(52, 17);
            this.rdioHuffmanY_AC.TabIndex = 134;
            this.rdioHuffmanY_AC.Text = "Y_AC";
            this.rdioHuffmanY_AC.UseVisualStyleBackColor = true;
            this.rdioHuffmanY_AC.CheckedChanged += new EventHandler(this.HuffmannChannelCheckedChanged_DisplayCorrectTable);
            // 
            // rdioHuffmanChr_DC
            // 
            this.rdioHuffmanChr_DC.AutoSize = true;
            this.rdioHuffmanChr_DC.Location = new Point(347, 10);
            this.rdioHuffmanChr_DC.Name = "rdioHuffmanChr_DC";
            this.rdioHuffmanChr_DC.Size = new Size(62, 17);
            this.rdioHuffmanChr_DC.TabIndex = 133;
            this.rdioHuffmanChr_DC.Text = "Chr_DC";
            this.rdioHuffmanChr_DC.UseVisualStyleBackColor = true;
            this.rdioHuffmanChr_DC.CheckedChanged += new EventHandler(this.HuffmannChannelCheckedChanged_DisplayCorrectTable);
            // 
            // pnlHuffmanChr_AC
            // 
            this.pnlHuffmanChr_AC.AutoScroll = true;
            this.pnlHuffmanChr_AC.Location = new Point(4, 30);
            this.pnlHuffmanChr_AC.Name = "pnlHuffmanChr_AC";
            this.pnlHuffmanChr_AC.Size = new Size(410, 244);
            this.pnlHuffmanChr_AC.TabIndex = 0;
            // 
            // rdioHuffmanChr_AC
            // 
            this.rdioHuffmanChr_AC.AutoSize = true;
            this.rdioHuffmanChr_AC.Location = new Point(281, 10);
            this.rdioHuffmanChr_AC.Name = "rdioHuffmanChr_AC";
            this.rdioHuffmanChr_AC.Size = new Size(61, 17);
            this.rdioHuffmanChr_AC.TabIndex = 132;
            this.rdioHuffmanChr_AC.Text = "Chr_AC";
            this.rdioHuffmanChr_AC.UseVisualStyleBackColor = true;
            this.rdioHuffmanChr_AC.CheckedChanged += new EventHandler(this.HuffmannChannelCheckedChanged_DisplayCorrectTable);
            // 
            // pnlHuffmanY_DC
            // 
            this.pnlHuffmanY_DC.AutoScroll = true;
            this.pnlHuffmanY_DC.Location = new Point(4, 30);
            this.pnlHuffmanY_DC.Name = "pnlHuffmanY_DC";
            this.pnlHuffmanY_DC.Size = new Size(410, 244);
            this.pnlHuffmanY_DC.TabIndex = 137;
            // 
            // pnlOptionsQuality
            // 
            this.pnlOptionsQuality.Controls.Add(this.tbarQualitySlider);
            this.pnlOptionsQuality.Controls.Add(this.grpQuality);
            this.pnlOptionsQuality.Enabled = false;
            this.pnlOptionsQuality.Location = new Point(169, 20);
            this.pnlOptionsQuality.Name = "pnlOptionsQuality";
            this.pnlOptionsQuality.Size = new Size(442, 311);
            this.pnlOptionsQuality.TabIndex = 28;
            this.pnlOptionsQuality.Visible = false;
            // 
            // tbarQualitySlider
            // 
            this.tbarQualitySlider.AutoSize = false;
            this.tbarQualitySlider.Location = new Point(20, 73);
            this.tbarQualitySlider.Maximum = 100;
            this.tbarQualitySlider.Name = "tbarQualitySlider";
            this.tbarQualitySlider.Size = new Size(219, 30);
            this.tbarQualitySlider.TabIndex = 26;
            this.tbarQualitySlider.TickStyle = TickStyle.None;
            this.tbarQualitySlider.ValueChanged += new EventHandler(this.tbarQualitySlider_ValueChanged);
            // 
            // grpQuality
            // 
            this.grpQuality.Controls.Add(this.cbMValue);
            this.grpQuality.Controls.Add(this.lblChooseMValue);
            this.grpQuality.Controls.Add(this.lblEncodingQuality);
            this.grpQuality.Controls.Add(this.lblEncodingQualityValue);
            this.grpQuality.Controls.Add(this.lblChooseQuality);
            this.grpQuality.Location = new Point(9, 0);
            this.grpQuality.Margin = new Padding(2);
            this.grpQuality.Name = "grpQuality";
            this.grpQuality.Padding = new Padding(2);
            this.grpQuality.Size = new Size(413, 305);
            this.grpQuality.TabIndex = 18;
            this.grpQuality.TabStop = false;
            this.grpQuality.Text = "Encoding quality";
            // 
            // cbMValue
            // 
            this.cbMValue.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cbMValue.FormatString = "N0";
            this.cbMValue.Items.AddRange(new object[] {
            "2",
            "4",
            "16"});
            this.cbMValue.Location = new Point(19, 138);
            this.cbMValue.Name = "cbMValue";
            this.cbMValue.Size = new Size(121, 21);
            this.cbMValue.TabIndex = 20;
            this.cbMValue.SelectedValueChanged += new EventHandler(this.cbMValue_SelectedValueChanged);
            // 
            // lblChooseMValue
            // 
            this.lblChooseMValue.AutoSize = true;
            this.lblChooseMValue.Location = new Point(16, 116);
            this.lblChooseMValue.Margin = new Padding(2, 0, 2, 0);
            this.lblChooseMValue.Name = "lblChooseMValue";
            this.lblChooseMValue.Size = new Size(87, 13);
            this.lblChooseMValue.TabIndex = 19;
            this.lblChooseMValue.Text = "Choose M value:";
            // 
            // lblEncodingQuality
            // 
            this.lblEncodingQuality.AutoSize = true;
            this.lblEncodingQuality.Location = new Point(21, 48);
            this.lblEncodingQuality.Name = "lblEncodingQuality";
            this.lblEncodingQuality.Size = new Size(42, 13);
            this.lblEncodingQuality.TabIndex = 18;
            this.lblEncodingQuality.Text = "Quality:";
            // 
            // lblEncodingQualityValue
            // 
            this.lblEncodingQualityValue.AutoSize = true;
            this.lblEncodingQualityValue.Location = new Point(65, 48);
            this.lblEncodingQualityValue.Name = "lblEncodingQualityValue";
            this.lblEncodingQualityValue.Size = new Size(13, 13);
            this.lblEncodingQualityValue.TabIndex = 17;
            this.lblEncodingQualityValue.Text = "0";
            // 
            // lblChooseQuality
            // 
            this.lblChooseQuality.AutoSize = true;
            this.lblChooseQuality.Location = new Point(16, 24);
            this.lblChooseQuality.Margin = new Padding(2, 0, 2, 0);
            this.lblChooseQuality.Name = "lblChooseQuality";
            this.lblChooseQuality.Size = new Size(113, 13);
            this.lblChooseQuality.TabIndex = 16;
            this.lblChooseQuality.Text = "Choose quality setting:";
            // 
            // pnlOptionsQuantization
            // 
            this.pnlOptionsQuantization.Controls.Add(this.grpQuantization);
            this.pnlOptionsQuantization.Enabled = false;
            this.pnlOptionsQuantization.Location = new Point(169, 20);
            this.pnlOptionsQuantization.Name = "pnlOptionsQuantization";
            this.pnlOptionsQuantization.Size = new Size(442, 305);
            this.pnlOptionsQuantization.TabIndex = 26;
            this.pnlOptionsQuantization.Visible = false;
            // 
            // grpQuantization
            // 
            this.grpQuantization.Controls.Add(this.lblQuantizationDescription);
            this.grpQuantization.Controls.Add(this.rdioQuantizationChrChannel);
            this.grpQuantization.Controls.Add(this.rdioQuantizationYChannel);
            this.grpQuantization.Controls.Add(this.pnlQuantization);
            this.grpQuantization.Location = new Point(9, 2);
            this.grpQuantization.Margin = new Padding(2);
            this.grpQuantization.Name = "grpQuantization";
            this.grpQuantization.Padding = new Padding(2);
            this.grpQuantization.Size = new Size(421, 297);
            this.grpQuantization.TabIndex = 8;
            this.grpQuantization.TabStop = false;
            this.grpQuantization.Text = "Custom Quantization";
            // 
            // lblQuantizationDescription
            // 
            this.lblQuantizationDescription.Location = new Point(5, 238);
            this.lblQuantizationDescription.Name = "lblQuantizationDescription";
            this.lblQuantizationDescription.Size = new Size(403, 40);
            this.lblQuantizationDescription.TabIndex = 132;
            this.lblQuantizationDescription.Text = resources.GetString("lblQuantizationDescription.Text");
            // 
            // rdioQuantizationChrChannel
            // 
            this.rdioQuantizationChrChannel.AutoSize = true;
            this.rdioQuantizationChrChannel.Location = new Point(313, 10);
            this.rdioQuantizationChrChannel.Name = "rdioQuantizationChrChannel";
            this.rdioQuantizationChrChannel.Size = new Size(82, 17);
            this.rdioQuantizationChrChannel.TabIndex = 131;
            this.rdioQuantizationChrChannel.Text = "Chr-channel";
            this.rdioQuantizationChrChannel.UseVisualStyleBackColor = true;
            // 
            // rdioQuantizationYChannel
            // 
            this.rdioQuantizationYChannel.AutoSize = true;
            this.rdioQuantizationYChannel.Location = new Point(234, 10);
            this.rdioQuantizationYChannel.Name = "rdioQuantizationYChannel";
            this.rdioQuantizationYChannel.Size = new Size(73, 17);
            this.rdioQuantizationYChannel.TabIndex = 8;
            this.rdioQuantizationYChannel.Text = "Y-channel";
            this.rdioQuantizationYChannel.UseVisualStyleBackColor = true;
            this.rdioQuantizationYChannel.CheckedChanged += new EventHandler(this.yQuantizationChannelChecked_DisplayYOrChrTable);
            // 
            // pnlQuantization
            // 
            this.pnlQuantization.Location = new Point(1, 15);
            this.pnlQuantization.Name = "pnlQuantization";
            this.pnlQuantization.Size = new Size(412, 273);
            this.pnlQuantization.TabIndex = 129;
            // 
            // btnClose
            // 
            this.btnClose.Location = new Point(554, 342);
            this.btnClose.Margin = new Padding(2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new Size(58, 24);
            this.btnClose.TabIndex = 30;
            this.btnClose.Text = "Cancel";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new EventHandler(this.btnClose_Click);
            // 
            // pnlOptionsEncodingMethod
            // 
            this.pnlOptionsEncodingMethod.Controls.Add(this.grpEncodingMethod);
            this.pnlOptionsEncodingMethod.Location = new Point(169, 20);
            this.pnlOptionsEncodingMethod.Name = "pnlOptionsEncodingMethod";
            this.pnlOptionsEncodingMethod.Size = new Size(442, 311);
            this.pnlOptionsEncodingMethod.TabIndex = 25;
            // 
            // grpEncodingMethod
            // 
            this.grpEncodingMethod.Controls.Add(this.rdioLSBMethod);
            this.grpEncodingMethod.Controls.Add(this.rdioGTMethod);
            this.grpEncodingMethod.Controls.Add(this.lblEncodingMethod);
            this.grpEncodingMethod.Location = new Point(9, 0);
            this.grpEncodingMethod.Margin = new Padding(2);
            this.grpEncodingMethod.Name = "grpEncodingMethod";
            this.grpEncodingMethod.Padding = new Padding(2);
            this.grpEncodingMethod.Size = new Size(421, 303);
            this.grpEncodingMethod.TabIndex = 23;
            this.grpEncodingMethod.TabStop = false;
            this.grpEncodingMethod.Text = "Encoding Method";
            // 
            // rdioLSBMethod
            // 
            this.rdioLSBMethod.AutoSize = true;
            this.rdioLSBMethod.Location = new Point(135, 44);
            this.rdioLSBMethod.Name = "rdioLSBMethod";
            this.rdioLSBMethod.Size = new Size(129, 17);
            this.rdioLSBMethod.TabIndex = 19;
            this.rdioLSBMethod.Text = "Least Significant Bit(s)";
            this.rdioLSBMethod.UseVisualStyleBackColor = true;
            // 
            // rdioGTMethod
            // 
            this.rdioGTMethod.AutoSize = true;
            this.rdioGTMethod.Checked = true;
            this.rdioGTMethod.Location = new Point(27, 44);
            this.rdioGTMethod.Name = "rdioGTMethod";
            this.rdioGTMethod.Size = new Size(102, 17);
            this.rdioGTMethod.TabIndex = 18;
            this.rdioGTMethod.TabStop = true;
            this.rdioGTMethod.Text = "Graph Theoretic";
            this.rdioGTMethod.UseVisualStyleBackColor = true;
            // 
            // lblEncodingMethod
            // 
            this.lblEncodingMethod.AutoSize = true;
            this.lblEncodingMethod.Location = new Point(21, 27);
            this.lblEncodingMethod.Margin = new Padding(2, 0, 2, 0);
            this.lblEncodingMethod.Name = "lblEncodingMethod";
            this.lblEncodingMethod.Size = new Size(125, 13);
            this.lblEncodingMethod.TabIndex = 17;
            this.lblEncodingMethod.Text = "Select encoding method:";
            // 
            // btnDefault
            // 
            this.btnDefault.Location = new Point(169, 342);
            this.btnDefault.Name = "btnDefault";
            this.btnDefault.Size = new Size(75, 23);
            this.btnDefault.TabIndex = 31;
            this.btnDefault.Text = "Default";
            this.btnDefault.UseVisualStyleBackColor = true;
            this.btnDefault.Click += new EventHandler(this.btnDefault_Click);
            // 
            // OptionsForm
            // 
            this.AutoScaleMode = AutoScaleMode.None;
            this.ClientSize = new Size(623, 377);
            this.Controls.Add(this.btnDefault);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.line);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.OptionsBox);
            this.Controls.Add(this.pnlOptionsQuality);
            this.Controls.Add(this.pnlOptionsEncodingMethod);
            this.Controls.Add(this.pnlOptionsHuffman);
            this.Controls.Add(this.pnlOptionsQuantization);
            this.Icon = ((Icon)(resources.GetObject("$this.Icon")));
            this.Name = "OptionsForm";
            this.SizeGripStyle = SizeGripStyle.Hide;
            this.Text = "Stegosaurus - Options";
            this.pnlOptionsHuffman.ResumeLayout(false);
            this.grpCustomHuffman.ResumeLayout(false);
            this.grpCustomHuffman.PerformLayout();
            this.pnlOptionsQuality.ResumeLayout(false);
            ((ISupportInitialize)(this.tbarQualitySlider)).EndInit();
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
        private Label line;
        private Button btnSave;
        private ListBox OptionsBox;
        private Panel pnlOptionsHuffman;
        private Panel pnlOptionsQuality;
        private GroupBox grpQuality;
        private Label lblChooseQuality;
        private Panel pnlOptionsQuantization;
        private GroupBox grpQuantization;
        private TrackBar tbarQualitySlider;
        private Panel pnlQuantization;
        private RadioButton rdioQuantizationChrChannel;
        private RadioButton rdioQuantizationYChannel;
        private Label lblQuantizationDescription;
        private Button btnHuffmanAddRow;
        private GroupBox grpCustomHuffman;
        private Panel pnlHuffmanY_AC;
        private Panel pnlHuffmanChr_DC;
        private RadioButton rdioHuffmanY_DC;
        private RadioButton rdioHuffmanY_AC;
        private RadioButton rdioHuffmanChr_DC;
        private Panel pnlHuffmanChr_AC;
        private RadioButton rdioHuffmanChr_AC;
        private Panel pnlHuffmanY_DC;
        private FolderBrowserDialog selectOutputFolder;
        private Label lblEncodingQualityValue;
        private Button btnClose;
        private Label lblEncodingQuality;
        private Panel pnlOptionsEncodingMethod;
        private GroupBox grpEncodingMethod;
        private RadioButton rdioLSBMethod;
        private RadioButton rdioGTMethod;
        private Label lblEncodingMethod;
        private Button btnDefault;
        private Label lblChooseMValue;
        private ComboBox cbMValue;
    }
}