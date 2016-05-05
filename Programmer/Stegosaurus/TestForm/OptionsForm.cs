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

        #region
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

        string[] defaultHuffmanTableChr_AC = {

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
        /*
        string[] defaultHuffmanTableY_AC =
        {
            
        }
        */

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
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            tbarQualitySlider.Value = stegosaurusForm.tbarQualityValue;
            initializeQuantizationBoxesY();
            initializeQuantizationBoxesChr();
            initializeHuffmanBoxesChr_AC();
            initializeHuffmanBoxesChr_DC();
            initializeHuffmanBoxesY_AC();
            initializeHuffmanBoxesY_DC();
            rdioQuantizationYChannel.Checked = true;
            rdioHuffmanChr_AC.Checked = true;
        }

        private void initializeHuffmanBoxesChr_AC()
        {
            int amount = 63;
            createHuffmanBoxes(pnlHuffmanChr_AC, huffmanBoxesChr_AC, amount);
            for (int i = 0; i < amount; i++)
            {
                huffmanBoxesChr_AC[i].Text = (i + 1).ToString() + "C";
            }
        }

        private void initializeHuffmanBoxesChr_DC()
        {
            int amount = defaultHuffmanTableChr_DC.Length;
            createHuffmanBoxes(pnlHuffmanChr_DC, huffmanBoxesChr_DC, amount);
            for (int i = 0; i < amount; i++)
            {
                huffmanBoxesChr_DC[i].Text = defaultHuffmanTableChr_DC[i];
            }
        }

        private void initializeHuffmanBoxesY_AC()
        {
            int amount = 63;
            createHuffmanBoxes(pnlHuffmanY_AC, huffmanBoxesY_AC, amount);
            for (int i = 0; i < amount; i++)
            {
                huffmanBoxesY_AC[i].Text = (i + 1).ToString() + "C";
            }
        }

        private void initializeHuffmanBoxesY_DC()
        {
            int amount = defaultHuffmanTableY_DC.Length;
            createHuffmanBoxes(pnlHuffmanY_DC, huffmanBoxesY_DC, amount);
            for (int i = 0; i < amount; i++)
            {
                huffmanBoxesY_DC[i].Text = defaultHuffmanTableY_DC[i];
            }
        }

        private void createHuffmanBoxes(Panel huffmanPanel, List<TextBox> huffmanBoxes, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                huffmanBoxes.Add(new TextBox());
                huffmanPanel.Controls.Add(huffmanBoxes[i]);
                huffmanBoxes[i].Size = new Size(76, 20);
                huffmanBoxes[i].Left = 8 + (i % 3) * 85;
                huffmanBoxes[i].Top = 5 + (i / 3) * 25;
            }
        }

        private void initializeQuantizationBoxesY()
        {
            createQuantizationBoxes(pnlQuantizationY, QuantizationBoxesY);

            for (int i = 0; i < 64; i++)
            {
                QuantizationBoxesY[i].Text = defaultQuantizationTableY[i];
            }
        }

        private void initializeQuantizationBoxesChr()
        {
            createQuantizationBoxes(pnlQuantizationChr, QuantizationBoxesChr);
            for (int i = 0; i < 64; i++)
            {
                QuantizationBoxesChr[i].Text = defaultQuantizationTableChr[i];
            }
        }

        private void createQuantizationBoxes(Panel quantizationPanel, TextBox[] QuantizationBoxes)
        {
            for (int i = 0; i < 64; i++)
            {
                QuantizationBoxes[i] = new TextBox();
                quantizationPanel.Controls.Add(QuantizationBoxes[i]);
                QuantizationBoxes[i].Size = new Size(38, 20);
                QuantizationBoxes[i].Left = 8 + (i % 8) * 47;
                QuantizationBoxes[i].Top = 5 + (i / 8) * 25;
            }
            
        }

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

        /*
        private void saveQuantization(TextBox[] quantizationBoxes)
        {
            foreach (TextBox tb in quantizationBoxes)
            {
                foreach (char character in tb.Text)
                {
                    character
                }
                quantizationBoxes[tb].Text;
            }
        }
        */
        private void OptionsForm_Load(object sender, EventArgs e)
        {

        }

        private void tbarQualitySlider2_ValueChanged(object sender, EventArgs e)
        {
            stegosaurusForm.tbarQualityValue = tbarQualitySlider.Value;
        }

        private void yQuantizationChannelChecked_DisplayYOrChrTable(object sender, EventArgs e)
        {
            if (rdioQuantizationYChannel.Checked)
            {
                pnlQuantizationChr.Visible = false;
                pnlQuantizationChr.Enabled = false;
                pnlQuantizationY.Visible = true;
                pnlQuantizationY.Enabled = true;
            }
            else if (rdioQuantizationChrChannel.Checked)
            {
                pnlQuantizationY.Visible = false;
                pnlQuantizationY.Enabled = false;
                pnlQuantizationChr.Visible = true;
                pnlQuantizationChr.Enabled = true;
            }
        }

        private void Okay_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormClosing_SaveQuantization(object sender, FormClosingEventArgs e)
        {
            //saveQuantization();

        }

        private void HuffmannChannelCheckedChanged_DisplayCorrectTable(object sender, EventArgs e)
        {
            deselectHuffmanTables();

            if (rdioHuffmanChr_AC.Checked)
            {
                pnlHuffmanChr_AC.Visible = true;
                pnlHuffmanChr_AC.Enabled = true;
            }
            else if (rdioHuffmanChr_DC.Checked)
            {
                pnlHuffmanChr_DC.Visible = true;
                pnlHuffmanChr_DC.Enabled = true;
            }
            else if (rdioHuffmanY_AC.Checked)
            {
                pnlHuffmanY_AC.Visible = true;
                pnlHuffmanY_AC.Enabled = true;
            }
            else if (rdioHuffmanY_DC.Checked)
            {
                pnlHuffmanY_DC.Visible = true;
                pnlHuffmanY_DC.Enabled = true;
            }
        }

        RadioButton GetCheckedRadio(Control container)
        {
            foreach (var control in container.Controls)
            {
                RadioButton radio = control as RadioButton;

                if (radio != null && radio.Checked)
                {
                    return radio;
                }
            }

            return null;
        }

        private void deselectHuffmanTables()
        {
            pnlHuffmanChr_AC.Visible = false;
            pnlHuffmanChr_AC.Enabled = false;

            pnlHuffmanChr_DC.Visible = false;
            pnlHuffmanChr_DC.Enabled = false;

            pnlHuffmanY_AC.Visible = false;
            pnlHuffmanY_AC.Enabled = false;

            pnlHuffmanY_DC.Visible = false;
            pnlHuffmanY_DC.Enabled = false;
        }

        private void btnHuffmanAddRow_Click(object sender, EventArgs e)
        {
            RadioButton checkedRadio = GetCheckedRadio(grpCustormHuffman);
            if (checkedRadio == rdioHuffmanChr_AC)
            {
                addRowToHuffmanTable(pnlHuffmanChr_AC, huffmanBoxesChr_AC);
            }
            else if (checkedRadio == rdioHuffmanChr_DC)
            {
                addRowToHuffmanTable(pnlHuffmanChr_DC, huffmanBoxesChr_DC);
            }
            else if (checkedRadio == rdioHuffmanY_AC)
            {
                addRowToHuffmanTable(pnlHuffmanY_AC, huffmanBoxesY_AC);
            }
            else if (checkedRadio == rdioHuffmanY_DC)
            {
                addRowToHuffmanTable(pnlHuffmanY_DC, huffmanBoxesY_DC);
            }
        }

        private void addRowToHuffmanTable(Panel huffmanPanel, List<TextBox> huffmanBoxes)
        {
            int numOfHuffmanBoxes = huffmanBoxes.Count();
            int scrollPos = huffmanPanel.VerticalScroll.Value;
            huffmanBoxes[0].Select();
            huffmanPanel.VerticalScroll.Value = 0;

            for (int i = 0; i < 3; i++)
            {
                int j = i + numOfHuffmanBoxes;
                huffmanBoxes.Add(new TextBox());
                huffmanPanel.Controls.Add(huffmanBoxes[j]);
                huffmanBoxes[j].Size = new Size(76, 20);
                huffmanBoxes[j].Left = 8 + (j % 3) * 85;
                huffmanBoxes[j].Top = 5 + (j / 3) * 25;
                huffmanBoxes[j].Text = "0x";
            }
            huffmanBoxes[huffmanBoxes.Count() - 1].Select();
        }
    }
}
