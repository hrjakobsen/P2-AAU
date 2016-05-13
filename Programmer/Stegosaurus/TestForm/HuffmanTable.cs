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
                codeWordsBoxes.Add(new TextBox());
                Controls.Add(codeWordsBoxes[i]);
                codeWordsBoxes[i].Size = new Size(110, 20);
                codeWordsBoxes[i].Left = 8;
                codeWordsBoxes[i].Top = 5 + i * 25;
                codeWordsBoxes[i].Font = new Font(FontFamily.GenericMonospace.ToString(), 8);

                string s = Convert.ToString(elementList[i].Value.CodeWord, 2);

                if (s.Length != elementList[i].Value.Length)
                {
                    s = s.PadLeft(elementList[i].Value.Length, '0');
                }

                codeWordsBoxes[i].Text = s;

                


                runSizeBoxes.Add(new TextBox());
                Controls.Add(runSizeBoxes[i]);
                runSizeBoxes[i].Size = new Size(76, 20);
                runSizeBoxes[i].Left = 8+116;
                runSizeBoxes[i].Top = 5 + i * 25;
                codeWordsBoxes[i].Font = new Font(FontFamily.GenericMonospace.ToString(), 8);

                runSizeBoxes[i].Text = Convert.ToString(elementList[i].Value.RunSize, 0x10);
            }

            InitializeComponent();
        }

        public HuffmanTableComponent(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
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

            runSizeBoxes.Add(new TextBox());
            Controls.Add(runSizeBoxes[j]);
            runSizeBoxes[j].Size = new Size(76, 20);
            runSizeBoxes[j].Left = 8 + 116;
            runSizeBoxes[j].Top = 5 + j * 25;
            codeWordsBoxes[j].Font = new Font(FontFamily.GenericMonospace.ToString(), 8);
            codeWordsBoxes[j].Font = new Font(FontFamily.GenericMonospace.ToString(), 8);

            //Brings focus to the first box in the added box
            codeWordsBoxes[runSizeBoxes.Count() - 1].Select();
        }

        public HuffmanTable SaveTable()
        {
            HuffmanTable h = new HuffmanTable();

            for (int i = 0; i < codeWordsBoxes.Count; i++)
            {
                byte runSize = Convert.ToByte(runSizeBoxes[i].Text, 16);
                h.Elements.Add(runSize, new HuffmanElement(runSize, Convert.ToUInt16(codeWordsBoxes[i].Text, 2), (byte) codeWordsBoxes[i].Text.Length));
            }

            return h;
        }
        /*
        public override string ToString()
        {
            //return base.ToString();
            string HuffmanTable
            for (int i = 0; i < codeWordsBoxes.Count; i++)
            {
                
            }
        }*/
    }
}
