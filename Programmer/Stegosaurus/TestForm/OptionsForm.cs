using System;
using System.Drawing;
using System.Windows.Forms;
using Stegosaurus;

namespace TestForm
{
    public partial class OptionsForm : Form
    {
        private readonly StegosaurusForm _stegosaurusForm = new StegosaurusForm();
        private bool _skipDialog;
        private bool _qualityLocked;

        public static HuffmanTableComponent HuffmanTableComponentYAC,
            HuffmanTableComponentYDC,
            HuffmanTableComponentChrAC,
            HuffmanTableComponentChrDC;
        public static QuantizationTableComponent QuantizationTableComponentY, QuantizationTableComponentChr;

        public static int Quality;
        public static bool SaveEnabled;
        public static bool LSBMethodSelected;
        public static bool ResetToDefault;
        public static bool SkipSettingsInitialization = false;

        public OptionsForm()
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            InitializeComponent();
            initializeSettings();
        }

        //Custom components _ created and settings are set.
        private void initializeSettings()
        {
            initializeQuantizationTable(out QuantizationTableComponentY, StegosaurusForm.QuantizationTableY, QuantizationTable.JpegDefaultYTable);
            initializeQuantizationTable(out QuantizationTableComponentChr, StegosaurusForm.QuantizationTableChr, QuantizationTable.JpegDefaultChrTable);
            initializeHuffmanTable(out HuffmanTableComponentYAC, StegosaurusForm.HuffmanTableYAC, HuffmanTable.JpegHuffmanTableYAC);
            initializeHuffmanTable(out HuffmanTableComponentYDC, StegosaurusForm.HuffmanTableYDC, HuffmanTable.JpegHuffmanTableYDC);
            initializeHuffmanTable(out HuffmanTableComponentChrAC, StegosaurusForm.HuffmanTableChrAC, HuffmanTable.JpegHuffmanTableChrAC);
            initializeHuffmanTable(out HuffmanTableComponentChrDC, StegosaurusForm.HuffmanTableChrDC, HuffmanTable.JpegHuffmanTableChrDC);
            Quality = StegosaurusForm.Quality;
            _qualityLocked = StegosaurusForm.QualityLocked;
            LSBMethodSelected = StegosaurusForm.LSBMethodSelected;

            OptionsBox.SelectedItem = OptionsBox.Items[0];
            rdioQuantizationYChannel.Checked = true;
            rdioHuffmanY_AC.Checked = true;
            tbarQualitySlider.Value = Quality;
            if (_qualityLocked)
            {
                tbarQualitySlider.Enabled = false;
            }
            else
            {
                tbarQualitySlider.Enabled = true;
            }

            if (!LSBMethodSelected)
            {
                rdioGTMethod.Checked = true;
            }
            else
            {
                rdioLSBMethod.Checked = true;
            }
        }

        //Adds defaultTable.Length amount of textboxes to a given Huffman panel and saves each in an array (to be looped through), sets
        //the size and position of each textbox and writes the default Quantization values in these.
        private void initializeHuffmanTable(out HuffmanTableComponent huffmanTableComponent, HuffmanTable settingsHuffmanTable, HuffmanTable defaultHuffmanTable)
        {

            if (settingsHuffmanTable != null)
            {
                huffmanTableComponent = new HuffmanTableComponent(settingsHuffmanTable);
            }
            else
            {
                huffmanTableComponent = new HuffmanTableComponent(defaultHuffmanTable);
            }
            grpCustomHuffman.Controls.Add(huffmanTableComponent);
            huffmanTableComponent.Location = new Point(4, 30);
            huffmanTableComponent.AutoScroll = Enabled;
            huffmanTableComponent.BringToFront();
        }

        private void initializeQuantizationTable(out QuantizationTableComponent quantizationTableComponent, QuantizationTable settingsQuantizationTable, QuantizationTable defaultQuantizationTable)
        {
            if (settingsQuantizationTable != null)
            {
                quantizationTableComponent = new QuantizationTableComponent(settingsQuantizationTable);
            }
            else
            {
                quantizationTableComponent = new QuantizationTableComponent(defaultQuantizationTable);
            }

            pnlQuantization.Controls.Add(quantizationTableComponent);
            quantizationTableComponent.Location = new Point(4, 18);
            quantizationTableComponent.BringToFront();
        }

        //Adds 64 (defaultTable length amount) textboxes to a quantization panel and saves each in an array (to be looped through), sets
        //the size and position of each textbox and writes the default Quantization values in these.

