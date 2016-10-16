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
    public class CustomLinkedList<T> : IEnumerable<T>
    {
        private Node<T> _head;
        
        /// <summary>
        /// Adds a list item to the end of the list
        /// </summary>
        /// <param name="newValue">The value you are adding ya dingus!</param>
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

        /// <summary>
        /// Adds a new value to the beginning of the list
        /// </summary>
        /// <param name="value">The value you are adding!</param>
        public void AddToBeginning(T value)
        {
            Node<T> newNode = new Node<T>(value);
            ConnectNewNode(null, newNode, _head);
        }

        /// <summary>
        /// Adds a node to the list after the first list value that matches the predicate.
        /// </summary>
        /// <param name="predicate">Expression or statement(s) that are used to evaluate if a match has been found</param>
        /// <param name="newValue">The value being added to the list</param>
        public void AddAfter(Func<T, bool> predicate, T newValue)
        {
            Node<T> newNode = new Node<T>(newValue);
            Node<T> currentNode = FindNode(predicate);

            ConnectNewNode(currentNode, newNode, currentNode.Next);
        }

        /// <summary>
        /// Adds a value to the list before the first match on the predicate.
        /// </summary>
        /// <param name="predicate">Expression or statement(s) that are used to evaluate if a match has been found</param>
        /// <param name="newValue">The value being added to the list</param>
        public void AddBefore(Func<T, bool> predicate, T newValue)
        {
            Node<T> newNode = new Node<T>(newValue);
            Node<T> currentNode = FindNode(predicate);

            ConnectNewNode(currentNode.Previous, newNode, currentNode);
        }

        /// <summary>
        /// Removes the first item from the list where the predicate is true.
        /// The list is traversed from beginning to end.
        /// </summary>
        /// <param name="predicate">Expression or statement(s) that are used to evaluate if a match has been found</param>
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

        /// <summary>
        /// Returns a value from the list based on the predicate passed in. The first 
        /// match on the predicate is returned.
        /// </summary>
        /// <param name="predicate">Expression or statement(s) that are used to evaluate if a match has been found</param>
        /// <returns>The first value to match the predicate</returns>
        public T Find(Func<T, bool> predicate)
        {
            Node<T> foundNode = FindNode(predicate);
            return foundNode != null ? foundNode.Value : default(T); 
        }
        
        /// <summary>
        /// Returns a node from the list based on the predicate passed in. The first 
        /// match on the predicate is returned.
        /// </summary>
        /// <param name="predicate">Expression or statement(s) that are used to evaluate if a match has been found</param>
        /// <returns>The first node to match the predicate</returns>
        private Node<T> FindNode(Func<T, bool> predicate)
        {
            return FindNode(node => predicate(node.Value) ? node : null);
        }

        /// <summary>
        /// Returns a node from the list based on the predicate passed in. The first 
        /// match on the predicate is returned.
        /// </summary>
        /// <param name="predicate">Expression or statement(s) that are used to evaluate if a match has been found</param>
        /// <returns>The first node to match the predicate</returns>
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

        /// <summary>
        /// Adds a new node to the list between the 'Previous' and 'Next' nodes
        /// </summary>
        /// <param name="previous">The node that will preceed our new node</param>
        /// <param name="newNode">The new node we are inserting into the list</param>
        /// <param name="next">The node that will immediately follow the new node we are inserting</param>
        private void ConnectNewNode(Node<T> previous, Node<T> newNode, Node<T> next)
        {
            // Wire up new node 
            newNode.Previous = previous;
            newNode.Next = next;

            // Wire up old nodes to the new node
            if (previous != null) { previous.Next = newNode; }
            if (next != null) { next.Previous = newNode; }
        }

        /// <summary>
        /// Implement IEnumerable so consumers of this class can use
        /// foreach to iterate over list items
        /// </summary>
        /// <returns>An enumerator for ForEach statements to use</returns>
        public IEnumerator<T> GetEnumerator()
        {
            Node<T> currentNode = _head;
            while (currentNode != null)
            {
                yield return currentNode.Value;
                currentNode = currentNode.Next;
            }
        }

        /// <summary>
        /// IEnumerable explicit implementation - delegates calls to the generic GetEnumerator
        /// </summary>
        /// <returns>An enumerator for ForEach statements to use</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Print a list of our nodes in head to tail order 
        /// </summary>
        /// <returns></returns>
        public string ToString()
        {
            string listAsString = string.Empty;
            Node<T> currentNode = _head;
            while (currentNode != null)
            {
                listAsString = listAsString + " " + currentNode.Value;
                currentNode = currentNode.Next;
            }

            return listAsString.TrimStart();
        }
    }
}
