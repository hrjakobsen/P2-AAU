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
    public partial class QuantizationTableComponent : Panel
    {
        TextBox[] QuantizationBoxes = new TextBox[64];

        public QuantizationTable Table { get; set; }

        public QuantizationTableComponent(QuantizationTable quantizationTable)
        {
            Table = quantizationTable;
            var entriesList = Table.Entries.ToList();
            this.Size = new Size(410, 244);

            for (int i = 0; i < Table.Entries.Count(); i++)
            {
                QuantizationBoxes[i] = new TextBox();
                Controls.Add(QuantizationBoxes[i]);
                QuantizationBoxes[i].Size = new Size(38, 20);
                QuantizationBoxes[i].Left = 8 + (i % 8) * 47;
                QuantizationBoxes[i].Top = 5 + (i / 8) * 25;
                QuantizationBoxes[i].Font = new Font(FontFamily.GenericMonospace.ToString(), 8);

                string s = Convert.ToString(entriesList[i], 0x10);

                if (s.Length != 2)
                {
                    s = s.PadLeft(2, '0');
                }

                QuantizationBoxes[i].Text = s;
            }

        }

        public QuantizationTableComponent(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
        
        public QuantizationTable saveTable()
        {
            byte[] entries = QuantizationBoxes.Select(x => Convert.ToByte(x.Text, 16)).ToArray();
            QuantizationTable Q = new QuantizationTable(entries);

            return Q;
        }
    }
}
