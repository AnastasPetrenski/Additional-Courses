namespace Problem03.Queue
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Queue<T> : IAbstractQueue<T>
    {
        private Node<T> head;

        public Queue()
        {
            this.head = null;
            this.Count = 0;
        }

        public int Count { get; private set; }

        public bool Contains(T item)
        {
            Node<T> current = this.head;

            while (current != null)
            {
                if (current.Value.Equals(item))
                {
                    return true;
                }

                current = current.Head;
            }

            return false;
        }

        public T Dequeue()
        {
            this.ValidateIsNotEmpty();
            Node<T> current = this.head;
            this.head = this.head.Head;
            this.Count--;
            return current.Value;
        }

        public void Enqueue(T item)
        {
            Node<T> element = new Node<T>(item);

            if (this.head == null)
            {
                this.head = element;
            }
            else
            {
                Node<T> current = this.head;
                while (current.Head != null)
                {
                    current = current.Head; 
                }

                current.Head = element;
            }

            this.Count++;
        }

        public T Peek()
        {
            this.ValidateIsNotEmpty();

            return this.head.Value;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node<T> current = this.head;

            while (current != null)
            {
                yield return current.Value;
                current = current.Head;
            }

        }

        IEnumerator IEnumerable.GetEnumerator()
            => this.GetEnumerator();

        private void ValidateIsNotEmpty()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException();
            }
        }

        public override string ToString()
        {
            return $"HEAD:{this.head.Head}, VALUE{this.head.Value} - {this.Count}";
        }
    }
}