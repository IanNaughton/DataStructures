using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinarySearchTree
{
    public class Node<T>
    {
        public Node<T> Left { get; set; }
        public T value { get; set; }
        public Node<T> Right { get; set; }

        public Node(T newValue)
        {
            value = newValue;
        }
    }
}
