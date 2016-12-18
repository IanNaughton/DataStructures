using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinarySearchTree
{
    public class Tree<T>
    {
        private Node<T> Root;

        public Tree()
        {
        }

        /// <summary>
        /// Recursively find where the new node should go
        /// </summary>
        /// <param name="newNodeValue">A leaf we want to add to our lovely tree</param>
        public void AddNode(T newNodeValue)
        {
            if (Root == null)
            {
                Root = new Node<T>(newNodeValue);
            }
            else
            {
                
            }
        }

        public T FindNode()
        {
            return default(T);
        }

        public List<T> TraversePrefix()
        {
            return new List<T>();
        }

        public List<T> TraverseInfix()
        {
            return new List<T>();
        }

        public List<T> TraversePostfix()
        {
            return new List<T>();
        }

        public int GetDepth()
        {
            return 0;
        }

    }
}
