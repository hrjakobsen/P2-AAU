using System;
using System.Collections;
using System.Collections.Generic;

namespace Stegosaurus {
    public class BitList : IEnumerable<bool> {
        private BitArray _bits;

        /// <summary>
        /// Returns how many bits are stored in the list
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Creates a new Bitlist with a number of false bits already added
        /// </summary>
        /// <param name="length">Number of bits added</param>
        public BitList(int length) {
            _bits = new BitArray(length);
            Count = length;
        }

        /// <summary>
        /// Creates an empty list
        /// </summary>
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

        public IEnumerator<bool> GetEnumerator() {
            for (int i = 0; i < Count; i++) {
                yield return _bits[i];
            }
        }
       
        /// <summary>
        /// Inserts the bool at the specified index
        /// </summary>
        /// <param name="index">The index where the value will be inserted</param>
        /// <param name="value">The value to be inserted</param>
        public void InsertAt(int index, bool value) {
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

        /// <summary>
        /// Adds a value to the bitlist at the end
        /// </summary>
        /// <param name="val">The value to be added (can only be 0 and 1)</param>
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

        /// <summary>
        /// Add a bool at the end of the list
        /// </summary>
        /// <param name="val"></param>
        public void Add(bool val) {
            if (Count == _bits.Length) {
                _bits.Length *= 2;
            }
            _bits[Count] = val;
            Count++;
        }
        
        private readonly BitArray _latestEntries = new BitArray(8);
        private int _addCounter;

        /// <summary>
        /// Adds a value at the end of the list. If 8 true are added in a row, 8 false will be added
        /// </summary>
        /// <param name="val">The value to be added (0 or 1)</param>
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

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }
}