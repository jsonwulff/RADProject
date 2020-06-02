using System;
using RADProject.HashFunctions;

namespace RADProject {
    public class Opgave1 {

        public static void Run(int n, int hashL, int streamL) {
            Console.WriteLine(">>>> OPGAVE 1 <<<<");

            ModPrime multiplyModPrime = new ModPrime(hashL, false);
            multiplyModPrime.TestMultiplyModPrime(n, streamL, true);

            MultiplyShift multiplyShift = new MultiplyShift(hashL, false);
            multiplyShift.TestMultiplyShift(n, streamL, true);
        }
    }
}