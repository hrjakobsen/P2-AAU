using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Stegosaurus;
using System.Linq;
using System.IO;
using System.Runtime.InteropServices;

namespace TestForm{
    public partial class StegosaurusForm:Form
    {
        public static HuffmanTable HuffmanTableChrAC, HuffmanTableChrDC, HuffmanTableYAC, HuffmanTableYDC;
        public static QuantizationTable QuantizationTableY, QuantizationTableChr;
        public static bool QualityLocked { get; private set; }
        public static bool LSBMethodSelected;
        public static int Quality { get; private set; }
        public static byte MValue { get; private set; }

        private IImageEncoder _imageEncoder;
        private IImageDecoder _imageDecoder;
        private bool _inputImageSet, _messageFileSet, _messageTextSet;
        private byte[] _message;
        private const string NoMessageWrittenMessage = "Enter the message you would like to encode into your image.";
        private int _messageLength;
        private readonly int _defaultQuality = 53;
        private readonly byte _defaultMValue = 4;
        private Bitmap CoverImage { get; set; }
        private string _decodeFilePath;
        private string _decodeFileName;

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

        public StegosaurusForm() {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            InitializeComponent();
        }

        private void StegosaurusForm_Load(object sender, EventArgs e)
        {
            try
            {
                tbMessage.Text = NoMessageWrittenMessage;
                loadSettings();
            }
            catch (Exception)
            {
                MessageBox.Show("An error occured while trying to load your settings, they seem to be invalid. " +
                                "Correct file or reset all settings to default in Options.", "Error loading settings");
            }
        }

        //Loads settings from Properties.Settings for more general settings and from a .CSV-file for custom tables.
        private void loadSettings()
        {
            LSBMethodSelected = Properties.Settings.Default.LSBMethodSelected;
            tbarEncodingQuality.Value = Properties.Settings.Default.Quality;
            QualityLocked = Properties.Settings.Default.QualityLocked;
            MValue = Properties.Settings.Default.MValue;

            this.Text = !LSBMethodSelected ? @"Stegosaurus (GT)" : @"Stegosaurus (LSB)";

            if (QualityLocked && LSBMethodSelected)
            {
                tbarEncodingQuality.Enabled = false;
                lblEncodingQualityValue.Text = "-";
            }
            else if (QualityLocked)
            {
                tbarEncodingQuality.Value = _defaultQuality;
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

        //Uses the HuffmanTable.Fromstring() method to create a Huffman table from a string optained from a file.
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

        //Uses the QuantizationTable.Fromstring() method to create a quantization table from a string optained from a file.
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

        //Opens a OptionsForm if the MenuItem is clicked and handles the loading of settings from the form and reversing to default settings.
        private void viewOptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OptionsForm optionsForm = new OptionsForm();
            optionsForm.ShowDialog();

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
                    MessageBox.Show("An error occured while trying to save your settings, they seem to be invalid!", "Error saving settings");
                    OptionsForm.SkipSettingsInitialization = true;
                }

                Cursor.Current = Cursors.Default;
            }

            if (OptionsForm.ResetToDefault)
            {
                Cursor.Current = Cursors.WaitCursor;
                resetSettingsToDefault();
                OptionsForm.ResetToDefault = false;
                OptionsForm optionsForm2 = new OptionsForm();
                Cursor.Current = Cursors.Default;
                optionsForm2.ShowDialog();
            }
        }

        private void resetSettingsToDefault()
        {
            Quality = _defaultQuality;
            MValue = _defaultMValue;
            QualityLocked = false;
            LSBMethodSelected = false;
            HuffmanTableYAC = null;
            HuffmanTableYDC = null;
            HuffmanTableChrAC = null;
            HuffmanTableChrDC = null;
            QuantizationTableY = null;
            QuantizationTableChr = null;
        }

