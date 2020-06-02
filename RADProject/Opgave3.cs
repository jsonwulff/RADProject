using System;
using System.Diagnostics;
using System.IO;
using RADProject.HashFunctions;
using RADProject.HashTabel;

namespace RADProject {
    public class Opgave3 {
        public static void Run() {
            Console.WriteLine(">>>> OPGAVE 3 <<<<");

            string projectDir = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            string resultsDir = Path.Combine(projectDir, "Results");
            string resultFile = Path.Combine(resultsDir, "3MultiplyModPrime.csv");

            string Headers = "l,runtime(Ms),quadratic sum\n";
            File.WriteAllText(resultFile, Headers);

            for (int i = 3; i < 64; i++) {
                var watch = Stopwatch.StartNew();
                Console.WriteLine("Running multiply-mod-prime with l = {0}", i);
                Hash multiplyModPrime = new ModPrime(i, false);
                ulong tableSize = 1UL << i;
                int n = 1 << 19;
                HashTable hashTable = new HashTable(tableSize, multiplyModPrime);
                foreach (var tuple in Stream.CreateStream(n, i, true)) {
                    hashTable.Increment(tuple.Item1, tuple.Item2);
                }

                ulong quadricSum = hashTable.calcQuadSum();
                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;
                Console.WriteLine("Quadratic sum: " + quadricSum);
                string result = string.Format("{0},{1},{2}\n", i, elapsedMs, quadricSum);
                File.AppendAllText(resultFile, result);
            }
        }
    }
}