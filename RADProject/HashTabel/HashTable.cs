using RADProject.HashFunctions;

namespace RADProject.HashTabel {
    public class HashTable {
        public int tableSize;
        public LinkNode[] table;
        private Hash h;

        public HashTable(int size, Hash t) {
            tableSize = size;
            table = new LinkNode[size];
            h = t;
        }

        public LinkNode Get(ulong x) {
            LinkNode cur = table[h.hash(x)];

            while (cur != null && cur.key != x) {
                cur = cur.next;
            }
            return cur;
        }
        
        public void Set(ulong x, int v) {
            LinkNode r = Get(x);

            if (r == null) {
                ulong hash = h.hash(x);
                LinkNode n = new LinkNode(x, v, table[hash]);
                table[hash] = n;
            } else {
                r.val = v;
            }
        }

        public void Increment(ulong x, int delta){
            LinkNode r = Get(x);

            if (r == null) {
                ulong hash = h.hash(x);
                LinkNode n = new LinkNode(x, delta, table[hash]);
                table[hash] = n;
            } else {
                r.val = r.val + delta;
            }
        }
    }
}