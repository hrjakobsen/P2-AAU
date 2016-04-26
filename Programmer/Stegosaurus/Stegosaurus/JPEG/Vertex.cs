using System;
using System.Collections.Generic;
using System.Linq;

namespace Stegosaurus {
    public class Vertex {
        public int SampleValue1 { get; set; }
        public int SampleValue2 { get; set; }
        public byte Message { get; set; }
        public bool HasMessage { get; set; }

        public Vertex(int sampleValue1, int sampleValue2, byte message, bool hasMessage = true) {
            SampleValue1 = sampleValue1;
            SampleValue2 = sampleValue2;
            Message = message;
            HasMessage = hasMessage;
        }

        public override string ToString() {
            return $"({SampleValue1},{SampleValue2})";
        }
    }
}