using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stegosaurus {
    public static class ExtensionMethods {
        public static int Mod(this int dividend, int divisor) {
            return (dividend % divisor + divisor) % divisor;
        }
    }
}
