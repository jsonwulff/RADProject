using System;
using System.Numerics;

namespace RADProject {
    public class FourUniversal {
        ///<summary>
        /// Instantiates the four public variables.null These are used in the g_hash function and are set in the
        /// constructor
        ///</summary>
        public BigInteger[] A;

        public BigInteger P;
        public ulong M;
        public int L;

        ///<summary>
        /// Constructor for the class. It sets the four public variables to the specified values.null Then there is a
        /// check of whether to use predefined or unique values for the a parameters.
        ///</summary>
        ///<params name="img"> The image size, read as a power of 2 (i.e 2^img) </params>
        ///<params name="random">
        /// A boolean that determines if the class should use predefined values or generate unique ones
        /// </params>
        public FourUniversal(int img, bool random) {
            P = BigInteger.Pow(2, 89) - 1;
            L = img;
            M = 1UL << L;
            A = new BigInteger[4];

            if (random) {
                HashGen();
            } else {
                Byte[] aBytes =
                    {0x32, 0x4c, 0x8e, 0xd8, 0x08, 0x8d, 0xb1, 0x13, 0xca, 0x4a, 0x22, 0x00};
                Byte[] bBytes =
                    {0x0d, 0xb5, 0x64, 0x33, 0xce, 0xf5, 0x7c, 0xdd, 0x63, 0x1a, 0xc8, 0x00};
                Byte[] cBytes =
                    {0x67, 0x95, 0x10, 0xe6, 0xfb, 0xe8, 0x1c, 0x49, 0x7b, 0x3d, 0xa1, 0x00};
                Byte[] dBytes =
                    {0xa1, 0x6a, 0x75, 0xb9, 0x84, 0x49, 0x56, 0x5a, 0x93, 0xb0, 0x63, 0x00};

                A[0] = new BigInteger(aBytes);
                A[1] = new BigInteger(bBytes);
                A[2] = new BigInteger(cBytes);
                A[3] = new BigInteger(dBytes);
            }
        }

        ///<summary>
        /// Generates four random BigInteger values <= p. We instanciate a random object, 4 byte[12]. Generate random
        /// bytes in each array. Sets the last byte to be a zero byte, and lastly use them to create the BigInteger
        /// values in A.
        ///</summary>
        ///<remarks>
        /// The size of the array is 12 since we need 11 bytes to represent a number <=p and to avoid generating a
        /// negative number, we need the most significant byte to be a zero byte.
        /// </remarks>
        public void HashGen() {
            Random rnd = new Random();
            Byte[] aBytes = new Byte[12];
            Byte[] bBytes = new Byte[12];
            Byte[] cBytes = new Byte[12];
            Byte[] dBytes = new Byte[12];

            rnd.NextBytes(aBytes);
            rnd.NextBytes(bBytes);
            rnd.NextBytes(cBytes);
            rnd.NextBytes(dBytes);

            aBytes[11] = 0x00;
            bBytes[11] = 0x00;
            cBytes[11] = 0x00;
            dBytes[11] = 0x00;

            A[0] = new BigInteger(aBytes);
            A[1] = new BigInteger(bBytes);
            A[2] = new BigInteger(cBytes);
            A[3] = new BigInteger(dBytes);
        }

        ///<summary>
        /// This algorithm follows "Algorithm 1" in the 2moment.pdf notes from absalon with k=4
        ///</summary>
        ///<params name="x"> The key we want to hash </params>
        ///<returns> A BigInteger hashvalue for the key x </returns>
        public BigInteger g_hash(ulong x) {
            BigInteger y = A[3];
            for (int i = 2; i >= 0; i--) {
                y = (y * x) + A[i];
                y = (y & P) + (y >> 89);
            }

            if (y >= P) {
                y = y - P;
            }

            return y;
        }

        ///<summary>
        /// This algorithm follows "Algorithm 2" in the 2moment.pdf notes from absalon. 
        /// We use the g_hash function to hash the key value x, and then perform our calculations of h(x) and s(x).
        ///</summary>
        ///<params name="x"> The key value we wish to calculate h(x) and s(x) from </params>
        ///<returns> h(x), s(x) as a tuple (ulong, int) = (h(x), s(x)) </returns>
        public Tuple<ulong, int> Hash(ulong x) {
            BigInteger g = g_hash(x);
            ulong h = (ulong) (g & (M - 1));
            int b = (int) (g >> (88));
            int s = (1 - (2 * b));

            return new Tuple<ulong, int>(h, s);
        }
    }
}