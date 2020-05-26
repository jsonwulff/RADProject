using System;

namespace RADProject 
{
    public class LinkNode{
        public LinkNode next;
        public ulong key;
        public ulong val;

        public LinkNode(ulong x, ulong delta, LinkNode head){
            key = x;
            val = delta;
            next = head;
        }
    }
}