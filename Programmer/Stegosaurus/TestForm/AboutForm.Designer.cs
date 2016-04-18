namespace TestForm
{
    partial class AboutForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.line = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(278, 219);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 146);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(314, 52);
            this.label2.TabIndex = 8;
            this.label2.Text = resources.GetString("label2.Text");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 91);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(241, 39);
            this.label1.TabIndex = 7;
            this.label1.Text = "DAT2-A423\r\nVersion 0.1\r\nCopyright © 2016 DAT2-A423. All rights reserved.";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(61, 16);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(244, 46);
            this.lblName.TabIndex = 6;
            this.lblName.Text = "Stegosaurus";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // line
            // 
            this.line.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.line.Location = new System.Drawing.Point(12, 72);
            this.line.Name = "line";
            this.line.Size = new System.Drawing.Size(341, 2);
            this.line.TabIndex = 5;
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 254);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.line);
            this.Name = "AboutForm";
            this.Text = "About";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label line;
    }
}