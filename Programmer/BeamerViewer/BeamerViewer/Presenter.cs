using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeamerViewer {
    public partial class Presenter : Form {
        private int timeElapsed = 0;
        private int currentPage = 0;
        private int maxPages = 0;

        private PageYielder pdf;
        private FullscreenView fv;
        public Presenter() {
            InitializeComponent();
        }

        private Screen GetSecondaryScreen() {
            foreach (Screen screen in Screen.AllScreens) {
                if (screen != Screen.PrimaryScreen) {
                    return screen;
                }
            }
            return Screen.PrimaryScreen;
        }

        private void Presenter_Load(object sender, EventArgs e) {
            Rectangle screenRes = Screen.AllScreens[1].Bounds;
            Rectangle primaryMonitorRes = GetSecondaryScreen().WorkingArea;

            notes.SizeMode = PictureBoxSizeMode.StretchImage;
            notes.Size = new Size((int)(primaryMonitorRes.Height * 1.33) / 2, primaryMonitorRes.Height / 2);
            nextslide.SizeMode = PictureBoxSizeMode.StretchImage;
            nextslide.Size = new Size((int)(primaryMonitorRes.Height * 1.33) / 4, primaryMonitorRes.Height / 4);
            nextslide.Location = new Point(nextslide.Location.X - nextslide.Width + 30, 0);

            Location = GetSecondaryScreen().WorkingArea.Location;
            Height = primaryMonitorRes.Height; 
            Width = primaryMonitorRes.Width;
            fv = new FullscreenView((int)(screenRes.Height * 1.3333), screenRes.Height);
            fv.Location = Screen.AllScreens[1].WorkingArea.Location;
            fv.Show();

            openFileDialog1.ShowDialog();

        }

        private void timer1_Tick(object sender, EventArgs e) {
            timeElapsed++;
            time.Text = timeElapsed/60 + ":" + timeElapsed%60;
        }

        void updateSlide() {
            System.GC.Collect();
            fv.UpdateImage(pdf.GetSlide(currentPage));
            notes.Image = pdf.GetNotes(currentPage);
            nextslide.Image = pdf.GetSlide(currentPage + 1);
        }

        private void Presenter_KeyUp(object sender, KeyEventArgs e) {
            switch (e.KeyCode) {
                case Keys.Right:
                case Keys.Space:
                    if (currentPage != maxPages) {
                        currentPage++;
                        updateSlide();
                    }
                    break;
                case Keys.Left:
                case Keys.Back:
                    if (currentPage != 0) {
                        currentPage--;
                        updateSlide();
                    }
                    updateSlide();
                    break;
                case Keys.Escape:
                    Close();
                    break;
            }
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e) {
            timer1.Enabled = true;
            pdf = new PageYielder(openFileDialog1.FileName);
            maxPages = pdf.NumberOfPages();
            updateSlide();
        }
    }
}
