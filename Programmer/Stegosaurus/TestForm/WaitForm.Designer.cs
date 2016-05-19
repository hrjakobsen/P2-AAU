namespace TestForm
{
    partial class WaitForm
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
            this.lblWaitMessage = new System.Windows.Forms.Label();
            this.lblPleaseWait = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblWaitMessage
            // 
            this.lblWaitMessage.AutoSize = true;
            this.lblWaitMessage.Location = new System.Drawing.Point(-3, 21);
            this.lblWaitMessage.Name = "lblWaitMessage";
            this.lblWaitMessage.Size = new System.Drawing.Size(260, 26);
            this.lblWaitMessage.TabIndex = 0;
            this.lblWaitMessage.Text = "\"Encoding using the \" + selectedMethod + \" method, \r\nplease wait!\"";
            // 
            // lblPleaseWait
            // 
            this.lblPleaseWait.AutoSize = true;
            this.lblPleaseWait.Location = new System.Drawing.Point(95, 52);
            this.lblPleaseWait.Name = "lblPleaseWait";
            this.lblPleaseWait.Size = new System.Drawing.Size(64, 13);
            this.lblPleaseWait.TabIndex = 1;
            this.lblPleaseWait.Text = "Please wait!";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblWaitMessage);
            this.panel1.Controls.Add(this.lblPleaseWait);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(284, 100);
            this.panel1.TabIndex = 2;
            // 
            // WaitForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 94);
            this.Controls.Add(this.panel1);
            this.Name = "WaitForm";
            this.Text = "WaitForm";
            this.Shown += new System.EventHandler(this.WaitForm_Shown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblWaitMessage;
        private System.Windows.Forms.Label lblPleaseWait;
        private System.Windows.Forms.Panel panel1;
    }
}