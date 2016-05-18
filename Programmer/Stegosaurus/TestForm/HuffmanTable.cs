using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Stegosaurus;
using System.Drawing;

namespace TestForm
{
    public partial class HuffmanTableComponent : Panel
    {
        List<TextBox> codeWordsBoxes = new List<TextBox>();
        List<TextBox> runSizeBoxes = new List<TextBox>();

        public HuffmanTable Table { get; set; }

        public HuffmanTableComponent(HuffmanTable huffmanTable)
        {
            Table = huffmanTable;
            var elementList = Table.Elements.ToList();
            Size = new Size(410, 244);

            for (int i = 0; i < Table.Elements.Count; i++)
            {
                addCodeWordsBox(i);

                string s = Convert.ToString(elementList[i].Value.CodeWord, 2);

                if (s.Length != elementList[i].Value.Length)
                {
                    s = s.PadLeft(elementList[i].Value.Length, '0');
                }

                codeWordsBoxes[i].Text = s;

                addRunSizeBox(i);
                runSizeBoxes[i].Text = Convert.ToString(elementList[i].Value.RunSize, 0x10).PadLeft(2,'0');
            }

            InitializeComponent();
        }

        public HuffmanTableComponent(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        private void addCodeWordsBox(int counter)
        {
            codeWordsBoxes.Add(new TextBox());
            Controls.Add(codeWordsBoxes[counter]);
            codeWordsBoxes[counter].Size = new Size(110, 20);
            codeWordsBoxes[counter].Left = 8;
            codeWordsBoxes[counter].Top = 5 + counter * 25;
            codeWordsBoxes[counter].Font = new Font(FontFamily.GenericMonospace.ToString(), 8);
        }

        private void addRunSizeBox(int counter)
        {
            runSizeBoxes.Add(new TextBox());
            Controls.Add(runSizeBoxes[counter]);
            runSizeBoxes[counter].Size = new Size(76, 20);
            runSizeBoxes[counter].Left = 8 + 116;
            runSizeBoxes[counter].Top = 5 + counter * 25;
            codeWordsBoxes[counter].Font = new Font(FontFamily.GenericMonospace.ToString(), 8);
        }

        //Adds a row (3 textboxes) to a Huffman-table and focuses on the latest added textbox
        public void AddRow()
        {
            int j = codeWordsBoxes.Count();

            //scrolls to the top to ensure correct placement of the textboxes
            codeWordsBoxes[0].Select();
            VerticalScroll.Value = 0;

            codeWordsBoxes.Add(new TextBox());
            Controls.Add(codeWordsBoxes[j]);
            codeWordsBoxes[j].Size = new Size(110, 20);
            codeWordsBoxes[j].Left = 8;
            codeWordsBoxes[j].Top = 5 + j * 25;
            codeWordsBoxes[j].Font = new Font(FontFamily.GenericMonospace.ToString(), 8);

            runSizeBoxes.Add(new TextBox());
            Controls.Add(runSizeBoxes[j]);
            runSizeBoxes[j].Size = new Size(76, 20);
            runSizeBoxes[j].Left = 8 + 116;
            runSizeBoxes[j].Top = 5 + j * 25;
            runSizeBoxes[j].Font = new Font(FontFamily.GenericMonospace.ToString(), 8);

            //Brings focus to the first box in the added box
            codeWordsBoxes[runSizeBoxes.Count() - 1].Select();
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
