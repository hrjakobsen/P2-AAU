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

namespace TestForm
{
    public partial class OptionsForm : Form
    {
        readonly StegosaurusForm stegosaurusForm = new StegosaurusForm();
        TextBox[] QuantizationBoxesY = new TextBox[64];
        TextBox[] QuantizationBoxesChr = new TextBox[64];
        List<TextBox> huffmanBoxesChr_AC = new List<TextBox>();
        List<TextBox> huffmanBoxesChr_DC = new List<TextBox>();
        List<TextBox> huffmanBoxesY_AC = new List<TextBox>();
        List<TextBox> huffmanBoxesY_DC = new List<TextBox>();
        HuffmanTableComponent huffmanTableY_AC = new HuffmanTableComponent(HuffmanTable.JpegHuffmanTableYAC);
        HuffmanTableComponent huffmanTableY_DC = new HuffmanTableComponent(HuffmanTable.JpegHuffmanTableYDC);
        HuffmanTableComponent huffmanTableChr_AC = new HuffmanTableComponent(HuffmanTable.JpegHuffmanTableChrAC);
        HuffmanTableComponent huffmanTableChr_DC = new HuffmanTableComponent(HuffmanTable.JpegHuffmanTableChrDC);
        QuantizationTableComponent quantizationTableY = new QuantizationTableComponent(QuantizationTable.JpegDefaultYTable);
        QuantizationTableComponent quantizationTableChr = new QuantizationTableComponent(QuantizationTable.JpegDefaultChrTable);

        #region default Tables
        string[] defaultQuantizationTableY = {
            "0x10", "0x0b", "0x0a", "0x10", "0x18", "0x28", "0x33", "0x3d",
            "0x0c", "0x0c", "0x0e", "0x13", "0x1a", "0x3a", "0x3c", "0x37",
            "0x0e", "0x0d", "0x10", "0x18", "0x28", "0x39", "0x45", "0x38",
            "0x0e", "0x11" , "0x16", "0x1d", "0x33", "0x57", "0x50", "0x3e",
            "0x12", "0x16", "0x25", "0x38", "0x44", "0x6d", "0x67", "0x4d",
            "0x18", "0x23", "0x37", "0x40", "0x51", "0x68", "0x71", "0x5c",
            "0x31", "0x40", "0x4e", "0x57", "0x67", "0x79", "0x78", "0x65",
            "0x48", "0x5c", "0x5f", "0x62", "0x70", "0x64", "0x67", "0x63"
        };

        string[] defaultQuantizationTableChr = {
            "0x11", "0x12", "0x18", "0x2f", "0x63", "0x63", "0x63", "0x63",
            "0x12", "0x15", "0x1a", "0x42", "0x63", "0x63", "0x63", "0x63",
            "0x18", "0x1a", "0x38", "0x63", "0x63", "0x63", "0x63", "0x63",
            "0x2f", "0x42", "0x63", "0x63", "0x63", "0x63", "0x63", "0x63",
            "0x63", "0x63", "0x63", "0x63", "0x63", "0x63", "0x63", "0x63",
            "0x63", "0x63", "0x63", "0x63", "0x63", "0x63", "0x63", "0x63",
            "0x63", "0x63", "0x63", "0x63", "0x63", "0x63", "0x63", "0x63",
            "0x63", "0x63", "0x63", "0x63", "0x63", "0x63", "0x63", "0x63"
        };

        //FORKERT
        string[] defaultHuffmanTableChr_AC = {
            "0x00", "0x00", "2",
            "0x01", "0x01", "2",
            "0x02", "0x02", "2",
            "0x03", "0x06", "3",
            "0x04", "0x0e", "4",
            "0x05", "0x1e", "5",
            "0x06", "0x3e", "6",
            "0x07", "0x7e", "7",
            "0x08", "0xfe", "8",
            "0x09", "0x1fe", "9",
            "0x0a", "0x3fe", "10",
            "0x0b", "0x7fe", "11"
        };

        string[] defaultHuffmanTableChr_DC = {
            "0x00", "0x00", "2",
            "0x01", "0x01", "2",
            "0x02", "0x02", "2",
            "0x03", "0x06", "3",
            "0x04", "0x0e", "4",
            "0x05", "0x1e", "5",
            "0x06", "0x3e", "6",
            "0x07", "0x7e", "7",
            "0x08", "0xfe", "8",
            "0x09", "0x1fe", "9",
            "0x0a", "0x3fe", "10",
            "0x0b", "0x7fe", "11"
        };

        //FORKERT
        string[] defaultHuffmanTableY_AC =
        {
            "0x00", "0x00", "2",
            "0x01", "0x01", "2",
            "0x02", "0x02", "2",
            "0x03", "0x06", "3",
            "0x04", "0x0e", "4",
            "0x05", "0x1e", "5",
            "0x06", "0x3e", "6",
            "0x07", "0x7e", "7",
            "0x08", "0xfe", "8",
            "0x09", "0x1fe", "9",
            "0x0a", "0x3fe", "10",
            "0x0b", "0x7fe", "11"
        };
        

        string[] defaultHuffmanTableY_DC = {
            "0x00", "0x00", "2",
            "0x01", "0x02", "3",
            "0x02", "0x03", "3",
            "0x03", "0x04", "3",
            "0x04", "0x05", "3",
            "0x05", "0x06", "3",
            "0x06", "0x0e", "4",
            "0x07", "0x1e", "5",
            "0x08", "0x3e", "6",
            "0x09", "0x7e", "7",
            "0x0a", "0xfe", "8",
            "0x0b", "0x1fe", "9"
        };
        #endregion


