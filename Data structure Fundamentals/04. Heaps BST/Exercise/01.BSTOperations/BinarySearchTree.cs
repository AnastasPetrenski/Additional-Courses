namespace _01.BSTOperations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

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

        public Node<T> Root { get; private set; }

        public Node<T> LeftChild { get; private set; }

        public Node<T> RightChild { get; private set; }

        public T Value => this.Root.Value;

        //public int Count => this.Root.Count; //work properly
        public int Count => CountElementsBST();

        public void Insert(T element)
        {
            Node<T> node = new Node<T>(element, null, null);

            if (this.Root == null)
            {
                this.Root = node;
            }
            else
            {
                this.InsertRecursively(node, this.Root);
            }
        }

        public bool Contains(T element)
        {
            var elements = new Queue<Node<T>>();
            elements.Enqueue(this.Root);

            while (elements.Count > 0)
            {
                var current = elements.Dequeue();
                if (current.Value.Equals(element))
                {
                    return true;
                }

                if (current.LeftChild != null)
                {
                    elements.Enqueue(current.LeftChild);
                }

                if (current.RightChild != null)
                {
                    elements.Enqueue(current.RightChild);
                }
            }

            return false;
        }

        public IAbstractBinarySearchTree<T> Search(T element)
        {
            Node<T> node = this.FindElement(this.Root, element);

            return new BinarySearchTree<T>(node);
        }

        public void EachInOrder(Action<T> action)
        {
            EachInOrderDfs(this.Root, action);
        }

        public List<T> Range(T lower, T upper)
        {
            List<T> collection = new List<T>();
            Action<T> addElement = T => collection.Add(T);
            EachInOrder(addElement);
            //collection = collection.Where(c => c.CompareTo(lower) >= 0 && c.CompareTo(upper) <= 0).ToList();
            return collection.Where(c => !IsLess(c, lower) && !IsGreater(c, upper)).ToList();

        }

        public void DeleteMin()
        {
            this.ValidateNotEmpty();

            Node<T> current = this.Root;
            Node<T> previous = null;

            if (this.Root.LeftChild == null)
            {
                this.Root = this.Root.RightChild;
            }
            else
            {
                while (current.LeftChild != null)
                {
                    this.Root.Count--; //we have to decrease before to change reference
                    previous = current;
                    current = current.LeftChild;
                }

                if (previous == null)
                {
                    this.Root = null;
                }
                else if (previous.RightChild != null && previous.RightChild.Value.CompareTo(this.Root.Value) < 0)
                {
                    current.Value = previous.Value;
                    previous.Value = previous.RightChild.Value;
                    previous.RightChild = null;
                }
                else
                {
                    previous.LeftChild = null;
                    if (previous == this.Root)
                    {
                        this.LeftChild = null;
                        current = null;
                    }
                }
            }
        }

        public void DeleteMax()
        {
            ValidateNotEmpty();
            
            Node<T> current = this.Root;
            Node<T> previous = null;

            if (this.Root.RightChild == null)
            {
                this.Root = this.Root.LeftChild;
            }
            else
            {
                while (current.RightChild != null)
                {
                    this.Root.Count--;
                    previous = current;
                    current = current.RightChild;
                }

                previous.RightChild = current.LeftChild;
            }
        }

        public int GetRank(T element)
        {
            //return GetElementRank(this.Root, element);
            return ElementRank(this.Root, element);
        }

        private int ElementRank(Node<T> root, T element)
        {
            if (root == null)
            {
                return 0;
            }

            if (IsLess(element, root.Value))
            {
                return ElementRank(root.LeftChild, element);
            }
            else if (AreEqual(element, root.Value))
            {
                return GetLeftForestCount(root, 0);
            }

            return GetLeftForestCount(root.LeftChild, 0) + 1 + ElementRank(root.RightChild, element);
        }

        private int GetLeftForestCount(Node<T> root, int result)
        {
            if (root == null)
            {
                return 0;
            }

            if (root.LeftChild != null)
            {
                result = GetLeftForestCount(root.LeftChild, result);
            }

            if (root.RightChild != null)
            {
                result = GetLeftForestCount(root.RightChild, result);
            }

            result++;
            return result;
        }

        private int GetElementRank(Node<T> root, T element)
        {
            if (root == null)
            {
                return 0;
            }

            if (IsLess(element, root.Value))
            {
                return this.GetElementRank(root.LeftChild, element);
            }
            else if (AreEqual(element, root.Value))
            {
                return GetElementCount(root);
            }

            return GetElementCount(root.LeftChild) + 1 + GetElementRank(root.RightChild, element);
        }

        private int GetElementCount(Node<T> root)
        {
            return root == null ? 0 : root.Count;
        }


        private int CountElementsBST()
        {
            List<int> initNumberElements = new List<int>();

            if (this.Root == null)
            {
                return 0;
            }

            initNumberElements = Counter(this.Root, initNumberElements);
            return initNumberElements.Sum();
        }

        private List<int> Counter(Node<T> root, List<int> counter)
        {
            if (root.LeftChild != null)
            {
                Counter(root.LeftChild, counter);
            }

            if (root.RightChild != null)
            {
                Counter(root.RightChild, counter);
            }

            counter.Add(1);

            return counter;
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

        private void InsertRecursively(Node<T> node, Node<T> root)
        {
            if (IsLess(node.Value, root.Value) && root.LeftChild == null)
            {
                root.LeftChild = node;
                root.Count++;
                if (this.LeftChild == null)
                {
                    this.LeftChild = node;
                }
            }
            else if (IsGreater(node.Value, root.Value) && root.RightChild == null)
            {
                root.RightChild = node;
                root.Count++;
                if (this.RightChild == null)
                {
                    this.RightChild = node;
                }
            }
            else
            {
                if (IsLess(node.Value, root.Value))
                {
                    InsertRecursively(node, root.LeftChild);
                }
                else if (IsGreater(node.Value, root.Value))
                {
                    InsertRecursively(node, root.RightChild);
                }
            }
        }

        private void EachInOrderDfs(Node<T> root, Action<T> action)
        {
            if (root != null)
            {
                this.EachInOrderDfs(root.LeftChild, action);
                action.Invoke(root.Value);
                this.EachInOrderDfs(root.RightChild, action);
            }
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

        private Node<T> FindElement(Node<T> root, T element)
        {
            if (root.Value.Equals(element))
            {
                return root;
            }

            if (root.LeftChild != null)
            {
                var current = FindElement(root.LeftChild, element);
                if (current.Value.Equals(element))
                {
                    root = current;
                }
            }

            if (root.RightChild != null)
            {
                var current = FindElement(root.RightChild, element);
                if (current.Value.Equals(element))
                {
                    root = current;
                }
            }

            return root;
        }

        private void ValidateNotEmpty()
        {
            if (this.Root == null)
            {
                throw new InvalidOperationException();
            }
        }
    }
}
