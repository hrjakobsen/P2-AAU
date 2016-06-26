GraphEncoder ge = 
    new GraphEncoder(_nonZeroValues.Select(x => (int) x).ToArray(), M);

short[] newNonZeroes = 
    ge.Encode(_message.ToArray()).Select(x => (short) x).ToArray();