        public OptionsForm()
        {
            InitializeComponent();
            initializeQuantizationTable(quantizationTableY);
            initializeQuantizationTable(quantizationTableChr);
            initializeHuffmanTable(huffmanTableY_AC);
            initializeHuffmanTable(huffmanTableY_DC);
            initializeHuffmanTable(huffmanTableChr_AC);
            initializeHuffmanTable(huffmanTableChr_DC);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;            

            initializeQuantizationBoxes(pnlQuantizationY, QuantizationBoxesY, defaultQuantizationTableY);
            initializeQuantizationBoxes(pnlQuantizationChr, QuantizationBoxesChr, defaultQuantizationTableChr);

            rdioQuantizationYChannel.Checked = true;
            rdioHuffmanChr_AC.Checked = true;
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
            grpQuantization.Controls.Add(quantizationTableComponent);
            quantizationTableComponent.Location = new Point(4, 30);
            quantizationTableComponent.BringToFront();
        }

        /*
        private void initializeHuffmanBoxes(Panel huffmanPanel, List<TextBox> huffmanBoxes, string[] defaultTable)
        {
            for (int i = 0; i < defaultTable.Length; i++)
            {
                huffmanBoxes.Add(new TextBox());
                huffmanPanel.Controls.Add(huffmanBoxes[i]);
                huffmanBoxes[i].Size = new Size(76, 20);
                huffmanBoxes[i].Left = 8 + (i % 3) * 85;
                huffmanBoxes[i].Top = 5 + (i / 3) * 25;
                huffmanBoxes[i].Text = defaultTable[i];
            }
        }
        */

        //Adds 64 (defaultTable length amount) textboxes to a quantization panel and saves each in an array (to be looped through), sets
        //the size and position of each textbox and writes the default Quantization values in these.
        private void initializeQuantizationBoxes(Panel quantizationPanel, TextBox[] QuantizationBoxes, string[] defaultTable)
        {
            for (int i = 0; i < defaultTable.Length; i++)
            {
                QuantizationBoxes[i] = new TextBox();
                quantizationPanel.Controls.Add(QuantizationBoxes[i]);
                QuantizationBoxes[i].Size = new Size(38, 20);
                QuantizationBoxes[i].Left = 8 + (i % 8) * 47;
                QuantizationBoxes[i].Top = 5 + (i / 8) * 25;
                QuantizationBoxes[i].Text = defaultTable[i];
            }
            
        }

        //The selected Options-panel is enabled and made visible, the opposite is done to the rest.
        private void OptionsBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            deselectAllOptionPanels();
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

        private void deselectAllOptionPanels()
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
                quantizationTableChr.Visible = false;
                quantizationTableChr.Enabled = false;
                quantizationTableY.Visible = true;
                quantizationTableY.Enabled = true;
            }
            else if (rdioQuantizationChrChannel.Checked)
            {
                quantizationTableY.Visible = false;
                quantizationTableY.Enabled = false;
                quantizationTableChr.Visible = true;
                quantizationTableChr.Enabled = true;
            }
        }

        private void Okay_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Asks the user whether they wish to save the currently shown setttings and saves these if 'yes' is clicked.
        //This event is run when OptionsForm is closing.
        private void FormClosing_SaveOptions(object sender, FormClosingEventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you wish to save current options?", "", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                saveQuantization();
                saveHuffmanTables();
            }

        }

        private void saveQuantization()
        {
            quantizationTableY.saveTable();
            quantizationTableChr.saveTable();
        }

        private void saveHuffmanTables()
        {
            stegosaurusForm.huffmanTableY_AC = huffmanTableY_AC.saveTable();
            stegosaurusForm.huffmanTableY_DC = huffmanTableY_DC.saveTable();
            stegosaurusForm.huffmanTableChr_AC = huffmanTableChr_AC.saveTable();
            stegosaurusForm.huffmanTableChr_AC = huffmanTableChr_AC.saveTable();
        }


        private void HuffmannChannelCheckedChanged_DisplayCorrectTable(object sender, EventArgs e)
        {
            deselectHuffmanTables();

            if (rdioHuffmanChr_AC.Checked)
            {
                huffmanTableChr_AC.Visible = true;
                huffmanTableChr_AC.Enabled = true;
            }
            else if (rdioHuffmanChr_DC.Checked)
            {
                huffmanTableChr_DC.Visible = true;
                huffmanTableChr_DC.Enabled = true;
            }
            else if (rdioHuffmanY_AC.Checked)
            {
                huffmanTableY_AC.Visible = true;
                huffmanTableY_AC.Enabled = true;
            }
            else if (rdioHuffmanY_DC.Checked)
            {
                huffmanTableY_DC.Visible = true;
                huffmanTableY_DC.Enabled = true;
            }
        }
        
        private void deselectHuffmanTables()
        {
            huffmanTableChr_AC.Visible = false;
            huffmanTableChr_AC.Enabled = false;

            huffmanTableChr_DC.Visible = false;
            huffmanTableChr_DC.Enabled = false;

            huffmanTableY_AC.Visible = false;
            huffmanTableY_AC.Enabled = false;

            huffmanTableY_DC.Visible = false;
            huffmanTableY_DC.Enabled = false;
        }

        //Adds a row to the selected Huffman-table when btnHuffmanAddRow is clicked
        private void btnHuffmanAddRow_Click(object sender, EventArgs e)
        {
            if (rdioHuffmanChr_AC.Checked)
            {
                huffmanTableChr_AC.addRow();
            }
            else if (rdioHuffmanChr_DC.Checked)
            {
                huffmanTableChr_DC.addRow();
            }
            else if (rdioHuffmanY_AC.Checked)
            {
                huffmanTableY_AC.addRow();
            }
            else if (rdioHuffmanY_DC.Checked)
            {
                huffmanTableY_DC.addRow();
            }
        }
    }
}
