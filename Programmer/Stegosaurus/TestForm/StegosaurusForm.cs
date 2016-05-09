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
        private bool InputImageSetLSB, MessageImageSetLSB, InputImageSetGT, MessageFileSetGT;
        private string noMessageWritten = "Enter the message you would like to encode into your image.";
        public string ImageSavePath { get; set; }

        public StegosaurusForm() {
            InitializeComponent();
            tbGTMessage.Text = noMessageWritten;
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
        private void getFileInput_FileOk(object sender, CancelEventArgs e) {
            StegoController.CoverImage = new Bitmap(getFileInputLSB.FileName);
            StegoController.StegoImage = new Bitmap(getFileInputLSB.FileName);
            picInput.Image = StegoController.StegoImage;
            InputImageSetLSB = true;

            if (MessageImageSetLSB || rdioDecode.Checked) {
                btnProceed.Enabled = true;
            }
        }

        private void getFileMessage_FileOk(object sender, CancelEventArgs e) {
            StegoController.MessageImage = new Bitmap(getFileMessageLSB.FileName);
            picMessage.Image = StegoController.MessageImage;
            MessageImageSetLSB = true;

            if (InputImageSetLSB) {
                btnProceed.Enabled = true;
            }
        }

        private void loadInputImage_Click_1(object sender, EventArgs e)
        {
            getFileInputLSB.ShowDialog();
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
                if (InputImageSetLSB)
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
                StegoController.StegoImage.Save(ImageSavePath + "./encrypted.png");
            }
            else if (rdioDecode.Checked == true)
            {
                Cursor.Current = Cursors.WaitCursor;
                StegoController.Decode();
                Cursor.Current = Cursors.Default;

                picResult.Image = StegoController.MessageImage;
                StegoController.MessageImage.Save(ImageSavePath + "./decrypted.png");
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
            tbGTMessageFilePath.Text = GetFileMessageGT.SafeFileName;
            MessageFileSetGT = true;

            if (InputImageSetGT || rdioGTEncode.Checked)
            {
                btnGTProceed.Enabled = true;
            }
        }

        private void rdioGTEncode_CheckedChanged(object sender, EventArgs e)
        {
            if (rdioGTEncode.Checked == true)
            {
                btnGTLoadMessageFile.Enabled = true;
                tbGTMessage.Enabled = true;
                btnGTProceed.Text = "Encode";
                if (!MessageFileSetGT)
                {
                    btnProceed.Enabled = false;
                }
            }
            else if (rdioGTDecode.Checked == true)
            {
                btnGTLoadMessageFile.Enabled = false;
                tbGTMessage.Enabled = false;
                MessageImageSetLSB = false;
                btnGTProceed.Text = "Decode";
                if (InputImageSetGT)
                {
                    btnGTProceed.Enabled = true;
                }
            }
        }

        private void tbGTMessage_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            tbGTMessage.SelectAll();
        }

        private void tbGTMessage_TextChanged(object sender, EventArgs e)
        {
            tbGTMessage.ForeColor = SystemColors.MenuText;
        }

        private void tbGTMessage_Leave(object sender, EventArgs e)
        {
            if (tbGTMessage.Text == "")
            {
                tbGTMessage.Text = noMessageWritten;
            }
        }

        private void btnGTProceed_Click(object sender, EventArgs e)
        {
            if (tbGTMessage.Text != noMessageWritten)
            {

            }
            //else if (Image = null)
            //{

            //}
        }

        private void btnGTLoadInput_Click(object sender, EventArgs e)
        {
            getFileInputGT.ShowDialog();
        }


        private void getFileInputGT_FileOk(object sender, CancelEventArgs e)
        {
            InputImageSetGT = true;
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
