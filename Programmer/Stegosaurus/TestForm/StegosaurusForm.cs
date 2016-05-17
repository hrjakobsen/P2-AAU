using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Stegosaurus;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace TestForm{
    public partial class StegosaurusForm:Form
    {
        public static HuffmanTable HuffmanTableChrAC, HuffmanTableChrDC, HuffmanTableYAC, HuffmanTableYDC;
        public static QuantizationTable QuantizationTableY, QuantizationTableChr;

        private readonly LeastSignificantBitImage _stegoLsbController;
        private IImageEncoder _imageEncoder;
        private IImageDecoder _imageDecoder;
        private bool _inputImageSetLsb, _messageImageSetLsb, _inputImageSetGt, _messageFileSetGt, _messageTextSetGt;
        private byte[] _messageGT, _messageLSB;
        private const string NoMessageWrittenMessage = "Enter the message you would like to encode into your image.";
        private int _messageLength;
        private int defaultQuality = 53;

        public static bool QualityGTLocked { get; private set; }
        public static bool LSBMethodSelected;
        public static int QualityGT { get; set; }
        //public string ImagesSavePath { get; set; }
        private string myVar;

        private string ImagesSavePath
        {
            get { return myVar; }
            set
            {
                string s = value;

               // s = s.Replace("\\", "/");

                myVar = s;
            }
        }

        private Bitmap CoverImageLSB { get; set; }
        private Bitmap CoverImage { get; set; }
        private Bitmap StegoImageGt { get; set; }

        public StegosaurusForm() {
            InitializeComponent();
            tbGTMessage.Text = NoMessageWrittenMessage;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            //_stegoLsbController = new LeastSignificantBitImage();
        }

        private void StegosaurusForm_Load(object sender, EventArgs e)
        {
            //loadSettings();
        }

        private void loadSettings()
        {
            tbarGTEncodingQuality.Value = Properties.Settings.Default.QualityGT;
            ImagesSavePath = Properties.Settings.Default.ImagesFilePath;
            QualityGTLocked = Properties.Settings.Default.QualityGTLocked;
            HuffmanTableYAC = Properties.Settings.Default.HuffmanTableYAC;
            HuffmanTableYDC = Properties.Settings.Default.HuffmanTableYDC;
            HuffmanTableChrAC = Properties.Settings.Default.HuffmanTableChrAC;
            HuffmanTableChrDC = Properties.Settings.Default.HuffmanTableChrDC;
            QuantizationTableY = Properties.Settings.Default.QuantizationTableY;
            QuantizationTableChr = Properties.Settings.Default.QuantizationTableChr;
        }

        private void viewOptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OptionsForm optionsForm = new OptionsForm();
            optionsForm.ShowDialog();
            if (OptionsForm.SaveEnabled)
            {
                Cursor.Current = Cursors.WaitCursor;
                //DELETE THIS
                LSBMethodSelected = OptionsForm.LSBMethodSelected;

                if (LSBMethodSelected)
                {
                    tbarGTEncodingQuality.Enabled = false;
                    lblGTEncodingQualityValue.Text = "";
                }
                else
                {
                    tbarGTEncodingQuality.Enabled = true;
                }
                //DELETE THIS

                //loadSettingsFromOptionsForm();
                Cursor.Current = Cursors.Default;
            }
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

        private void loadSettingsFromOptionsForm()
        {
            tbarGTEncodingQuality.Value = OptionsForm.QualityGT;

            LSBMethodSelected = OptionsForm.LSBMethodSelected;

            if (LSBMethodSelected)
            {
                tbarGTEncodingQuality.Enabled = false;
                lblGTEncodingQualityValue.Text = "";
            }
            else
            {
                tbarGTEncodingQuality.Enabled = true;
            }

            if (!string.IsNullOrWhiteSpace(OptionsForm.ImagesSavePath))
            {
                ImagesSavePath = OptionsForm.ImagesSavePath;
            }

            if (OptionsForm.HuffmanTableComponentYAC.SaveTable().Equals(HuffmanTable.JpegHuffmanTableYAC))
            {
                HuffmanTableYAC = HuffmanTable.JpegHuffmanTableYAC;
            }
            else
            {
                HuffmanTableYAC = OptionsForm.HuffmanTableComponentYAC.SaveTable();
            }

            if (OptionsForm.HuffmanTableComponentYDC.SaveTable().Equals(HuffmanTable.JpegHuffmanTableYDC))
            {
                HuffmanTableYDC = HuffmanTable.JpegHuffmanTableYDC;
            }
            else
            {
                HuffmanTableYDC = OptionsForm.HuffmanTableComponentYDC.SaveTable();
            }

            if (OptionsForm.HuffmanTableComponentChrAC.SaveTable().Equals(HuffmanTable.JpegHuffmanTableChrAC))
            {
                HuffmanTableChrAC = HuffmanTable.JpegHuffmanTableChrAC;
            }
            else
            {
                HuffmanTableChrAC = OptionsForm.HuffmanTableComponentChrAC.SaveTable();
            }

            if (OptionsForm.HuffmanTableComponentChrDC.SaveTable().Equals(HuffmanTable.JpegHuffmanTableChrDC))
            {
                HuffmanTableChrDC = HuffmanTable.JpegHuffmanTableChrDC;
            }
            else
            {
                HuffmanTableChrDC = OptionsForm.HuffmanTableComponentChrDC.SaveTable();
            }

            if (OptionsForm.QuantizationTableComponentY.SaveTable().Equals(QuantizationTable.JpegDefaultYTable))
            {
                QuantizationTableY = QuantizationTable.JpegDefaultYTable;
            }
            else
            {
                QuantizationTableY = OptionsForm.QuantizationTableComponentY.SaveTable();
                tbarGTEncodingQuality.Value = defaultQuality;
            }

            if (OptionsForm.QuantizationTableComponentChr.SaveTable().Equals(QuantizationTable.JpegDefaultChrTable))
            {
                QuantizationTableChr = QuantizationTable.JpegDefaultChrTable;
            }
            else
            {
                QuantizationTableChr = OptionsForm.QuantizationTableComponentChr.SaveTable();
                tbarGTEncodingQuality.Value = defaultQuality;
            }

            //If changes have been made to a QuantizationTable, lock the quality-slider to prevet errors.
            if (!OptionsForm.QuantizationTableComponentY.SaveTable().Equals(QuantizationTable.JpegDefaultYTable) 
                || !OptionsForm.QuantizationTableComponentChr.SaveTable().Equals(QuantizationTable.JpegDefaultChrTable))
            {
                QualityGTLocked = true;
                tbarGTEncodingQuality.Enabled = false;
            }
            else
            {
                QualityGTLocked = false;
                tbarGTEncodingQuality.Enabled = true;
            }            
        }

        #region LSB

        private void loadInputImage_Click_1(object sender, EventArgs e)
        {
            getFileInputLSB.ShowDialog();
        }

        private void loadMessage_Click_1(object sender, EventArgs e)
        {
            getFileMessageLSB.ShowDialog();
        }

        private void DisplayLoadMessage(object sender, EventArgs e)
        {
            if (rdioEncode.Checked)
            {
                btnLoadMessage.Enabled = true;
                btnProceed.Text = @"Encode";
                if (!_messageImageSetLsb)
                {
                    btnProceed.Enabled = false;
                }
            }
            else
            {
                btnLoadMessage.Enabled = false;
                picMessageGT.Image = null;
                _messageImageSetLsb = false;
                btnProceed.Text = @"Decode";
                if (_inputImageSetLsb)
                {
                    btnProceed.Enabled = true;
                }
            }
        }

        private void getFileInput_FileOk(object sender, CancelEventArgs e) {
            CoverImageLSB = new Bitmap(getFileInputLSB.FileName);

            picInput.Image = CoverImageLSB;
            _inputImageSetLsb = true;

            if (_messageImageSetLsb || rdioDecode.Checked) {
                btnProceed.Enabled = true;
            }
        }

        private void getFileMessageLSB_FileOk(object sender, CancelEventArgs e) {
            


            _messageLSB = File.ReadAllBytes(getFileMessageLSB.FileName);
            picMessageGT.Image = new Bitmap(getFileMessageLSB.FileName);
            _messageImageSetLsb = true;

            if (_inputImageSetLsb) {
                btnProceed.Enabled = true;
            }
        }

        //encode or decode
        private void btProceed_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (rdioEncode.Checked)
            {
                _imageEncoder = new LeastSignificantBitImage(CoverImageLSB);
                Bitmap oldStegoImage = (Bitmap)picMessageGT.Image;
                _imageEncoder.Encode(_messageLSB);
                _imageEncoder.Save(ImagesSavePath + "encryptedImageLSB.jpg");
                picGTResult.Image = Image.FromFile(ImagesSavePath + "encryptedImageLSB.jpg");
            }
            else if (rdioDecode.Checked)
            {
                //picGTResult.Image = null;
                //tbGTMessage.Text = "";
                //_imageDecoder = new JPEGDecoder(ImagesSavePath + "encryptedImageGT.jpg");
                //byte[] message = _imageDecoder.Decode();
                //tbGTMessage.Text = (new string(message.Select(x => (char)x).ToArray()));

                _imageDecoder = new LeastSignificantBitDecoder(ImagesSavePath + "encryptedImageLSB.jpg");

                byte[] message = _imageDecoder.Decode();

                TypeConverter tc = TypeDescriptor.GetConverter(typeof(Bitmap));
                Bitmap messageImage = (Bitmap)tc.ConvertFrom(message);

                picResultGT.Image = messageImage;
                //_imageDecoder.Save();
                //_stegoLsbController.MessageImage.Save(ImagesSavePath + "./decryptedImageLSB.png");
                //_stegoLsbController.MessageImage = null;
            }
            Cursor.Current = Cursors.Default;
        }
        /*
        private void getFileStego_FileOk(object sender, CancelEventArgs e)
        {
            StegoLSBController.StegoImage = new Bitmap(getFileStego.FileName);
            picResultGT.Image = StegoLSBController.StegoImage;

            Decode.Enabled = true;
        }*/

        #endregion

        #region Graph Theoretic
        private void btnGTLoadMessageFile_Click(object sender, EventArgs e)
        {
            GetFileMessageGT.ShowDialog();
        }

        private void rdioGTEncode_CheckedChanged(object sender, EventArgs e)
        {
            if (rdioGTEncode.Checked)
            {
                btnGTLoadMessageFile.Enabled = true;
                tbGTMessage.Enabled = true;
                btnGTProceed.Text = @"Encode";
                if (!_messageFileSetGt)
                {
                    btnProceed.Enabled = false;
                }
            }
            else if (rdioGTDecode.Checked)
            {
                btnGTLoadMessageFile.Enabled = false;
                tbGTMessage.Enabled = false;
                _messageImageSetLsb = false;
                btnGTProceed.Text = @"Decode";
                if (_inputImageSetGt)
                {
                    btnGTProceed.Enabled = true;
                }
            }
        }

        private void tbGTMessage_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            tbGTMessage.SelectAll();
        }

        private void tbGTMessage_Leave(object sender, EventArgs e)
        {
            if (tbGTMessage.Text == "")
            {
                tbGTMessage.Text = NoMessageWrittenMessage;
            }
        }

        private void tbarGTEncodingQuality_ValueChanged(object sender, EventArgs e)
        {
            QualityGT = tbarGTEncodingQuality.Value;
            lblGTEncodingQualityValue.Text = QualityGT.ToString();

            if (QualityGT != defaultQuality)
            {
                lblGTEncodingQualityValue.Text = QualityGT.ToString();
            }
            else
            {
                lblGTEncodingQualityValue.Text = QualityGT.ToString() + @"  (default)";
            }
        }

        private void tbGTMessage_TextChanged(object sender, EventArgs e)
        {
            tbGTMessage.ForeColor = SystemColors.MenuText;
            if (tbGTMessage.Text != NoMessageWrittenMessage && tbGTMessage.Text != "" && !_messageFileSetGt)
            {
                _messageTextSetGt = true;
                btnGTLoadMessageFile.Enabled = false;
                if (_inputImageSetGt)
                {
                    _messageLength = tbGTMessage.Text.Length;
                    btnGTProceed.Enabled = true;
                }
            }
            else
            {
                btnGTProceed.Enabled = false;
                if (rdioGTEncode.Checked)
                {
                    btnGTLoadMessageFile.Enabled = true;
                }
            }
        }

        private void StegosaurusForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            saveSettings();
        }

        private void btnGTLoadInput_Click(object sender, EventArgs e)
        {
            getFileInputGT.ShowDialog();
        }

        private void getFileInputGT_FileOk(object sender, CancelEventArgs e)
        {
            CoverImage = new Bitmap(getFileInputGT.FileName);

            _inputImageSetGt = true;
            picGTInput.Image = CoverImage;

            if (_messageFileSetGt || _messageTextSetGt)
            {
                btnGTProceed.Enabled = true;
            }
        }

        private void GetFileMessageGT_FileOk(object sender, CancelEventArgs e)
        {
            tbGTMessage.Enabled = false;
            tbGTMessageFilePath.Text = GetFileMessageGT.SafeFileName;
            _messageFileSetGt = true;
            _messageGT = File.ReadAllBytes(GetFileMessageGT.FileName);
            _messageLength = _messageGT.Length;

            if (_inputImageSetGt || _messageFileSetGt)
            {
                btnGTProceed.Enabled = true;
            }
        }

        private void btnGTProceed_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (rdioGTEncode.Checked)
            {
                //DELETE
                ImagesSavePath = "";
                //DELETE
                byte[] msg = new byte[_messageLength];

                if (_messageTextSetGt)
                {
                    for (int i = 0; i < _messageLength; i++)
                    {
                        msg[i] = (byte)(tbGTMessage.Text.ToCharArray()[i]);
                    }
                }
                else if (_messageFileSetGt)
                {
                    for (int i = 0; i < _messageLength; i++)
                    {
                        msg[i] = _messageGT[i];
                    }
                }

                if (!LSBMethodSelected)
                {
                    if (QuantizationTableY == null || QuantizationTableChr == null || HuffmanTableYAC == null || HuffmanTableYDC == null || HuffmanTableChrAC == null || HuffmanTableChrDC == null)
                    {
                        _imageEncoder = new JpegImage(CoverImage, QualityGT, 4);
                    }
                    else
                    {
                        _imageEncoder = new JpegImage(CoverImage, QualityGT, 4, QuantizationTableY, QuantizationTableChr, HuffmanTableYDC, HuffmanTableYAC, HuffmanTableChrDC, HuffmanTableChrAC);
                    }
                }
                else
                {
                    _imageEncoder = new LeastSignificantBitImage(CoverImage);
                }

                _imageEncoder.Encode(msg);

                if (!LSBMethodSelected)
                {
                    _imageEncoder.Save(ImagesSavePath + "encryptedImageGT.jpg");
                    picGTResult.Image = Image.FromFile(ImagesSavePath + "encryptedImageGT.jpg");
                }
                else
                {
                    _imageEncoder.Save(ImagesSavePath + "encryptedImageLSB.jpg");
                    picGTResult.Image = Image.FromFile(ImagesSavePath + "encryptedImageLSB.jpg");
                }
            }
            else if (rdioGTDecode.Checked)
            {
                picGTResult.Image = null;
                tbGTMessage.Text = "";
                if (!LSBMethodSelected)
                {
                    _imageDecoder = new JPEGDecoder(ImagesSavePath + "encryptedImageGT.jpg");
                }
                else
                {
                    _imageDecoder = new LeastSignificantBitDecoder(ImagesSavePath + "encryptedImageLSB.jpg");
                }
                byte[] message = _imageDecoder.Decode();
                tbGTMessage.Text = new string(message.Select(x => (char)x).ToArray());
            }
            Cursor.Current = Cursors.Default;
        }
        #endregion

        private void saveSettings()
        {
            Properties.Settings.Default.HuffmanTableYAC = HuffmanTableYAC;
            Properties.Settings.Default.HuffmanTableYDC = HuffmanTableYDC;
            Properties.Settings.Default.HuffmanTableChrAC = HuffmanTableChrAC;
            Properties.Settings.Default.HuffmanTableChrDC = HuffmanTableChrDC;
            Properties.Settings.Default.QuantizationTableY = QuantizationTableY;
            Properties.Settings.Default.QuantizationTableChr = QuantizationTableChr;
            Properties.Settings.Default.QualityGTLocked = QualityGTLocked;
            Properties.Settings.Default.ImagesFilePath = ImagesSavePath;
            Properties.Settings.Default.QualityGT = QualityGT;

            Properties.Settings.Default.Save();
        }

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
