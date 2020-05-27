using System;
using System.Diagnostics;

namespace RADProject
{
    class Program
    {
        static void Main(string[] args)
        {
            ModPrime modPrime = new ModPrime(12, false);
            MultiplyShift mulShift = new MultiplyShift(12, false);
            mulShift.hashgen();
            var watch = System.Diagnostics.Stopwatch.StartNew();
            foreach (var tuple in Stream.CreateStream(10000, 50)) {
                modPrime.hash(tuple.Item1);
            }
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine(elapsedMs);
            
            watch = Stopwatch.StartNew();
            foreach (var tuple in Stream.CreateStream(10000, 50)) {
                mulShift.hash(tuple.Item1);
            }
            watch.Stop();
            elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine(elapsedMs);
            
            
        }
    }
}