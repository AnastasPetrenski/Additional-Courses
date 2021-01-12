using System;
using System.Collections.Generic;
using System.Text;

namespace CustomNode
{
    public class Node<T>
    {
        public Node(T item)
        {
            this.Value = item;
        }

        public T Value { get; set; }

        public Node<T> Head { get; set; }

        public Node<T> Tail { get; set; }
    }
}
