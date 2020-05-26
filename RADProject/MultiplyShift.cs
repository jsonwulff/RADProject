using System;
using System.Numerics;

namespace RADProject {
    public class MultiplyShift {
        public ulong a;
        public int l;
        public BigInteger p;
        
        public void hashgen() {
            Random rnd = new System.Random();
            ulong z = 0UL;
            Byte[] b = new Byte[8];
            rnd.NextBytes(b);
            for (int i = 0; i < 8; ++i) {
                z = (z << 8) + (ulong) b[i];
            }
            a = z;
            l = 10;
        }

        public ulong shiftHash(ulong x) {
            return (a * x)>>(64-l);
        }
    }
}