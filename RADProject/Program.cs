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
            
            Hash mP = new ModPrime(8, false);
            Hash mS = new MultiplyShift(8, false);
            HashTable multShiftTable = new HashTable(256, mS);
            HashTable modPrimeTable = new HashTable(256, mP);

            foreach (var tuple in Stream.CreateStream(10000, 50)){
                multShiftTable.increment(tuple.Item1, tuple.Item2);
                modPrimeTable.increment(tuple.Item1, tuple.Item2);
            }

            ulong mS_QuadSum = 0UL;
            ulong mP_QuadSum = 0UL;

            for (int i = 0; i < 256; i++){
                LinkNode mS_cur = multShiftTable.table[i];
                LinkNode mP_cur = modPrimeTable.table[i];

                while (mS_cur != null){
                    //This method of using Math.Pow may risk having floating point errors.
                    mS_QuadSum = mS_QuadSum +  (ulong)(Math.Pow(mS_cur.val,2));
                    mS_cur = mS_cur.next;
                }
                while (mP_cur != null){
                    //This method of using Math.Pow may risk having floating point errors.
                    mP_QuadSum = mP_QuadSum + (ulong)(Math.Pow(mP_cur.val,2));
                    mP_cur = mP_cur.next;
                }
            }
            
            Console.WriteLine("multshift quadratic sum: " + mS_QuadSum);
            Console.WriteLine("modPrime  quadratic sum: " + mP_QuadSum);
        }
    }
}