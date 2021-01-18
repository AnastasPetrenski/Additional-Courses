namespace _01.BinaryTree
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class BinaryTree<T> : IAbstractBinaryTree<T>
    {
        public BinaryTree(T value
            , IAbstractBinaryTree<T> leftChild
            , IAbstractBinaryTree<T> rightChild)
        {
            this.Value = value;
            this.LeftChild = leftChild;
            this.RightChild = rightChild;
        }

        public T Value { get; private set; }

        public IAbstractBinaryTree<T> LeftChild { get; private set; }

        public IAbstractBinaryTree<T> RightChild { get; private set; }

        public string AsIndentedPreOrder(int indent)
        {
            StringBuilder result = new StringBuilder();
            this.AsIndentedPreOrderDfs(this, indent, result);
            return result.ToString();
        }

        public List<IAbstractBinaryTree<T>> InOrder()
        {
            List<IAbstractBinaryTree<T>> inOrder = new List<IAbstractBinaryTree<T>>();
            return GetTreeInOrder(this, inOrder);
        }

        public List<IAbstractBinaryTree<T>> PostOrder()
        {
            List<IAbstractBinaryTree<T>> order = new List<IAbstractBinaryTree<T>>();
            return GetTreePostOrder(this, order);
        }

        public List<IAbstractBinaryTree<T>> PreOrder()
        {
            List<IAbstractBinaryTree<T>> order = new List<IAbstractBinaryTree<T>>();
            return GetTreePreOrder(this, order);
        }

        public void ForEachInOrder(Action<T> action)
        {
            if (this.LeftChild != null)
            {
                this.LeftChild.ForEachInOrder(action);
            }

            action.Invoke(this.Value);

            if (this.RightChild != null)
            {
                this.RightChild.ForEachInOrder(action);
            }
        }

        private void AsIndentedPreOrderDfs(IAbstractBinaryTree<T> tree, int indent, StringBuilder result)
        {
            result.AppendLine($"{new String(' ', indent)}{tree.Value}");

            if (tree.LeftChild != null)
            {
                this.AsIndentedPreOrderDfs(tree.LeftChild, indent + 2, result);
            }

            if (tree.RightChild != null)
            {
                this.AsIndentedPreOrderDfs(tree.RightChild, indent + 2, result);
            }
        }

        private List<IAbstractBinaryTree<T>> GetTreeInOrder(
            IAbstractBinaryTree<T> tree, List<IAbstractBinaryTree<T>> inOrder)
        {
            if (tree.LeftChild != null)
            {
                this.GetTreeInOrder(tree.LeftChild, inOrder);
            }

            inOrder.Add(tree);

            if (tree.RightChild != null)
            {
                this.GetTreeInOrder(tree.RightChild, inOrder);
            }

            return inOrder;
        }

        private List<IAbstractBinaryTree<T>> GetTreePostOrder(
            IAbstractBinaryTree<T> tree, List<IAbstractBinaryTree<T>> order)
        {
            if (tree.LeftChild != null)
            {
                this.GetTreePostOrder(tree.LeftChild, order);
            }

            if (tree.RightChild != null)
            {
                this.GetTreePostOrder(tree.RightChild, order);
            }

            order.Add(tree);

            return order;
        }

        private List<IAbstractBinaryTree<T>> GetTreePreOrder(
            IAbstractBinaryTree<T> tree, List<IAbstractBinaryTree<T>> order)
        {
            order.Add(tree);

            if (tree.LeftChild != null)
            {
                this.GetTreePreOrder(tree.LeftChild, order);
            }

            if (tree.RightChild != null)
            {
                this.GetTreePreOrder(tree.RightChild, order);
            }

            return order;
        }
    }
}
