using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Stegosaurus;
using System.Linq;
using System.IO;
using System.Runtime.InteropServices;

namespace SteGUI {
    public partial class StegosaurusForm : Form {
        private IImageEncoder _imageEncoder;
        private IImageDecoder _imageDecoder;
        private bool _inputImageSet, _messageFileSet, _messageTextSet;
        private byte[] _message;
        private int _messageLength;
        private Bitmap CoverImage { get; set; }
        private string _decodeFilePath, _decodeFileName;
        private string _userSavePath;
        private const string NoMessageWrittenMessage = "Enter the message you would like to encode into your image.";

        public StegosaurusForm() {
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            InitializeComponent();
        }

        private void StegosaurusForm_Load(object sender, EventArgs e) {
            try {
                tbMessage.Text = NoMessageWrittenMessage;
                tbMessage.MaxLength = Int32.MaxValue;
                OptionsForm.LoadSettingsFromFile();
                _refreshMainGUI();
            }
            catch (Exception) {
                MessageBox.Show("An error occured while trying to load your settings, they seem to be invalid. " +
                                "Correct files or reset all settings to default in Options.", "Error loading settings");
            }
        }

        //Opens a OptionsForm if the MenuItem is clicked and handles the loading of settings from the form and reversing to default settings.
        private void viewOptionsToolStripMenuItem_Click(object sender, EventArgs e) {
            OptionsForm optionsForm = new OptionsForm();
            optionsForm.ShowDialog();
            _refreshMainGUI();
        }

