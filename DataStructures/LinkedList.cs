using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    /// <summary>
    /// A doubly linked list--for your health.
    /// DISCLAIMER - This is not sorted soooo accessing elements that need
    /// to be searched for will be O(n)
    /// </summary>
    public class CustomLinkedList<T>
    {
        private Node<T> _head;

        public void AddToBeginning(T value)
        {
            Node<T> newNode = new Node<T>(value);
            ConnectNewNode(_head.Next, newNode, _head.Next.Previous);
        }
        
        public void Add(T newValue)
        {
            Node<T> newNode = new Node<T>(newValue);
            
            // If this is the first node, just assign it to the head
            if (_head == null)
            {
                _head = newNode;
            }
            // otherwise, add it as the new tail
            else
            {
                Node<T> tailNode = FindNode(node => node.Next == null ? node : null);
                ConnectNewNode(tailNode, newNode, null);
            }
            
        }

        public void AddAfter(Func<T, bool> predicate, T newValue)
        {
            Node<T> newNode = new Node<T>(newValue);
            Node<T> currentNode = FindNode(predicate);

            ConnectNewNode(currentNode, newNode, currentNode.Next);
        }

        public void AddBefore(Func<T, bool> predicate, T newValue)
        {
            Node<T> newNode = new Node<T>(newValue);
            Node<T> currentNode = FindNode(predicate);

            ConnectNewNode(currentNode.Previous, newNode, currentNode);
        }

        public void Remove(Func<T, bool> predicate)
        {
            Node<T> nodeToRemove = FindNode(predicate);

            if (nodeToRemove != null)
            {
                if (nodeToRemove.Previous != null) { nodeToRemove.Previous.Next = nodeToRemove.Next; }
                if (nodeToRemove.Next != null) { nodeToRemove.Next.Previous = nodeToRemove.Previous; }
                nodeToRemove = null;
            }  
        }

        public T Find(Func<T, bool> predicate)
        {
            Node<T> foundNode = FindNode(predicate);
            return foundNode != null ? foundNode.Value : default(T); 
        }

        private Node<T> FindNode(Func<T, bool> predicate)
        {
            return FindNode(node => predicate(node.Value) ? node : null);
        }

        private Node<T> FindNode(Func<Node<T>, Node<T>> predicate)
        {
            Node<T> response = null;
            Node<T> currentNode = _head;
            
            // While we have not reached the end of the list, and 
            // we have not found the first match
            while (currentNode != null && response == null)
            {
                response = predicate(currentNode);
                currentNode = currentNode.Next;
            }
            return response;
        }

        private void ConnectNewNode(Node<T> previous, Node<T> newNode, Node<T> next)
        {
            // Wire up new node 
            newNode.Previous = previous;
            newNode.Next = next;

            // Wire up old nodes to the new node
            if (previous != null) { previous.Next = newNode; }
            if (next != null) { next.Previous = newNode; }
        }

        public string ToString()
        {
            string listAsString = string.Empty;
            Node<T> currentNode = _head;
            while (currentNode != null)
            {
                listAsString = listAsString + " " + currentNode.Value;
                currentNode = currentNode.Next;
            }

            return listAsString;
        }
    }
}
