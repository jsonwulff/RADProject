using System;
using System.Numerics;

namespace RADProject {
    public class HashTable {
        public int tableSize;
        public LinkNode[] table;
        private Hash h;

        public HashTable(int size, Hash t){
            tableSize = size;
            table = new LinkNode[size];
            h = t;
        }

        public LinkNode get(ulong x){

            LinkNode cur = table[h.hash(x)];

            while (cur != null && cur.key != x)
            {
                cur = cur.next;
            }

            return cur;
        }
        
        public void set(ulong x, int v){
            
            LinkNode r = get(x);

            if (r == null){
                ulong hash = h.hash(x);
                LinkNode n = new LinkNode(x, v, table[hash]);
                table[hash] = n;
            }else{
                r.val = v;
            }
        }

        public void increment(ulong x, int delta){
            
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