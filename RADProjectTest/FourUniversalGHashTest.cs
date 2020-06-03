using System;
using NUnit.Framework;
using RADProject;
using System.Numerics;
using RADProject.HashFunctions;

namespace RADProjectTest {
    public class FourUniversalGHashTest {
        [SetUp]
        public void Setup() { }

        [Test]
        public void TestGHash() {
            FourUniversal fourUniversal = new FourUniversal(13, false);
            BigInteger a0 = fourUniversal.A[0];
            BigInteger a1 = fourUniversal.A[1];
            BigInteger a2 = fourUniversal.A[2];
            BigInteger a3 = fourUniversal.A[3];
            BigInteger p = fourUniversal.P;
            foreach (var tuple in Stream.CreateStream(100, 63, true)) {
                BigInteger x = (BigInteger) tuple.Item1;
                BigInteger gHash = (a0 + a1 * x % p + a2 * (x * x) + a3 * (x * x * x)) % p;
                Assert.AreEqual(gHash, fourUniversal.GHash((ulong) x));
            }
        }
        
        [Test]
        public void TestGHashRandom() {
            FourUniversal fourUniversal = new FourUniversal(13, true);
            BigInteger a0 = fourUniversal.A[0];
            BigInteger a1 = fourUniversal.A[1];
            BigInteger a2 = fourUniversal.A[2];
            BigInteger a3 = fourUniversal.A[3];
            BigInteger p = fourUniversal.P;
            foreach (var tuple in Stream.CreateStream(100, 63, true)) {
                BigInteger x = (BigInteger) tuple.Item1;
                BigInteger gHash = (a0 + a1 * x % p + a2 * (x * x) + a3 * (x * x * x)) % p;
                Assert.AreEqual(gHash, fourUniversal.GHash((ulong) x));
            }
        }

        [Test]
        public void TestHHash() {
            FourUniversal fourUniversal = new FourUniversal(13, false);
            ulong M = fourUniversal.M;
            foreach (var tuple in Stream.CreateStream(100, 63, true)) {
                ulong x = tuple.Item1;
                BigInteger gHash = fourUniversal.GHash(x);
                ulong hHash = (ulong)(gHash % M);
                Assert.AreEqual(hHash, fourUniversal.Hash(x).Item1);
            }
        }
        
        [Test]
        public void TestHHashRandom() {
            FourUniversal fourUniversal = new FourUniversal(13, true);
            ulong M = fourUniversal.M;
            foreach (var tuple in Stream.CreateStream(100, 63, true)) {
                ulong x = tuple.Item1;
                BigInteger gHash = fourUniversal.GHash(x);
                ulong hHash = (ulong)(gHash % M);
                Assert.AreEqual(hHash, fourUniversal.Hash(x).Item1);
            }
        }
        
        [Test]
        public void TestSHash() {
            FourUniversal fourUniversal = new FourUniversal(13, false);
            foreach (var tuple in Stream.CreateStream(100, 63, true)) {
                ulong x = tuple.Item1;
                BigInteger gHash = fourUniversal.GHash(x);
                BigInteger twoPowB = (BigInteger)1 << 88;
                double division = (double) (gHash / twoPowB);
                int sHash= 1 - 2 * (int)Math.Floor(division);
                Assert.AreEqual(sHash, fourUniversal.Hash(x).Item2);
            }
        }
        
        [Test]
        public void TestSHashRandom() {
            FourUniversal fourUniversal = new FourUniversal(13, true);
            foreach (var tuple in Stream.CreateStream(100, 63, true)) {
                ulong x = tuple.Item1;
                BigInteger gHash = fourUniversal.GHash(x);
                BigInteger twoPowB = (BigInteger)1 << 88;
                double division = (double) (gHash / twoPowB);
                int sHash= 1 - 2 * (int)Math.Floor(division);
                Assert.AreEqual(sHash, fourUniversal.Hash(x).Item2);
            }
        }
    }
}