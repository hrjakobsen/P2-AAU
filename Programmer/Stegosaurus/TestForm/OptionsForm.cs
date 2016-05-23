using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Stegosaurus;
using TestForm.Properties;

namespace TestForm {
    public partial class OptionsForm : Form {
        private bool _skipDialog;
        private const byte DefaultMValue = 4;
        private byte _mValue;
        public static byte MValue { get; set; }
        public static HuffmanTable HuffmanTableChrAC, HuffmanTableChrDC, HuffmanTableYAC, HuffmanTableYDC;
        public static QuantizationTable QuantizationTableY, QuantizationTableChr;
        public static HuffmanTableComponent HuffmanTableComponentYAC,
            HuffmanTableComponentYDC,
            HuffmanTableComponentChrAC,
            HuffmanTableComponentChrDC;
        public static QuantizationTableComponent QuantizationTableComponentY, QuantizationTableComponentChr;
        public static bool QualityLocked;
        public static bool SaveEnabled { get; private set; }
        public static bool LSBMethodSelected { get; set; }
        public static bool ResetToDefault { get; set; }
        public static int Quality { get; set; }
        public static int DefaultQualityWithCustomQTable = 52;
        public static int DefaultQuality = 80;

        public OptionsForm() {
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            InitializeComponent();
            _refreshSettings();
        }

        //Custom components are created and the interface is refreshed.
        private void _refreshSettings() {
            _initializeQuantizationTableComponent(out QuantizationTableComponentY, QuantizationTableY, QuantizationTable.JpegDefaultYTable);
            _initializeQuantizationTableComponent(out QuantizationTableComponentChr, QuantizationTableChr, QuantizationTable.JpegDefaultChrTable);
            _initializeHuffmanTableComponent(out HuffmanTableComponentYAC, HuffmanTableYAC, HuffmanTable.JpegHuffmanTableYAC);
            _initializeHuffmanTableComponent(out HuffmanTableComponentYDC, HuffmanTableYDC, HuffmanTable.JpegHuffmanTableYDC);
            _initializeHuffmanTableComponent(out HuffmanTableComponentChrAC, HuffmanTableChrAC, HuffmanTable.JpegHuffmanTableChrAC);
            _initializeHuffmanTableComponent(out HuffmanTableComponentChrDC, HuffmanTableChrDC, HuffmanTable.JpegHuffmanTableChrDC);

            OptionsBox.SelectedItem = OptionsBox.Items[0];

            rdioHuffmanY_AC.Checked = true;
            HuffmannChannelCheckedChanged_DisplayCorrectTable(null, null);

            rdioQuantizationYChannel.Checked = true;
            rdioHuffmanY_AC.Checked = true;
            tbarQualitySlider.Value = Quality;
            cbMValue.Text = MValue.ToString();

            if (QualityLocked) {
                tbarQualitySlider.Enabled = false;
            } else {
                tbarQualitySlider.Enabled = true;
            }

            if (!LSBMethodSelected) {
                rdioGTMethod.Checked = true;
            } else {
                rdioLSBMethod.Checked = true;
            }
        }

        //Adds defaultTable.Length amount of textboxes to a given Huffman panel and saves each in an array (to be looped through), sets
        //the size and position of each textbox and writes the default Quantization values in these.
        private void _initializeHuffmanTableComponent(out HuffmanTableComponent huffmanTableComponent, HuffmanTable settingsHuffmanTable, HuffmanTable defaultHuffmanTable) {

            if (settingsHuffmanTable != null) {
                huffmanTableComponent = new HuffmanTableComponent(settingsHuffmanTable);
            } else {
                huffmanTableComponent = new HuffmanTableComponent(defaultHuffmanTable);
            }
            grpCustomHuffman.Controls.Add(huffmanTableComponent);
            huffmanTableComponent.Location = new Point(4, 30);
            huffmanTableComponent.AutoScroll = Enabled;
            huffmanTableComponent.BringToFront();
        }

        private void _initializeQuantizationTableComponent(out QuantizationTableComponent quantizationTableComponent, QuantizationTable settingsQuantizationTable, QuantizationTable defaultQuantizationTable) {
            if (settingsQuantizationTable != null) {
                quantizationTableComponent = new QuantizationTableComponent(settingsQuantizationTable);
            } else {
                quantizationTableComponent = new QuantizationTableComponent(defaultQuantizationTable);
            }

            pnlQuantization.Controls.Add(quantizationTableComponent);
            quantizationTableComponent.Location = new Point(4, 18);
            quantizationTableComponent.BringToFront();
        }

