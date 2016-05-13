using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestForm
{
    class OptionsSaver
    {
        public OptionsSaver()
        {
                
        }

        public void SaveToFile(string filePath)
        {
            using (StreamWriter sw = File.CreateText(filePath))
            {
                /*
                sw.WriteLine(transaction.ToString());
                sw.WriteLine();
                */
            }
        }
    }
}
