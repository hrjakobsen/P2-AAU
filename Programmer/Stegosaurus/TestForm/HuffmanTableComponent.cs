using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Stegosaurus;
using System.Drawing;

namespace TestForm
{
    public partial class HuffmanTableComponent : Panel
    {
        List<TextBox> codeWordsBoxes = new List<TextBox>();
        List<TextBox> runSizeBoxes = new List<TextBox>();

        public HuffmanTableComponent(HuffmanTable huffmanTable)
        {
            HuffmanTable table = huffmanTable;
            var elementList = table.Elements.ToList();
            Size = new Size(410, 244);

            _addTopDescription();

            for (int i = 0; i < table.Elements.Count; i++)
            {
                AddRow();

                string codeWord = Convert.ToString(elementList[i].Value.CodeWord, 2);

                if (codeWord.Length != elementList[i].Value.Length)
                {
                    codeWord = codeWord.PadLeft(elementList[i].Value.Length, '0');
                }

                codeWordsBoxes[i].Text = codeWord;

                runSizeBoxes[i].Text = Convert.ToString(elementList[i].Value.RunSize, 0x10).PadLeft(2,'0');
            }

            InitializeComponent();
        }

        public HuffmanTableComponent(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        private void _addTopDescription()
        {
            Label codeWord = new Label();
            Controls.Add(codeWord);
            codeWord.BringToFront();
            codeWord.Left = 8 + 22;
            codeWord.Top = 5;
            codeWord.Text = "Codeword";
            codeWord.Font = new Font(FontFamily.GenericMonospace.ToString(), 8);

            Label runSize = new Label();
            Controls.Add(runSize);
            runSize.Left = 8 + 22 + 116;
            runSize.Top = 5;
            runSize.Text = "Runsize";
            runSize.Font = new Font(FontFamily.GenericMonospace.ToString(), 8);

            Label line = new Label();
            Controls.Add(line);
            line.Left = 7 + 22;
            line.Top = 22;
            line.Size = new Size(195,2) ;
            line.BorderStyle = BorderStyle.Fixed3D;
            line.BringToFront();
            line.ForeColor = SystemColors.ControlDarkDark;
        }

        //Adds a row (3 textboxes) to a Huffman-table and focuses on the latest added textbox
        public void AddRow()
        {
            int j = codeWordsBoxes.Count();

            //scrolls to the top to ensure correct placement of the textboxes
            if (j != 0)
            {
            codeWordsBoxes[0].Select();
            }
            VerticalScroll.Value = 0;

            _addCodeWordsBox(j);
            _addRunSizeBox(j);
            
            _addNumberIndicator(j);

            //Brings focus to the first box in the added box
            codeWordsBoxes[runSizeBoxes.Count() - 1].Select();
        }

        private void _addCodeWordsBox(int counter)
        {
            codeWordsBoxes.Add(new TextBox());
            Controls.Add(codeWordsBoxes[counter]);
            codeWordsBoxes[counter].Size = new Size(110, 20);
            codeWordsBoxes[counter].Left = 8 + 22;
            codeWordsBoxes[counter].Top = 5 + (counter + 1) * 25;
            codeWordsBoxes[counter].MaxLength = 16;
            codeWordsBoxes[counter].Font = new Font(FontFamily.GenericMonospace.ToString(), 8);
        }

        private void _addRunSizeBox(int counter)
        {
            runSizeBoxes.Add(new TextBox());
            Controls.Add(runSizeBoxes[counter]);
            runSizeBoxes[counter].Size = new Size(76, 20);
            runSizeBoxes[counter].Left = 8 + 22 +116;
            runSizeBoxes[counter].Top = 5 + (counter + 1) * 25;
            runSizeBoxes[counter].MaxLength = 2;
            runSizeBoxes[counter].Font = new Font(FontFamily.GenericMonospace.ToString(), 8);
        }

        private void _addNumberIndicator(int counter)
        {
            if (counter != 0 && counter % 5 == 0)
            {
                Label number = new Label();
                Controls.Add(number);
                number.Size = new Size(25, 25);
                number.Left = 2;
                number.Top = 8 + (counter + 1) * 25;
                number.Font = new Font(FontFamily.GenericMonospace.ToString(), 8);
                number.Text = counter.ToString();
                number.ForeColor = SystemColors.ScrollBar;
            }
        }

        public HuffmanTable SaveTable()
        {
            HuffmanTable h = new HuffmanTable();

            for (int i = 0; i < codeWordsBoxes.Count; i++)
            {
                if (string.IsNullOrWhiteSpace(runSizeBoxes[i].Text) || string.IsNullOrWhiteSpace(codeWordsBoxes[i].Text))
                {
                    continue;
                }

                byte runSize = Convert.ToByte(runSizeBoxes[i].Text, 16);
                ushort codeword = Convert.ToUInt16(codeWordsBoxes[i].Text, 2);
                h.Elements.Add(runSize, new HuffmanElement(runSize, codeword, (byte)codeWordsBoxes[i].Text.Length));
            }
            return h;
        }
    }
}