        private void _refreshMainGUI() {
            Text = !OptionsForm.LSBMethodSelected ? @"Stegosaurus (GT)" : @"Stegosaurus (LSB)";

            tbarEncodingQuality.Value = OptionsForm.Quality;
            OptionsForm.MValue = OptionsForm.MValue;

            //If changes have been made to a QuantizationTable or LSB is selected, lock the quality-slider to prevent errors.
            if (!OptionsForm.QuantizationTableY.Equals(QuantizationTable.JpegDefaultYTable)
                || !OptionsForm.QuantizationTableChr.Equals(QuantizationTable.JpegDefaultChrTable)) {
                tbarEncodingQuality.Value = OptionsForm.DefaultQualityWithCustomQTable;
                OptionsForm.QualityLocked = true;
                tbarEncodingQuality.Enabled = false;
            } else if (OptionsForm.LSBMethodSelected) {
                OptionsForm.QualityLocked = true;
                tbarEncodingQuality.Enabled = false;
            } else {
                OptionsForm.QualityLocked = false;
                tbarEncodingQuality.Enabled = true;
            }

            if (OptionsForm.QualityLocked && OptionsForm.LSBMethodSelected) {
                tbarEncodingQuality.Enabled = false;
                lblEncodingQualityValue.Text = "-";
            } else if (OptionsForm.QualityLocked) {
                tbarEncodingQuality.Value = OptionsForm.DefaultQualityWithCustomQTable;
                tbarEncodingQuality.Enabled = false;
            } else {
                tbarEncodingQuality.Enabled = true;
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e) {
            AboutForm aboutForm = new AboutForm();
            aboutForm.Show();
        }

        private void showHelpToolStripMenuItem_Click(object sender, EventArgs e) {
            HelpForm helpForm = new HelpForm();
            helpForm.Show();
        }

        #region Main Form
        private void rdioEncode_CheckedChanged(object sender, EventArgs e) {
            if (rdioEncode.Checked) {
                btnProceed.Text = @"Encode";
                btnLoadMessageFile.Enabled = true;
                btnRemoveMsgFile.Enabled = true;

                if (_inputImageSet && _messageTextSet) {
                    btnLoadMessageFile.Enabled = false;
                    btnRemoveMsgFile.Enabled = false;
                    btnProceed.Enabled = true;
                    tbMessage.Enabled = true;
                } else if (_inputImageSet && _messageFileSet) {
                    btnProceed.Enabled = true;
                    tbMessage.Enabled = false;
                } else {
                    btnProceed.Enabled = false;
                    tbMessage.Enabled = true;
                }
            } else if (rdioDecode.Checked) {
                btnLoadMessageFile.Enabled = false;
                btnRemoveMsgFile.Enabled = false;
                tbMessage.Enabled = false;
                btnProceed.Text = @"Decode";
                btnProceed.Enabled = _inputImageSet;
            }
        }

        private void tbMessage_MouseDoubleClick(object sender, MouseEventArgs e) {
            tbMessage.SelectAll();
        }

        private void tbMessage_Leave(object sender, EventArgs e) {
            if (string.IsNullOrWhiteSpace(tbMessage.Text)) {
                tbMessage.Text = NoMessageWrittenMessage;
            }
        }

        private void tbMessage_TextChanged(object sender, EventArgs e) {
            tbMessage.ForeColor = SystemColors.MenuText;
            if (tbMessage.Text != NoMessageWrittenMessage && !string.IsNullOrEmpty(tbMessage.Text) && !_messageFileSet) {
                _messageTextSet = true;
                btnLoadMessageFile.Enabled = false;
                btnRemoveMsgFile.Enabled = false;
                _messageLength = tbMessage.Text.Length;
                if (_inputImageSet) {
                    btnProceed.Enabled = true;
                }
            } else {
                _messageTextSet = false;
                btnProceed.Enabled = false;
                if (rdioEncode.Checked) {
                    btnLoadMessageFile.Enabled = true;
                    btnRemoveMsgFile.Enabled = true;
                }
            }
        }

        private void tbarEncodingQuality_ValueChanged(object sender, EventArgs e) {
            OptionsForm.Quality = tbarEncodingQuality.Value;
            lblEncodingQualityValue.Text = OptionsForm.Quality.ToString();

            if (OptionsForm.Quality == OptionsForm.DefaultQuality) {
                lblEncodingQualityValue.Text = OptionsForm.Quality.ToString() + @"  (default)";
            } else if (OptionsForm.Quality == OptionsForm.DefaultQualityWithCustomQTable && OptionsForm.QualityLocked) {
                lblEncodingQualityValue.Text = OptionsForm.Quality.ToString() + @"  (CQT)";
            } else {
                lblEncodingQualityValue.Text = OptionsForm.Quality.ToString();
            }
        }

        private void btnLoadInputImage_Click(object sender, EventArgs e) {
            getFileInputImage.ShowDialog();
        }

        private void getFileInputImage_FileOk(object sender, CancelEventArgs e) {
            CoverImage = new Bitmap(getFileInputImage.FileName);
            _decodeFilePath = getFileInputImage.FileName;
            _decodeFileName = Path.GetFileNameWithoutExtension(getFileInputImage.FileName);
            _inputImageSet = true;
            picInput.Image = CoverImage;

            if (_messageFileSet || _messageTextSet) {
                btnProceed.Enabled = true;
            }

            if (rdioDecode.Checked) {
                btnProceed.Enabled = true;
            }
        }

        private void btnLoadMessageFile_Click(object sender, EventArgs e) {
            GetFileMessage.ShowDialog();
        }

        private void GetFileMessage_FileOk(object sender, CancelEventArgs e) {
            tbMessage.Enabled = false;
            tbMessageFilePath.Text = GetFileMessage.SafeFileName;
            _messageFileSet = true;
            _messageTextSet = false;
            _message = File.ReadAllBytes(GetFileMessage.FileName);
            _messageLength = _message.Length;

            if (_inputImageSet && (_messageFileSet || _messageTextSet)) {
                btnProceed.Enabled = true;
            }
        }

        private void btnRemoveMsgFile_Click(object sender, EventArgs e) {
            _removeMsgFile();
        }

        private void _removeMsgFile() {
            tbMessage.Enabled = true;
            tbMessageFilePath.Text = "Message file";
            btnProceed.Enabled = false;

            _messageFileSet = false;
            _message = null;
            _messageLength = 0;
        }

        //Starts encoding/decoding using the correct method and settings when the 'Proceed' button is pressed.
        private void btnProceed_Click(object sender, EventArgs e) {
            try {
                _userSavePath = _getFilePath();
                Cursor.Current = Cursors.WaitCursor;
                _encodeOrDecodeImage();
            }
            catch (IOException) {
                MessageBox.Show("Could not access file!");
            }
            catch (NoSavePathSelectedException) {
                MessageBox.Show("No save location was selected!");
            }
            catch (Exception) {
                MessageBox.Show("An unknown error occured!");
            }

            lblProcessing.Text = "";
            lblProcessing.Visible = false;
            Application.DoEvents();
            if (string.IsNullOrWhiteSpace(tbMessage.Text)) {
                tbMessage.Text = NoMessageWrittenMessage;
            }
            Cursor.Current = Cursors.Default;
        }

        //Asks for the correct filetype according to encoding/decoding method selected.
        private string _getFilePath() {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            if (rdioEncode.Checked && !OptionsForm.LSBMethodSelected) {
                saveFileDialog.FileName = _decodeFileName + "_encoded.jpeg";
                saveFileDialog.Filter = "Image Files (*.jpeg)|*.jpeg";
                saveFileDialog.FilterIndex = 2;
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.Title = "Save encoded image as";
            } else if (rdioEncode.Checked && OptionsForm.LSBMethodSelected) {
                saveFileDialog.FileName = _decodeFileName + "_encoded.png";
                saveFileDialog.Filter = "Image Files (*.png)|*.png";
                saveFileDialog.FilterIndex = 2;
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.Title = "Save encoded image as";
            } else if (rdioDecode.Checked) {
                saveFileDialog.FileName = _decodeFileName + "_decoded";
                saveFileDialog.Filter = "No file Extension ()|";
                saveFileDialog.FilterIndex = 2;
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.Title = "Save decoded message as";
            }


            if (saveFileDialog.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(saveFileDialog.FileName)) {
                string savePath = saveFileDialog.FileName;
                return savePath;
            } else {
                throw new NoSavePathSelectedException();
            }
        }

        //Handles encoding/decoding using the correct method and settings when the 'Proceed' button is pressed.
        private void _encodeOrDecodeImage() {
            if (rdioEncode.Checked) {
                if (picResult.Image != null) {
                    picResult.Image.Dispose();
                }

                byte[] msg = _getMessageFromTextboxOrFile();

                //Create an _imageEncoder according according to selected method
                if (!OptionsForm.LSBMethodSelected) {
                    lblProcessing.Text = "Encoding using GT method...";
                    lblProcessing.Visible = true;
                    Application.DoEvents();

                    //Use simple constructor if a table is null 
                    if (OptionsForm.QuantizationTableY == null || OptionsForm.QuantizationTableChr == null || OptionsForm.HuffmanTableYAC == null || OptionsForm.HuffmanTableYDC == null || OptionsForm.HuffmanTableChrAC == null || OptionsForm.HuffmanTableChrDC == null) {
                        _imageEncoder = new JpegImage(CoverImage, OptionsForm.Quality, OptionsForm.MValue);
                    } else {
                        _imageEncoder = new JpegImage(CoverImage, OptionsForm.Quality, OptionsForm.MValue, OptionsForm.QuantizationTableY, OptionsForm.QuantizationTableChr, OptionsForm.HuffmanTableYDC, OptionsForm.HuffmanTableYAC, OptionsForm.HuffmanTableChrDC, OptionsForm.HuffmanTableChrAC);
                    }
                } else {
                    lblProcessing.Text = "Encoding using LSB method...";
                    lblProcessing.Visible = true;
                    Application.DoEvents();
                    _imageEncoder = new LeastSignificantBitImage(CoverImage);
                }

                //Encode
                try {
                    _imageEncoder.Encode(msg);
                    _imageEncoder.Save(_userSavePath);
                    picResult.Image = Image.FromFile(_userSavePath);
                }
                catch (ImageCannotContainDataException) {
                    MessageBox.Show("Image cannot contain data!");
                }
                catch (ExternalException) {
                    MessageBox.Show("Failed to load result picture! Your Huffman table may be invalid");
                }
                catch (Exception) {
                    MessageBox.Show("An error occured when encoding!");
                }
            } else if (rdioDecode.Checked) {
                picResult.Image = null;
                tbMessage.Text = "";

                if (!OptionsForm.LSBMethodSelected) {
                    lblProcessing.Text = "Decoding using GT method...";
                    lblProcessing.Visible = true;
                    Application.DoEvents();
                    _imageDecoder = new JPEGDecoder(_decodeFilePath);
                } else {
                    lblProcessing.Text = "Decoding using LSB method...";
                    lblProcessing.Visible = true;
                    Application.DoEvents();
                    _imageDecoder = new LeastSignificantBitDecoder(_decodeFilePath);
                }

                //Decode
                try {
                    byte[] message = _imageDecoder.Decode();
                    tbMessage.Text = new string(message.Select(x => (char)x).ToArray());
                    File.WriteAllBytes(_userSavePath, message);
                }
                catch (Exception) {
                    MessageBox.Show("An Error occured when decoding! Cover image might not contain a message.");
                }
            }
        }

        //Checks whether the message is in the TextBox or as a file and returns the message as a byte[]
        private byte[] _getMessageFromTextboxOrFile() {
            byte[] msg = new byte[_messageLength];

            if (_messageTextSet) {
                for (int i = 0; i < _messageLength; i++) {
                    msg[i] = (byte)(tbMessage.Text.ToCharArray()[i]);
                }
            } else if (_messageFileSet) {
                for (int i = 0; i < _messageLength; i++) {
                    msg[i] = _message[i];
                }
            }

            return msg;
        }
        #endregion

        //'Escape' closes form
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {
            if (keyData == Keys.Escape) {
                DialogResult dialogResult = MessageBox.Show("Do you want to close Stegosaurus?", "", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes) {
                    this.Close();
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
