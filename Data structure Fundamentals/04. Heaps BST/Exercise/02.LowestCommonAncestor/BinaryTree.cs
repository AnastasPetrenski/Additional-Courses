namespace _02.LowestCommonAncestor
{
    using System;
    using System.Collections.Generic;

    public class BinaryTree<T> : IAbstractBinaryTree<T>
        where T : IComparable<T>
    {
        public BinaryTree(
            T value,
            BinaryTree<T> leftChild,
            BinaryTree<T> rightChild)
        {
            this.Value = value;
            this.RightChild = rightChild;
            this.LeftChild = leftChild;

            if (this.RightChild != null)
            {
                this.RightChild.Parent = this;
            }
            if (this.LeftChild != null)
            {
                this.LeftChild.Parent = this;
            }

        }

        public T Value { get; set; }

        public BinaryTree<T> LeftChild { get; set; }

        public BinaryTree<T> RightChild { get; set; }

        public BinaryTree<T> Parent { get; set; }

        public T FindLowestCommonAncestor(T first, T second)
        {
            List<BinaryTree<T>> firstList = new List<BinaryTree<T>>();
            List<BinaryTree<T>> secondList = new List<BinaryTree<T>>();

            FindElementDfs(this, first, firstList);
            FindElementDfs(this, second, secondList);

            var firstElement = firstList[0];
            var secondElement = secondList[0];

            T commonParent = default;
            //T parentToLookFor = firstElement.Parent.Value;

            //while (!parentToLookFor.Equals(firstElement.Value)
            //    || !parentToLookFor.Equals(secondElement.Value))
            //{
            //    if (!parentToLookFor.Equals(firstElement.Value))
            //    {
            //        firstElement = firstElement.Parent;
            //    }

            //    if (!parentToLookFor.Equals(secondElement.Value))
            //    {
            //        secondElement = secondElement.Parent;
            //    }
            //}

            //return firstElement.Value;

            var firstElementParents = new List<T>();
            while (firstElement.Parent != null)
            {
                var parent = firstElement.Parent.Value;
                firstElementParents.Add(parent);

                firstElement = firstElement.Parent;
            }


            while (secondElement.Parent != null)
            {
                if (firstElementParents.Contains(secondElement.Parent.Value))
                {
                    commonParent = secondElement.Parent.Value;
                    break;
                }

                secondElement = secondElement.Parent;
            }

            return commonParent;
        }

        private void FindElementDfs(BinaryTree<T> current, T value, List<BinaryTree<T>> list)
        {
            if (current == null)
            {
                return;
            }

            if (current.Value.Equals(value))
            {
                list.Add(current);
            }

            FindElementDfs(current.LeftChild, value, list);
            FindElementDfs(current.RightChild, value, list);
        }
    }
}
