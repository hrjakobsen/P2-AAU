using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Stegosaurus;
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
        private string decodeFilePath;
        private string decodeFileName;

        private string _userSavePath;

        private string UserSavePath
        {
            get { return _userSavePath; }
            set
            {
                string s = value;

               // s = s.Replace("\\", "/");

                _userSavePath = s;
            }
        }
        private Bitmap CoverImage { get; set; }

        public StegosaurusForm() {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            InitializeComponent();
            tbMessage.Text = NoMessageWrittenMessage;
        }

        private void StegosaurusForm_Load(object sender, EventArgs e)
        {
            try
            {
                loadSettings();
            }
            catch (Exception)
            {
                MessageBox.Show("An error occured while trying to load your settings");
            }
        }

        private void loadSettings()
        {
            LSBMethodSelected = Properties.Settings.Default.LSBMethodSelected;
            tbarEncodingQuality.Value = Properties.Settings.Default.Quality;
            UserSavePath = Properties.Settings.Default.ImagesFilePath;
            QualityLocked = Properties.Settings.Default.QualityLocked;

            if (QualityLocked && LSBMethodSelected)
            {
                tbarEncodingQuality.Enabled = false;
                lblEncodingQualityValue.Text = "-";
            }
            else if (QualityLocked)
            {
                tbarEncodingQuality.Value = defaultQuality;
                tbarEncodingQuality.Enabled = false;
            } else
            {
                tbarEncodingQuality.Enabled = true;
            }

            loadHuffmanTableFromFile(out HuffmanTableYAC, "HuffmanTableYAC.txt");
            loadHuffmanTableFromFile(out HuffmanTableYAC, "HuffmanTableYAC.txt");
            loadHuffmanTableFromFile(out HuffmanTableYAC, "HuffmanTableYAC.txt");
            loadHuffmanTableFromFile(out HuffmanTableYAC, "HuffmanTableYAC.txt");
            loadQuantizationTableFromFile(out QuantizationTableY, "QuantizationTableY.txt");
            loadQuantizationTableFromFile(out QuantizationTableChr, "QuantizationTableChr.txt");
        }

        
        private void loadHuffmanTableFromFile(out HuffmanTable huffmanTable, string filePath)
        {
            if (File.Exists(filePath))
            {
                string input = File.ReadAllText(filePath);
                huffmanTable = HuffmanTable.FromString(input);
            }
            else
            {
                huffmanTable = null;
            }
        }

        private void loadQuantizationTableFromFile(out QuantizationTable quantizationTable, string filePath)
        {
            if (File.Exists(filePath))
            {
                string input = File.ReadAllText(filePath);
                quantizationTable = QuantizationTable.FromString(input);
            }
            else
            {
                quantizationTable = null;
            }
        }

        private void viewOptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OptionsForm optionsForm = new OptionsForm();
            optionsForm.ShowDialog();

            if (OptionsForm.ResetToDefault)
            {
                Cursor.Current = Cursors.WaitCursor;
                resetSettingsToDefault();
                OptionsForm.ResetToDefault = false;
                OptionsForm optionsForm2 = new OptionsForm();
                Cursor.Current = Cursors.Default;
                optionsForm2.ShowDialog();
            }

            if (OptionsForm.SaveEnabled)
            {
                Cursor.Current = Cursors.WaitCursor;
                try
                {
                    loadSettingsFromOptionsForm();
                    OptionsForm.SkipSettingsInitialization = false;
                }
                catch (Exception)
                {
                    MessageBox.Show("An error occured while trying to save your settings!");
                    OptionsForm.SkipSettingsInitialization = true;
                }

                Cursor.Current = Cursors.Default;
            }
        }

        private void loadSettingsFromOptionsForm()
        {
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

            LSBMethodSelected = OptionsForm.LSBMethodSelected;

            tbarEncodingQuality.Value = OptionsForm.Quality;

            if (LSBMethodSelected)
            {
                lblEncodingQualityValue.Text = "-";
            }
            else
            {
                tbarEncodingQuality.Value = Quality + 1;
                tbarEncodingQuality.Value = Quality - 1;
            }

            //If changes have been made to a QuantizationTable or LSB is selected, lock the quality-slider to prevent errors.
            if (LSBMethodSelected || !OptionsForm.QuantizationTableComponentY.SaveTable().Equals(QuantizationTable.JpegDefaultYTable) 
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
        private void rdioEncode_CheckedChanged(object sender, EventArgs e)
        {
            if (rdioEncode.Checked)
            {
                btnLoadMessageFile.Enabled = true;
                btnRemoveMsgFile.Enabled = true;
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
                btnRemoveMsgFile.Enabled = false;
                tbMessage.Enabled = false;
                tbMessage.Text = NoMessageWrittenMessage;
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
                btnRemoveMsgFile.Enabled = false;
                _messageLength = tbMessage.Text.Length;
                if (_inputImageSet)
                {
                    btnProceed.Enabled = true;
                }
            }
            else
            {
                _messageTextSet = false;
                btnProceed.Enabled = false;
                if (rdioEncode.Checked)
                {
                    btnLoadMessageFile.Enabled = true;
                    btnRemoveMsgFile.Enabled = true;
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
            decodeFilePath = getFileInput.FileName;
            decodeFileName = Path.GetFileNameWithoutExtension(getFileInput.FileName);
            _inputImageSet = true;
            picInput.Image = CoverImage;

            if (_messageFileSet || _messageTextSet)
            {
                btnProceed.Enabled = true;
            }
        }

        private void btnLoadMessageFile_Click(object sender, EventArgs e)
        {
            GetFileMessage.ShowDialog();
        }

        private void GetFileMessage_FileOk(object sender, CancelEventArgs e)
        {
            tbMessage.Enabled = false;
            tbMessageFilePath.Text = GetFileMessage.SafeFileName;
            _messageFileSet = true;
            _message = File.ReadAllBytes(GetFileMessage.FileName);
            _messageLength = _message.Length;

            if (_inputImageSet || _messageFileSet)
            {
                btnProceed.Enabled = true;
            }
        }

        private void btnRemoveMsgFile_Click(object sender, EventArgs e)
        {
            tbMessage.Enabled = true;
            tbMessageFilePath.Text = "Message file";

            _messageFileSet = false;
            _message = null;
            _messageLength = 0;
        }

        //Handles encoding/decoding using the correct method and settings when the 'Proceed' button is pressed.
        private void btnProceed_Click(object sender, EventArgs e)
        {
            try
            {
                getFilePath();
            }
            catch (IOException)
            {
                MessageBox.Show("Could not access file!");
            }

            Cursor.Current = Cursors.WaitCursor;
            if (rdioEncode.Checked)
            {
                if (picResult.Image != null)
                {
                    picResult.Image.Dispose();
                }

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

                try
                {
                    _imageEncoder.Encode(msg);

                        _imageEncoder.Save(UserSavePath);
                        picResult.Image = Image.FromFile(UserSavePath);
                }
                catch (ImageCannotContainDataException)
                {
                    MessageBox.Show("Image cannot contain data!");
                }
            }
            else if (rdioDecode.Checked)
            {
                picResult.Image = null;
                tbMessage.Text = "";

                try
                {
                    if (!LSBMethodSelected)
                    {
                        _imageDecoder = new JPEGDecoder(decodeFilePath);
                    }
                    else
                    {
                        _imageDecoder = new LeastSignificantBitDecoder(decodeFilePath);
                    }
                    byte[] message = _imageDecoder.Decode();
                    tbMessage.Text = new string(message.Select(x => (char)x).ToArray());
                    File.WriteAllBytes(UserSavePath, message);
                }
                catch (Exception)
                {
                    MessageBox.Show("Unknown error (Image might not contain a message)");
                }
            }
            Cursor.Current = Cursors.Default;
        }

        private void getFilePath()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.ShowHelp = true;

            if (rdioEncode.Checked && !LSBMethodSelected)
            {
                saveFileDialog.FileName = decodeFileName + " (encoded).jpeg";
                saveFileDialog.Filter = "Image Files (*.jpeg)|*.jpeg";
                saveFileDialog.FilterIndex = 2;
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.Title = "Save encoded image as";
            } else if (rdioEncode.Checked && LSBMethodSelected)
            {
                saveFileDialog.FileName = decodeFileName + " (encoded).png";
                saveFileDialog.Filter = "Image Files (*.png)|*.png";
                saveFileDialog.FilterIndex = 2;
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.Title = "Save encoded image as";
            }
            else if (rdioDecode.Checked)
            {
                saveFileDialog.FileName = decodeFileName + " (decoded)";
                saveFileDialog.Filter = "No file Extension ()|";
                saveFileDialog.FilterIndex = 2;
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.Title = "Save decoded message as";
            }
            

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                UserSavePath = saveFileDialog.FileName;

                //if ((saveFileStream = saveFileDialog.OpenFile()) != null)
                //{
                //    saveFileStream.Close();
                //}
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

        private void StegosaurusForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            saveSettings();
        }

        private void resetSettingsToDefault()
        {
            UserSavePath = "";
            Quality = defaultQuality;
            QualityLocked = false;
            LSBMethodSelected = false;
            HuffmanTableYAC = null;
            HuffmanTableYDC = null;
            HuffmanTableChrAC = null;
            HuffmanTableChrDC = null;
            QuantizationTableY = null;
            QuantizationTableChr = null;

        }

        private void saveSettings()
        {
            saveHuffmanTableToFile(HuffmanTableYAC, "HuffmanTableYAC.txt");
            saveHuffmanTableToFile(HuffmanTableYDC, "HuffmanTableYDC.txt");
            saveHuffmanTableToFile(HuffmanTableChrAC, "HuffmanTableChrAC.txt");
            saveHuffmanTableToFile(HuffmanTableChrDC, "HuffmanTableChrDC.txt");
            saveQuantizationTableToFile(QuantizationTableY, "QuantizationTableY.txt");
            saveQuantizationTableToFile(QuantizationTableChr, "QuantizationTableChr.txt");
            Properties.Settings.Default.QualityLocked = QualityLocked;
            Properties.Settings.Default.ImagesFilePath = UserSavePath;
            Properties.Settings.Default.Quality = Quality;
            Properties.Settings.Default.LSBMethodSelected = LSBMethodSelected;

            Properties.Settings.Default.Save();
        }

        private void saveHuffmanTableToFile(HuffmanTable huffmanTable, string filePath)
        {
            if (huffmanTable != null)
            {
                File.WriteAllText(filePath, huffmanTable.ToString());
            }
        }

        private void saveQuantizationTableToFile(QuantizationTable quantizationTable, string filePath)
        {
            if (quantizationTable != null)
            {
                File.WriteAllText(filePath, quantizationTable.ToString());
            }
        }
    }
}