        //Loads settings from Properties.Settings for more general settings and from a .CSV-file for custom tables.
        public static void LoadSettingsFromFile() {
            LSBMethodSelected = Settings.Default.LSBMethodSelected;
            Quality = Settings.Default.Quality;
            QualityLocked = Settings.Default.QualityLocked;
            MValue = Settings.Default.MValue;

            _loadHuffmanTableFromFile(out HuffmanTableYAC, "HuffmanTableYAC.txt");
            _loadHuffmanTableFromFile(out HuffmanTableYAC, "HuffmanTableYAC.txt");
            _loadHuffmanTableFromFile(out HuffmanTableYAC, "HuffmanTableYAC.txt");
            _loadHuffmanTableFromFile(out HuffmanTableYAC, "HuffmanTableYAC.txt");
            _loadQuantizationTableFromFile(out QuantizationTableY, "QuantizationTableY.txt");
            _loadQuantizationTableFromFile(out QuantizationTableChr, "QuantizationTableChr.txt");
        }

        //Uses the HuffmanTable.Fromstring() method to create a Huffman table from a string optained from a file.
        private static void _loadHuffmanTableFromFile(out HuffmanTable huffmanTable, string filePath) {
            if (File.Exists(filePath)) {
                string input = File.ReadAllText(filePath);
                huffmanTable = HuffmanTable.FromString(input);
            } else {
                huffmanTable = null;
            }
        }

        //Uses the QuantizationTable.Fromstring() method to create a quantization table from a string optained from a file.
        private static void _loadQuantizationTableFromFile(out QuantizationTable quantizationTable, string filePath) {
            if (File.Exists(filePath)) {
                string input = File.ReadAllText(filePath);
                quantizationTable = QuantizationTable.FromString(input);
            } else {
                quantizationTable = null;
            }
        }

        //The selected Options-panel is enabled and made visible, the opposite is done to the rest.
        private void OptionsBox_SelectedIndexChanged(object sender, EventArgs e) {
            DeselectAllOptionPanels();
            if (OptionsBox.SelectedItem == OptionsBox.Items[0]) {
                pnlOptionsHuffman.Visible = true;
                pnlOptionsHuffman.Enabled = true;
            } else if (OptionsBox.SelectedItem == OptionsBox.Items[1]) {
                pnlOptionsQuantization.Visible = true;
                pnlOptionsQuantization.Enabled = true;
            } else if (OptionsBox.SelectedItem == OptionsBox.Items[2]) {
                pnlOptionsQuality.Visible = true;
                pnlOptionsQuality.Enabled = true;
            } else if (OptionsBox.SelectedItem == OptionsBox.Items[3]) {
                pnlOptionsEncodingMethod.Visible = true;
                pnlOptionsEncodingMethod.Enabled = true;
            }
        }

        private void DeselectAllOptionPanels() {
            pnlOptionsHuffman.Visible = false;
            pnlOptionsHuffman.Enabled = false;

            pnlOptionsQuality.Visible = false;
            pnlOptionsQuality.Enabled = false;

            pnlOptionsQuantization.Visible = false;
            pnlOptionsQuantization.Enabled = false;

            pnlOptionsEncodingMethod.Visible = false;
            pnlOptionsEncodingMethod.Enabled = false;
        }

