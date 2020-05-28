using RADProject.HashFunctions;

namespace RADProject.CountSketch {
    public class CountSketch {
        public ulong[] table;
        private FourUniversal g;

        ///<summary>
        /// The constructor. It accepts the parameters that define the variables of this object. It instansiates the table with 0UL on all entries.
        ///</summary>
        ///<params name="size">The size of our table. It is a power of two and the image size of our hash function</params>
        ///<params name="hash">A fourUniversal hash approximator object. This does the hashing for our table</params>
        public CountSketch(int size, FourUniversal hash){
            g = hash
            table = new ulong[size];
            for (int i = 0; i < size; i++){
                table[i] = 0UL;
            }
        }
        
        ///<summary>
        /// Adds the delta value (signed) to the entry in our table that "x" hashes too. 
        ///</summary>
        ///<params name="x"> The key value "x" for which to hash into our table</params>
        ///<params name="delta"> The value to add to our table entry </params>
        public void Add(ulong x, int delta){
            Tuple<ulong, int> hash = g.hash(x);
            table[hash.Item1] += (hash.Item2 * delta);
        }

        ///<summary>
        /// Calculates the Chi value of our table (as described in the implementation notes). Loops over our array, squares the values and adds them to a sum.
        ///</summary>
        ///<returns>The sum of the squared values of our table</returns>
        public ulong Chi(){
            ulong sum = 0UL;
            foreach (ulong y in table){
                sum += Math.Pow(y, 2);
            }
            return sum;
        }
    }
}