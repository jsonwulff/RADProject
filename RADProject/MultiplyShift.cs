using System;
using System.Numerics;

namespace RADProject {
    public class MultiplyShift : Hash<ulong> {
        public ulong a;
        public int l;
        public BigInteger p;
        
        public MultiplyShift(int img, bool random){
            p = BigInteger.Pow(2, 89) - 1;
            l = img;
            
            if (random){
                hashgen();
            }else{
                Byte[] a_bytes = {0x53, 0x54, 0x96, 0x72, 0xa8, 0xb1, 0x09, 0xd4};
                a = BitConverter.ToUInt64(a_bytes,0);
            }
        }
        
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

        public override ulong hash(ulong x) {
            return (a * x)>>(64-l);
        }
    }
}