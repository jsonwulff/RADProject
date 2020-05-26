using System;
using System.Numerics;

namespace RADProject {
    public class ModPrime {
        public BigInteger a ;
        public BigInteger b;
        public BigInteger p ;
        public int l;

        public void hashgen() {
            p = BigInteger.Pow(2, 89) - 1;
            l = 10;
            Random rnd = new Random();
            Byte[] a_bytes = new Byte[11];
            Byte[] b_bytes = new Byte[11];
            rnd.NextBytes(a_bytes);
            rnd.NextBytes(b_bytes);
            a = new BigInteger(a_bytes);
            b = new BigInteger(b_bytes);
        }

        public BigInteger modHash(ulong x) {
            
            BigInteger z = (a*x+b);
            BigInteger y = (z&p)+(z>>89);
            if (y>=p) {y-=p;}

            return y & ((1<<l)-1);
        }
    }
}