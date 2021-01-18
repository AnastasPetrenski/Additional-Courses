namespace _04.BinarySearchTree
{
    using System;

    public class BinarySearchTree<T> : IAbstractBinarySearchTree<T>
        where T : IComparable<T>
    {
        public BinarySearchTree()
        {
        }

        public BinarySearchTree(Node<T> root)
        {
            // TODO: Create copy from root
            this.Copy(root);
        }

        private void Copy(Node<T> root)
        {
            if (root != null)
            {
                this.Insert(root.Value);
                this.Copy(root.LeftChild);
                this.Copy(root.RightChild);
            }
        }

        public Node<T> Root { get; private set; }

        public Node<T> LeftChild { get; private set; }

        public Node<T> RightChild { get; private set; }

        public T Value => this.Root.Value;

        public bool Contains(T element)
        {
            var current = this.Root;

            while (current != null)
            {
                if (IsLess(element, current.Value))
                {
                    current = current.LeftChild;
                }
                else if (IsGreater(element, current.Value))
                {
                    current = current.RightChild;
                }
                else
                {
                    return true;
                }
            }

            return false;
        }

        public void Insert(T element)
        {
            var toInsert = new Node<T>(element, null, null);

            if (this.Root == null)
            {
                this.Root = toInsert;
            }
            else
            {
                InsertRecursively(toInsert, this.Root);
            }
            
        }

        private void InsertRecursively(Node<T> toInsert, Node<T> root)
        {
            if (IsLess(toInsert.Value, root.Value) && root.LeftChild == null)
            {
                root.LeftChild = toInsert;
                if (this.LeftChild == null)
                {
                    this.LeftChild = toInsert;
                }
            }
            else if (IsGreater(toInsert.Value, root.Value) && root.RightChild == null)
            {
                root.RightChild = toInsert;
                if (this.RightChild == null)
                {
                    this.RightChild = toInsert;
                }
            }
            else
            {
                if (IsLess(toInsert.Value, root.Value))
                {
                    InsertRecursively(toInsert, root.LeftChild);
                }
                else if (IsGreater(toInsert.Value, root.Value))
                {
                    InsertRecursively(toInsert, root.RightChild);
                }
            }
        }

        public void InsertIterative(T element)
        {
            //check if first added element is root
            //find where to insert element
            //if exist return
            var toInsert = new Node<T>(element, null, null);

            if (this.Root == null)
            {
                this.Root = toInsert;
            }
            else
            {
                var current = this.Root;
                Node<T> previous = null;

                while (current != null)
                {
                    previous = current;
                    if (IsLess(element, current.Value))
                    {
                        current = current.LeftChild;
                    }
                    else if (IsGreater(element, current.Value))
                    {
                        current = current.RightChild;
                    }
                    else
                    {
                        return;
                    }
                }

                if (this.IsLess(element, previous.Value))
                {
                    previous.LeftChild = toInsert;
                    if (this.LeftChild == null)
                    {
                        this.LeftChild = toInsert;
                    }
                }
                else
                {
                    previous.RightChild = toInsert;
                    if (this.RightChild == null)
                    {
                        this.RightChild = toInsert;
                    }
                }
            }
        }

        public IAbstractBinarySearchTree<T> Search(T element)
        {
            var current = this.Root;
            while (current != null && !this.AreEqual(element, current.Value))
            {
                if (this.IsLess(element, current.Value))
                {
                    current = current.LeftChild;
                }
                else if (this.IsGreater(element, current.Value))
                {
                    current = current.RightChild;
                }
            }

            return new BinarySearchTree<T>(current);
        }

        private bool IsLess(T element, T value)
        {
            return element.CompareTo(value) < 0;
        }

        private bool IsGreater(T element, T value)
        {
            return element.CompareTo(value) > 0;
        }

        private bool AreEqual(T element, T value)
        {
            return element.CompareTo(value) == 0;
        }
    }
}
