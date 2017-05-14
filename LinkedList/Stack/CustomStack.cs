using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedList.Stack
{
    /// <summary>
    /// This is your garden-variety LIFO 
    /// datastructure you remember from your 
    /// data structures class. 
    /// </summary>
    public class CustomStack<T>
    {
        private StackItem<T> _head;

        public void Push(T value)
        {
            // Add a stack item
            var newItem = new StackItem<T>(value);

            if (_head == null)
            {
                _head = newItem;
            }
            else
            {
                StackItem<T> currentItem = _head;
                FindTopOfStack(currentItem, topItem => currentItem = topItem);
                
                currentItem.Next = newItem; 
            }
        }

        private void FindTopOfStack(StackItem<T> currentItem, Action<StackItem<T>> actionToTake)
        {
            while (currentItem.Next != null)
            {
                currentItem = currentItem.Next;
            }

            actionToTake(currentItem);
        }

        public T Pop()
        {
            // Remove an item from the top of the stack

            // Make sure there is at least one element on the stack! 
            if (_head == null)
            {
                throw new InvalidOperationException("There are no elements on the stack to pop! :O");
            }

            StackItem<T> topOfStack = null;
          
            

            // Find the last element. At this point we have three options:
            
            // The head is the last element
            if (_head.Next == null)
            {
                topOfStack = _head;
                _head = null;
            }
            else
            {
                // The nth element is the last element 
                StackItem<T> currentItem = _head;

                // Iterate until we find the second to last 
                // item in the list.
                while (currentItem.Next.Next != null)
                {
                    currentItem = currentItem.Next;
                }

                topOfStack = currentItem.Next;
                currentItem.Next = null;

            }

            return topOfStack.Value;
        }


        

    }
}
