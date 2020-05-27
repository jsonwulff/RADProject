using System;
using System.Numerics;
using RADProject.HashFunctions;

namespace RADProject {
    public class FourUniversal : Hash {
        ///<summary>
        ///Instansiates the four public variables.null These are used in the g_hash function and are set in the constructor
        ///</summary>
        public BigInteger[] a;

        public BigInteger p;
        public ulong m;
        public int l;

        ///<summary>
        /// Constructor for the class. It sets the four public variables to the specified values.null Then there is a check of wether to use
        /// predefined or unique values for the a parameters.
        ///</summary>
        ///<params name="img"> The image size, read as a power of 2 (i.e 2^img) </params>
        ///<params name="random"> A boolean that determines if the class should use predefined values or generate unique ones</params>
        public FourUniversal(int img, bool random) : base(img, random) {
            p = BigInteger.Pow(2, 89) - 1;
            l = img;
            m = 1UL << l;
            a = new BigInteger[4];

            if (random) {
                hashgen();
            } else {
                Byte[] a_bytes =
                    {0x32, 0x4c, 0x8e, 0xd8, 0x08, 0x8d, 0xb1, 0x13, 0xca, 0x4a, 0x22, 0x00};
                Byte[] b_bytes =
                    {0x0d, 0xb5, 0x64, 0x33, 0xce, 0xf5, 0x7c, 0xdd, 0x63, 0x1a, 0xc8, 0x00};
                Byte[] c_bytes =
                    {0x67, 0x95, 0x10, 0xe6, 0xfb, 0xe8, 0x1c, 0x49, 0x7b, 0x3d, 0xa1, 0x00};
                Byte[] d_bytes =
                    {0xa1, 0x6a, 0x75, 0xb9, 0x84, 0x49, 0x56, 0x5a, 0x93, 0xb0, 0x63, 0x00};

                a[0] = new BigInteger(a_bytes);
                a[1] = new BigInteger(b_bytes);
                a[2] = new BigInteger(c_bytes);
                a[3] = new BigInteger(d_bytes);
            }
        }

        ///<summary>
        /// Generates four random BigInteger values <= p. We instanciate a random object, 4 byte[12]. Generate random bytes in each array.
        /// Sets the last byte to be a zero byte, and lastly use them to create the BigInteger values in a[] 
        ///</summary>
        ///<remarks> The size of the array is 12 since we need 11 bytes to represent a number <=p and to avoid generating a negative number,
        ///  we need the most significant byte to be a zero byte.</remarks>
        public void hashgen() {
            Random rnd = new Random();
            Byte[] a_bytes = new Byte[12];
            Byte[] b_bytes = new Byte[12];
            Byte[] c_bytes = new Byte[12];
            Byte[] d_bytes = new Byte[12];

            rnd.NextBytes(a_bytes);
            rnd.NextBytes(b_bytes);
            rnd.NextBytes(c_bytes);
            rnd.NextBytes(d_bytes);

            a_bytes[11] = 0x00;
            b_bytes[11] = 0x00;
            c_bytes[11] = 0x00;
            d_bytes[11] = 0x00;

            a[0] = new BigInteger(a_bytes);
            a[1] = new BigInteger(b_bytes);
            a[2] = new BigInteger(c_bytes);
            a[3] = new BigInteger(d_bytes);
        }

        ///<summary>
        /// This algorithm follows "Algorithm 1" in the 2moment.pdf notes from absalon with k=4
        ///</summary>
        ///<params name="x"> The key we want to hash </params>
        ///<returns> A BigInteger hashvalue for the key x </returns>
        public BigInteger g_hash(ulong x) {
            BigInteger y = a[3];
            for (int i = 2; i >= 0; i--) {
                y = (y * x) + a[i];
                y = (y & p) + (y >> 89);
            }

            if (y >= p) {
                y = y - p;
            }

            return y;
        }

        ///<summary>
        /// This algorithm follows "Algorithm 2" in the 2moment.pdf notes from absalon. 
        /// We use the g_hash function to hash the key value x, and then perform our calculations of h(x) and s(x).
        ///</summary>
        ///<params name="x"> The key value we wish to calculate h(x) and s(x) from </params>
        ///<returns> h(x), s(x) as a tuple (ulong, int) = (h(x), s(x)) </returns>
        public Tuple<ulong, int> hash(ulong x) {
            BigInteger g = g_hash(x);
            ulong h = (ulong) (g & (m - 1));
            int b = (int) (g >> (88));
            int s = (1 - (2 * b));

            return new Tuple<ulong, int>(h, s);
        }
    }
}