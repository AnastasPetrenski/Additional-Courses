using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CustomNode
{
    public class MyCustomNode<T> : IEnumerable<T>
    {
        private Node<T> _node;

        public MyCustomNode()
        {
            this._node = null;
            this.Count = 0;
        }

        public int Count { get; private set; }

        public void Enqueue(T item)
        {
            Node<T> element = new Node<T>(item);

            if (this._node == null)
            {
                element.Head = element;
                element.Tail = element;
                this._node = element;
                this.Count = 1;
            }
            else
            {
                element.Head = this._node;
                this._node.Tail = element;
                this._node = element;
                this.Count++;
            }
        }

        public T Dequeue()
        {
            throw new NotImplementedException();
        }

        public bool Contains(T item)
        {
            throw new NotImplementedException();
        }

        public T Peek()
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            var current = this._node;

            while (true)
            {

            }

            T element = default;
            yield return element;
        }

        IEnumerator IEnumerable.GetEnumerator()
            => this.GetEnumerator();
    }
}