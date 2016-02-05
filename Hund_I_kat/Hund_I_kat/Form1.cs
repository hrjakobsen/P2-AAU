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
        public Bitmap originalImg;
        public Bitmap secretImg;

        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e) {
            originalImg = new Bitmap(openFileDialog1.FileName);

            decodeImg();

            //openFileDialog2.ShowDialog();
            Application.Exit();
        }

        private void openFileDialog2_FileOk(object sender, CancelEventArgs e) {
            secretImg = new Bitmap(openFileDialog2.FileName);

            combineImgs();
        }

        public void combineImgs() {
            Bitmap finalImg = new Bitmap(originalImg.Width, originalImg.Height);

            for (int y = 0; y < originalImg.Height; y++) {
                for (int x = 0; x < originalImg.Width; x++) {
                    finalImg.SetPixel(x, y, copyOriginal(originalImg.GetPixel(x, y)));
                }
            }

            Color[] secretPixels = new Color[secretImg.Height * secretImg.Width];

            for (int y = 0; y < secretImg.Height; y++) {
                for (int x = 0; x < secretImg.Width; x++) {
                    secretPixels[y*secretImg.Width + x] = secretImg.GetPixel(x, y);
                }
            }

            int secretPixelsCounter = 0;

            for (int y = 0; y < originalImg.Height; y++) {
                for (int x = 0; x < originalImg.Width; x += 4) {
                    for (int secretByte = 0; secretByte < 4; secretByte++) {
                        finalImg.SetPixel(x + secretByte, y, addCol(finalImg.GetPixel(x + secretByte, y), secretPixels[secretPixelsCounter], secretByte));
                    }
                    secretPixelsCounter++;
                }
            }

            finalImg.Save("C:\\Users\\hhs19\\Downloads\\final.bmp");
        }

        public void decodeImg() {
            Bitmap finalImg = new Bitmap(originalImg.Width / 2, originalImg.Height / 2);
            Color[] originalPixel = new Color[originalImg.Height * originalImg.Width];
            int originalPixelsCounter = 0;

            for (int y = 0; y < originalImg.Height; y++) {
                for (int x = 0; x < originalImg.Width; x++) {
                    originalPixel[y * originalImg.Width + x] = originalImg.GetPixel(x, y);
                }
            }

            for (int y = 0; y < finalImg.Height; y++) {
                for (int x = 0; x < finalImg.Width; x++) {
                    Color combinedColor = combineToColor(originalPixel[originalPixelsCounter], originalPixel[originalPixelsCounter + 1], originalPixel[originalPixelsCounter + 2], originalPixel[originalPixelsCounter + 3]);
                    finalImg.SetPixel(x, y, combinedColor);
                    originalPixelsCounter += 4;
                }
            }

            finalImg.Save("C:\\Users\\hhs19\\Downloads\\decoded.bmp");
            Application.Exit();
        }

        public Color copyOriginal(Color originalCol) {
            byte maskOriginal = 0xFC;
            Color finalCol = new Color();
            byte R, G, B;

            R = (byte)(originalCol.R & maskOriginal);
            G = (byte)(originalCol.G & maskOriginal);
            B = (byte)(originalCol.B & maskOriginal);

            finalCol = Color.FromArgb(R, G, B);

            return finalCol;
        }

        public Color addCol(Color originalCol, Color secretCol, int secretByte) {
            byte[] maskSecret = new byte[] { 0x3, 0xC, 0x30, 0xC0 };
            Color finalCol = new Color();
            byte R, G, B;

            R = (byte)((byte)((secretCol.R & maskSecret[secretByte]) >> (secretByte * 2)) + originalCol.R);
            G = (byte)((byte)((secretCol.G & maskSecret[secretByte]) >> (secretByte * 2)) + originalCol.G);
            B = (byte)((byte)((secretCol.B & maskSecret[secretByte]) >> (secretByte * 2)) + originalCol.B);

            finalCol = Color.FromArgb(R, G, B);

            return finalCol;
        }

        public Color combineToColor(Color c1, Color c2, Color c3, Color c4) {
            byte maskSecret = 0x3;
            Color finalCol = new Color();
            byte R, G, B;

            R = (byte)((byte)(c1.R & maskSecret) << 6);
            R += (byte)((byte)(c2.R & maskSecret) << 4);
            R += (byte)((byte)(c3.R & maskSecret) << 2);
            R += (byte)(c4.R & maskSecret);
            
            G = (byte)((byte)((byte)(c1.G & maskSecret) << 6) + (byte)((byte)(c2.G & maskSecret) << 4) + (byte)((byte)(c3.G & maskSecret) << 2) + ((byte)(c4.G & maskSecret)));
            B = (byte)((byte)((byte)(c1.B & maskSecret) << 6) + (byte)((byte)(c2.B & maskSecret) << 4) + (byte)((byte)(c3.B & maskSecret) << 2) + ((byte)(c4.B & maskSecret)));
            
            finalCol = Color.FromArgb(R, G, B);

            return finalCol;
        }

    }
}
