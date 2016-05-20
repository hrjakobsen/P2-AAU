﻿namespace TestForm
{
    partial class HelpForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HelpForm));
            this.pnlHelp2 = new System.Windows.Forms.Panel();
            this.grpQuality = new System.Windows.Forms.GroupBox();
            this.lblQuality = new System.Windows.Forms.Label();
            this.pnlHelp3 = new System.Windows.Forms.Panel();
            this.grpEncoding = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.line = new System.Windows.Forms.Label();
            this.Okay = new System.Windows.Forms.Button();
            this.grpHelpGeneral = new System.Windows.Forms.GroupBox();
            this.General = new System.Windows.Forms.Label();
            this.pnlHelp4 = new System.Windows.Forms.Panel();
            this.grpHuffman = new System.Windows.Forms.GroupBox();
            this.lblHuffman = new System.Windows.Forms.Label();
            this.pnlHelp5 = new System.Windows.Forms.Panel();
            this.grpHelp5 = new System.Windows.Forms.GroupBox();
            this.Quantization = new System.Windows.Forms.Label();
            this.pnlHelpGeneral = new System.Windows.Forms.Panel();
            this.HelpBox = new System.Windows.Forms.ListBox();
            this.pnlHelp2.SuspendLayout();
            this.grpQuality.SuspendLayout();
            this.pnlHelp3.SuspendLayout();
            this.grpEncoding.SuspendLayout();
            this.grpHelpGeneral.SuspendLayout();
            this.pnlHelp4.SuspendLayout();
            this.grpHuffman.SuspendLayout();
            this.pnlHelp5.SuspendLayout();
            this.grpHelp5.SuspendLayout();
            this.pnlHelpGeneral.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHelp2
            // 
            this.pnlHelp2.Controls.Add(this.grpQuality);
            this.pnlHelp2.Enabled = false;
            this.pnlHelp2.Location = new System.Drawing.Point(169, 20);
            this.pnlHelp2.Name = "pnlHelp2";
            this.pnlHelp2.Size = new System.Drawing.Size(442, 271);
            this.pnlHelp2.TabIndex = 28;
            this.pnlHelp2.Visible = false;
            // 
            // grpQuality
            // 
            this.grpQuality.Controls.Add(this.lblQuality);
            this.grpQuality.Location = new System.Drawing.Point(9, 0);
            this.grpQuality.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.grpQuality.Name = "grpQuality";
            this.grpQuality.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.grpQuality.Size = new System.Drawing.Size(413, 269);
            this.grpQuality.TabIndex = 8;
            this.grpQuality.TabStop = false;
            this.grpQuality.Text = "Quality Setting";
            // 
            // lblQuality
            // 
            this.lblQuality.AutoSize = true;
            this.lblQuality.Location = new System.Drawing.Point(14, 24);
            this.lblQuality.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblQuality.Name = "lblQuality";
            this.lblQuality.Size = new System.Drawing.Size(366, 65);
            this.lblQuality.TabIndex = 0;
            this.lblQuality.Text = resources.GetString("lblQuality.Text");
            // 
            // pnlHelp3
            // 
            this.pnlHelp3.Controls.Add(this.grpEncoding);
            this.pnlHelp3.Enabled = false;
            this.pnlHelp3.Location = new System.Drawing.Point(169, 20);
            this.pnlHelp3.Name = "pnlHelp3";
            this.pnlHelp3.Size = new System.Drawing.Size(442, 271);
            this.pnlHelp3.TabIndex = 29;
            this.pnlHelp3.Visible = false;
            // 
            // grpEncoding
            // 
            this.grpEncoding.Controls.Add(this.label1);
            this.grpEncoding.Location = new System.Drawing.Point(9, 0);
            this.grpEncoding.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.grpEncoding.Name = "grpEncoding";
            this.grpEncoding.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.grpEncoding.Size = new System.Drawing.Size(413, 269);
            this.grpEncoding.TabIndex = 8;
            this.grpEncoding.TabStop = false;
            this.grpEncoding.Text = "Encoding Method";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 24);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(397, 130);
            this.label1.TabIndex = 0;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // line
            // 
            this.line.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.line.Location = new System.Drawing.Point(166, 294);
            this.line.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.line.Name = "line";
            this.line.Size = new System.Drawing.Size(446, 2);
            this.line.TabIndex = 29;
            // 
            // Okay
            // 
            this.Okay.Location = new System.Drawing.Point(554, 315);
            this.Okay.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Okay.Name = "Okay";
            this.Okay.Size = new System.Drawing.Size(58, 24);
            this.Okay.TabIndex = 28;
            this.Okay.Text = "Okay";
            this.Okay.UseVisualStyleBackColor = true;
            this.Okay.Click += new System.EventHandler(this.Okay_Click);
            // 
            // grpHelpGeneral
            // 
            this.grpHelpGeneral.Controls.Add(this.General);
            this.grpHelpGeneral.Location = new System.Drawing.Point(9, 0);
            this.grpHelpGeneral.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.grpHelpGeneral.Name = "grpHelpGeneral";
            this.grpHelpGeneral.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.grpHelpGeneral.Size = new System.Drawing.Size(413, 269);
            this.grpHelpGeneral.TabIndex = 8;
            this.grpHelpGeneral.TabStop = false;
            this.grpHelpGeneral.Text = "General help";
            this.grpHelpGeneral.Enter += new System.EventHandler(this.grpHelpGeneral_Enter);
            // 
            // General
            // 
            this.General.AutoSize = true;
            this.General.Location = new System.Drawing.Point(15, 24);
            this.General.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.General.Name = "General";
            this.General.Size = new System.Drawing.Size(371, 143);
            this.General.TabIndex = 32;
            this.General.Text = resources.GetString("General.Text");
            // 
            // pnlHelp4
            // 
            this.pnlHelp4.Controls.Add(this.grpHuffman);
            this.pnlHelp4.Enabled = false;
            this.pnlHelp4.Location = new System.Drawing.Point(169, 20);
            this.pnlHelp4.Name = "pnlHelp4";
            this.pnlHelp4.Size = new System.Drawing.Size(442, 271);
            this.pnlHelp4.TabIndex = 30;
            this.pnlHelp4.Visible = false;
            // 
            // grpHuffman
            // 
            this.grpHuffman.Controls.Add(this.lblHuffman);
            this.grpHuffman.Location = new System.Drawing.Point(9, 0);
            this.grpHuffman.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.grpHuffman.Name = "grpHuffman";
            this.grpHuffman.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.grpHuffman.Size = new System.Drawing.Size(413, 269);
            this.grpHuffman.TabIndex = 8;
            this.grpHuffman.TabStop = false;
            this.grpHuffman.Text = "Custom Huffman";
            // 
            // lblHuffman
            // 
            this.lblHuffman.AutoSize = true;
            this.lblHuffman.Location = new System.Drawing.Point(14, 24);
            this.lblHuffman.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblHuffman.Name = "lblHuffman";
            this.lblHuffman.Size = new System.Drawing.Size(375, 104);
            this.lblHuffman.TabIndex = 0;
            this.lblHuffman.Text = resources.GetString("lblHuffman.Text");
            // 
            // pnlHelp5
            // 
            this.pnlHelp5.Controls.Add(this.grpHelp5);
            this.pnlHelp5.Enabled = false;
            this.pnlHelp5.Location = new System.Drawing.Point(169, 20);
            this.pnlHelp5.Name = "pnlHelp5";
            this.pnlHelp5.Size = new System.Drawing.Size(442, 271);
            this.pnlHelp5.TabIndex = 31;
            this.pnlHelp5.Visible = false;
            // 
            // grpHelp5
            // 
            this.grpHelp5.Controls.Add(this.Quantization);
            this.grpHelp5.Location = new System.Drawing.Point(9, 0);
            this.grpHelp5.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.grpHelp5.Name = "grpHelp5";
            this.grpHelp5.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.grpHelp5.Size = new System.Drawing.Size(413, 269);
            this.grpHelp5.TabIndex = 8;
            this.grpHelp5.TabStop = false;
            this.grpHelp5.Text = "Custom Quantization";
            // 
            // Quantization
            // 
            this.Quantization.AutoSize = true;
            this.Quantization.Location = new System.Drawing.Point(14, 24);
            this.Quantization.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Quantization.Name = "Quantization";
            this.Quantization.Size = new System.Drawing.Size(375, 117);
            this.Quantization.TabIndex = 32;
            this.Quantization.Text = resources.GetString("Quantization.Text");
            // 
            // pnlHelpGeneral
            // 
            this.pnlHelpGeneral.Controls.Add(this.grpHelpGeneral);
            this.pnlHelpGeneral.Location = new System.Drawing.Point(169, 20);
            this.pnlHelpGeneral.Name = "pnlHelpGeneral";
            this.pnlHelpGeneral.Size = new System.Drawing.Size(442, 271);
            this.pnlHelpGeneral.TabIndex = 27;
            // 
            // HelpBox
            // 
            this.HelpBox.FormattingEnabled = true;
            this.HelpBox.Items.AddRange(new object[] {
            "General",
            "Quality Setting",
            "Encoding Method",
            "Huffman Tables",
            "Quantization Tables"});
            this.HelpBox.Location = new System.Drawing.Point(14, 20);
            this.HelpBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.HelpBox.Name = "HelpBox";
            this.HelpBox.Size = new System.Drawing.Size(144, 277);
            this.HelpBox.TabIndex = 25;
            this.HelpBox.SelectedIndexChanged += new System.EventHandler(this.HelpBox_SelectedIndexChanged);
            // 
            // HelpForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(623, 350);
            this.Controls.Add(this.line);
            this.Controls.Add(this.HelpBox);
            this.Controls.Add(this.Okay);
            this.Controls.Add(this.pnlHelp5);
            this.Controls.Add(this.pnlHelp4);
            this.Controls.Add(this.pnlHelp3);
            this.Controls.Add(this.pnlHelp2);
            this.Controls.Add(this.pnlHelpGeneral);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "HelpForm";
            this.Text = "Stegosaurus - Help";
            this.pnlHelp2.ResumeLayout(false);
            this.grpQuality.ResumeLayout(false);
            this.grpQuality.PerformLayout();
            this.pnlHelp3.ResumeLayout(false);
            this.grpEncoding.ResumeLayout(false);
            this.grpEncoding.PerformLayout();
            this.grpHelpGeneral.ResumeLayout(false);
            this.grpHelpGeneral.PerformLayout();
            this.pnlHelp4.ResumeLayout(false);
            this.grpHuffman.ResumeLayout(false);
            this.grpHuffman.PerformLayout();
            this.pnlHelp5.ResumeLayout(false);
            this.grpHelp5.ResumeLayout(false);
            this.grpHelp5.PerformLayout();
            this.pnlHelpGeneral.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pnlHelp2;
        private System.Windows.Forms.Panel pnlHelp3;
        private System.Windows.Forms.GroupBox grpEncoding;
        private System.Windows.Forms.GroupBox grpQuality;
        private System.Windows.Forms.Label line;
        private System.Windows.Forms.Button Okay;
        private System.Windows.Forms.GroupBox grpHelpGeneral;
        private System.Windows.Forms.Panel pnlHelp4;
        private System.Windows.Forms.GroupBox grpHuffman;
        private System.Windows.Forms.Panel pnlHelp5;
        private System.Windows.Forms.GroupBox grpHelp5;
        private System.Windows.Forms.Panel pnlHelpGeneral;
        private System.Windows.Forms.ListBox HelpBox;
        private System.Windows.Forms.Label General;
        private System.Windows.Forms.Label Quantization;
        private System.Windows.Forms.Label lblHuffman;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblQuality;
    }
}