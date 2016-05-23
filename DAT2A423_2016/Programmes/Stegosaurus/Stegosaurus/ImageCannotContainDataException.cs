using System;

namespace Stegosaurus {
    public class ImageCannotContainDataException : Exception{
        public ImageCannotContainDataException(int messageLength, int maxMessageLength) : base($"Message with length {messageLength} cannot be encoded in the image. The maximum length is {maxMessageLength}"){}
    }
}