        //The selected Options-panel is enabled and made visible, the opposite is done to the rest.
        private void OptionsBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DeselectAllOptionPanels();
            if (OptionsBox.SelectedItem == OptionsBox.Items[0])
            {
                pnlOptionsHuffman.Visible = true;
                pnlOptionsHuffman.Enabled = true;
            }
            else if (OptionsBox.SelectedItem == OptionsBox.Items[1])
            {
                pnlOptionsQuantization.Visible = true;
                pnlOptionsQuantization.Enabled = true;
            }
            else if (OptionsBox.SelectedItem == OptionsBox.Items[2])
            {
                pnlOptionsQuality.Visible = true;
                pnlOptionsQuality.Enabled = true;
            }
            else if (OptionsBox.SelectedItem == OptionsBox.Items[3])
            {
                pnlOptionsEncodingMethod.Visible = true;
                pnlOptionsEncodingMethod.Enabled = true;
            }
        }

        private void DeselectAllOptionPanels()
        {
            pnlOptionsHuffman.Visible = false;
            pnlOptionsHuffman.Enabled = false;

            pnlOptionsQuality.Visible = false;
            pnlOptionsQuality.Enabled = false;

            pnlOptionsQuantization.Visible = false;
            pnlOptionsQuantization.Enabled = false;

            pnlOptionsEncodingMethod.Visible = false;
            pnlOptionsEncodingMethod.Enabled = false;
        }

        //'Escape' closes form 
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        //The selected Quantization-table is enabled and made visible, the opposite is done to the other.
        private void yQuantizationChannelChecked_DisplayYOrChrTable(object sender, EventArgs e)
        {
            if (rdioQuantizationYChannel.Checked)
            {
                QuantizationTableComponentChr.Visible = false;
                QuantizationTableComponentChr.Enabled = false;
                QuantizationTableComponentY.Visible = true;
                QuantizationTableComponentY.Enabled = true;
            }
            else if (rdioQuantizationChrChannel.Checked)
            {
                QuantizationTableComponentY.Visible = false;
                QuantizationTableComponentY.Enabled = false;
                QuantizationTableComponentChr.Visible = true;
                QuantizationTableComponentChr.Enabled = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Quality = tbarQualitySlider.Value;
            _skipDialog = true;
            SaveEnabled = true;
            Cursor.Current = Cursors.Default;

            this.Close();
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            switch (MessageBox.Show(this, "Are you sure you want to set all settings to default?", "Resetting to default", MessageBoxButtons.YesNo))
            {
                case DialogResult.No:
                    break;
                default:
                    ResetToDefault = true;
                    _skipDialog = true;
                    Close();
                    break;
            }
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (!_skipDialog)
            {
                if (e.CloseReason == CloseReason.WindowsShutDown) return;

                // Confirm user wants to close
                switch (MessageBox.Show(this, "Are you sure you want to close without saving?", "Closing", MessageBoxButtons.YesNo))
                {
                    case DialogResult.No:
                        e.Cancel = true;
                        break;
                    default:
                        break;
                }
            }
        }

        private void HuffmannChannelCheckedChanged_DisplayCorrectTable(object sender, EventArgs e)
        {
            deselectHuffmanTables();

            if (rdioHuffmanChr_AC.Checked)
            {
                HuffmanTableComponentChrAC.Visible = true;
                HuffmanTableComponentChrAC.Enabled = true;
            }
            else if (rdioHuffmanChr_DC.Checked)
            {
                HuffmanTableComponentChrDC.Visible = true;
                HuffmanTableComponentChrDC.Enabled = true;
            }
            else if (rdioHuffmanY_AC.Checked)
            {
                HuffmanTableComponentYAC.Visible = true;
                HuffmanTableComponentYAC.Enabled = true;
            }
            else if (rdioHuffmanY_DC.Checked)
            {
                HuffmanTableComponentYDC.Visible = true;
                HuffmanTableComponentYDC.Enabled = true;
            }
        }

        private void deselectHuffmanTables()
        {
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
        private void btnHuffmanAddRow_Click(object sender, EventArgs e)
        {
            if (rdioHuffmanChr_AC.Checked)
            {
                HuffmanTableComponentChrAC.AddRow();
            }
            else if (rdioHuffmanChr_DC.Checked)
            {
                HuffmanTableComponentChrDC.AddRow();
            }
            else if (rdioHuffmanY_AC.Checked)
            {
                HuffmanTableComponentYAC.AddRow();
            }
            else if (rdioHuffmanY_DC.Checked)
            {
                HuffmanTableComponentYDC.AddRow();
            }
            else
            {
                MessageBox.Show("Please select a table to which you want to add a row!");
            }
        }

        private void tbarQualitySlider_ValueChanged(object sender, EventArgs e)
        {
            lblEncodingQualityValue.Text = tbarQualitySlider.Value.ToString();
        }

        private void rdioGT_CheckedChangedSetMethod(object sender, EventArgs e)
        {
            if (rdioGTMethod.Checked)
            {
                LSBMethodSelected = false;
            }
            else
            {
                LSBMethodSelected = true;
            }
        }
    }
}
