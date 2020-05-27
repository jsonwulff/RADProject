using System;
using System.Numerics;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using RADProject;

namespace RADProjectTest {
    public class ModPrimeTest {
        [SetUp]
        public void Setup() {
            
        }

        [Test]
        public void TestModPrime() {
            ModPrime modPrime = new ModPrime(63, false);
            BigInteger a = modPrime.a;
            BigInteger b = modPrime.b;
            BigInteger p = modPrime.p;
            BigInteger twoPowL = (BigInteger) Math.Pow(2, modPrime.l);
            foreach (var tuple in Stream.CreateStream(100,50)) {
                ulong x = tuple.Item1;
                ulong multiplyModPrime = (ulong)(((a * x + b) % p) % twoPowL);
                Assert.AreEqual(multiplyModPrime, modPrime.hash(x));
            }
        }
        
        [Test]
        public void TestModPrimeRandom() {
            ModPrime modPrime = new ModPrime(63, true);
            BigInteger a = modPrime.a;
            BigInteger b = modPrime.b;
            BigInteger p = modPrime.p;
            BigInteger twoPowL = (BigInteger) Math.Pow(2, modPrime.l);
            foreach (var tuple in Stream.CreateStream(100,63)) {
                ulong x = tuple.Item1;
                ulong multiplyModPrime = (ulong)(((a * x + b) % p) % twoPowL);
                Assert.AreEqual(multiplyModPrime, modPrime.hash(x));
            }
        }
    }
}