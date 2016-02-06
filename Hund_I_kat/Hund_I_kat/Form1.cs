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
            cryptoImg.Save(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\encrypted.bmp");
        }

        private void decrypt_Click(object sender, EventArgs e) {
            plainImg = new Bitmap(cryptoImg.Width / 2, cryptoImg.Height / 2);

            DecryptCrypto();

            picPlain.Image = plainImg;
            plainImg.Save(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\decrypted.bmp");
        }
        
        private void getFileVessel_FileOk(object sender, CancelEventArgs e) {
            vesselImg = new Bitmap(getFileVessel.FileName);
            picVessel.Image = vesselImg;

            vessel = true;
            if (plain) {
                btnEncrypt.Enabled = true;
            }
        }

        private void getFilePlain_FileOk(object sender, CancelEventArgs e) {
            plainImg = new Bitmap(getFilePlain.FileName);
            picPlain.Image = plainImg;

            plain = true;
            if (vessel) {
                btnEncrypt.Enabled = true;
            }
        }

        private void getFileCrypto_FileOk(object sender, CancelEventArgs e) {
            cryptoImg = new Bitmap(getFileCrypto.FileName);

            picCrypto.Image = cryptoImg;
            btnDecrypt.Enabled = true;
        }

        private void EncryptPlain() {
            Color[] plainFlattened = new Color[plainImg.Height * plainImg.Width];
            progressBar.Maximum = 2 * vesselImg.Height + plainImg.Height;
            progressBar.Value = 0;

            /* Copy vessel image to crypto image */
            for (int y = 0; y < vesselImg.Height; y++) {
                progressBar.PerformStep();
                for (int x = 0; x < vesselImg.Width; x++) {
                    cryptoImg.SetPixel(x, y, removeLeastSignificant(vesselImg.GetPixel(x, y)));
                }
            }

            /* Flatten plain image */
            for (int y = 0; y < plainImg.Height; y++) {
                progressBar.PerformStep();
                for (int x = 0; x < plainImg.Width; x++) {
                    plainFlattened[y*plainImg.Width + x] = plainImg.GetPixel(x, y);
                }
            }

            int plainFlattenedPos = 0;

            /* Insert flattened plain image into crypto image */
            for (int y = 0; y < vesselImg.Height; y++) {
                progressBar.PerformStep();
                for (int x = 0; x < vesselImg.Width; x += 4) {
                    for (int plainBytePos = 0; plainBytePos < 4; plainBytePos++) {
                        cryptoImg.SetPixel(x + plainBytePos, y, addCol(cryptoImg.GetPixel(x + plainBytePos, y), plainFlattened[plainFlattenedPos], plainBytePos));
                    }
                    plainFlattenedPos++;
                }
            }
        }

        private Color removeLeastSignificant(Color vesselCol) {
            const byte maskvessel = 0xFC;

            byte r = (byte)(vesselCol.R & maskvessel);
            byte g = (byte)(vesselCol.G & maskvessel);
            byte b = (byte)(vesselCol.B & maskvessel);
            
            return Color.FromArgb(r, g, b); ;
        }

        private Color addCol(Color vesselCol, Color plainCol, int plainBytePos) {
            byte[] maskplain = { 0x3, 0xC, 0x30, 0xC0 };

            byte r = (byte)((byte)((plainCol.R & maskplain[plainBytePos]) >> (plainBytePos * 2)) + vesselCol.R);
            byte g = (byte)((byte)((plainCol.G & maskplain[plainBytePos]) >> (plainBytePos * 2)) + vesselCol.G);
            byte b = (byte)((byte)((plainCol.B & maskplain[plainBytePos]) >> (plainBytePos * 2)) + vesselCol.B);
            
            return Color.FromArgb(r, g, b);
        }

        private void DecryptCrypto() {            
            Color[] cryptoPixel = new Color[cryptoImg.Height * cryptoImg.Width];
            int cryptoPixelsCounter = 0;

            progressBar.Maximum = plainImg.Height;
            progressBar.Value = 0;

            for (int y = 0; y < cryptoImg.Height; y++) {
                for (int x = 0; x < cryptoImg.Width; x++) {
                    cryptoPixel[y * cryptoImg.Width + x] = cryptoImg.GetPixel(x, y);
                }
            }

            for (int y = 0; y < plainImg.Height; y++) {
                progressBar.PerformStep();
                for (int x = 0; x < plainImg.Width; x++) {
                    Color combinedColor = combineColors(cryptoPixel[cryptoPixelsCounter], cryptoPixel[cryptoPixelsCounter + 1], cryptoPixel[cryptoPixelsCounter + 2], cryptoPixel[cryptoPixelsCounter + 3]);
                    plainImg.SetPixel(x, y, combinedColor);
                    cryptoPixelsCounter += 4;
                }
            }
        }

        private Color combineColors(Color c1, Color c2, Color c3, Color c4) {
            const byte maskplain = 0x3;

            byte r = (byte)((byte)(c1.R & maskplain) << 6);
            r += (byte)((byte)(c2.R & maskplain) << 4);
            r += (byte)((byte)(c3.R & maskplain) << 2);
            r += (byte)(c4.R & maskplain);
            
            byte g = (byte)((byte)((byte)(c1.G & maskplain) << 6) + (byte)((byte)(c2.G & maskplain) << 4) + (byte)((byte)(c3.G & maskplain) << 2) + (byte)(c4.G & maskplain));
            byte b = (byte)((byte)((byte)(c1.B & maskplain) << 6) + (byte)((byte)(c2.B & maskplain) << 4) + (byte)((byte)(c3.B & maskplain) << 2) + (byte)(c4.B & maskplain));

            return Color.FromArgb(r, g, b);
        }
    }
}
