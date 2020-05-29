using System;
using RADProject.HashFunctions;

namespace RADProject.HashTabel {
    public class HashTable {
        public LinkNode[] table;
        private Hash h;

        ///<summary>
        /// The constructor of the HashTable class. Sets the table as a LinkNode[] of the appropriate size and defines the hashfunction that is used.
        ///</summary>
        ///<params name="size">The number of entries in our array. It is a power of 2 and the image size of the hash function t</params>
        ///<params name="t">The Hash object used to handle all hashing in the methods of this class</params>
        public HashTable(ulong size, Hash t) {
            table = new LinkNode[size];
            h = t;
        }


        ///<summary>
        /// Determines wether the key "x" is represented by a LinkNode in our table.
        /// Finds the head of the linked list at the entry of our array that the hashing of x points to.
        /// traverses the list checking if a node in the list has the key "x". Stops when it finds said node, or reaches the end of the list
        /// Returns the node (or null)
        ///</summary>
        ///<params name="x">The key to look for in our table</params>
        ///<returns> The LinkNode with key "x" or null if no such node is found</returns>
        public LinkNode Get(ulong x) {
            LinkNode cur = table[h.hash<ulong>(x)];

            while (cur != null && cur.key != x) {
                cur = cur.next;
            }

            return cur;
        }

        ///<summary>
        /// Sets the value of the LinkNode representing "x" in our table. If "x" is not represented in the table, constructs a new node and inserts it.
        ///</summary>
        ///<params name="x">The key for which the value needs to be set</params>
        ///<params name="v">The value to override the value of the "x" node</params>
        public void Set(ulong x, int v) {
            LinkNode r = Get(x);

            if (r == null) {
                ulong hash = h.hash<ulong>(x);
                LinkNode n = new LinkNode(x, v, table[hash]);
                table[hash] = n;
            } else {
                r.val = v;
            }
        }

        ///<summary>
        /// Updates the value of the LinkNode representing "x" in our table. If "x" is not represented in the table, constructs a new node and inserts it.
        ///</summary>
        ///<params name="x">The key for which the value needs to be set</params>
        ///<params name="delta">The value to increment the value of the "x" node by</params>
        public void Increment(ulong x, int delta) {
            LinkNode r = Get(x);

            if (r == null) {
                ulong hash = h.hash<ulong>(x);
                LinkNode n = new LinkNode(x, delta, table[hash]);
                table[hash] = n;
            } else {
                r.val = r.val + delta;
            }
        }

        /// <summary>
        /// Suggestion for implementation of quad sum calculation
        /// </summary>
        /// <param name="obj">HashTable to calculate quad sum of</param>
        /// <returns>ulong Quad sum</returns>
        public ulong calcQuadSum() {
            ulong QuadSum = 0UL;

            for (int i = 0; i < table.Length; i++) {
                LinkNode QS_cur = table[i];

                while (QS_cur != null) {
                    //This method of using Math.Pow may risk having floating point errors.
                    QuadSum = QuadSum + (ulong) (Math.Pow(QS_cur.val, 2));
                    QS_cur = QS_cur.next;
                }
            }
            return QuadSum;
        }

        public void TestHashTable(int n, Hash hashFunction, int[] lValues) { }
    }
}