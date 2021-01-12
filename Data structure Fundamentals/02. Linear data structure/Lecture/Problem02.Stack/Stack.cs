namespace Problem02.Stack
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Stack<T> : IAbstractStack<T>
    {
        private Node<T> _head;

        public Stack()
        {
            this._head = null;
            this.Count = 0;
        }

        public Stack(Node<T> node)
        {
            this._head = node;
            this.Count = 1;
        }

        public int Count { get; private set; }

        public bool Contains(T item)
        {
            var current = this._head;

            while (current != null)
            {
                if (current.Value.Equals(item))
                {
                    return true;
                }

                current = current.Next;
            }

            return false;
        }

        public T Peek()
        {
            this.ValidateNotEmpty();

            return this._head.Value;
        }

        public T Pop()
        {
            this.ValidateNotEmpty();

            T element = this._head.Value;
            this._head = this._head.Next;
            this.Count--;

            return element;
        }

        public void Push(T item)
        {
            Node<T> element = new Node<T>(item);
            element.Next = this._head;        
            this._head = element;
            this.Count++;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var current = this._head;

            while (current != null)
            {
                yield return current.Value;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() 
            => this.GetEnumerator();

        private void ValidateNotEmpty()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException();
            }
        }


        public override string ToString()
        {
            return $"Value:{this._head.Value}, HEAD:{this._head.Next}";
        }
    }
}