namespace Stegosaurus {
    public class Vertex {
        public short SampleValue1 { get; set; }
        public short SampleValue2 { get; set; }
        public byte Message { get; }
        public byte Modulo { get; }
        public bool HasMessage { get; set; }

        public Vertex(short sampleValue1, short sampleValue2, byte message, byte modulo) {
            SampleValue1 = sampleValue1;
            SampleValue2 = sampleValue2;
            Message = message;
            Modulo = modulo;
            HasMessage = true;
        }

        public Vertex(short sampleValue1, short sampleValue2, byte message, byte modulo, bool hasMessage) {
            SampleValue1 = sampleValue1;
            SampleValue2 = sampleValue2;
            Message = message;
            Modulo = modulo;
            HasMessage = hasMessage;
        }

        public override string ToString() {
            return $"({SampleValue1},{SampleValue2})";
        }
    }
}