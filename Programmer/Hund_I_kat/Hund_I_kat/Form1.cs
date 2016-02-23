using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace WindowsFormsApplication1 {
    public partial class Form1:Form {
        private Bitmap coverImg, plainImg, cryptoImg;
        private bool cover, plain;

        public Form1() {
            InitializeComponent();
        }

        private void loadCover_Click(object sender, EventArgs e) {
            getFileCover.ShowDialog();
        }

        private void loadPlain_Click(object sender, EventArgs e) {
            getFilePlain.ShowDialog();
        }

        private void loadCrypto_Click(object sender, EventArgs e) {
            getFileCrypto.ShowDialog();
        }

        private void encrypt_Click(object sender, EventArgs e) {
            cryptoImg = new Bitmap(coverImg.Width, coverImg.Height);

            EncryptPlain();

            picCrypto.Image = cryptoImg;
            btnDecrypt.Enabled = true;
            cryptoImg.Save("./encrypted.jpg", ImageFormat.Jpeg);
        }

        private void decrypt_Click(object sender, EventArgs e) {
            plainImg = new Bitmap(cryptoImg.Width / 2, cryptoImg.Height / 2);

            DecryptCrypto();

            picPlain.Image = plainImg;
            plainImg.Save("./decrypted.jpg", ImageFormat.Jpeg);
        }
        
        private void getFileCover_FileOk(object sender, CancelEventArgs e) {
            coverImg = new Bitmap(getFileCover.FileName);
            if (plainImg == null || (plainImg.Width / 2 == coverImg.Width && plainImg.Height / 2 == coverImg.Height)) {
                picCover.Image = coverImg;

                cover = true;
                if (plain) {
                    btnEncrypt.Enabled = true;
                }
            } else {
                MessageBox.Show("The width and height of the cover image must be exactly double of those of the plain image!");
            }
        }

        private void getFilePlain_FileOk(object sender, CancelEventArgs e) {
            plainImg = new Bitmap(getFilePlain.FileName);
            if (coverImg == null || (plainImg.Width * 2 == coverImg.Width && plainImg.Height * 2 == coverImg.Height)) {
                picPlain.Image = plainImg;

                plain = true;
                if (cover) {
                    btnEncrypt.Enabled = true;
                }
            } else {
                MessageBox.Show("The width and height of the plain image must be exactly half of those of the cover image!");
            }
        }

        private void getFileCrypto_FileOk(object sender, CancelEventArgs e) {
            cryptoImg = new Bitmap(getFileCrypto.FileName);

            picCrypto.Image = cryptoImg;
            btnDecrypt.Enabled = true;
        }

        private void EncryptPlain() {
            progressBar.Maximum = (int)(2.5*coverImg.Height);
            progressBar.Value = 0;

            const byte coverMask = 0xFC;
            byte[] plainMasks = { 0xC0, 0x30, 0xC, 0x3 };
            int plainArrIndex = 0;
            
            /* Flatten cover image */
            Color[] coverArr = ImageToArray(coverImg);
            
            /* Flatten plain image */
            Color[] plainArr = ImageToArray(plainImg);

            /* Array for holding flattened crypto image */
            Color[] cryptoArr = new Color[coverImg.Width * coverImg.Height];
            
            for (int coverArrIndex = 0; coverArrIndex < coverArr.Length; coverArrIndex += 4) {
                for (int plainBytePos = 0; plainBytePos < 4; plainBytePos++) {
                    byte r = (byte)((byte)(coverArr[coverArrIndex + plainBytePos].R & coverMask) + ((byte)(plainArr[plainArrIndex].R & plainMasks[plainBytePos]) >> 2 * (3 - plainBytePos)));
                    byte g = (byte)((byte)(coverArr[coverArrIndex + plainBytePos].G & coverMask) + ((byte)(plainArr[plainArrIndex].G & plainMasks[plainBytePos]) >> 2 * (3 - plainBytePos)));
                    byte b = (byte)((byte)(coverArr[coverArrIndex + plainBytePos].B & coverMask) + ((byte)(plainArr[plainArrIndex].B & plainMasks[plainBytePos]) >> 2 * (3 - plainBytePos)));

                    cryptoArr[coverArrIndex + plainBytePos] = Color.FromArgb(r, g, b);
                }
                plainArrIndex++;
            }

            cryptoImg = ArrayToImage(coverImg.Width, coverImg.Height, cryptoArr);
        }

        private void DecryptCrypto() {
            progressBar.Maximum = (int)(1.5 * cryptoImg.Height);
            progressBar.Value = 0;

            /* Flatten crypto image */
            Color[] cryptoArr = ImageToArray(cryptoImg);

            /* Array for holding flattened plain image */
            Color[] plainArr = new Color[cryptoImg.Width / 2 * cryptoImg.Height / 2];
            const byte maskPlain = 0x3;

            for (int plainArrIndex = 0; plainArrIndex < plainArr.Length; plainArrIndex++) {
                byte r = 0, g = 0, b = 0;
                for (int cryptoBitPos = 0; cryptoBitPos < 4; cryptoBitPos++) {
                    r += (byte)((byte)(cryptoArr[plainArrIndex * 4 + cryptoBitPos].R & maskPlain) << ((3 - cryptoBitPos) * 2));
                    g += (byte)((byte)(cryptoArr[plainArrIndex * 4 + cryptoBitPos].G & maskPlain) << ((3 - cryptoBitPos) * 2));
                    b += (byte)((byte)(cryptoArr[plainArrIndex * 4 + cryptoBitPos].B & maskPlain) << ((3 - cryptoBitPos) * 2));
                }
                plainArr[plainArrIndex] = Color.FromArgb(r, g, b);
            }

            plainImg = ArrayToImage(cryptoImg.Width / 2, cryptoImg.Height / 2, plainArr);
        }

        private Color[] ImageToArray(Bitmap imgIn) {
            int height = imgIn.Height;
            int width = imgIn.Width;
            Color[] arrOut = new Color[width * height];

            for (int y = 0; y < height; y++) {
                progressBar.PerformStep();
                for (int x = 0; x < width; x++) {
                    arrOut[y * width + x] = imgIn.GetPixel(x, y);
                }
            }

            return arrOut;
        }

        private Bitmap ArrayToImage(int width, int height, Color[] arrIn) {
            Bitmap imgOut = new Bitmap(width, height);
            int arrIndex = 0;

            for (int y = 0; y < height; y++) {
                progressBar.PerformStep();
                for (int x = 0; x < width; x++) {
                    imgOut.SetPixel(x, y, arrIn[arrIndex]);
                    arrIndex++;
                }
            }

            return imgOut;
        }
    }
}
