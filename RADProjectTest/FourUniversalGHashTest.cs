using System;
using NUnit.Framework;
using RADProject;
using  System.Numerics;

namespace RADProjectTest {
    public class FourUniversalGHashTest {
        [SetUp]
        public void Setup() { }

        [Test]
        public void TestGHash() {
            FourUniversal fourUniversal = new FourUniversal(63, false);
            BigInteger a0 = fourUniversal.A[0];
            BigInteger a1 = fourUniversal.A[1];
            BigInteger a2 = fourUniversal.A[2];
            BigInteger a3 = fourUniversal.A[3];
            BigInteger p  = fourUniversal.P;
            foreach (var tuple in Stream.CreateStream(100, 10, true)) {
                ulong x = tuple.Item1;
                BigInteger GHash = ((a0 % p )+ ((a1 * x) % p ) + ((a2 * (x * x)) % p) + (a3 * (x * x * x)) % p);
                Assert.AreEqual(GHash, fourUniversal.g_hash(x));
            }
            
        }

    }
}