
using LinkedList.LinkedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedList
{
    public class LinkedList<T>
    {
        private Node<T> _head;

        public void Add(T value)
        {
            if (_head == null)
            {
                _head = new Node<T>(value);
            }
            else
            {
                _head.Next = new Node<T>(value);
            }
        }

        public void Remove(T value)
        {
            TraverseNodes(_head, value, (node) => {
                // So we stop at the node BEFORE the node we want to remove, and just 
                // skip over the node we want to delete :) 
                node.Next = node.Next.Next;
            });
        }

        private void TraverseNodes(Node<T> currentNode, T valueToFind, Action<Node<T>> WhatToDoWhenYouFindIt)
        {
            // So this is kind of weird, but we really need to do all of our work from the perspective of 
            // the node BEOFRE the node we are looking for, since we can't look or navigate back up the chain :(
            if (currentNode != null)
            {
                if (currentNode.Next != null)
                {
                    if (currentNode.Next.Value.Equals(valueToFind))
                    {
                        WhatToDoWhenYouFindIt(currentNode);
                    }
                    else
                    {
                        TraverseNodes(currentNode.Next, valueToFind, WhatToDoWhenYouFindIt);
                    }
                }
            }
        }
    }
}
