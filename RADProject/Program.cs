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
            
            Console.WriteLine("multshift quadratic sum: " + HashTable.calcQuadSum(multShiftTable));
            Console.WriteLine("modPrime  quadratic sum: " + HashTable.calcQuadSum(modPrimeTable));
        }
    }
}