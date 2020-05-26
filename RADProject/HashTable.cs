using System;
using System.Numerics;

namespace RADProject {
    public class HashTable {
        private int tableSize;
        private LinkNode[] table;
        private Hash<BigInteger> h;

        public HashTable(int size){
            tableSize = size;
            table = new LinkNode[size];
        }

        public LinkNode get(ulong x){

            LinkNode cur = table[(int)h.hash(x)];

            while (cur != null && cur.key != x)
            {
                cur = cur.next;
            }

            return cur;
        }
        
        public void set(ulong x, ulong v){
            
            LinkNode r = get(x);

            if (r == null){
                ulong hash = h.hash(x);
                LinkNode n = new LinkNode(x, v, table[hash]);
                table[hash] = n;
            }else{
                r.val = v;
            }
        }

        public void increment(ulong x, ulong delta){
            
            LinkNode r = get(x);

            if (r == null){
                ulong hash = h.hash(x);
                LinkNode n = new LinkNode(x, delta, table[hash]);
                table[hash] = n;
            }else{
                r.val = r.val + delta;
            }
        }

    }
}