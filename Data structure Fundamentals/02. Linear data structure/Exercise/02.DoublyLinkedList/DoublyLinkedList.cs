namespace Problem02.DoublyLinkedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class DoublyLinkedList<T> : IAbstractLinkedList<T>
    {
        private Node<T> head;
        private Node<T> tail;

        public DoublyLinkedList()
        {
            this.head = null;
            this.tail = null;
            this.Count = 0;
        }

        public DoublyLinkedList(Node<T> node)
        {
            this.head = this.tail = node;
            this.Count = 1;
        }

        public int Count { get; private set; }

        public void AddFirst(T item)
        {
            Node<T> element = new Node<T>
            {
                Item = item
            };

            if (this.Count == 0)
            {
                this.head = element;
                this.tail = element;
            }
            else
            {
                this.head.Previous = element;
                element.Next = this.head;
                this.head = element;
            }

            this.Count++;
            //What if it is a first element?
            //01. this.head.Previous = element
            //02. element.Next = this.head
            //03. this.head = element
        }

        public void AddLast(T item)
        {
            Node<T> element = new Node<T>
            {
                Item = item
            };

            if (this.Count == 0)
            {
                this.head = this.tail = element;
            }
            else
            {
                element.Previous = this.tail;
                this.tail.Next = element;
                this.tail = element;
            }

            this.Count++;
        }

        public T GetFirst()
        {
            ValidateIsNotEmpty();

            var first = this.head;

            return head.Item;
        }

        public T GetLast()
        {
            ValidateIsNotEmpty();

            var last = this.tail;

            return last.Item;
        }

        public T RemoveFirst()
        {
            ValidateIsNotEmpty();
            var current = this.head;

            if (this.Count == 1)
            {
                this.head = this.tail = null;
            }
            else
            {
                var newHead = this.head.Next;
                newHead.Previous = null;
                this.head = newHead;
            }

            this.Count--;
            return current.Item;
        }

        public T RemoveLast()
        {
            ValidateIsNotEmpty();
            var current = this.tail;

            if (this.Count == 1)
            {
                this.head = this.tail = null;
            }
            else
            {
                Node<T> newTail = this.tail.Previous;
                newTail.Next = null;
                this.tail = newTail;
            }

            this.Count--;
            return current.Item;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var current = this.head;

            while (current != null)
            {
                yield return current.Item;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private void ValidateIsNotEmpty()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException();
            }
        }
    }
}