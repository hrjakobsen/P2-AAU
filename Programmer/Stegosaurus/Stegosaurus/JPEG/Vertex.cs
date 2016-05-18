namespace Stegosaurus {
    public class Vertex {
        public short SampleValue1 { get; set; }
        public short SampleValue2 { get; set; }
        public byte Message { get; }
        public byte Modulo { get; }

        /// <summary>
        /// Create a vertex from two DCT coefficients, a message and modulo (m) value.
        /// </summary>
        /// <param name="sampleValue1"></param>
        /// <param name="sampleValue2"></param>
        /// <param name="message">Message must be less than modulo (m)</param>
        /// <param name="modulo">Value that will be used for the addition operation (m value)</param>
        public Vertex(short sampleValue1, short sampleValue2, byte message, byte modulo) {
            SampleValue1 = sampleValue1;
            SampleValue2 = sampleValue2;
            Message = message;
            Modulo = modulo;
        }

        public override string ToString() {
            return $"({SampleValue1},{SampleValue2})";
        }
    }
}