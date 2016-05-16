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
        private HuffmanTable _huffmanTableChrAC;
        private HuffmanTable _huffmanTableChrDC;
        private HuffmanTable _huffmanTableYAC;
        private HuffmanTable _huffmanTableYDC;
        private QuantizationTable _quantizationTableY;
        private QuantizationTable _quantizationTableChr;

        private readonly LeastSignificantBitImage _stegoLsbController;
        private IImageEncoder _stegoGtEncoderController;
        private IImageDecoder _stegoGtDecoderController;
        private bool _inputImageSetLsb, _messageImageSetLsb, _inputImageSetGt, _messageFileSetGt, _messageTextSetGt;
        private byte[] _message;
        private const string NoMessageWrittenMessage = "Enter the message you would like to encode into your image.";
        private int _messageLength;
        private int defaultQuality = 53;

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


        private Bitmap CoverImageGT { get; set; }
        private Bitmap StegoImageGt { get; set; }

        public StegosaurusForm() {
            InitializeComponent();
            tbGTMessage.Text = NoMessageWrittenMessage;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            _stegoLsbController = new LeastSignificantBitImage();
        }

        private void viewOptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OptionsForm optionsForm = new OptionsForm();
            optionsForm.ShowDialog();
            if (OptionsForm.SaveEnabled)
            {
                Cursor.Current = Cursors.WaitCursor;
                loadSettings();
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

        private void loadSettings()
        {
            tbarGTEncodingQuality.Value = OptionsForm.QualityGT;

            if (OptionsForm.HuffmanTableComponentYAC.SaveTable().Equals(HuffmanTable.JpegHuffmanTableYAC))
            {
                _huffmanTableYAC = HuffmanTable.JpegHuffmanTableYAC;
            }
            else
            {
                _huffmanTableYAC = OptionsForm.HuffmanTableComponentYAC.SaveTable();
            }

            if (OptionsForm.HuffmanTableComponentYDC.SaveTable().Equals(HuffmanTable.JpegHuffmanTableYDC))
            {
                _huffmanTableYDC = HuffmanTable.JpegHuffmanTableYDC;
            }
            else
            {
                _huffmanTableYDC = OptionsForm.HuffmanTableComponentYDC.SaveTable();
            }

            if (OptionsForm.HuffmanTableComponentChrAC.SaveTable().Equals(HuffmanTable.JpegHuffmanTableChrAC))
            {
                _huffmanTableChrAC = HuffmanTable.JpegHuffmanTableChrAC;
            }
            else
            {
                _huffmanTableChrAC = OptionsForm.HuffmanTableComponentChrAC.SaveTable();
            }

            if (OptionsForm.HuffmanTableComponentChrDC.SaveTable().Equals(HuffmanTable.JpegHuffmanTableChrDC))
            {
                _huffmanTableChrDC = HuffmanTable.JpegHuffmanTableChrDC;
            }
            else
            {
                _huffmanTableChrDC = OptionsForm.HuffmanTableComponentChrDC.SaveTable();
            }

            if (OptionsForm.QuantizationTableComponentY.SaveTable().Equals(QuantizationTable.JpegDefaultYTable))
            {
                _quantizationTableY = QuantizationTable.JpegDefaultYTable;
            }
            else
            {
                _quantizationTableY = OptionsForm.QuantizationTableComponentY.SaveTable();
                tbarGTEncodingQuality.Value = defaultQuality;
            }

            if (OptionsForm.QuantizationTableComponentChr.SaveTable().Equals(QuantizationTable.JpegDefaultChrTable))
            {
                _quantizationTableChr = QuantizationTable.JpegDefaultChrTable;
            }
            else
            {
                _quantizationTableChr = OptionsForm.QuantizationTableComponentChr.SaveTable();
                tbarGTEncodingQuality.Value = defaultQuality;
            }

            if (!string.IsNullOrWhiteSpace(OptionsForm.ImagesSavePath))
            {
                ImagesSavePath = OptionsForm.ImagesSavePath;
            }
            //_huffmanTableYAC = OptionsForm.HuffmanTableComponentYAC.SaveTable();
            //_huffmanTableYDC = OptionsForm.HuffmanTableComponentYDC.SaveTable();
            //_huffmanTableChrAC = OptionsForm.HuffmanTableComponentChrAC.SaveTable();
            //_huffmanTableChrDC = OptionsForm.HuffmanTableComponentChrDC.SaveTable();
            //_quantizationTableY = OptionsForm.QuantizationTableComponentY.SaveTable();
            //_quantizationTableChr = OptionsForm.QuantizationTableComponentChr.SaveTable();
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
                picMessage.Image = null;
                _messageImageSetLsb = false;
                btnProceed.Text = @"Decode";
                if (_inputImageSetLsb)
                {
                    btnProceed.Enabled = true;
                }
            }
        }

        private void getFileInput_FileOk(object sender, CancelEventArgs e) {
            _stegoLsbController.CoverImage = new Bitmap(getFileInputLSB.FileName);
            _stegoLsbController.StegoImage = new Bitmap(getFileInputLSB.FileName);
            picInput.Image = _stegoLsbController.StegoImage;
            _inputImageSetLsb = true;

            if (_messageImageSetLsb || rdioDecode.Checked) {
                btnProceed.Enabled = true;
            }
        }

        private void getFileMessage_FileOk(object sender, CancelEventArgs e) {
            _stegoLsbController.MessageImage = new Bitmap(getFileMessageLSB.FileName);
            picMessage.Image = _stegoLsbController.MessageImage;
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
                Bitmap oldStegoImage = _stegoLsbController.StegoImage;
                _stegoLsbController.Encode();

                picResult.Image = _stegoLsbController.StegoImage;
                _stegoLsbController.StegoImage.Save(ImagesSavePath + "./encryptedImageLSB.png");
                _stegoLsbController.StegoImage = oldStegoImage;
            }
            else if (rdioDecode.Checked)
            {
                _stegoLsbController.Decode();

                picResult.Image = _stegoLsbController.MessageImage;
                _stegoLsbController.MessageImage.Save(ImagesSavePath + "./decryptedImageLSB.png");
                _stegoLsbController.MessageImage = null;
            }
            Cursor.Current = Cursors.Default;
        }
        /*
        private void getFileStego_FileOk(object sender, CancelEventArgs e)
        {
            StegoLSBController.StegoImage = new Bitmap(getFileStego.FileName);
            picResult.Image = StegoLSBController.StegoImage;

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

        private void btnGTLoadInput_Click(object sender, EventArgs e)
        {
            getFileInputGT.ShowDialog();
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
            SaveToFile("./savedSettings.txt");
        }

        private void getFileInputGT_FileOk(object sender, CancelEventArgs e)
        {
            CoverImageGT = new Bitmap(getFileInputGT.FileName);

            picGTInput.Image = _stegoLsbController.StegoImage;
            _inputImageSetGt = true;
            picGTInput.Image = CoverImageGT;

            if (_messageFileSetGt || _messageTextSetGt)
            {
                btnGTProceed.Enabled = true;
            }
        }

        private void StegosaurusForm_Load(object sender, EventArgs e)
        {
            tbarGTEncodingQuality.Value = defaultQuality;
        }

        private void GetFileMessageGT_FileOk(object sender, CancelEventArgs e)
        {
            tbGTMessage.Enabled = false;
            tbGTMessageFilePath.Text = GetFileMessageGT.SafeFileName;
            _messageFileSetGt = true;
            _message = File.ReadAllBytes(GetFileMessageGT.FileName);
            _messageLength = _message.Length;

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
                if (_quantizationTableY == null || _quantizationTableChr == null || _huffmanTableYAC == null || _huffmanTableYDC == null || _huffmanTableChrAC == null || _huffmanTableChrDC == null)
                {
                    _stegoGtEncoderController = new JpegImage(CoverImageGT, QualityGT, 4);
                }
                else
                {
                    _stegoGtEncoderController = new JpegImage(CoverImageGT, QualityGT, 4, _quantizationTableY, _quantizationTableChr, _huffmanTableYDC, _huffmanTableYAC, _huffmanTableChrDC, _huffmanTableChrAC);
                }

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
                        msg[i] = _message[i];
                    }
                }

                _stegoGtEncoderController.Encode(msg);
                _stegoGtEncoderController.Save(ImagesSavePath + "encryptedImageGT.jpg");
                picGTResult.Image = Image.FromFile(ImagesSavePath + "encryptedImageGT.jpg");

            }
            else if (rdioGTDecode.Checked)
            {
                picGTResult.Image = null;
                tbGTMessage.Text = "";
                _stegoGtDecoderController = new Decoder(ImagesSavePath + "encryptedImageGT.jpg");
                byte[] message = _stegoGtDecoderController.Decode();
                tbGTMessage.Text = (new string(message.Select(x => (char)x).ToArray()) + " AND IT WOOOOORKS!");
            }
            Cursor.Current = Cursors.Default;
        }
        #endregion

        public void SaveToFile(string filePath)
        {
            using (StreamWriter sw = File.CreateText(filePath))
            {
                sw.WriteLine("Foo ");
                sw.WriteLine();

            }
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
