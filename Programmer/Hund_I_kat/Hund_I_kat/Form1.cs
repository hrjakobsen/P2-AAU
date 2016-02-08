using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApplication1 {
    public partial class Form1:Form {
        private Bitmap vesselImg, plainImg, cryptoImg;
        private bool vessel, plain;

        public Form1() {
            InitializeComponent();
        }

        private void loadVessel_Click(object sender, EventArgs e) {
            getFileVessel.ShowDialog();
        }

        private void loadPlain_Click(object sender, EventArgs e) {
            getFilePlain.ShowDialog();
        }

        private void loadCrypto_Click(object sender, EventArgs e) {
            getFileCrypto.ShowDialog();
        }

        private void encrypt_Click(object sender, EventArgs e) {
            cryptoImg = new Bitmap(vesselImg.Width, vesselImg.Height);

            EncryptPlain();

            picCrypto.Image = cryptoImg;
            btnDecrypt.Enabled = true;
            cryptoImg.Save(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\encrypted.png");
        }

        private void decrypt_Click(object sender, EventArgs e) {
            plainImg = new Bitmap(cryptoImg.Width / 2, cryptoImg.Height / 2);

            DecryptCrypto();

            picPlain.Image = plainImg;
            plainImg.Save(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\decrypted.png");
        }
        
        private void getFileVessel_FileOk(object sender, CancelEventArgs e) {
            vesselImg = new Bitmap(getFileVessel.FileName);
            if (plainImg == null || (plainImg.Width / 2 == vesselImg.Width && plainImg.Height / 2 == vesselImg.Height)) {
                picVessel.Image = vesselImg;

                vessel = true;
                if (plain) {
                    btnEncrypt.Enabled = true;
                }
            } else {
                MessageBox.Show("The width and height of the vessel image must be exactly double of those of the plain image!");
            }
        }

        private void getFilePlain_FileOk(object sender, CancelEventArgs e) {
            plainImg = new Bitmap(getFilePlain.FileName);
            if (vesselImg == null || (plainImg.Width * 2 == vesselImg.Width && plainImg.Height * 2 == vesselImg.Height)) {
                picPlain.Image = plainImg;

                plain = true;
                if (vessel) {
                    btnEncrypt.Enabled = true;
                }
            } else {
                MessageBox.Show("The width and height of the plain image must be exactly half of those of the vessel image!");
            }
        }

        private void getFileCrypto_FileOk(object sender, CancelEventArgs e) {
            cryptoImg = new Bitmap(getFileCrypto.FileName);

            picCrypto.Image = cryptoImg;
            btnDecrypt.Enabled = true;
        }

        private void EncryptPlain() {
            progressBar.Maximum = (int)(2.5*vesselImg.Height);
            progressBar.Value = 0;

            const byte vesselMask = 0xFC;
            byte[] plainMasks = { 0xC0, 0x30, 0xC, 0x3 };
            int plainArrIndex = 0;
            
            /* Flatten vessel image */
            Color[] vesselArr = ImageToArray(vesselImg);
            
            /* Flatten plain image */
            Color[] plainArr = ImageToArray(plainImg);

            /* Array for holding flattened crypto image */
            Color[] cryptoArr = new Color[vesselImg.Width * vesselImg.Height];
            
            for (int vesselArrIndex = 0; vesselArrIndex < vesselArr.Length; vesselArrIndex += 4) {
                for (int plainBytePos = 0; plainBytePos < 4; plainBytePos++) {
                    byte r = (byte)((byte)(vesselArr[vesselArrIndex + plainBytePos].R & vesselMask) + ((byte)(plainArr[plainArrIndex].R & plainMasks[plainBytePos]) >> 2 * (3 - plainBytePos)));
                    byte g = (byte)((byte)(vesselArr[vesselArrIndex + plainBytePos].G & vesselMask) + ((byte)(plainArr[plainArrIndex].G & plainMasks[plainBytePos]) >> 2 * (3 - plainBytePos)));
                    byte b = (byte)((byte)(vesselArr[vesselArrIndex + plainBytePos].B & vesselMask) + ((byte)(plainArr[plainArrIndex].B & plainMasks[plainBytePos]) >> 2 * (3 - plainBytePos)));

                    cryptoArr[vesselArrIndex + plainBytePos] = Color.FromArgb(r, g, b);
                }
                plainArrIndex++;
            }

            cryptoImg = ArrayToImage(vesselImg.Width, vesselImg.Height, cryptoArr);
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
