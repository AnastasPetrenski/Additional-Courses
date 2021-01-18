namespace _05.TopView
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BinaryTree<T> : IAbstractBinaryTree<T>
        where T : IComparable<T>
    {
        public BinaryTree(T value, BinaryTree<T> left, BinaryTree<T> right)
        {
            this.Value = value;
            this.LeftChild = left;
            this.RightChild = right;
        }

        public T Value { get; set; }

        public BinaryTree<T> LeftChild { get; set; }

        public BinaryTree<T> RightChild { get; set; }

        public List<T> TopView()
        {
            List<T> elements = new List<T>();
            GetLeftOffset(this, elements);
            GetRightOffset(this.RightChild, elements);
            elements.Sort();
            return elements;
        }

        private void GetRightOffset(BinaryTree<T> tree, List<T> elements)
        {
            if (tree == null)
            {
                return;
            }

            elements.Add(tree.Value);

            GetLeftOffset(tree.RightChild, elements);
        }

        private void GetLeftOffset(BinaryTree<T> tree, List<T> elements)
        {
            if (tree == null)
            {
                return;
            }

            elements.Add(tree.Value);

            GetLeftOffset(tree.LeftChild, elements);
        }
    }
}
