using System;
using System.Numerics;

namespace RADProject {
    public class HashTable {
        private int tableSize;
        private LinkNode[] table;
        private Hash h;

        public HashTable(int size){
            tableSize = size;
            table = new LinkNode[size];
        }

        public ulong get(ulong x){
            ulong res = 0UL;
            LinkNode cur = table[h.hash(x)]
            while (cur != null && cur.key != x)
            {
                cur = cur.next;
            }
            if (cur != null){
                res = cur.val;
            }
            return res;
        }
        
        public void set(ulong x, ulong v){
            //We need somekind of pointer/refrence functionality here.
            ulong r = get(x);

            if (r == 0){
                ulong hash = h.hash(x);
                LinkNode n = new LinkNode(x, v, table[hash]);
                table[hash] = n;
            }else{
                r = v;
            }
        }

        public void increment(ulong x, ulong delta){
            //We need somekind of pointer/refrence functionality here.
            ulong r = get(x);

            if (r == 0){
                ulong hash = h.hash(x);
                LinkNode n = new LinkNode(x, delta, table[hash]);
                table[hash] = n;
            }else{
                r = r + delta;
            }
        }

    }
}