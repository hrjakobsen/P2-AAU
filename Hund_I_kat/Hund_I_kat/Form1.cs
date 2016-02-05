using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1 {
    public partial class Form1:Form {
        public Bitmap vesselImg;
        public Bitmap plainImg;

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

        }

        private void decrypt_Click(object sender, EventArgs e) {

        }


        private void openFileDialog1_FileOk(object sender, CancelEventArgs e) {
            vesselImg = new Bitmap(getFileVessel.FileName);
        }

        private void openFileDialog2_FileOk(object sender, CancelEventArgs e) {
            plainImg = new Bitmap(getFilePlain.FileName);
        }

        public void combineImgs() {
            Bitmap cryptoImg = new Bitmap(vesselImg.Width, vesselImg.Height);

            for (int y = 0; y < vesselImg.Height; y++) {
                for (int x = 0; x < vesselImg.Width; x++) {
                    cryptoImg.SetPixel(x, y, copyvessel(vesselImg.GetPixel(x, y)));
                }
            }

            Color[] plainPixels = new Color[plainImg.Height * plainImg.Width];

            for (int y = 0; y < plainImg.Height; y++) {
                for (int x = 0; x < plainImg.Width; x++) {
                    plainPixels[y*plainImg.Width + x] = plainImg.GetPixel(x, y);
                }
            }

            int plainPixelsCounter = 0;

            for (int y = 0; y < vesselImg.Height; y++) {
                for (int x = 0; x < vesselImg.Width; x += 4) {
                    for (int plainByte = 0; plainByte < 4; plainByte++) {
                        cryptoImg.SetPixel(x + plainByte, y, addCol(cryptoImg.GetPixel(x + plainByte, y), plainPixels[plainPixelsCounter], plainByte));
                    }
                    plainPixelsCounter++;
                }
            }

            cryptoImg.Save("C:\\Users\\hhs19\\Downloads\\crypto.bmp");
        }

        public void decodeImg() {
            Bitmap cryptoImg = new Bitmap(vesselImg.Width / 2, vesselImg.Height / 2);
            Color[] vesselPixel = new Color[vesselImg.Height * vesselImg.Width];
            int vesselPixelsCounter = 0;

            for (int y = 0; y < vesselImg.Height; y++) {
                for (int x = 0; x < vesselImg.Width; x++) {
                    vesselPixel[y * vesselImg.Width + x] = vesselImg.GetPixel(x, y);
                }
            }

            for (int y = 0; y < cryptoImg.Height; y++) {
                for (int x = 0; x < cryptoImg.Width; x++) {
                    Color combinedColor = combineToColor(vesselPixel[vesselPixelsCounter], vesselPixel[vesselPixelsCounter + 1], vesselPixel[vesselPixelsCounter + 2], vesselPixel[vesselPixelsCounter + 3]);
                    cryptoImg.SetPixel(x, y, combinedColor);
                    vesselPixelsCounter += 4;
                }
            }

            cryptoImg.Save("C:\\Users\\hhs19\\Downloads\\decoded.bmp");
            Application.Exit();
        }

        public Color copyvessel(Color vesselCol) {
            byte maskvessel = 0xFC;
            Color cryptoCol = new Color();
            byte R, G, B;

            R = (byte)(vesselCol.R & maskvessel);
            G = (byte)(vesselCol.G & maskvessel);
            B = (byte)(vesselCol.B & maskvessel);

            cryptoCol = Color.FromArgb(R, G, B);

            return cryptoCol;
        }

        public Color addCol(Color vesselCol, Color plainCol, int plainByte) {
            byte[] maskplain = new byte[] { 0x3, 0xC, 0x30, 0xC0 };
            Color cryptoCol = new Color();
            byte R, G, B;

            R = (byte)((byte)((plainCol.R & maskplain[plainByte]) >> (plainByte * 2)) + vesselCol.R);
            G = (byte)((byte)((plainCol.G & maskplain[plainByte]) >> (plainByte * 2)) + vesselCol.G);
            B = (byte)((byte)((plainCol.B & maskplain[plainByte]) >> (plainByte * 2)) + vesselCol.B);

            cryptoCol = Color.FromArgb(R, G, B);

            return cryptoCol;
        }

        public Color combineToColor(Color c1, Color c2, Color c3, Color c4) {
            byte maskplain = 0x3;
            Color cryptoCol = new Color();
            byte R, G, B;

            R = (byte)((byte)(c1.R & maskplain) << 6);
            R += (byte)((byte)(c2.R & maskplain) << 4);
            R += (byte)((byte)(c3.R & maskplain) << 2);
            R += (byte)(c4.R & maskplain);
            
            G = (byte)((byte)((byte)(c1.G & maskplain) << 6) + (byte)((byte)(c2.G & maskplain) << 4) + (byte)((byte)(c3.G & maskplain) << 2) + ((byte)(c4.G & maskplain)));
            B = (byte)((byte)((byte)(c1.B & maskplain) << 6) + (byte)((byte)(c2.B & maskplain) << 4) + (byte)((byte)(c3.B & maskplain) << 2) + ((byte)(c4.B & maskplain)));
            
            cryptoCol = Color.FromArgb(R, G, B);

            return cryptoCol;
        }
    }
}
