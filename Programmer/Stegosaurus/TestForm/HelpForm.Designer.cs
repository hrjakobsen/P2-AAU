namespace TestForm
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
            this.HelpBox = new System.Windows.Forms.ListBox();
            this.pnlHelpGeneral = new System.Windows.Forms.Panel();
            this.grpHelpGeneral = new System.Windows.Forms.GroupBox();
            this.pnlHelp2 = new System.Windows.Forms.Panel();
            this.grpHelp2 = new System.Windows.Forms.GroupBox();
            this.pnlHelp3 = new System.Windows.Forms.Panel();
            this.grpHelp3 = new System.Windows.Forms.GroupBox();
            this.pnlHelp4 = new System.Windows.Forms.Panel();
            this.grpHelp4 = new System.Windows.Forms.GroupBox();
            this.pnlHelp5 = new System.Windows.Forms.Panel();
            this.grpHelp5 = new System.Windows.Forms.GroupBox();
            this.line = new System.Windows.Forms.Label();
            this.Okay = new System.Windows.Forms.Button();
            this.pnlHelpGeneral.SuspendLayout();
            this.pnlHelp2.SuspendLayout();
            this.pnlHelp3.SuspendLayout();
            this.pnlHelp4.SuspendLayout();
            this.pnlHelp5.SuspendLayout();
            this.SuspendLayout();
            // 
            // HelpBox
            // 
            this.HelpBox.FormattingEnabled = true;
            this.HelpBox.Items.AddRange(new object[] {
            "General",
            "Help 2",
            "Help 3",
            "Help 4",
            "Help 5"});
            this.HelpBox.Location = new System.Drawing.Point(14, 20);
            this.HelpBox.Margin = new System.Windows.Forms.Padding(2);
            this.HelpBox.Name = "HelpBox";
            this.HelpBox.Size = new System.Drawing.Size(144, 277);
            this.HelpBox.TabIndex = 25;
            this.HelpBox.SelectedIndexChanged += new System.EventHandler(this.HelpBox_SelectedIndexChanged);
            // 
            // pnlHelpGeneral
            // 
            this.pnlHelpGeneral.Controls.Add(this.grpHelpGeneral);
            this.pnlHelpGeneral.Location = new System.Drawing.Point(169, 20);
            this.pnlHelpGeneral.Name = "pnlHelpGeneral";
            this.pnlHelpGeneral.Size = new System.Drawing.Size(442, 271);
            this.pnlHelpGeneral.TabIndex = 27;
            // 
            // grpHelpGeneral
            // 
            this.grpHelpGeneral.Location = new System.Drawing.Point(9, 0);
            this.grpHelpGeneral.Margin = new System.Windows.Forms.Padding(2);
            this.grpHelpGeneral.Name = "grpHelpGeneral";
            this.grpHelpGeneral.Padding = new System.Windows.Forms.Padding(2);
            this.grpHelpGeneral.Size = new System.Drawing.Size(413, 269);
            this.grpHelpGeneral.TabIndex = 8;
            this.grpHelpGeneral.TabStop = false;
            this.grpHelpGeneral.Text = "General help";
            // 
            // pnlHelp2
            // 
            this.pnlHelp2.Controls.Add(this.grpHelp2);
            this.pnlHelp2.Enabled = false;
            this.pnlHelp2.Location = new System.Drawing.Point(169, 20);
            this.pnlHelp2.Name = "pnlHelp2";
            this.pnlHelp2.Size = new System.Drawing.Size(442, 271);
            this.pnlHelp2.TabIndex = 28;
            this.pnlHelp2.Visible = false;
            // 
            // grpHelp2
            // 
            this.grpHelp2.Location = new System.Drawing.Point(9, 0);
            this.grpHelp2.Margin = new System.Windows.Forms.Padding(2);
            this.grpHelp2.Name = "grpHelp2";
            this.grpHelp2.Padding = new System.Windows.Forms.Padding(2);
            this.grpHelp2.Size = new System.Drawing.Size(413, 269);
            this.grpHelp2.TabIndex = 8;
            this.grpHelp2.TabStop = false;
            this.grpHelp2.Text = "Custom Quantization";
            // 
            // pnlHelp3
            // 
            this.pnlHelp3.Controls.Add(this.grpHelp3);
            this.pnlHelp3.Enabled = false;
            this.pnlHelp3.Location = new System.Drawing.Point(169, 20);
            this.pnlHelp3.Name = "pnlHelp3";
            this.pnlHelp3.Size = new System.Drawing.Size(442, 271);
            this.pnlHelp3.TabIndex = 29;
            this.pnlHelp3.Visible = false;
            // 
            // grpHelp3
            // 
            this.grpHelp3.Location = new System.Drawing.Point(9, 0);
            this.grpHelp3.Margin = new System.Windows.Forms.Padding(2);
            this.grpHelp3.Name = "grpHelp3";
            this.grpHelp3.Padding = new System.Windows.Forms.Padding(2);
            this.grpHelp3.Size = new System.Drawing.Size(413, 269);
            this.grpHelp3.TabIndex = 8;
            this.grpHelp3.TabStop = false;
            this.grpHelp3.Text = "Custom Quantization";
            // 
            // pnlHelp4
            // 
            this.pnlHelp4.Controls.Add(this.grpHelp4);
            this.pnlHelp4.Enabled = false;
            this.pnlHelp4.Location = new System.Drawing.Point(169, 20);
            this.pnlHelp4.Name = "pnlHelp4";
            this.pnlHelp4.Size = new System.Drawing.Size(442, 271);
            this.pnlHelp4.TabIndex = 30;
            this.pnlHelp4.Visible = false;
            // 
            // grpHelp4
            // 
            this.grpHelp4.Location = new System.Drawing.Point(9, 0);
            this.grpHelp4.Margin = new System.Windows.Forms.Padding(2);
            this.grpHelp4.Name = "grpHelp4";
            this.grpHelp4.Padding = new System.Windows.Forms.Padding(2);
            this.grpHelp4.Size = new System.Drawing.Size(413, 269);
            this.grpHelp4.TabIndex = 8;
            this.grpHelp4.TabStop = false;
            this.grpHelp4.Text = "Custom Quantization";
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
            this.grpHelp5.Location = new System.Drawing.Point(9, 0);
            this.grpHelp5.Margin = new System.Windows.Forms.Padding(2);
            this.grpHelp5.Name = "grpHelp5";
            this.grpHelp5.Padding = new System.Windows.Forms.Padding(2);
            this.grpHelp5.Size = new System.Drawing.Size(413, 269);
            this.grpHelp5.TabIndex = 8;
            this.grpHelp5.TabStop = false;
            this.grpHelp5.Text = "Custom Quantization";
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
            this.Okay.Margin = new System.Windows.Forms.Padding(2);
            this.Okay.Name = "Okay";
            this.Okay.Size = new System.Drawing.Size(58, 24);
            this.Okay.TabIndex = 28;
            this.Okay.Text = "Okay";
            this.Okay.UseVisualStyleBackColor = true;
            this.Okay.Click += new System.EventHandler(this.Okay_Click);
            // 
            // HelpForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(623, 350);
            this.Controls.Add(this.line);
            this.Controls.Add(this.HelpBox);
            this.Controls.Add(this.Okay);
            this.Controls.Add(this.pnlHelpGeneral);
            this.Controls.Add(this.pnlHelp5);
            this.Controls.Add(this.pnlHelp4);
            this.Controls.Add(this.pnlHelp3);
            this.Controls.Add(this.pnlHelp2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "HelpForm";
            this.Text = "Stegosaurus - Help";
            this.pnlHelpGeneral.ResumeLayout(false);
            this.pnlHelp2.ResumeLayout(false);
            this.pnlHelp3.ResumeLayout(false);
            this.pnlHelp4.ResumeLayout(false);
            this.pnlHelp5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox HelpBox;
        private System.Windows.Forms.Panel pnlHelpGeneral;
        private System.Windows.Forms.GroupBox grpHelpGeneral;
        private System.Windows.Forms.Panel pnlHelp2;
        private System.Windows.Forms.Panel pnlHelp3;
        private System.Windows.Forms.Panel pnlHelp4;
        private System.Windows.Forms.GroupBox grpHelp5;
        private System.Windows.Forms.GroupBox grpHelp4;
        private System.Windows.Forms.GroupBox grpHelp3;
        private System.Windows.Forms.GroupBox grpHelp2;
        private System.Windows.Forms.Panel pnlHelp5;
        private System.Windows.Forms.Label line;
        private System.Windows.Forms.Button Okay;
    }
}