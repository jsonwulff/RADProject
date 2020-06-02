using System;
using System.Numerics;
using System.Diagnostics;

namespace RADProject.HashFunctions {
    public class ModPrime : IHashFunction {
        public BigInteger A;
        public BigInteger B;
        public BigInteger P;
        public int L;

        public ModPrime(int img, bool random) {
            P = BigInteger.Pow(2, 89) - 1;
            L = img;

            if (random) {
                HashGen();
            } else {
                Byte[] aBytes =
                    {0x32, 0x4c, 0x8e, 0xd8, 0x08, 0x8d, 0xb1, 0x13, 0xca, 0x4a, 0x22, 0x00};
                Byte[] bBytes =
                    {0x0d, 0xb5, 0x64, 0x33, 0xce, 0xf5, 0x7c, 0xdd, 0x63, 0x1a, 0xc8, 0x00};
                A = new BigInteger(aBytes);
                B = new BigInteger(bBytes);
            }
        }

        private void HashGen() {
            Random rnd = new Random();
            Byte[] aBytes = new Byte[12];
            Byte[] bBytes = new Byte[12];
            rnd.NextBytes(aBytes);
            rnd.NextBytes(bBytes);
            aBytes[11] = 0x00;
            bBytes[11] = 0x00;
            A = new BigInteger(aBytes);
            B = new BigInteger(bBytes);
        }

        public ulong Hash(ulong x) {
            BigInteger z = (A * x + B);
            BigInteger y = (z & P) + (z >> 89);
            if (y >= P) {
                y -= P;
            }

            return (ulong) (y & ((1UL << L) - 1));
        }

        public void TestMultiplyModPrime(int n, int l, bool useSeed) {
            Console.WriteLine(">> Testing multiply-mod-prime with n = {0}, l = {1}", n, l);
            ulong hashSum = 0;
            var watch = Stopwatch.StartNew();
            foreach (var tuple in Stream.CreateStream(n, l, useSeed)) {
                hashSum += Hash(tuple.Item1);
            }

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine("Took {0}ms to execute with sum {1}", elapsedMs, hashSum);
        }
    }
}