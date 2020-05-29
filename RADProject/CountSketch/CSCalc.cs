using RADProject.HashFunctions;
using RADProject.HashTabel;
using System;

namespace RADProject.CountSketch {
    public class CSCalc {
        private HashTable t;
        private CountSketch[] cs;
        private ulong[] ChiValues;
        private ulong m;
        private int streamSize;
        private int hash_l;
        private int stream_l;

        public CSCalc(int s, int s_l, int h_l){
            Hash h = new ModPrime(hash_l, true);
            m = 1UL << hash_l;
            t = new HashTable(m, h);
            cs = new CountSketch[100];
            ChiValues = new ulong[100];
            streamSize = s;
            hash_l = h_l;
            stream_l = s_l;

        }
        public Tuple<ulong[], ulong> Estimates(){
            for (int i = 0; i < 100; i++){
                FourUniversal g = new FourUniversal(hash_l, true);
                cs[i] = new CountSketch(m, g);
            }

            foreach (var tuple in Stream.CreateStream(streamSize, stream_l, true)){
                t.Increment(tuple.Item1, tuple.Item2);

                foreach (CountSketch c in cs){
                    c.Add(tuple.Item1, tuple.Item2);
                }
            }

            ulong s = t.calcQuadSum();

            for (int i = 0; i < 100; i++){
                ChiValues[i] = cs[i].Chi();
            }

            return new Tuple<ulong[], ulong>(ChiValues, s);
        }
    }
}