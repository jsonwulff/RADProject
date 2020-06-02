using System;
using System.Diagnostics;

namespace RADProject.HashFunctions {
    public class MultiplyShift : IHashFunction {
        public ulong A;
        public int L;

        public MultiplyShift(int img, bool random) {
            L = img;

            if (random) {
                HashGen();
            } else {
                Byte[] aBytes = {0x53, 0x54, 0x96, 0x72, 0xa8, 0xb1, 0x09, 0xd4};
                A = BitConverter.ToUInt64(aBytes, 0);
            }
        }

        public void HashGen() {
            Random rnd = new Random();
            Byte[] b = new Byte[8];
            rnd.NextBytes(b);
            A = BitConverter.ToUInt64(b, 0) | 1; // "| 1" Ensures odd number
        }

        public ulong Hash(ulong x) {
            return (A * x) >> (64 - L);
        }

        public void TestMultiplyShift(int streamN, int streamL, bool useSeed) {
            Console.WriteLine(">> Testing multiply-shift with n = {0}, l = {1}", streamN, streamL);
            ulong hashSum = 0;
            var watch = Stopwatch.StartNew();
            foreach (var tuple in Stream.CreateStream(streamN, streamL, useSeed)) {
                hashSum += Hash(tuple.Item1);
            }

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine("Took {0}ms to execute with sum {1}", elapsedMs, hashSum);
        }
    }
}