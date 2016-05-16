using System;
using System.Collections;

namespace Stegosaurus {
    public class BitList : IEnumerable {
        private readonly BitArray _bits;

        public int Count { get; private set; }

        public BitList() {
            _bits = new BitArray(1);
            Count = 0;
        }

        public bool this[int i] => _bits[i];

        public IEnumerator GetEnumerator() {
            for (int i = 0; i < Count; i++) {
                yield return _bits[i];
            }
        }

        private readonly BitArray _latestEntries = new BitArray(8);
        private int _addCounter;

        public void CheckedAdd(int val) {
            if (_addCounter % 8 == 0) {
                _latestEntries.SetAll(false);
            }
            _latestEntries[_addCounter % 8] = (val == 1);
            Add(val == 1);
            bool allOne = false;

            if (_addCounter % 8 == 7) {
                allOne = true;
                for (int i = 0; i < 8; i++) {
                    if (!_latestEntries[i]) {
                        allOne = false;
                        break;
                    }
                }
            }

            if (allOne) {
                for (int i = 0; i < 8; i++) {
                    Add(false);
                }
            }

            _addCounter++;
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
    }
}