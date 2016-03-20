namespace BeamerViewer {
    partial class Presenter {
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
            this.nextslide = new System.Windows.Forms.PictureBox();
            this.notes = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.time = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.nextslide)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.notes)).BeginInit();
            this.SuspendLayout();
            // 
            // nextslide
            // 
            this.nextslide.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nextslide.Location = new System.Drawing.Point(172, 12);
            this.nextslide.Name = "nextslide";
            this.nextslide.Size = new System.Drawing.Size(100, 90);
            this.nextslide.TabIndex = 0;
            this.nextslide.TabStop = false;
            // 
            // notes
            // 
            this.notes.Location = new System.Drawing.Point(12, 12);
            this.notes.Name = "notes";
            this.notes.Size = new System.Drawing.Size(154, 190);
            this.notes.TabIndex = 1;
            this.notes.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // time
            // 
            this.time.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.time.AutoSize = true;
            this.time.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.time.Location = new System.Drawing.Point(204, 222);
            this.time.Name = "time";
            this.time.Size = new System.Drawing.Size(68, 30);
            this.time.TabIndex = 2;
            this.time.Text = "label1";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // Presenter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.time);
            this.Controls.Add(this.notes);
            this.Controls.Add(this.nextslide);
            this.Name = "Presenter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Presenter";
            this.Load += new System.EventHandler(this.Presenter_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Presenter_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.nextslide)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.notes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox nextslide;
        private System.Windows.Forms.PictureBox notes;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label time;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}