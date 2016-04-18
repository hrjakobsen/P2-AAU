﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stegosaurus {
    public class BitList : IEnumerable{
        private BitArray _bits;

        public int Count { get; private set; }

        public BitList(int length) {
            _bits = new BitArray(length);
            Count = length;
        }

        public BitList() {
            _bits = new BitArray(1);
            Count = 1;
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
            if (val == 1) {
                Add(true);
            } else if (val == 0) {
                Add(false);
            } else {
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
