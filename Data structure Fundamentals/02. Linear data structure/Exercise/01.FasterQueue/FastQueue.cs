namespace Problem01.FasterQueue
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class FastQueue<T> : IAbstractQueue<T>
    {
        private Node<T> _head;
        private Node<T> _tail;

        public FastQueue()
        {
            this._head = null;
            this._tail = null;
            this.Count = 0;
        }

        public int Count { get; private set; }

        public bool Contains(T item)
        {
            var current = this._head;
            while (current != null)
            {
                if (current.Item.Equals(item))
                {
                    return true;
                }

                current = current.Next;
            }

            return false;
        }

        public T Dequeue()
        {
            ValidateNotEmpty();
            var current = this._head;

            if (this.Count == 1)
            {
                this._head = this._tail = null;
            }
            else
            {
                this._head = current.Next;
            }

            this.Count--;
            return current.Item;
        }

        public void Enqueue(T item)
        {
            var element = new Node<T>
            {
                Item = item
            };

            if (this.Count == 0)
            {
                this._head = this._tail = element;
            }
            else
            {
                this._tail.Next = element;
                this._tail = element;
            }

            this.Count++;
        }

        public T Peek()
        {
            ValidateNotEmpty();

            return this._head.Item;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var current = this._head;

            while (current != null)
            {
                yield return current.Item;
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
    }
}