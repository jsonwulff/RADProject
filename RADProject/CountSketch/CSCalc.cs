using RADProject.HashFunctions;
using RADProject.HashTabel;

namespace RADProject.CountSketch {
    public class CSCalc {
        private HashTabel t;
        private CountSketch[] cs;
        private ulong[] ChiValues;
        private ulong m;

        public ulong[] CSCalc(int streamSize, int stream_l, int hash_l){
            Hash h = new ModPrime(hash_l, true);
            t = new HashTable(m, h);
            m = 1UL << hash_l;
            cs = new CountSketch[100];
            ChiValues = new ulong[100];

            for (int i = 0; i < 100; i++){
                FourUniversal g = new FourUniversal(hash_l, true);
                cs[i] = new CountSketch(m, g);
            }

            foreach (var tuple in Stream.CreateStream(streamSize, streamSize)){
                t.Increment(tuple.Item1, tuple.Item2);

                foreach (CountSketch c in cs){
                    c.Add(tuple.Item1, tuple.Item2);
                }
            }

            // calculate S as in (Program lines 31-46)
            for (int i = 0; i < 100; i++){
                ChiValues[i] = cs[i].Chi;
            }

            return ChiValues;
        }
    }
}