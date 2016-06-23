using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PdfiumViewer;

namespace BeamerViewer {
    public partial class FullscreenView : Form {
        private Presenter pres;
        public FullscreenView(int width, int height, Presenter presenter) {
            InitializeComponent();
            pictureBox1.Location = new Point(Width / 2 - width / 2, 0);
            pictureBox1.Width = (int)(Screen.PrimaryScreen.Bounds.Height * 1.33);
            pictureBox1.Height = Screen.PrimaryScreen.Bounds.Height;
            pres = presenter;
        }

        public void UpdateImage(Image newImage) {
            pictureBox1.Location = new Point(Width / 2 - newImage.Width / 2, 0);
            pictureBox1.Image = newImage;
        }

        private void FullscreenView_Activated(object sender, EventArgs e) {
            pres.Focus();
        }
    }


}
