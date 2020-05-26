using System;
using System.Numerics;
using NUnit.Framework;
using RADProject;

namespace RADProjectTest
{
    public class Tests {
        public ulong x;
        [SetUp]
        public void Setup() {
            }

        [Test]
        public void Test1()
        {
            Assert.IsTrue(Stream.HelloWorld() == "Hello World!");
        }

        [Test]
        public void TestModuloModPrime() {
            ModPrime modPrime = new ModPrime();
            foreach (var tuple in Stream.CreateStream(1,50)) {
                x = tuple.Item1;
                modPrime.hashgen();
                BigInteger aTest = modPrime.a;
                Assert.GreaterOrEqual(modPrime.p, (BigInteger)0);
                Assert.GreaterOrEqual(modPrime.a, (BigInteger)0);
                Assert.GreaterOrEqual(modPrime.b, (BigInteger)0);
                Assert.GreaterOrEqual(modPrime.l, (int)0);
                BigInteger modTest = ((aTest * x + modPrime.b) % modPrime.p) % (BigInteger) Math.Pow(2, modPrime.l);
                Assert.AreEqual(modTest, modPrime.hash(x));
            }

        }
    }
}