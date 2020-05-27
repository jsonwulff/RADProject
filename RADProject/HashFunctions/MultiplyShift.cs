using System;
using System.Diagnostics;

namespace RADProject.HashFunctions {
    public class MultiplyShift : Hash {
        public ulong a;
        public int l;

        public MultiplyShift(int img, bool random) : base(img, random) {
            l = img;

            if (random) {
                hashgen();
            } else {
                Byte[] a_bytes = {0x53, 0x54, 0x96, 0x72, 0xa8, 0xb1, 0x09, 0xd4};
                a = BitConverter.ToUInt64(a_bytes, 0);
            }
        }

        private void hashgen() {
            Random rnd = new System.Random();
            Byte[] b = new Byte[8];
            rnd.NextBytes(b);
            a = BitConverter.ToUInt64(b, 0) | 1; // "| 1" Ensures odd number
        }

        public ulong hash(ulong x) {
            return (a * x) >> (64 - l);
        }

        public void TestMultiplyShift(int n, int l) {
            Console.WriteLine(">> Testing multiply-shift with n = {0}, l = {1}", n, l);
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