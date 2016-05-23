using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stegosaurus {
    public static class ExtensionMethods {
        /// <summary>
        /// Computes mathematical MOD instead of the builtin mod operator.
        /// </summary>
        /// <param name="dividend"></param>
        /// <param name="divisor"></param>
        /// <returns>dividend MOD divisor</returns>
        public static int Mod(this int dividend, int divisor) {
            return (dividend % divisor + divisor) % divisor;
        }
    }
}
