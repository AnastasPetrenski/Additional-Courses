using System;
using System.Collections.Generic;
using System.Text;

namespace _01._BrowserHistory
{
    public class Node<T>
    {
        public Node(T value, Node<T> head = null, Node<T> tail = null)
        {
            this.Value = value;
            this.Next = head;
            this.Previous = tail;
        }

        public T Value { get; set; }

        public Node<T> Next { get; set; }

        public Node<T> Previous { get; set; }
    }
}
