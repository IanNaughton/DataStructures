using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinarySearchTree
{
    public class Node<T> where T : IComparable
    {
        public Node<T> Left;
        public T value { get; set; }
        public Node<T> Right;

        public Node(T newValue)
        {
            value = newValue;
        }
    }
}
