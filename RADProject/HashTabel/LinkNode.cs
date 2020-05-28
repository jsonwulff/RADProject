using System;

        ///<summary>
        ///  Object structure for a "one way linked list". Each node has three variables:
        /// LinkNode next - The next node in the linked list (null if the node is the last in the list)
        /// ulong key     - The "x" value that hashes to this linked list and is stored in this node
        /// int val       - The delta value (weight) of this particular "x".
        ///</summary>
namespace RADProject.HashTabel {
    public class LinkNode {
        public LinkNode next;
        public ulong key;
        public int val;

        ///<summary>
        /// The constructor of a LinkNode. It accepts the parameters that define the variables of this object
        ///</summary>
        ///<params name="x">The value that hashes to the linked list in which this node will appear</params>
        ///<params name="delta">The starting weight of this node</params>
        ///<params name="head">The head of the linked list this node will appear in. This node is then set as the new head</params>
        public LinkNode(ulong x, int delta, LinkNode head) {
            key = x;
            val = delta;
            next = head;
        }
    }
}