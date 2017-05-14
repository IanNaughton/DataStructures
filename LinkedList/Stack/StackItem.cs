using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedList.Stack
{
    public class StackItem<T>
    {
        public StackItem(T newValue)
        {
            Value = newValue;
        }

        public StackItem<T> Next { get; set; }

        public T Value { get; set; }
    }
}
