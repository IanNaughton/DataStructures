using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    public class Node<T>
    {
        public Node<T> Next { get; set; }
        public Node<T> Previous { get; set; }
        public T Value { get; set; }

        /// <summary>
        /// Used internally for constructing the head and tail nodes
        /// </summary>
        internal Node()
        {
        }

        /// <summary>
        /// In case you are just interesed in setting the payload of the node 'ya choad! 
        /// </summary>
        /// <param name="value"></param>
        public Node(T newValue)
        {
            Value = newValue;
        }

        /// <summary>
        /// Yup your typical doubly linked list node
        /// </summary>
        /// <param name="previousNode">The node that comes before this one</param>
        /// <param name="newValue">The payload of the node (Hey that rhymes!)</param>
        /// <param name="nextNode">You guessed it, the next node</param>
        public Node(Node<T> previousNode, T newValue, Node<T> nextNode)
        {
            Previous = previousNode;
            Value = newValue;
            Next = nextNode;
        }
    }
}
