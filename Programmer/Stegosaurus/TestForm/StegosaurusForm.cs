using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Stegosaurus;
using System.Collections.Generic;

namespace TestForm{
    public partial class StegosaurusForm:Form {
        public HuffmanTable huffmanTableChr_AC;
        public HuffmanTable huffmanTableChr_DC;
        public HuffmanTable huffmanTableY_AC;
        public HuffmanTable huffmanTableY_DC;

        private readonly LeastSignificantBitImage StegoController;
        private bool CoverImageSetLSB, MessageImageSetLSB, CoverImageSetGT, MessageFileSetGT;


        public int tbarQualityValue {
            get
            {
                return this.tbarQualitySlider.Value;
            }
            set
            {
                this.tbarQualitySlider.Value = value;
            }
        }

        public StegosaurusForm() {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            StegoController = new LeastSignificantBitImage();
        }

        private void loadStego_Click(object sender, EventArgs e) {
            getFileStego.ShowDialog();
        }

        /*
        private void Encode_Click(object sender, EventArgs e) {
            StegoController.Encode();

            picStego.Image = StegoController.StegoImage;
            Decode.Enabled = true;
            StegoController.StegoImage.Save("./encrypted.png");
        }

        private void Decode_Click(object sender, EventArgs e) {
            StegoController.Decode();

            picMessage.Image = StegoController.MessageImage;
            StegoController.MessageImage.Save("./decrypted.png");
        }
        */
        private void getFileCover_FileOk(object sender, CancelEventArgs e) {
            StegoController.InputImage = new Bitmap(getFileCoverLSB.FileName);
            StegoController.StegoImage = new Bitmap(getFileCoverLSB.FileName);
            picInput.Image = StegoController.InputImage;
            CoverImageSetLSB = true;

            if (MessageImageSetLSB || rdioDecode.Checked) {
                btnProceed.Enabled = true;
            }
        }

        private void getFileMessage_FileOk(object sender, CancelEventArgs e) {
            StegoController.MessageImage = new Bitmap(getFileMessageLSB.FileName);
            picMessage.Image = StegoController.MessageImage;
            MessageImageSetLSB = true;

            if (CoverImageSetLSB) {
                btnProceed.Enabled = true;
            }
        }

        private void loadCover_Click_1(object sender, EventArgs e)
        {
            getFileCoverLSB.ShowDialog();
        }

        private void loadMessage_Click_1(object sender, EventArgs e)
        {
            getFileMessageLSB.ShowDialog();
        }

        private void viewOptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OptionsForm optionsForm = new OptionsForm();
            optionsForm.ShowDialog();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.Show();
        }

        private void showHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HelpForm helpForm = new HelpForm();
            helpForm.Show();
        }

        private void tbarQualityChanged(object sender, EventArgs e)
        {
            tbarQualityValue = tbarQualitySlider.Value;
            lblQuality.Text = tbarQualityValue.ToString();
        }

        private void DisplayLoadMessage(object sender, EventArgs e)
        {
            if (rdioEncode.Checked == true)
            {
                btnLoadMessage.Enabled = true;
                btnProceed.Text = "Encode";
                if (!MessageImageSetLSB)
                {
                    btnProceed.Enabled = false;
                }
            }
            else
            {
                btnLoadMessage.Enabled = false;
                picMessage.Image = null;
                MessageImageSetLSB = false;
                btnProceed.Text = "Decode";
                if (CoverImageSetLSB)
                {
                    btnProceed.Enabled = true;
                }
            }
        }

        //encode or decode
        private void btProceed_Click(object sender, EventArgs e) 
        {
            if (rdioEncode.Checked == true)
            {
                Cursor.Current = Cursors.WaitCursor;
                StegoController.Encode();
                Cursor.Current = Cursors.Default;

                picResult.Image = StegoController.StegoImage;
                StegoController.StegoImage.Save("./encrypted.png");
            }
            else if (rdioDecode.Checked == true)
            {
                Cursor.Current = Cursors.WaitCursor;
                StegoController.Decode();
                Cursor.Current = Cursors.Default;

                picResult.Image = StegoController.MessageImage;
                StegoController.MessageImage.Save("./decrypted.png");
            }
        }

        private void getFileStego_FileOk(object sender, CancelEventArgs e) {
            StegoController.StegoImage = new Bitmap(getFileStego.FileName);
            picResult.Image = StegoController.StegoImage;
            
            Decode.Enabled = true;
        }

        #region Graph Theoretic
        private void btnGTLoadMessageFile_Click(object sender, EventArgs e)
        {
            GetFileMessageGT.ShowDialog();
        }

        private void GetFileMessageGT_FileOk(object sender, CancelEventArgs e)
        {
            textBox1.Text = GetFileMessageGT.SafeFileName;
            MessageFileSetGT = true;

            if (CoverImageSetGT || rdioGTEncode.Checked)
            {
                btnGTProceed.Enabled = true;
            }
        }

        private void rdioGTEncode_CheckedChanged(object sender, EventArgs e)
        {
            if (rdioGTEncode.Checked == true)
            {
                btnGTLoadMessageFile.Enabled = true;
                btnGTProceed.Text = "Encode";
                if (!MessageFileSetGT)
                {
                    btnProceed.Enabled = false;
                }
            }
            else if (rdioGTDecode.Checked == true)
            {
                btnGTLoadMessageFile.Enabled = false;
                textBox1.Text = null;
                MessageImageSetLSB = false;
                btnGTProceed.Text = "Decode";
                if (CoverImageSetGT)
                {
                    btnGTProceed.Enabled = true;
                }
            }
        }

        private void btnGTLoadInput_Click(object sender, EventArgs e)
        {
            getFileInputGT.ShowDialog();
        }


        private void getFileInputGT_FileOk(object sender, CancelEventArgs e)
        {
            CoverImageSetGT = true;
            //picGTInput.Image =

            if (MessageFileSetGT || rdioGTDecode.Checked)
            {
                btnGTProceed.Enabled = true;
            }
        }
        #endregion


        //'Escape' closes form
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                DialogResult dialogResult = MessageBox.Show("Do you want to close Stegosaurus?", "", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    this.Close();
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
