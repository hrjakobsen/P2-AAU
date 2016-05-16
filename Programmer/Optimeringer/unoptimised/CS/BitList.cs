using System;
using System.Collections;

namespace Stegosaurus {
    public class BitList:IEnumerable {
        private BitArray _bits;

        public int Count { get; private set; }

        public BitList(int length) {
            _bits = new BitArray(length);
            Count = length;
        }

        public BitList() {
            _bits = new BitArray(1);
            Count = 0;
        }

        public bool this[int i] {
            get {
                return _bits[i];
            }
            set {
                _bits[i] = value;
            }
        }

        public IEnumerator GetEnumerator() {
            for (int i = 0; i < Count; i++) {
                yield return _bits[i];
            }
        }

        public void Insert(int index, bool value) {
            BitArray newArr = new BitArray(Count + 1);
            for (int i = 0; i < index; i++) {
                newArr[i] = this[i];
            }
            newArr[index] = value;
            for (int i = index; i < Count; i++) {
                newArr[i + 1] = this[i];
            }

            Count++;
            _bits = newArr;
        }

        public void Add(int val) {
            switch (val) {
                case 1:
                    Add(true);
                    break;
                case 0:
                    Add(false);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Add(bool val) {
            if (Count == _bits.Length) {
                _bits.Length *= 2;
            }
            _bits[Count] = val;
            Count++;
        }

        public BitList SubList(int startindex, int endindex) {
            int length = endindex - startindex;
            if (length <= 0) {
                throw new IndexOutOfRangeException();
            }
            BitList b = new BitList(length);
            for (int i = startindex; i < endindex + 1; i++) {
                b.Add(this[i]);
            }
            return b;
        }

        private readonly BitArray _latestEntries = new BitArray(8);
        private int _addCounter;

        public void CheckedAdd(int val) {
            if (_addCounter % 8 == 0) {
                _latestEntries.SetAll(false);
            }
            _latestEntries[_addCounter % 8] = (val == 1);
            Add(val == 1);
            bool allOne = true;

            for (int i = 0; i < 8; i++) {
                if (!_latestEntries[i]) {
                    allOne = false;
                    break;
                }
            }

            if (allOne) {
                for (int i = 0; i < 8; i++) {
                    Add(false);
                }
            }

            _addCounter++;
        }
    }
}