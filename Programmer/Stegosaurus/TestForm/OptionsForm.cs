using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Stegosaurus;
using System.IO;

namespace TestForm
{
    public partial class OptionsForm : Form
    {
        private readonly StegosaurusForm _stegosaurusForm = new StegosaurusForm();
        private bool _skipDialog;
        public static readonly HuffmanTableComponent HuffmanTableYAC = new HuffmanTableComponent(HuffmanTable.JpegHuffmanTableYAC);
        public static readonly HuffmanTableComponent HuffmanTableYDC = new HuffmanTableComponent(HuffmanTable.JpegHuffmanTableYDC);
        public static readonly HuffmanTableComponent HuffmanTableChrAC = new HuffmanTableComponent(HuffmanTable.JpegHuffmanTableChrAC);
        public static readonly HuffmanTableComponent HuffmanTableChrDC = new HuffmanTableComponent(HuffmanTable.JpegHuffmanTableChrDC);
        public static readonly QuantizationTableComponent QuantizationTableY = new QuantizationTableComponent(QuantizationTable.JpegDefaultYTable);
        public static readonly QuantizationTableComponent QuantizationTableChr = new QuantizationTableComponent(QuantizationTable.JpegDefaultChrTable);
        public static string ImagesSavePath;
        public static int QualityGT;
        public static bool SaveEnabled;

        public OptionsForm()
        {
            InitializeComponent();
            initializeQuantizationTable(QuantizationTableY);
            initializeQuantizationTable(QuantizationTableChr);
            initializeHuffmanTable(HuffmanTableYAC);
            initializeHuffmanTable(HuffmanTableYDC);
            initializeHuffmanTable(HuffmanTableChrAC);
            initializeHuffmanTable(HuffmanTableChrDC);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;            

            rdioQuantizationYChannel.Checked = true;
            rdioHuffmanY_AC.Checked = true;
            QualityGT = StegosaurusForm.QualityGT;
            tbarQualitySlider.Value = QualityGT;
        }

        //Adds defaultTable.Length amount of textboxes to a given Huffman panel and saves each in an array (to be looped through), sets
        //the size and position of each textbox and writes the default Quantization values in these.
        private void initializeHuffmanTable(HuffmanTableComponent huffmanTableComponent)
        {
            grpCustomHuffman.Controls.Add(huffmanTableComponent);
            huffmanTableComponent.Location = new Point(4, 30);
            huffmanTableComponent.AutoScroll = Enabled;
            huffmanTableComponent.BringToFront();
        }

        private void initializeQuantizationTable(QuantizationTableComponent quantizationTableComponent)
        {
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
                pnlOptionsGeneral.Visible = true;
                pnlOptionsGeneral.Enabled = true;
            }
            else if (OptionsBox.SelectedItem == OptionsBox.Items[1])
            {
                pnlOptionsHuffman.Visible = true;
                pnlOptionsHuffman.Enabled = true;
            }
            else if (OptionsBox.SelectedItem == OptionsBox.Items[2])
            {
                //pnlOptionsQuantization.Visible = true;
                //pnlOptionsQuantization.Enabled = true;
            }
            else if (OptionsBox.SelectedItem == OptionsBox.Items[3])
            {
                pnlOptionsQuality.Visible = true;
                pnlOptionsQuality.Enabled = true;
            }
            else if (OptionsBox.SelectedItem == OptionsBox.Items[4])
            {
                pnlOptionsQuantization.Visible = true;
                pnlOptionsQuantization.Enabled = true;
            }
        }

        private void DeselectAllOptionPanels()
        {
            pnlOptionsGeneral.Visible = false;
            pnlOptionsGeneral.Enabled = false;

            pnlOptionsHuffman.Visible = false;
            pnlOptionsHuffman.Enabled = false;

            pnlOptionsQuality.Visible = false;
            pnlOptionsQuality.Enabled = false;

            pnlOptionsQuantization.Visible = false;
            pnlOptionsQuantization.Enabled = false;
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
                QuantizationTableChr.Visible = false;
                QuantizationTableChr.Enabled = false;
                QuantizationTableY.Visible = true;
                QuantizationTableY.Enabled = true;
            }
            else if (rdioQuantizationChrChannel.Checked)
            {
                QuantizationTableY.Visible = false;
                QuantizationTableY.Enabled = false;
                QuantizationTableChr.Visible = true;
                QuantizationTableChr.Enabled = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (!string.IsNullOrEmpty(ImagesSavePath))
            {
               // _stegosaurusForm.ImagesSavePath = ImagesSavePath;
            }

            QualityGT = tbarQualitySlider.Value;
            _skipDialog = true;
            SaveEnabled = true;
            Cursor.Current = Cursors.Default;

            this.Close();
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
                HuffmanTableChrAC.Visible = true;
                HuffmanTableChrAC.Enabled = true;
            }
            else if (rdioHuffmanChr_DC.Checked)
            {
                HuffmanTableChrDC.Visible = true;
                HuffmanTableChrDC.Enabled = true;
            }
            else if (rdioHuffmanY_AC.Checked)
            {
                HuffmanTableYAC.Visible = true;
                HuffmanTableYAC.Enabled = true;
            }
            else if (rdioHuffmanY_DC.Checked)
            {
                HuffmanTableYDC.Visible = true;
                HuffmanTableYDC.Enabled = true;
            }
        }
        
        private void deselectHuffmanTables()
        {
            HuffmanTableChrAC.Visible = false;
            HuffmanTableChrAC.Enabled = false;

            HuffmanTableChrDC.Visible = false;
            HuffmanTableChrDC.Enabled = false;

            HuffmanTableYAC.Visible = false;
            HuffmanTableYAC.Enabled = false;

            HuffmanTableYDC.Visible = false;
            HuffmanTableYDC.Enabled = false;
        }

        //Adds a row to the selected Huffman-table when btnHuffmanAddRow is clicked
        private void btnHuffmanAddRow_Click(object sender, EventArgs e)
        {
            if (rdioHuffmanChr_AC.Checked)
            {
                HuffmanTableChrAC.AddRow();
            }
            else if (rdioHuffmanChr_DC.Checked)
            {
                HuffmanTableChrDC.AddRow();
            }
            else if (rdioHuffmanY_AC.Checked)
            {
                HuffmanTableYAC.AddRow();
            }
            else if (rdioHuffmanY_DC.Checked)
            {
                HuffmanTableYDC.AddRow();
            }
        }

        private void selectOutputFolder_HelpRequest(object sender, EventArgs e)
        {
            
        }

        private void btnSelectOutputFolder_Click(object sender, EventArgs e)
        {
            DialogResult result = selectOutputFolder.ShowDialog();

            if (!string.IsNullOrWhiteSpace(selectOutputFolder.SelectedPath))
            {
                ImagesSavePath = selectOutputFolder.SelectedPath;
                tbSaveLocation.Text = ImagesSavePath;
            }
        }

        private void tbarQualitySlider_ValueChanged(object sender, EventArgs e)
        {
            lblEncodingQualityValue.Text = tbarQualitySlider.Value.ToString();
        }
    }
}
