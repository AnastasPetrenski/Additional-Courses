namespace Problem03.ReversedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class ReversedList<T> : IAbstractList<T>
    {
        private const int DefaultCapacity = 4;

        private T[] _items;

        public ReversedList()
            : this(DefaultCapacity) { }

        public ReversedList(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException(nameof(capacity));

            this._items = new T[capacity];
        }

        public T this[int index]
        {
            get
            {
                ValidateIndex(index);
                return this._items[this.Count - 1 - index];
            }
            set
            {
                ValidateIndex(index);
                this._items[index] = value;
            }
        }

        public int Count { get; private set; }

        public void Add(T item)
        {
            ResizeArrayIfNecessary();
            this._items[this.Count++] = item;
        }

        public bool Contains(T item)
        {
            return IndexOf(item) != -1;
        }

        public int IndexOf(T item)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this._items[this.Count - 1 - i].Equals(item))
                {
                    return i;
                }
            }

            return -1;
        }

        public void Insert(int index, T item)
        {
            ValidateIndex(index);
            ResizeArrayIfNecessary();

            if (index == 0)
            {
                this._items[this.Count] = item;
            }
            else
            {
                for (int i = this.Count; i > this.Count - index; i--)
                {
                    this._items[i] = this._items[i - 1];
                }
                this._items[this.Count - index] = item;
            }

            this.Count++;
        }

        public bool Remove(T item)
        {
            int index = this.IndexOf(item);

            if (index == -1)
            {
                return false;
            }

            RemoveAt(index);

            //this.Count--;
            return true;
        }

        public void RemoveAt(int index)
        {
            ValidateIndex(index);
            var indexOfElement = this.Count - 1 - index;
            for (int i = indexOfElement; i < this.Count - 1; i++)
            {
                this._items[i] = this._items[i + 1];
            }

            this._items[this.Count - 1] = default;
            this.Count--;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = this.Count - 1; i >= 0; i--)
            {
                yield return this._items[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private void ResizeArrayIfNecessary()
        {
            if (this.Count == this._items.Length)
            {
                T[] newArray = new T[this._items.Length * 2];

                for (int i = 0; i < this._items.Length; i++)
                {
                    newArray[i] = this._items[i];
                }

                this._items = newArray;
            }
        }

        private void ValidateIndex(int index)
        {
            if (index < 0 || index >= this.Count)
            {
                throw new IndexOutOfRangeException();
            }
        }
    }
}