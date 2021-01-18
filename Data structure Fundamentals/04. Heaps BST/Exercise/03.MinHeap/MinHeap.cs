namespace _03.MinHeap
{
    using System;
    using System.Collections.Generic;

    public class MinHeap<T> : IAbstractHeap<T>
        where T : IComparable<T>
    {
        private List<T> _elements;

        public MinHeap()
        {
            this._elements = new List<T>();
        }

        public int Size => this._elements.Count;

        public T Dequeue()
        {
            this.EnsureNotEmpty();
            var firstElement = this._elements[0];
            this.Swap(0, this.Size - 1);
            this._elements.RemoveAt(this.Size - 1);
            this.HeapifyDown();
            return firstElement;
        }

        public void Add(T element)
        {
            this._elements.Add(element);

            this.HeapifyUp();
        }

        public T Peek()
        {
            this.EnsureNotEmpty();

            return this._elements[0];
        }

        private void HeapifyDown()
        {
            var parentIndex = 0;
            var leftChildIndex = GetLeftChildIndex(parentIndex);
            while (this.IndexIsValid(leftChildIndex) && this.IsGreater(parentIndex, leftChildIndex))
            {
                var toSwap = leftChildIndex;
                var rightChildIndex = GetRightChildIndex(parentIndex);
                if (this.IndexIsValid(rightChildIndex) && this.IsGreater(toSwap, rightChildIndex))
                {
                    toSwap = rightChildIndex;
                }

                this.Swap(toSwap, parentIndex);
                parentIndex = toSwap;
                leftChildIndex = GetLeftChildIndex(parentIndex);
            }
        }

        private void HeapifyUp()
        {
            int currentIndex = this.Size - 1;
            var parentIndex = GetParentIndex(currentIndex);
            while (currentIndex > 0 && IsLess(currentIndex, parentIndex))
            {
                this.Swap(currentIndex, parentIndex);
                currentIndex = parentIndex;
                parentIndex = GetParentIndex(currentIndex);
            }
        }

        private void Swap(int currentIndex, int parentIndex)
        {
            var temp = this._elements[parentIndex];
            this._elements[parentIndex] = this._elements[currentIndex];
            this._elements[currentIndex] = temp;
        }

        private bool IsGreater(int childIndex, int parentIndex)
        {
            return this._elements[childIndex].CompareTo(this._elements[parentIndex]) > 0;
        }

        private bool IsLess(int childIndex, int parentIndex)
        {
            var result = this._elements[childIndex].CompareTo(this._elements[parentIndex]);
            return result < 0;
        }

        private int GetParentIndex(int childIndex)
        {
            return (childIndex - 1) / 2;
        }

        private int GetLeftChildIndex(int parentIndex)
        {
            return 2 * parentIndex + 1;
        }

        private int GetRightChildIndex(int parentIndex)
        {
            return 2 * parentIndex + 2;
        }

        private bool IndexIsValid(int index)
        {
            return index < this.Size;
        }

        private void EnsureNotEmpty()
        {
            if (this._elements.Count == 0)
            {
                throw new InvalidOperationException();
            }
        }
    }
}
