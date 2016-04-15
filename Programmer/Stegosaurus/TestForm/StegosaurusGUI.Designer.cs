namespace TestForm {
    partial class StegosaurusGUI {
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
            this.getFileCover = new System.Windows.Forms.OpenFileDialog();
            this.getFileMessage = new System.Windows.Forms.OpenFileDialog();
            this.getFileStego = new System.Windows.Forms.OpenFileDialog();
            this.line = new System.Windows.Forms.Label();
            this.picStego = new System.Windows.Forms.PictureBox();
            this.btnDecode = new System.Windows.Forms.Button();
            this.btnEncode = new System.Windows.Forms.Button();
            this.loadMessage = new System.Windows.Forms.Button();
            this.loadCover = new System.Windows.Forms.Button();
            this.picMessage = new System.Windows.Forms.PictureBox();
            this.picCover = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.showHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.picStego)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMessage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCover)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // getFileCover
            // 
            this.getFileCover.FileName = "Select an image to be the Cover";
            this.getFileCover.FileOk += new System.ComponentModel.CancelEventHandler(this.getFileCover_FileOk);
            // 
            // getFileMessage
            // 
            this.getFileMessage.FileName = "Select an image with half the width and height and width of the Cover image";
            this.getFileMessage.FileOk += new System.ComponentModel.CancelEventHandler(this.getFileMessage_FileOk);
            // 
            // getFileStego
            // 
            this.getFileStego.FileName = "Select an Encodeed photo to extract a photo from";
            this.getFileStego.FileOk += new System.ComponentModel.CancelEventHandler(this.getFileStego_FileOk);
            // 
            // line
            // 
            this.line.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.line.Location = new System.Drawing.Point(4, 25);
            this.line.Name = "line";
            this.line.Size = new System.Drawing.Size(576, 2);
            this.line.TabIndex = 24;
            // 
            // picStego
            // 
            this.picStego.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picStego.Location = new System.Drawing.Point(352, 34);
            this.picStego.Name = "picStego";
            this.picStego.Size = new System.Drawing.Size(216, 216);
            this.picStego.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picStego.TabIndex = 22;
            this.picStego.TabStop = false;
            // 
            // btnDecode
            // 
            this.btnDecode.Location = new System.Drawing.Point(249, 227);
            this.btnDecode.Name = "btnDecode";
            this.btnDecode.Size = new System.Drawing.Size(86, 23);
            this.btnDecode.TabIndex = 21;
            this.btnDecode.Text = "Decode";
            this.btnDecode.UseVisualStyleBackColor = true;
            // 
            // btnEncode
            // 
            this.btnEncode.Enabled = false;
            this.btnEncode.Location = new System.Drawing.Point(249, 198);
            this.btnEncode.Name = "btnEncode";
            this.btnEncode.Size = new System.Drawing.Size(86, 23);
            this.btnEncode.TabIndex = 20;
            this.btnEncode.Text = "Encode";
            this.btnEncode.UseVisualStyleBackColor = true;
            // 
            // loadMessage
            // 
            this.loadMessage.Location = new System.Drawing.Point(249, 169);
            this.loadMessage.Name = "loadMessage";
            this.loadMessage.Size = new System.Drawing.Size(86, 23);
            this.loadMessage.TabIndex = 19;
            this.loadMessage.Text = "Load image";
            this.loadMessage.UseVisualStyleBackColor = true;
            // 
            // loadCover
            // 
            this.loadCover.Location = new System.Drawing.Point(81, 256);
            this.loadCover.Name = "loadCover";
            this.loadCover.Size = new System.Drawing.Size(86, 23);
            this.loadCover.TabIndex = 18;
            this.loadCover.Text = "Load image";
            this.loadCover.UseVisualStyleBackColor = true;
            // 
            // picMessage
            // 
            this.picMessage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picMessage.Location = new System.Drawing.Point(238, 55);
            this.picMessage.Name = "picMessage";
            this.picMessage.Size = new System.Drawing.Size(108, 108);
            this.picMessage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picMessage.TabIndex = 17;
            this.picMessage.TabStop = false;
            // 
            // picCover
            // 
            this.picCover.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picCover.Location = new System.Drawing.Point(16, 34);
            this.picCover.Name = "picCover";
            this.picCover.Size = new System.Drawing.Size(216, 216);
            this.picCover.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picCover.TabIndex = 16;
            this.picCover.TabStop = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem,
            this.helpToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(583, 24);
            this.menuStrip1.TabIndex = 23;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "View";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.optionsToolStripMenuItem.Text = "Options";
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
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // StegosaurusGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(583, 290);
            this.Controls.Add(this.line);
            this.Controls.Add(this.picStego);
            this.Controls.Add(this.btnDecode);
            this.Controls.Add(this.btnEncode);
            this.Controls.Add(this.loadMessage);
            this.Controls.Add(this.loadCover);
            this.Controls.Add(this.picMessage);
            this.Controls.Add(this.picCover);
            this.Controls.Add(this.menuStrip1);
            this.Name = "StegosaurusGUI";
            this.Text = "Stegosaurus";
            ((System.ComponentModel.ISupportInitialize)(this.picStego)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMessage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCover)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog getFileCover;
        private System.Windows.Forms.OpenFileDialog getFileMessage;
        private System.Windows.Forms.OpenFileDialog getFileStego;
        private System.Windows.Forms.Label line;
        private System.Windows.Forms.PictureBox picStego;
        private System.Windows.Forms.Button btnDecode;
        private System.Windows.Forms.Button btnEncode;
        private System.Windows.Forms.Button loadMessage;
        private System.Windows.Forms.Button loadCover;
        private System.Windows.Forms.PictureBox picMessage;
        private System.Windows.Forms.PictureBox picCover;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem showHelpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
    }
}

