using RADProject.HashFunctions;
using RADProject.Table;
using System;

namespace RADProject.CountSketch {
    public class CSCalc {
        private HashTable hashTable;
        private CountSketch[] countSketch;
        private ulong[] chiValues;
        private ulong m;
        private int streamSize;
        private int hashL;
        private int streamL;

        public CSCalc(int s, int sL, int hL) {
            ModPrime h = new ModPrime(hL, true);
            m = 1UL << hL;
            hashTable = new HashTable(m, h);
            countSketch = new CountSketch[100];
            chiValues = new ulong[100];
            streamSize = s;
            hashL = hL;
            streamL = sL;
        }

        public Tuple<ulong[], ulong> Estimates() {
            for (int i = 0; i < 100; i++) {
                FourUniversal g = new FourUniversal(hashL, true);
                countSketch[i] = new CountSketch(m, g);
            }

            foreach (var tuple in Stream.CreateStream(streamSize, streamL, true)) {
                hashTable.Increment(tuple.Item1, tuple.Item2);

                foreach (CountSketch c in countSketch) {
                    c.Add(tuple.Item1, tuple.Item2);
                }
            }

            ulong s = hashTable.CalcQuadSum();

            for (int i = 0; i < 100; i++) {
                chiValues[i] = countSketch[i].Chi();
            }

            return new Tuple<ulong[], ulong>(chiValues, s);
        }
    }
}