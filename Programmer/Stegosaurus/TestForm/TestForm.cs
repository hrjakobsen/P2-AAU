using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Stegosaurus;

namespace TestForm{
    public partial class TestForm:Form {
        private readonly LeastSignificantBitImage StegoController;
        private bool CoverImageSet, MessageImageSet;

        public TestForm() {
            InitializeComponent();
            StegoController = new LeastSignificantBitImage();
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
            StegoController.Encode();

            picStego.Image = StegoController.StegoImage;
            btnDecode.Enabled = true;
            StegoController.StegoImage.Save("./encrypted.png");
        }

        private void Decode_Click(object sender, EventArgs e) {
            StegoController.Decode();

            picMessage.Image = StegoController.MessageImage;
            StegoController.MessageImage.Save("./decrypted.png");
        }

        private void getFileCover_FileOk(object sender, CancelEventArgs e) {
            StegoController.CoverImage = new Bitmap(getFileCover.FileName);
            picCover.Image = StegoController.CoverImage;
            CoverImageSet = true;

            if (MessageImageSet) {
                btnEncode.Enabled = true;
            }
        }

        private void getFileMessage_FileOk(object sender, CancelEventArgs e) {
            StegoController.MessageImage = new Bitmap(getFileMessage.FileName);
            picMessage.Image = StegoController.MessageImage;
            MessageImageSet = true;

            if (CoverImageSet) {
                btnEncode.Enabled = true;
            }
        }

        private void getFileStego_FileOk(object sender, CancelEventArgs e) {
            StegoController.StegoImage = new Bitmap(getFileStego.FileName);
            picStego.Image = StegoController.StegoImage;
            
            btnDecode.Enabled = true;
        }
    }
}
