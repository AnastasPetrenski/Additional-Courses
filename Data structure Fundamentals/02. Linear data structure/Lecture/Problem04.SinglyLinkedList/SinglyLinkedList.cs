namespace Problem04.SinglyLinkedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class SinglyLinkedList<T> : IAbstractLinkedList<T>
    {
        private Node<T> _head;

        public SinglyLinkedList()
        {
            this._head = null;
            this.Count = 0;
        }

        public int Count { get; private set; }

        public void AddFirst(T item)
        {
            Node<T> element = new Node<T>(item);
            element.Head = this._head;
            this._head = element;
            this.Count++;
        }

        public void AddLast(T item)
        {
            Node<T> element = new Node<T>(item);
            if (this.Count == 0)
            {
                this._head = element;
            }
            else
            {
                var current = this._head;
                while (current.Head != null)
                {
                    current = current.Head;
                }

                current.Head = element;
            }

            this.Count++;
        }

        public T GetFirst()
        {
            ValidateIsNotEmpty();

            return this._head.Value;
        }

        public T GetLast()
        {
            ValidateIsNotEmpty();

            var current = this._head;
            while (current.Head != null)
            {
                current = current.Head;
            }

            return current.Value;
        }

        public T RemoveFirst()
        {
            ValidateIsNotEmpty();

            var current = this._head;

            this._head = current.Head;
            this.Count--;

            return current.Value;
        }

        public T RemoveLast()
        {
            ValidateIsNotEmpty();

            var current = this._head;
            T currentValue = default;

            if (current.Head == null)
            {
                currentValue = current.Value;
                this._head = null;

            }
            else
            {
                while (current.Head.Head != null)
                {

                    //if (current.Head.Head == null)
                    //{
                    //    currentValue = current.Head.Value;
                    //    current.Head = null;
                    //    break;
                    //}
                    current = current.Head;
                }

                currentValue = current.Head.Value;
                current.Head = null;

            }

            this.Count--;
            return currentValue;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var current = this._head;

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
    }
}