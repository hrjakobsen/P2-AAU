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
        public FullscreenView(int width, int height) {
            InitializeComponent();
            pictureBox1.Location = new Point(Width / 2 - width / 2, 0);
            pictureBox1.Width = width;
            pictureBox1.Height = height;
        }

        public void UpdateImage(Image newImage) {
            pictureBox1.Location = new Point(Width / 2 - newImage.Width / 2, 0);
            pictureBox1.Image = newImage;
        }

    }


}
