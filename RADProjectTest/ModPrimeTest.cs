using System;
using System.Numerics;
using NUnit.Framework;
using RADProject;
using RADProject.HashFunctions;

namespace RADProjectTest {
    public class ModPrimeTest {
        [SetUp]
        public void Setup() { }

        [Test]
        public void TestModPrime() {
            ModPrime modPrime = new ModPrime(63, false);
            BigInteger a = modPrime.A;
            BigInteger b = modPrime.B;
            BigInteger p = modPrime.P;
            BigInteger twoPowL = (BigInteger) Math.Pow(2, modPrime.L);
            foreach (var tuple in Stream.CreateStream(100, 50, false)) {
                ulong x = tuple.Item1;
                ulong multiplyModPrime = (ulong) (((a * x + b) % p) % twoPowL);
                Assert.AreEqual(multiplyModPrime, modPrime.Hash(x));
            }
        }

        [Test]
        public void TestModPrimeRandom() {
            ModPrime modPrime = new ModPrime(63, true);
            BigInteger a = modPrime.A;
            BigInteger b = modPrime.B;
            BigInteger p = modPrime.P;
            BigInteger twoPowL = (BigInteger) Math.Pow(2, modPrime.L);
            foreach (var tuple in Stream.CreateStream(100, 63, false)) {
                ulong x = tuple.Item1;
                ulong multiplyModPrime = (ulong) (((a * x + b) % p) % twoPowL);
                Assert.AreEqual(multiplyModPrime, modPrime.Hash(x));
            }
        }
    }
}