        //The selected Quantization-table is enabled and made visible, the opposite is done to the other.
        private void yQuantizationChannelChecked_DisplayYOrChrTable(object sender, EventArgs e) {
            if (rdioQuantizationYChannel.Checked) {
                QuantizationTableComponentChr.Visible = false;
                QuantizationTableComponentChr.Enabled = false;
                QuantizationTableComponentY.Visible = true;
                QuantizationTableComponentY.Enabled = true;
            } else if (rdioQuantizationChrChannel.Checked) {
                QuantizationTableComponentY.Visible = false;
                QuantizationTableComponentY.Enabled = false;
                QuantizationTableComponentChr.Visible = true;
                QuantizationTableComponentChr.Enabled = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e) {
            Cursor.Current = Cursors.WaitCursor;
            Quality = tbarQualitySlider.Value;
            _skipDialog = true;
            try {
                _saveSettingsInternally();
                _saveSettingsToFile();
                Close();
            }
            catch (Exception) {
                MessageBox.Show("An error occured while trying to save your settings, they seem to be invalid!", "Error saving settings");
            }
            Cursor.Current = Cursors.Default;
        }

        private void _saveSettingsInternally() {
            Quality = tbarQualitySlider.Value;

            if (_mValue != 0) {
                MValue = _mValue;
            } else {
                MValue = 4;
            }

            if (rdioGTMethod.Checked) {
                LSBMethodSelected = false;
            } else {
                LSBMethodSelected = true;
            }

            HuffmanTableYAC = _defaultOrCustomHuffmanTable(HuffmanTableComponentYAC.SaveTable(), HuffmanTable.JpegHuffmanTableYAC);
            HuffmanTableYDC = _defaultOrCustomHuffmanTable(HuffmanTableComponentYDC.SaveTable(), HuffmanTable.JpegHuffmanTableYDC);
            HuffmanTableChrAC = _defaultOrCustomHuffmanTable(HuffmanTableComponentChrAC.SaveTable(), HuffmanTable.JpegHuffmanTableChrAC);
            HuffmanTableChrDC = _defaultOrCustomHuffmanTable(HuffmanTableComponentChrDC.SaveTable(), HuffmanTable.JpegHuffmanTableChrDC);
            QuantizationTableY = _defaultOrCustomQuantizationTable(QuantizationTableComponentY.SaveTable(), QuantizationTable.JpegDefaultYTable);
            QuantizationTableChr = _defaultOrCustomQuantizationTable(QuantizationTableComponentChr.SaveTable(), QuantizationTable.JpegDefaultChrTable);
        }

        //Saves settings to Properties.Settings for more general settings and to a .txt-file for custom tables
        private static void _saveSettingsToFile() {
            _saveHuffmanTableToFile(HuffmanTableYAC, "HuffmanTableYAC.txt");
            _saveHuffmanTableToFile(HuffmanTableYDC, "HuffmanTableYDC.txt");
            _saveHuffmanTableToFile(HuffmanTableChrAC, "HuffmanTableChrAC.txt");
            _saveHuffmanTableToFile(HuffmanTableChrDC, "HuffmanTableChrDC.txt");
            _saveQuantizationTableToFile(QuantizationTableY, "QuantizationTableY.txt");
            _saveQuantizationTableToFile(QuantizationTableChr, "QuantizationTableChr.txt");
            Properties.Settings.Default.QualityLocked = QualityLocked;
            Properties.Settings.Default.Quality = Quality;
            Properties.Settings.Default.MValue = MValue;
            Properties.Settings.Default.LSBMethodSelected = LSBMethodSelected;

            Properties.Settings.Default.Save();
        }

        private static void _saveHuffmanTableToFile(HuffmanTable huffmanTable, string filePath) {
            if (huffmanTable != null) {
                File.WriteAllText(filePath, huffmanTable.ToString());
            }
        }

        private static void _saveQuantizationTableToFile(QuantizationTable quantizationTable, string filePath) {
            if (quantizationTable != null) {
                File.WriteAllText(filePath, quantizationTable.ToString());
            }
        }

        private void btnDefault_Click(object sender, EventArgs e) {
            switch (MessageBox.Show(this, "Are you sure you want to set all settings to default?", "Resetting to default", MessageBoxButtons.YesNo)) {
                case DialogResult.No:
                    break;
                default:
                    _resetSettingsToDefault();
                    _refreshSettings();
                    break;
            }
        }

        private void _resetSettingsToDefault() {
            Quality = DefaultQuality;
            MValue = DefaultMValue;
            QualityLocked = false;
            LSBMethodSelected = false;

            HuffmanTableYAC = HuffmanTable.JpegHuffmanTableYAC;
            HuffmanTableYDC = HuffmanTable.JpegHuffmanTableYDC;
            HuffmanTableChrAC = HuffmanTable.JpegHuffmanTableChrAC;
            HuffmanTableChrDC = HuffmanTable.JpegHuffmanTableChrDC;
            QuantizationTableY = QuantizationTable.JpegDefaultYTable;
            QuantizationTableChr = QuantizationTable.JpegDefaultChrTable;
        }

        private void btnClose_Click(object sender, EventArgs e) {
            Close();
        }

        protected override void OnFormClosing(FormClosingEventArgs e) {
            base.OnFormClosing(e);

            if (!_skipDialog) {
                if (e.CloseReason == CloseReason.WindowsShutDown) return;

                // Confirm user wants to close
                switch (MessageBox.Show(this, "Are you sure you want to close without saving?", "Closing", MessageBoxButtons.YesNo)) {
                    case DialogResult.No:
                        e.Cancel = true;
                        break;
                }
            }
        }

        //'Escape' closes form 
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {
            if (keyData == Keys.Escape) {
                Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        //Returns default table if the table made from the tablecomponent is the same as defaultTable, custom table if they are different.
        //Doing this prevents an error from ocuring when changing quality while using a 'default' table manually set.
        private static HuffmanTable _defaultOrCustomHuffmanTable(HuffmanTable customHuffmanTable, HuffmanTable defaultTable) {
            HuffmanTable H;
            if (customHuffmanTable.Equals(defaultTable)) {
                H = defaultTable;
            } else {
                H = customHuffmanTable;
            }

            return H;
        }

        //Returns default table if the table made from the tablecomponent is the same as defaultTable, custom table if they are different.
        //Doing this prevents an error from ocuring when changing quality while using a 'default' non-default table
        private static QuantizationTable _defaultOrCustomQuantizationTable(QuantizationTable customQuantizationTable,
            QuantizationTable defaultTable) {
            QuantizationTable q;

            if (customQuantizationTable.Equals(defaultTable)) {
                q = defaultTable;
            } else {
                q = customQuantizationTable;
            }

            return q;
        }

        private void HuffmannChannelCheckedChanged_DisplayCorrectTable(object sender, EventArgs e) {
            deselectHuffmanTables();

            if (rdioHuffmanChr_AC.Checked) {
                HuffmanTableComponentChrAC.Visible = true;
                HuffmanTableComponentChrAC.Enabled = true;
            } else if (rdioHuffmanChr_DC.Checked) {
                HuffmanTableComponentChrDC.Visible = true;
                HuffmanTableComponentChrDC.Enabled = true;
            } else if (rdioHuffmanY_AC.Checked) {
                HuffmanTableComponentYAC.Visible = true;
                HuffmanTableComponentYAC.Enabled = true;
            } else if (rdioHuffmanY_DC.Checked) {
                HuffmanTableComponentYDC.Visible = true;
                HuffmanTableComponentYDC.Enabled = true;
            }
        }

        private void deselectHuffmanTables() {
            HuffmanTableComponentChrAC.Visible = false;
            HuffmanTableComponentChrAC.Enabled = false;

            HuffmanTableComponentChrDC.Visible = false;
            HuffmanTableComponentChrDC.Enabled = false;

            HuffmanTableComponentYAC.Visible = false;
            HuffmanTableComponentYAC.Enabled = false;

            HuffmanTableComponentYDC.Visible = false;
            HuffmanTableComponentYDC.Enabled = false;
        }

        //Adds a row to the selected Huffman-table when btnHuffmanAddRow is clicked
        private void btnHuffmanAddRow_Click(object sender, EventArgs e) {
            if (rdioHuffmanChr_AC.Checked) {
                HuffmanTableComponentChrAC.AddRow();
            } else if (rdioHuffmanChr_DC.Checked) {
                HuffmanTableComponentChrDC.AddRow();
            } else if (rdioHuffmanY_AC.Checked) {
                HuffmanTableComponentYAC.AddRow();
            } else if (rdioHuffmanY_DC.Checked) {
                HuffmanTableComponentYDC.AddRow();
            } else {
                MessageBox.Show("Please select a table to which you want to add a row!");
            }
        }

        private void tbarQualitySlider_ValueChanged(object sender, EventArgs e) {
            if (tbarQualitySlider.Value != DefaultQuality) {
                lblEncodingQualityValue.Text = tbarQualitySlider.Value.ToString();
            } else {
                lblEncodingQualityValue.Text = tbarQualitySlider.Value.ToString() + @"  (default)";
            }
        }

        private void cbMValue_SelectedValueChanged(object sender, EventArgs e) {
            if (cbMValue.GetItemText(cbMValue.SelectedItem) == "2") {
                _mValue = 2;
            } else if (cbMValue.GetItemText(cbMValue.SelectedItem) == "4") {
                _mValue = 4;
            } else if (cbMValue.GetItemText(cbMValue.SelectedItem) == "16") {
                _mValue = 16;
            }
        }
    }
}
