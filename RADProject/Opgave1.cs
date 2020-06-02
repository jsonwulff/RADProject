using System;
using RADProject.HashFunctions;

namespace RADProject {
    public class Opgave1 {

        public static void Run() {
            Console.WriteLine(">>>> OPGAVE 1 <<<<");

            ModPrime multiplyModPrime = new ModPrime(63, false);
            multiplyModPrime.TestMultiplyModPrime(10000000, 63, true);

            MultiplyShift multiplyShift = new MultiplyShift(63, false);
            multiplyShift.TestMultiplyShift(10000000, 63, true);
        }
    }
}