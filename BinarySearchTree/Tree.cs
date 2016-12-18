using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinarySearchTree
{
    public class Tree<T> where T : IComparable, IEquatable<T>
    {
        private Node<T> _root;

        #region Public Methods
        public Tree()
        {
        }

        /// <summary>
        /// Recursively find where the new node should go
        /// </summary>
        /// <param name="newNodeValue">A leaf we want to add to our lovely tree</param>
        public void AddNode(T newNodeValue)
        {
            AddHelper(ref _root, newNodeValue);
        }

        /// <summary>
        /// Returns the have of a node that equals the value of the search term
        /// </summary>
        /// <param name="value">A value to search for. Equality is based on the object's implementation
        /// of IEquatable</param>
        /// <returns>The tree element that equals the value that was passed in</returns>
        public T GetNode(T valueToFind)
        {
            return FindNodeHelper(ref _root, valueToFind);
        }

        /// <summary>
        /// Deletes a node from the tree that is equal to the value passed in. Equality is based on the object's implementation
        /// of IEquatable</param>
        /// </summary>
        /// <param name="value">The value to delete</param>
        public void DeleteNode(T value)
        {
            FindNodeToDelete(ref _root, value);
        }

        /// <summary>
        /// Returns the nodes of the tree as a flattened list. The nodes 
        /// appear in the list in Left -> Root -> Right order (Sorted least to greatest)
        /// </summary>
        /// <returns>All nodes of the tree in Left -> Root -> Right order</returns>
        public List<T> TraverseInOrder()
        {
            return GetNodesInOrder(_root, new List<T>());
        }

        /// <summary>
        /// Returns the nodes of the tree as a flattened list. The nodes 
        /// appear in the list in Root -> Left -> Right order
        /// </summary>
        /// <returns>All nodes of the tree in Root -> Left -> Right order</returns>
        public List<T> TraversePreOrder()
        {
            return GetNodesPreOrder(_root, new List<T>());
        }

        /// <summary>
        /// Returns the nodes of the tree as a flattened list. The nodes 
        /// appear in the list in Left -> Right -> Root order 
        /// </summary>
        /// <returns>All nodes of the tree in Left -> Right -> Root order</returns>
        public List<T> TraversePostOrder()
        {
            return GetNodesPostOrder(_root, new List<T>());
        }

        /// <summary>
        /// Gets the depth of the deepest path in the tree. 
        /// </summary>
        /// <returns>The depth of the deepest path</returns>
        public int GetDepth()
        {
            return GetDepthHelper(_root);
        }

        /// <summary>
        /// Recursively add a new node to the tree
        /// </summary>
        /// <param name="currentNode">Where we currently are in the tree</param>
        /// <param name="newValue">The value of the new node being added</param>
        private void AddHelper(ref Node<T> currentNode, T newValue)
        {
            // If we have arrived at a null node, we've found where our new value belongs!
            if (currentNode == null)
            {
                currentNode = new Node<T>(newValue);
            }
            else 
            {
                // All values less than the current node belong to the left tree
                if (currentNode.value.CompareTo(newValue) >= 0)
                {
                    AddHelper(ref currentNode.Left, newValue);
                }
                else
                {
                    AddHelper(ref currentNode.Right, newValue);
                }
            }
        }

        /// <summary>
        /// Recursively traverse nodes in our tree until we reach the node we 
        /// are interested in. 
        /// </summary>
        /// <param name="currentNode">The node we are currently traversing</param>
        /// <param name="valueToFind">The value of the node we are looing for</param>
        private T FindNodeHelper(ref Node<T> currentNode, T valueToFind)
        {
            if (currentNode != null && currentNode.value != null)
            {
                // If we've found the node we're looking for
                if (currentNode.value.Equals(valueToFind))
                {
                    return currentNode.value;
                }
                else
                {
                    // All values less than the current node belong to the left tree
                    if (currentNode.value.CompareTo(valueToFind) >= 0)
                    {
                        return FindNodeHelper(ref currentNode.Left, valueToFind);
                    }
                    else
                    {
                        return FindNodeHelper(ref currentNode.Right, valueToFind);
                    }
                }
            }
            else
            {
                throw new KeyNotFoundException("Node does not exists in the current tree.");
            }
        }
        #endregion

        #region Private Helpers
        /// <summary>
        /// As the name impies, we are recursively searching for the node 
        /// whose value matches the node we would like to delete
        /// </summary>
        /// <param name="currentNode">The node we are currently traversing</param>
        /// <param name="valueToDelete">Exactly what it sounds like, the value of the node to remove</param>
        private void FindNodeToDelete(ref Node<T> currentNode, T valueToDelete)
        {
            if (currentNode != null && currentNode.value != null)
            {
                if (currentNode.value.Equals(valueToDelete))
                {
                    // We've found the node we want to delete
                    DeleteNode(ref currentNode);
                }
                else
                {
                    // Keep on traversing the tree
                    if (currentNode.value.CompareTo(valueToDelete) >= 0)
                    {
                        FindNodeToDelete(ref currentNode.Left, valueToDelete);
                    }
                    else
                    {
                        FindNodeToDelete(ref currentNode.Right, valueToDelete);
                    }
                }
            }
            else
            {
                throw new KeyNotFoundException("Node does not exists in the current tree.");
            }
        }

        /// <summary>
        /// Recursively remove a node from the tree. This is done by finding the next in
        /// order successor, assigning the current node the successor's value, and 
        /// kicking off a call to recursively find and delete the in order successor we 
        /// just duplicated. Yikes!
        /// </summary>
        /// <param name="currentNode">The node that we would like to delete</param>
        private void DeleteNode(ref Node<T> currentNode)
        {
            // If the node we are deleting only has one child, we 
            // can simply replace it with its only child. The tree
            // will still be in a valid state
            if (currentNode.Left == null)
            {
                currentNode = currentNode.Right;
            }
            else
            {
                // Same as above, if the node we are deleting only has one child, we 
                // can simply replace it with its only child. The tree
                // will still be in a valid state
                if (currentNode.Right == null)
                {
                    currentNode = currentNode.Left;
                }
                else
                {
                    // If there are multiple decendants, replace the node we want
                    // to delete with its "in order descendant" i.e. the least value of
                    // the current node's right tree. This node will always be greater than the 
                    // left tree of the node we are deleting, and less than the right tree! :D
                    currentNode.value = GetInOrderSuccessor(currentNode.Right);

                    // Now that we have replaced the value of the current node, 
                    // we need to delete the node who's value we just copied. 
                    // This is as simple as calling DeleteNodeHelper again!  
                    FindNodeToDelete(ref currentNode.Right, currentNode.value);
                }
            }
        }

        /// <summary>
        /// This method finds the next in order successor. In our case,
        /// that means getting the least value of the tree "currentNode"  
        ///  is root of.
        /// </summary>
        /// <param name="currentNode">The node we are starting </param>
        /// <returns>The next in order successor for the sub tree "currentNode" represents</returns>
        private T GetInOrderSuccessor(Node<T> currentNode)
        {
            if (currentNode.Left == null)
            {
                // We've found the least value of the right tree
                return currentNode.value;
            }
            else
            {
                return GetInOrderSuccessor(currentNode.Left);
            }
        }

        /// <summary>
        /// Pretty much what it sounds like. Returns nodes in sorted order
        /// --least to greatest. Sort order is defined in the object's 
        /// IComparable implementation.
        /// </summary>
        /// <param name="currentNode">The node we are currently traversing</param>
        /// <param name="inorderValues">A list of the values we have traversed so far</param>
        /// <returns>A list of values containing the current node value, and the values 
        /// of all nodes in the left and right subtrees</returns>
        private List<T> GetNodesInOrder(Node<T> currentNode, List<T> inorderValues)
        {
            if (currentNode != null)
            {
                // If we have the option, traverse left
                if (currentNode.Left != null)
                {
                    inorderValues = GetNodesInOrder(currentNode.Left, inorderValues);
                }

                // Grab the value of the current node
                inorderValues.Add(currentNode.value);

                // Traverse right if we can
                if (currentNode.Right != null)
                {
                    inorderValues = GetNodesInOrder(currentNode.Right, inorderValues);
                }
            }
            return inorderValues;
        }

        /// <summary>
        /// Returns nodes from the tree in Root -> Left -> Right order. If you want 
        /// a nice sorted list, this isn't the method for you.
        /// </summary>
        /// <param name="currentNode">The node we are currently traversing</param>
        /// <param name="inorderValues">A list of the values we have traversed so far</param>
        /// <returns>A list of values containing the current node value, and the values 
        /// of all nodes in the left and right subtrees</returns>
        private List<T> GetNodesPreOrder(Node<T> currentNode, List<T> preOrderValues)
        {
            if (currentNode != null)
            {

                // Grab the value of the current node
                preOrderValues.Add(currentNode.value);

                // If we have the option, traverse left
                if (currentNode.Left != null)
                {
                    preOrderValues = GetNodesPreOrder(currentNode.Left, preOrderValues);
                }

                // Traverse right if we can
                if (currentNode.Right != null)
                {
                    preOrderValues = GetNodesPreOrder(currentNode.Right, preOrderValues);
                }
            }
            return preOrderValues;
        }

        /// <summary>
        /// Returns nodes from the tree in Left -> Right -> Root order. If you want 
        /// a nice sorted list, this isn't the method for you.
        /// </summary>
        /// <param name="currentNode">The node we are currently traversing</param>
        /// <param name="inorderValues">A list of the values we have traversed so far</param>
        /// <returns>A list of values containing the current node value, and the values 
        /// of all nodes in the left and right subtrees</returns>
        private List<T> GetNodesPostOrder(Node<T> currentNode, List<T> postOrderValues)
        {
            if (currentNode != null)
            {
                // If we have the option, traverse left
                if (currentNode.Left != null)
                {
                    postOrderValues = GetNodesPostOrder(currentNode.Left, postOrderValues);
                }

                // Traverse right if we can
                if (currentNode.Right != null)
                {
                    postOrderValues = GetNodesPostOrder(currentNode.Right, postOrderValues);
                }

                // Grab the value of the current node
                postOrderValues.Add(currentNode.value);
            }
            return postOrderValues;
        }

        /// <summary>
        /// Traverse the entire tree to find the maximum depth of the 
        /// tree. This feels like an inefficient way of doing this, and
        /// I feel like the number of iterations can be reduced, either
        /// by adding metadata to the nodes, or by using a more efficient
        /// algorithm (or maybe both?)
        /// </summary>
        /// <param name="currentNode">The current node we are traversing</param>
        /// <returns>Either, the max depth of the right and left subtree, or the current depth if we are looking at a leaf</returns>
        private int GetDepthHelper(Node<T> currentNode)
        {   
            // If the current node is null, then we have hit the end of the current subtree
            if (currentNode == null)
            {
                return 0;
            }
            else
            {
                // Get the depth of the left and right subtrees
                int rightTreeDepth = GetDepthHelper(currentNode.Right);
                int leftTreeDepth = GetDepthHelper(currentNode.Left);

                // Return the greater of the two depths
                return rightTreeDepth >= leftTreeDepth ? rightTreeDepth + 1 : leftTreeDepth + 1;
            }
        }
        #endregion
    }
}
