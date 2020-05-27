using System;

namespace RADProject 
{
    public class LinkNode{
        public LinkNode next;
        public ulong key;
        public int val;

        public LinkNode(ulong x, int delta, LinkNode head){
            key = x;
            val = delta;
            next = head;
        }
    }
}