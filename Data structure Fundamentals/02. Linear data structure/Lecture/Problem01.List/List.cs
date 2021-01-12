namespace Problem01.List
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class List<T> : IAbstractList<T>
    {
        private const int DEFAULT_CAPACITY = 4;
        private T[] _items;

        public List()
            : this(DEFAULT_CAPACITY) {
        }

        public List(int capacity) // = DEFAULT_CAPACITY) <-- we can skip the empty constructor
        {
            if (capacity <= 0)
            {
                throw new IndexOutOfRangeException(String.Format("{0} can't be less than zero", nameof(capacity)));
            }
            this._items = new T[capacity];
        }

        public T this[int index]
        {
            get
            {
                ValidateIndexOutOfRange(index);
                return this._items[index];
            }
            set
            {
                this.ValidateNotNull(value);
                this.ValidateIndexOutOfRange(index);
                this._items[index] = value;
            }
        }

        public int Count { get; private set; }

        public void Add(T item)
        {
            this.ValidateNotNull(item);
            this.ValidateArraySize();
            this._items[Count++] = item;
        }

        public void Clear()
        {
            for (int i = 0; i < this.Count; i++)
            {
                this._items[i] = default;
            }

            this._items = new T[DEFAULT_CAPACITY];
            this.Count = default;
        }

        public bool Contains(T item)
        {
            if (this.IndexOf(item) != -1)
            {
                return true;
            }

            return false;
        }

        public int IndexOf(T item)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this._items[i].Equals(item))
                {
                    return i;
                }
            }

            return -1;
        }

        public void Insert(int index, T item)
        {
            ValidateIndexOutOfRange(index);
            ValidateArraySize();

            for (int i = Count; i > index; i--)
            {
                this._items[i] = this._items[i - 1];
            }

            this._items[index] = item;
            Count++;
        }

        public bool Remove(T item)
        {
            this.ValidateNotNull(item);

            int index = this.IndexOf(item);
            if (index != -1)
            {
                RemoveArrayItem((int)index);
                
                return true;
            }

            return false;
        }

        public void RemoveAt(int index)
        {
            this.ValidateIndexOutOfRange(index);

            this.RemoveArrayItem(index);
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < this.Count; i++)
            {
                yield return this._items[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator() 
            => this.GetEnumerator();


        private void ValidateArraySize()
        {
            if (Count == this._items.Length)
            {
                Resize();
            }
        }

        private void Resize()
        {
            T[] newArr = new T[this._items.Length * 2];
            for (int i = 0; i < this._items.Length; i++)
            {
                newArr[i] = this._items[i];
            }

            this._items = newArr;
        }

        private void ValidateIndexOutOfRange(int index)
        {
            if (index < 0 || index >= Count)
            {
                throw new IndexOutOfRangeException();
            }
        }

        private void ValidateNotNull(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(String.Format("Value {0} can't be null", value));
            }
        }

        private void RemoveArrayItem(int index)
        {
            for (int i = index; i < this.Count - 1; i++)
            {
                this._items[i] = this._items[i + 1];
            }

            this._items[Count - 1] = default;
            this.Count--;
        }
    }
}