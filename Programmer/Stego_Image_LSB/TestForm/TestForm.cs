using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using Stego_Image_LSB;

namespace TestForm{
    public partial class TestForm:Form {
        private BaseLSB _lsbController;
        private bool CoverImageSet, MessageImageSet;

        public TestForm() {
            InitializeComponent();
        }

        private void loadCover_Click(object sender, EventArgs e) {
            getFileCover.ShowDialog();
        }

        private void loadMessage_Click(object sender, EventArgs e) {
            getFileMessage.ShowDialog();
        }

        private void loadStego_Click(object sender, EventArgs e) {
            getFileStego.ShowDialog();
        }

        private void Encode_Click(object sender, EventArgs e) {
            _lsbController = new EncodeLSB((Bitmap)picCover.Image, (Bitmap)picMessage.Image);
            
            picStego.Image = _lsbController.Steganography();
        }

        private void Decode_Click(object sender, EventArgs e) {
            _lsbController = new DecodeLSB((Bitmap)picStego.Image);

            picMessage.Image = _lsbController.Steganography();
        }

        private void getFileCover_FileOk(object sender, CancelEventArgs e) {
            picCover.Image = new Bitmap(getFileCover.FileName);
            if (picMessage.Image == null || (picMessage.Image.Width / 2 == picCover.Image.Width && picMessage.Image.Height / 2 == picCover.Image.Height)) {
                CoverImageSet = true;
                if (MessageImageSet) {
                    btnEncode.Enabled = true;
                }
            } else {
                picCover.Image = null;
                MessageBox.Show("The width and height of the cover image must be exactly double of those of the message image!");
            }
        }

        private void getFileMessage_FileOk(object sender, CancelEventArgs e) {
            picMessage.Image = new Bitmap(getFileMessage.FileName);
            if (picCover.Image == null || (picMessage.Image.Width * 2 == picCover.Image.Width && picMessage.Image.Height * 2 == picCover.Image.Height)) {
                MessageImageSet = true;
                if (CoverImageSet) {
                    btnEncode.Enabled = true;
                }
            } else {
                picMessage.Image = null;
                MessageBox.Show("The width and height of the message image must be exactly half of those of the cover image!");
            }
        }

        private void getFileStego_FileOk(object sender, CancelEventArgs e) {
            picStego.Image = new Bitmap(getFileStego.FileName);
            btnDecode.Enabled = true;
        }
    }
}
