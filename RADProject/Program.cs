using System;
using RADProject.HashFunctions;
using RADProject.HashTabel;
using System.Diagnostics;

namespace RADProject {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine(">>>> OPGAVE 1 <<<<");

            ModPrime multiplyModPrime = new ModPrime(63, false);
            multiplyModPrime.TestMultiplyModPrime(1000000, 63);

            MultiplyShift multiplyShift = new MultiplyShift(63, false);
            multiplyShift.TestMultiplyShift(1000000, 63);


            Hash mP = new ModPrime(8, false);
            Hash mS = new MultiplyShift(8, false);
            HashTable multShiftTable = new HashTable(256, mS);
            HashTable modPrimeTable = new HashTable(256, mP);

            foreach (var tuple in Stream.CreateStream(10000, 50, false)) {
                multShiftTable.Increment(tuple.Item1, tuple.Item2);
                modPrimeTable.Increment(tuple.Item1, tuple.Item2);
            }

            
           //Implementering af Quad sum er blevet flyttet til HashTable.cs
           /* ulong mS_QuadSum = 0UL;
            ulong mP_QuadSum = 0UL;

            for (int i = 0; i < 256; i++) {
                LinkNode mS_cur = multShiftTable.table[i];
                LinkNode mP_cur = modPrimeTable.table[i];

                while (mS_cur != null) {
                    //This method of using Math.Pow may risk having floating point errors.
                    mS_QuadSum = mS_QuadSum + (ulong) (Math.Pow(mS_cur.val, 2));
                    mS_cur = mS_cur.next;
                }

                while (mP_cur != null) {
                    //This method of using Math.Pow may risk having floating point errors.
                    mP_QuadSum = mP_QuadSum + (ulong) (Math.Pow(mP_cur.val, 2));
                    mP_cur = mP_cur.next;
                }
            }*/

            Console.WriteLine("multshift quadratic sum: " + HashTable.calcQuadSum(multShiftTable));
            Console.WriteLine("modPrime  quadratic sum: " + HashTable.calcQuadSum(modPrimeTable));
        }
    }
}