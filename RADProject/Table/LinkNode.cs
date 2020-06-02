using System;

namespace RADProject.Table {
    public class LinkNode {
        public LinkNode Next;
        public ulong Key;
        public int Val;

        ///<summary>
        /// The constructor of a LinkNode. It accepts the parameters that define the variables of this object
        ///</summary>
        ///<params name="x">The value that hashes to the linked list in which this node will appear</params>
        ///<params name="delta">The starting weight of this node</params>
        ///<params name="head">
        /// The head of the linked list this node will appear in. This node is then set as the new head
        /// </params>
        public LinkNode(ulong x, int delta, LinkNode head) {
            Key = x;
            Val = delta;
            Next = head;
        }
    }
}