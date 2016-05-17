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

        private IImageEncoder _imageEncoder;
        private IImageDecoder _imageDecoder;
        private bool _inputImageSet, _messageFileSet, _messageTextSet;
        private byte[] _message;
        private const string NoMessageWrittenMessage = "Enter the message you would like to encode into your image.";
        private int _messageLength;
        private int defaultQuality = 53;

        public static bool QualityLocked { get; private set; }
        public static bool LSBMethodSelected;
        public static int Quality { get; set; }
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
        private Bitmap CoverImage { get; set; }

        public StegosaurusForm() {
            InitializeComponent();
            tbMessage.Text = NoMessageWrittenMessage;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }

        private void StegosaurusForm_Load(object sender, EventArgs e)
        {
            //loadSettings();
        }

        private void loadSettings()
        {
            LSBMethodSelected = Properties.Settings.Default.LSBMethodSelected;
            tbarEncodingQuality.Value = Properties.Settings.Default.Quality;
            ImagesSavePath = Properties.Settings.Default.ImagesFilePath;
            QualityLocked = Properties.Settings.Default.QualityLocked;
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

                //TODO: Delete this
                //LSBMethodSelected = OptionsForm.LSBMethodSelected;

                //if (LSBMethodSelected)
                //{
                //    tbarEncodingQuality.Enabled = false;
                //    lblEncodingQualityValue.Text = "";
                //}
                //else
                //{
                //    tbarEncodingQuality.Enabled = true;
                //}
                //

                loadSettingsFromOptionsForm();
                Cursor.Current = Cursors.Default;
            }
        }

        private void loadSettingsFromOptionsForm()
        {
            tbarEncodingQuality.Value = OptionsForm.Quality;

            LSBMethodSelected = OptionsForm.LSBMethodSelected;

            if (LSBMethodSelected)
            {
                tbarEncodingQuality.Enabled = false;
                lblEncodingQualityValue.Text = "";
            }
            else
            {
                tbarEncodingQuality.Enabled = true;
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
                tbarEncodingQuality.Value = defaultQuality;
            }

            if (OptionsForm.QuantizationTableComponentChr.SaveTable().Equals(QuantizationTable.JpegDefaultChrTable))
            {
                QuantizationTableChr = QuantizationTable.JpegDefaultChrTable;
            }
            else
            {
                QuantizationTableChr = OptionsForm.QuantizationTableComponentChr.SaveTable();
                tbarEncodingQuality.Value = defaultQuality;
            }

            //If changes have been made to a QuantizationTable, lock the quality-slider to prevent errors.
            if (!OptionsForm.QuantizationTableComponentY.SaveTable().Equals(QuantizationTable.JpegDefaultYTable) 
                || !OptionsForm.QuantizationTableComponentChr.SaveTable().Equals(QuantizationTable.JpegDefaultChrTable))
            {
                QualityLocked = true;
                tbarEncodingQuality.Enabled = false;
            }
            else
            {
                QualityLocked = false;
                tbarEncodingQuality.Enabled = true;
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

        #region Main Form
        private void btnLoadMessageFile_Click(object sender, EventArgs e)
        {
            GetFileMessage.ShowDialog();
        }

        private void rdioEncode_CheckedChanged(object sender, EventArgs e)
        {
            if (rdioEncode.Checked)
            {
                btnLoadMessageFile.Enabled = true;
                tbMessage.Enabled = true;
                btnProceed.Text = @"Encode";
                if (!_messageFileSet)
                {
                    btnProceed.Enabled = false;
                }
            }
            else if (rdioDecode.Checked)
            {
                btnLoadMessageFile.Enabled = false;
                tbMessage.Enabled = false;
                _messageFileSet = false;
                btnProceed.Text = @"Decode";
                if (_inputImageSet)
                {
                    btnProceed.Enabled = true;
                }
            }
        }

        private void tbMessage_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            tbMessage.SelectAll();
        }

        private void tbMessage_Leave(object sender, EventArgs e)
        {
            if (tbMessage.Text == "")
            {
                tbMessage.Text = NoMessageWrittenMessage;
            }
        }

        private void tbMessage_TextChanged(object sender, EventArgs e)
        {
            tbMessage.ForeColor = SystemColors.MenuText;
            if (tbMessage.Text != NoMessageWrittenMessage && !string.IsNullOrEmpty(tbMessage.Text) && !_messageFileSet)
            {
                _messageTextSet = true;
                btnLoadMessageFile.Enabled = false;
                _messageLength = tbMessage.Text.Length;
                if (_inputImageSet)
                {
                    btnProceed.Enabled = true;
                }
            }
            else
            {
                btnProceed.Enabled = false;
                if (rdioEncode.Checked)
                {
                    btnLoadMessageFile.Enabled = true;
                }
            }
        }

        private void tbarEncodingQuality_ValueChanged(object sender, EventArgs e)
        {
            Quality = tbarEncodingQuality.Value;
            lblEncodingQualityValue.Text = Quality.ToString();

            if (Quality != defaultQuality)
            {
                lblEncodingQualityValue.Text = Quality.ToString();
            }
            else
            {
                lblEncodingQualityValue.Text = Quality.ToString() + @"  (default)";
            }
        }

        private void btnLoadInput_Click(object sender, EventArgs e)
        {
            getFileInput.ShowDialog();
        }

        private void getFileInput_FileOk(object sender, CancelEventArgs e)
        {
            CoverImage = new Bitmap(getFileInput.FileName);

            _inputImageSet = true;
            picInput.Image = CoverImage;

            if (_messageFileSet || _messageTextSet)
            {
                btnProceed.Enabled = true;
            }
        }

        private void GetFileMessage_FileOk(object sender, CancelEventArgs e)
        {
            tbMessage.Enabled = false;
            tbMessageFilePath.Text = GetFileMessage.SafeFileName;
            _messageFileSet = true;
            _message = File.ReadAllBytes(GetFileMessage.FileName);
            cbMessageFile.Checked = true;
            _messageLength = _message.Length;

            if (_inputImageSet || _messageFileSet)
            {
                btnProceed.Enabled = true;
            }
        }

        //Handles encoding/decoding using the correct method and settings when the 'Proceed' button is pressed.
        private void btnProceed_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (rdioEncode.Checked)
            {
                //DELETE
                ImagesSavePath = "";
                //DELETE
                byte[] msg = new byte[_messageLength];

                if (_messageTextSet)
                {
                    for (int i = 0; i < _messageLength; i++)
                    {
                        msg[i] = (byte)(tbMessage.Text.ToCharArray()[i]);
                    }
                }
                else if (_messageFileSet)
                {
                    for (int i = 0; i < _messageLength; i++)
                    {
                        msg[i] = _message[i];
                    }
                }

                if (!LSBMethodSelected)
                {
                    if (QuantizationTableY == null || QuantizationTableChr == null || HuffmanTableYAC == null || HuffmanTableYDC == null || HuffmanTableChrAC == null || HuffmanTableChrDC == null)
                    {
                        _imageEncoder = new JpegImage(CoverImage, Quality, 4);
                    }
                    else
                    {
                        _imageEncoder = new JpegImage(CoverImage, Quality, 4, QuantizationTableY, QuantizationTableChr, HuffmanTableYDC, HuffmanTableYAC, HuffmanTableChrDC, HuffmanTableChrAC);
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
                    picResult.Image = Image.FromFile(ImagesSavePath + "encryptedImageGT.jpg");
                }
                else
                {
                    _imageEncoder.Save(ImagesSavePath + "encryptedImageLSB.jpg");
                    picResult.Image = Image.FromFile(ImagesSavePath + "encryptedImageLSB.jpg");
                }
            }
            else if (rdioDecode.Checked)
            {
                picResult.Image = null;
                tbMessage.Text = "";
                if (!LSBMethodSelected)
                {
                    _imageDecoder = new JPEGDecoder(ImagesSavePath + "encryptedImageGT.jpg");
                }
                else
                {
                    _imageDecoder = new LeastSignificantBitDecoder(ImagesSavePath + "encryptedImageLSB.jpg");
                }
                byte[] message = _imageDecoder.Decode();
                tbMessage.Text = new string(message.Select(x => (char)x).ToArray());
            }
            Cursor.Current = Cursors.Default;
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

        private void StegosaurusForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            saveSettings();
        }

        private void saveSettings()
        {
            Properties.Settings.Default.HuffmanTableYAC = HuffmanTableYAC;
            Properties.Settings.Default.HuffmanTableYDC = HuffmanTableYDC;
            Properties.Settings.Default.HuffmanTableChrAC = HuffmanTableChrAC;
            Properties.Settings.Default.HuffmanTableChrDC = HuffmanTableChrDC;
            Properties.Settings.Default.QuantizationTableY = QuantizationTableY;
            Properties.Settings.Default.QuantizationTableChr = QuantizationTableChr;
            Properties.Settings.Default.QualityLocked = QualityLocked;
            Properties.Settings.Default.ImagesFilePath = ImagesSavePath;
            Properties.Settings.Default.Quality = Quality;
            Properties.Settings.Default.LSBMethodSelected = LSBMethodSelected;

            Properties.Settings.Default.Save();
        }
    }
}