        private void loadSettingsFromOptionsForm()
        {
            HuffmanTableYAC = defaultOrCustomHuffmanTable(OptionsForm.HuffmanTableComponentYAC, HuffmanTable.JpegHuffmanTableYAC);
            HuffmanTableYDC = defaultOrCustomHuffmanTable(OptionsForm.HuffmanTableComponentYDC, HuffmanTable.JpegHuffmanTableYDC);
            HuffmanTableChrAC = defaultOrCustomHuffmanTable(OptionsForm.HuffmanTableComponentChrAC, HuffmanTable.JpegHuffmanTableChrAC);
            HuffmanTableChrAC = defaultOrCustomHuffmanTable(OptionsForm.HuffmanTableComponentChrDC, HuffmanTable.JpegHuffmanTableChrDC);
            QuantizationTableY = defaultOrCustomQuantizationTable(OptionsForm.QuantizationTableComponentY, QuantizationTable.JpegDefaultYTable);
            QuantizationTableChr = defaultOrCustomQuantizationTable(OptionsForm.QuantizationTableComponentChr, QuantizationTable.JpegDefaultChrTable);

            LSBMethodSelected = OptionsForm.LSBMethodSelected;
            Text = !LSBMethodSelected ? @"Stegosaurus (GT)" : @"Stegosaurus (LSB)";

            tbarEncodingQuality.Value = OptionsForm.Quality;
            MValue = OptionsForm.MValue;

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
                tbarEncodingQuality.Value = _defaultQuality;
                tbarEncodingQuality.Enabled = false;
            }
            else
            {
                QualityLocked = false;
                tbarEncodingQuality.Enabled = true;
            }
        }

        //Returns default table if the table made from the tablecomponent is the same as defaultTable, custom table if they are different.
        //Doing this prevents an error from ocuring when changing quality while using a 'default' table manually set.
        private HuffmanTable defaultOrCustomHuffmanTable(HuffmanTableComponent customHuffmanTable, HuffmanTable defaultTable)
        {
            HuffmanTable H;
            if (customHuffmanTable.SaveTable().Equals(defaultTable))
            {
                H = defaultTable;
            }
            else
            {
                H = customHuffmanTable.SaveTable();
            }

            return H;
        }

        //Returns default table if the table made from the tablecomponent is the same as defaultTable, custom table if they are different.
        //Doing this prevents an error from ocuring when changing quality while using a 'default' non-default table
        private QuantizationTable defaultOrCustomQuantizationTable(QuantizationTableComponent customQuantizationTable,
            QuantizationTable defaultTable)
        {
            QuantizationTable Q;

            if (customQuantizationTable.SaveTable().Equals(defaultTable))
            {
                Q = defaultTable;
            }
            else
            {
                Q = customQuantizationTable.SaveTable();
            }

            return Q;
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
                btnProceed.Text = @"Encode";
                btnLoadMessageFile.Enabled = true;
                btnRemoveMsgFile.Enabled = true;

                if (_inputImageSet && _messageTextSet)
                {
                    btnLoadMessageFile.Enabled = false;
                    btnRemoveMsgFile.Enabled = false;
                    btnProceed.Enabled = true;
                    tbMessage.Enabled = true;                    
                } else if (_inputImageSet && _messageFileSet)
                {
                    btnProceed.Enabled = true;
                    tbMessage.Enabled = false;
                }
                else
                {
                    btnProceed.Enabled = false;
                    tbMessage.Enabled = true;
                }
            }
            else if (rdioDecode.Checked)
            {
                btnLoadMessageFile.Enabled = false;
                btnRemoveMsgFile.Enabled = false;
                tbMessage.Enabled = false;
                btnProceed.Text = @"Decode";
                if (_inputImageSet)
                {
                    btnProceed.Enabled = true;
                }
                else
                {
                    btnProceed.Enabled = false;
                }
            }
        }

        private void tbMessage_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            tbMessage.SelectAll();
        }

        private void tbMessage_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbMessage.Text ))
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

            if (Quality != _defaultQuality)
            {
                lblEncodingQualityValue.Text = Quality.ToString();
            }
            else
            {
                lblEncodingQualityValue.Text = Quality.ToString() + @"  (default)";
            }
        }

        private void btnLoadInputImage_Click(object sender, EventArgs e)
        {
            getFileInputImage.ShowDialog();
        }

        private void getFileInputImage_FileOk(object sender, CancelEventArgs e)
        {
            CoverImage = new Bitmap(getFileInputImage.FileName);
            _decodeFilePath = getFileInputImage.FileName;
            _decodeFileName = Path.GetFileNameWithoutExtension(getFileInputImage.FileName);
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
            _messageTextSet = false;
            _message = File.ReadAllBytes(GetFileMessage.FileName);
            _messageLength = _message.Length;

            if (_inputImageSet && (_messageFileSet || _messageTextSet))
            {
                btnProceed.Enabled = true;
            }
        }

        private void btnRemoveMsgFile_Click(object sender, EventArgs e)
        {
            removeMsgFile();
        }

        private void removeMsgFile()
        {
            tbMessage.Enabled = true;
            tbMessageFilePath.Text = "Message file";
            btnProceed.Enabled = false;

            _messageFileSet = false;
            _message = null;
            _messageLength = 0;
        }

        //Starts encoding/decoding using the correct method and settings when the 'Proceed' button is pressed.
        private void btnProceed_Click(object sender, EventArgs e)
        {
            try
            {
                getFilePath();
                encodeOrDecodeImage();
            }
            catch (IOException)
            {
                MessageBox.Show("Could not access file!");
            }
            catch (NoSavePathSelectedException)
            {
                MessageBox.Show("No save location was selected!");
            }

            Cursor.Current = Cursors.WaitCursor;
           
            lblProcessing.Text = "";
            lblProcessing.Visible = false;
            Application.DoEvents();
            Cursor.Current = Cursors.Default;
        }

        //Asks for the correct filetype according to encoding/decoding method selected.
        private void getFilePath()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            if (rdioEncode.Checked && !LSBMethodSelected)
            {
                saveFileDialog.FileName = _decodeFileName + " (encoded).jpeg";
                saveFileDialog.Filter = "Image Files (*.jpeg)|*.jpeg";
                saveFileDialog.FilterIndex = 2;
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.Title = "Save encoded image as";
            } else if (rdioEncode.Checked && LSBMethodSelected)
            {
                saveFileDialog.FileName = _decodeFileName + " (encoded).png";
                saveFileDialog.Filter = "Image Files (*.png)|*.png";
                saveFileDialog.FilterIndex = 2;
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.Title = "Save encoded image as";
            }
            else if (rdioDecode.Checked)
            {
                saveFileDialog.FileName = _decodeFileName + " (decoded)";
                saveFileDialog.Filter = "No file Extension ()|";
                saveFileDialog.FilterIndex = 2;
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.Title = "Save decoded message as";
            }
            

            if (saveFileDialog.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(saveFileDialog.FileName))
            {
                UserSavePath = saveFileDialog.FileName;
            }
            else
            {
                throw new NoSavePathSelectedException();
            }
        }

        //Handles encoding/decoding using the correct method and settings when the 'Proceed' button is pressed.
        private void encodeOrDecodeImage()
        {
            if (rdioEncode.Checked)
            {
                if (picResult.Image != null)
                {
                    picResult.Image.Dispose();
                }

                byte[] msg = getMessageFromTextboxOrFile();

                //Create an _imageEncoder according according to selected method
                if (!LSBMethodSelected)
                {
                    lblProcessing.Text = "Encoding using GT method...";
                    lblProcessing.Visible = true;
                    Application.DoEvents();

                    //Use simple constructor if a table is null 
                    if (QuantizationTableY == null || QuantizationTableChr == null || HuffmanTableYAC == null || HuffmanTableYDC == null || HuffmanTableChrAC == null || HuffmanTableChrDC == null)
                    {
                        _imageEncoder = new JpegImage(CoverImage, Quality, MValue);
                    }
                    else
                    {
                        _imageEncoder = new JpegImage(CoverImage, Quality, MValue, QuantizationTableY, QuantizationTableChr, HuffmanTableYDC, HuffmanTableYAC, HuffmanTableChrDC, HuffmanTableChrAC);
                    }
                }
                else
                {
                    lblProcessing.Text = "Encoding using LSB method...";
                    lblProcessing.Visible = true;
                    Application.DoEvents();
                    _imageEncoder = new LeastSignificantBitImage(CoverImage);
                }

                //Encode
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
                catch (ExternalException)
                {
                    MessageBox.Show("Failed to load result picture! Your Huffman table may be invalid");
                }
            }
            else if (rdioDecode.Checked)
            {
                picResult.Image = null;
                tbMessage.Text = "";

                if (!LSBMethodSelected)
                {
                    lblProcessing.Text = "Decoding using GT method...";
                    lblProcessing.Visible = true;
                    Application.DoEvents();
                    _imageDecoder = new JPEGDecoder(_decodeFilePath);
                }
                else
                {
                    lblProcessing.Text = "Decoding using LSB method...";
                    lblProcessing.Visible = true;
                    Application.DoEvents();
                    _imageDecoder = new LeastSignificantBitDecoder(_decodeFilePath);
                }

                //Decode
                try
                {
                    byte[] message = _imageDecoder.Decode();
                    tbMessage.Text = new string(message.Select(x => (char)x).ToArray());
                    File.WriteAllBytes(UserSavePath, message);
                }
                catch (Exception)
                {
                    MessageBox.Show("Unknown error (Cover image might not contain a message)");
                }
            }
        }

        //Checks whether the message is in the TextBox or as a file and returns the message as a byte[]
        private byte[] getMessageFromTextboxOrFile()
        {
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

            return msg;
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

        //Saves settings to Properties.Settings for more general settings and to a .CSV-file for custom tables
        private void saveSettings()
        {
            saveHuffmanTableToFile(HuffmanTableYAC, "HuffmanTableYAC.txt");
            saveHuffmanTableToFile(HuffmanTableYDC, "HuffmanTableYDC.txt");
            saveHuffmanTableToFile(HuffmanTableChrAC, "HuffmanTableChrAC.txt");
            saveHuffmanTableToFile(HuffmanTableChrDC, "HuffmanTableChrDC.txt");
            saveQuantizationTableToFile(QuantizationTableY, "QuantizationTableY.txt");
            saveQuantizationTableToFile(QuantizationTableChr, "QuantizationTableChr.txt");
            Properties.Settings.Default.QualityLocked = QualityLocked;
            Properties.Settings.Default.Quality = Quality;
            Properties.Settings.Default.MValue = MValue;
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
