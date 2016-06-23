using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PdfiumViewer;

namespace BeamerViewer {
    class PageYielder {
        PdfDocument doc;
        public List<Image> slides { get; } = new List<Image>();
        public List<Image> notes { get; } = new List<Image>();

        public PageYielder(string path) {
            doc = PdfDocument.Load(path);
            preRender();

        }
        private void preRender() {
            int length = NumberOfPages() + 1;
            for (int i = 0; i < length; i++) {
                slides.Add(RenderSlide(i));
                notes.Add(RenderNote(i));
            }
        }

        public Image RenderSlide(int page) {
            Rectangle screenRes = Screen.PrimaryScreen.Bounds;
            Image i = doc.Render(page, (int)(screenRes.Height * 1.33), screenRes.Height * 2, 300, 300, PdfRenderFlags.None);
            return cropImage(i, new Rectangle(0, 0, (int)(screenRes.Height * 1.33), screenRes.Height));
        }

        public Image RenderNote(int page) {
            Rectangle screenRes = Screen.PrimaryScreen.Bounds;
            Image i = doc.Render(page, (int)(screenRes.Height * 1.33), screenRes.Height * 2, 300, 300, PdfRenderFlags.None);
            i.Save("this.png", ImageFormat.Png);
            return cropImage(i, new Rectangle(0,screenRes.Height, (int)(screenRes.Height * 1.33), screenRes.Height));
        }

        private static Image cropImage(Image img, Rectangle cropArea) {
            Bitmap bmpImage = new Bitmap(img);
            return bmpImage.Clone(cropArea, bmpImage.PixelFormat);
        }

        public int NumberOfPages() {
            return doc.PageCount - 1;
        }
    }
}
