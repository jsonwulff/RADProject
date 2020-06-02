using RADProject.HashFunctions;

namespace RADProject.Table {
    public class HashTable {
        public LinkNode[] Table;
        private IHashFunction hashFunction;

        ///<summary>
        /// The constructor of the HashTable class. Sets the table as a LinkNode[] of the appropriate size and defines
        /// the hashfunction that is used.
        ///</summary>
        ///<params name="size">
        /// The number of entries in our array. It is a power of 2 and the image size of the hash function t
        ///</params>
        ///<params name="t">The Hash object used to handle all hashing in the methods of this class</params>
        public HashTable(ulong size, IHashFunction function) {
            Table = new LinkNode[size];
            hashFunction = function;
        }

        ///<summary>
        /// Determines wether the key "x" is represented by a LinkNode in our table.
        /// Finds the head of the linked list at the entry of our array that the hashing of x points to.
        /// traverses the list checking if a node in the list has the key "x". Stops when it finds said node, or reaches
        /// the end of the list
        /// Returns the node (or null)
        ///</summary>
        ///<params name="x">The key to look for in our table</params>
        ///<returns> The LinkNode with key "x" or null if no such node is found</returns>
        public LinkNode Get(ulong x) {
            LinkNode cur = Table[hashFunction.Hash(x)];

            while (cur != null && cur.Key != x) {
                cur = cur.Next;
            }

            return cur;
        }

        ///<summary>
        /// Sets the value of the LinkNode representing "x" in our table. If "x" is not represented in the table,
        /// constructs a new node and inserts it.
        ///</summary>
        ///<params name="x">The key for which the value needs to be set</params>
        ///<params name="v">The value to override the value of the "x" node</params>
        public void Set(ulong x, int v) {
            LinkNode r = Get(x);

            if (r == null) {
                ulong hash = hashFunction.Hash(x);
                LinkNode n = new LinkNode(x, v, Table[hash]);
                Table[hash] = n;
            } else {
                r.Val = v;
            }
        }

        ///<summary>
        /// Updates the value of the LinkNode representing "x" in our table. If "x" is not represented in the table,
        /// constructs a new node and inserts it.
        ///</summary>
        ///<params name="x">The key for which the value needs to be set</params>
        ///<params name="delta">The value to increment the value of the "x" node by</params>
        public void Increment(ulong x, int delta) {
            LinkNode r = Get(x);

            if (r == null) {
                ulong hash = hashFunction.Hash(x);
                LinkNode n = new LinkNode(x, delta, Table[hash]);
                Table[hash] = n;
            } else {
                r.Val = r.Val + delta;
            }
        }

        /// <summary>
        /// Suggestion for implementation of quad sum calculation
        /// </summary>
        /// <returns>ulong Quad sum</returns>
        public ulong CalcQuadSum() {
            ulong quadSum = 0UL;
            for (int i = 0; i < Table.Length; i++) {
                var qsCur = Table[i];

                while (qsCur != null) {
                    int valToPower = qsCur.Val * qsCur.Val;
                    quadSum = quadSum + (ulong) valToPower;
                    qsCur = qsCur.Next;
                }
            }

            return quadSum;
        }
    }
}