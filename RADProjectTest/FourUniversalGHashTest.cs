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
            BigInteger a0 = fourUniversal.a[0];
            BigInteger a1 = fourUniversal.a[1];
            BigInteger a2 = fourUniversal.a[2];
            BigInteger a3 = fourUniversal.a[3];
            BigInteger p  = fourUniversal.p;
            foreach (var tuple in Stream.CreateStream(100, 50, true)) {
                ulong x = tuple.Item1;
                BigInteger GHash = (a0 + a1 * x + a2 * (x * x) + a3 * (x * x * x)) % p;
                Assert.AreEqual(GHash, fourUniversal.g_hash(x));
            }
            
        }

    }
}