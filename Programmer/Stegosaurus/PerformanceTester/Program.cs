using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stegosaurus;

namespace PerformanceTester {
    class Program {
        static void Main(string[] args) {
            string location = AppDomain.CurrentDomain.BaseDirectory;
            string[] files = Directory.GetFiles(location, "*.png", SearchOption.TopDirectoryOnly);

            foreach (string file in files) {
                //RunTests(file);
                graphLimit(file);
            }
            //graphLimit(files[0]);

            Console.WriteLine("done");
            Console.ReadKey();
        }

        static void RunTests(string pathToImage) {
            Console.Write(@"\begin{table}[]
\centering
\caption{"+ pathToImage.Replace('_','-').Split('\\').Last() + @"}
\label{ my - label}
\begin{tabular}{@{ } lllll@{ } }
\toprule
Encoder & Message Length & Limit & Forces & Time \\ \midrule");
            int[] messageLengths = { 10, 100, 1000 };
            int[] limits = { 10, 100, 1000, int.MaxValue };
            bool[] encoders = { true, false };
            Bitmap image = new Bitmap(pathToImage);
            foreach (bool encoder in encoders) {
                string enc = encoder ? "Old" : "New";
                Console.Write($"\\multirow{{{messageLengths.Length * limits.Length}}}{{*}}{{{enc}}}");
                foreach (int messageLength in messageLengths) {
                    Console.Write($"&\\multirow{{{limits.Length}}}{{*}}{{{messageLength}}} &");
                    byte[] message = Enumerable.Repeat((byte)'A', messageLength).ToArray();
                    foreach (int limit in limits) {
                        string start = "&&";
                        if (limit == limits[0]) start = "";

                        Console.Write($"{start}{limit} & ");
                        JpegImage ji = new JpegImage(image, 100, 4, limit, encoder);
                        ji.Encode(message);
                        int lineStart = 3;

                        if ( limit == limits.Last()) {
                            lineStart = 2;
                            if (messageLength == messageLengths.Last()) {
                                lineStart = 1;
                                if (encoder == encoders.Last()) {
                                    Console.Write("\\\\ \\bottomrule");
                                    continue;
                                }
                            }
                        }

                        Console.Write($"\\\\ \\cmidrule{{{lineStart} - 5}}\n");

                    }
                }
            }
            Console.Write(@"\end{tabular}
\end{table}" + "\n");
        }

        static void graphLimit(string pathToImage) {
            Console.WriteLine(pathToImage.Replace('_', '-').Split('\\').Last());
            Bitmap image = new Bitmap(pathToImage);
            byte[] message = Enumerable.Repeat((byte)'A', 1000).ToArray();
            int[] limits = {int.MaxValue};


            for (int i = 0; i < 1; i+= 10) {
                JpegImage ji = new JpegImage(image, 100, 4, limits[i], true);
                Console.Write(i + "\t");
                ji.Encode(message);
                Console.WriteLine();
                
            }
            Console.WriteLine("\n\n");
            for (int i = 0; i < 1; i += 10) {
                JpegImage ji = new JpegImage(image, 100, 4, limits[i], false);
                Console.Write(i + "\t");
                ji.Encode(message);
                Console.WriteLine();

            }

        }
    }
}
