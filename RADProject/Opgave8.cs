using System;
using System.Diagnostics;
using RADProject.HashFunctions;

namespace RADProject {
    public class Opgave8 {
        public static void Run() {
            Console.WriteLine(">>>> Opgave 8 - Testing runtime <<<<");
            Console.WriteLine("runtime(Ms),Chi Value");
            for (int i = 3; i < 31; i++) {
                
                var watch = Stopwatch.StartNew();
                FourUniversal fourUniversal = new FourUniversal(i, false);
                ulong tableSize = 1UL << i;
                int n = 1 << 19;
                CountSketch.CountSketch countSketch = new CountSketch.CountSketch(tableSize, fourUniversal);
                foreach (var tuple in Stream.CreateStream(n, i, true)) {
                    countSketch.Add(tuple.Item1, tuple.Item2);
                }
                ulong chi = countSketch.Chi();
                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;
                Console.WriteLine("{2},{0},{1}", elapsedMs, chi, i);
            }
        }
    }
}