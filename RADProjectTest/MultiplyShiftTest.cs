using NUnit.Framework;
using RADProject;
using RADProject.HashFunctions;

namespace RADProjectTest {
    public class MultiplyShiftTest {
        [SetUp]
        public void Setup() { }

        [Test]
        public void TestModPrime() {
            MultiplyShift multiplyShift = new MultiplyShift(63, false);
            ulong a = multiplyShift.a;
            int l = multiplyShift.l;
            foreach (var tuple in Stream.CreateStream(100, 50, false)) {
                ulong x = tuple.Item1;
                ulong multiplyModPrimeCalc = (a * x) >> (64 - l);
                Assert.AreEqual(multiplyModPrimeCalc, multiplyShift.hash(x));
            }
        }

        [Test]
        public void TestModPrimeRandom() {
            MultiplyShift multiplyShift = new MultiplyShift(63, true);
            ulong a = multiplyShift.a;
            int l = multiplyShift.l;
            foreach (var tuple in Stream.CreateStream(100, 50, false)) {
                ulong x = tuple.Item1;
                ulong multiplyModPrimeCalc = (a * x) >> (64 - l);
                Assert.AreEqual(multiplyModPrimeCalc, multiplyShift.hash(x));
            }
        }
    }
}