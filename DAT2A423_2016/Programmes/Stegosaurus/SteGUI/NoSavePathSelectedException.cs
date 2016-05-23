using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteGUI
{
    class NoSavePathSelectedException: Exception
    {
        public NoSavePathSelectedException() : base($"No save location for the encoded image/decoded message was selected!"){}
    }
}
