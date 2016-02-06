using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ImageEncoding;

namespace TestOfImageEncoder {
    public partial class Form1 : Form {
        string ImageToEncodeFilename = "",
               ImageToEncodeInFilename = "",
               imageToExtractFromFilename = "";
        

        public Form1() {
            InitializeComponent();
        }

        private void EncodedImagePicker_FileOk(object sender, CancelEventArgs e) {
            ImageToEncodeFilename = EncodedImagePicker.FileName;
        }

        private void button2_Click(object sender, EventArgs e) {
            EncodedToPicker.ShowDialog();
        }

        private void EncodedToPicker_FileOk(object sender, CancelEventArgs e) {
            ImageToEncodeInFilename = EncodedToPicker.FileName;
        }

        private void button3_Click(object sender, EventArgs e) {
            if (!string.IsNullOrEmpty(ImageToEncodeFilename) && !string.IsNullOrEmpty(ImageToEncodeInFilename)) {
                encodeImage();
            } else {
                MessageBox.Show("Vælg begge filer inden du begynder at encode");
            }
        }

        private void encodeImage() {
            SaveEncoded.ShowDialog();
        }

        private void SaveEncoded_FileOk(object sender, CancelEventArgs e) {
            if (string.IsNullOrEmpty(SaveEncoded.FileName)) {
                MessageBox.Show("Du valgte ikke en gyldig placering");
            } else {
                ImageEncoder IE = new ImageEncoder(ImageToEncodeFilename);
                Bitmap EncodedImage = IE.EncodeToImage(ImageToEncodeInFilename);
                EncodedImage.Save(SaveEncoded.FileName);
            }
        }

        private void SaveExtracted_FileOk(object sender, CancelEventArgs e) {
            if (string.IsNullOrEmpty(SaveExtracted.FileName)) {
                MessageBox.Show("Du valgte ikke en gyldig placering");
            } else {
                Bitmap extractedImage = ImageEncoder.ExtractImageFromImage(imageToExtractFromFilename);
                extractedImage.Save(SaveExtracted.FileName);
            }
        }

        private void button4_Click(object sender, EventArgs e) {
            ExtractPicker.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e) {
            if (!string.IsNullOrEmpty(imageToExtractFromFilename)) {
                SaveExtracted.ShowDialog();
            }
        }

        private void ExtractPicker_FileOk(object sender, CancelEventArgs e) {
            imageToExtractFromFilename = ExtractPicker.FileName;
        }

        private void button1_Click(object sender, EventArgs e) {
            EncodedImagePicker.ShowDialog();
        }
    }
}
