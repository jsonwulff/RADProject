using System;
using System.Numerics;
using System.Diagnostics;

namespace RADProject {
    public class ModPrime : Hash {
        public BigInteger a ;
        public BigInteger b;
        public BigInteger p ;
        public int l;

        public ModPrime(int img, bool random) : base(img, random){
            p = BigInteger.Pow(2, 89) - 1;
            l = img;
            
            if (random){
                hashgen();
            }else{
                Byte[] a_bytes = {0x32, 0x4c, 0x8e, 0xd8, 0x08, 0x8d, 0xb1, 0x13, 0xca, 0x4a, 0x22, 0x00};
                Byte[] b_bytes = {0x0d, 0xb5, 0x64, 0x33, 0xce, 0xf5, 0x7c, 0xdd, 0x63, 0x1a, 0xc8, 0x00};
                a = new BigInteger(a_bytes);
                b = new BigInteger(b_bytes);
            }
        }

        public void hashgen() {
            Random rnd = new Random();
            Byte[] a_bytes = new Byte[12];
            Byte[] b_bytes = new Byte[12];
            rnd.NextBytes(a_bytes);
            rnd.NextBytes(b_bytes);
            a_bytes[11] = 0x00;
            b_bytes[11] = 0x00;
            a = new BigInteger(a_bytes);
            b = new BigInteger(b_bytes);
            
        }

        public override ulong hash(ulong x) {
            BigInteger z = (a*x+b);
            BigInteger y = (z&p)+(z>>89);
            if (y>=p) {y-=p;}

            return (ulong)(y & ((1UL<<l)-1));
        }

        public void TestMultiplyModPrime(int n, int l) {
            Console.WriteLine(">> Testing multiply-mod-prime with n = {0}, l = {1}", n, l);
            ulong hashSum = 0;
            var watch = Stopwatch.StartNew();
            foreach (var tuple in Stream.CreateStream(n, l)) {
                hashSum += hash(tuple.Item1);
            }
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine("Took {0}ms to execute with sum {1}", elapsedMs, hashSum);
        }
    }
}