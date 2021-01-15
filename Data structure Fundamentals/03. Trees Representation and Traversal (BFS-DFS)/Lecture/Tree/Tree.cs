namespace Tree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Tree<T> : IAbstractTree<T>
    {
        private readonly List<Tree<T>> _children;

        public Tree(T value)
        {
            this.Value = value;
            this.Parent = null;
            this._children = new List<Tree<T>>();
            this.IsRootDeleted = false;
        }

        public Tree(T value, params Tree<T>[] children)
            : this(value)
        {
            foreach (var child in children)
            {
                child.Parent = this;
                this._children.Add(child);
            }
        }

        public T Value { get; private set; }
        public Tree<T> Parent { get; private set; }
        public IReadOnlyCollection<Tree<T>> Children => this._children.AsReadOnly();
        public bool IsRootDeleted { get; private set; }

        public ICollection<T> OrderBfs()
        {
            var result = new List<T>();
            var queue = new Queue<Tree<T>>();

            if (IsRootDeleted)
            {
                return result;
            }

            queue.Enqueue(this);
            while (queue.Count > 0)
            {
                var element = queue.Dequeue();
                result.Add(element.Value);

                foreach (var child in element._children)
                {
                    queue.Enqueue(child);
                }
            }

            return result;
        }

        public ICollection<T> OrderDfs()
        {
            ICollection<T> result = new List<T>();

            if (IsRootDeleted)
            {
                return result;
            }

            return GetValue(this, result);
        }

        public void AddChild(T parentKey, Tree<T> child)
        {
            Tree<T> parent = FindParent(parentKey, this);

            CheckEmptyNode(parent, parentKey);

            parent._children.Add(child);
        }

        public void RemoveNode(T nodeKey)
        {
            if (this.Value.Equals(nodeKey))
            {
                this.Value = default(T);
                this.Parent = null;
                this._children.Clear();
                this.IsRootDeleted = true;
            }
            else
            {
                Tree<T> toRemove = FindParent(nodeKey, this);

                CheckEmptyNode(toRemove, nodeKey);

                var parentOfRemoved = toRemove.Parent;
                parentOfRemoved._children.Remove(toRemove);
            }
        }

        public void Swap(T firstKey, T secondKey)
        {
            var firstElement = FindParent(firstKey, this);
            var secondElement = FindParent(secondKey, this);

            CheckEmptyNode(firstElement, firstKey);
            CheckEmptyNode(secondElement, secondKey);

            var parentFirstElement = firstElement.Parent;
            var parentSecondElement = secondElement.Parent;

            if (parentFirstElement == null || parentSecondElement == null)
            {
                var root = firstElement.Parent == null ? firstElement : secondElement;
                var child = secondElement.Parent != null ? secondElement : firstElement;

                root.Value = child.Value;
                root._children.Clear();

                foreach (var item in child._children)
                {
                    root._children.Add(item);
                }

                return;
            }

            firstElement.Parent = secondElement;
            secondElement.Parent = parentFirstElement;

            var firstIndex = parentFirstElement._children.IndexOf(firstElement);
            var secondIndex = parentSecondElement._children.IndexOf(secondElement);

            parentFirstElement._children[firstIndex] = secondElement;
            parentSecondElement._children[secondIndex] = firstElement;
        }

        public void MySwap(T firstKey, T secondKey)
        {
            var firstElement = FindParent(firstKey, this);
            var secondElement = FindParent(secondKey, this);

            CheckEmptyNode(firstElement, firstKey);
            CheckEmptyNode(secondElement, secondKey);

            if (firstElement.Parent == null || secondElement.Parent == null)
            {
                var root = firstElement.Parent == null ? firstElement : secondElement;
                var child = secondElement.Parent != null ? secondElement : firstElement;

                Stack<T> path = GetPath(child);
            }

            if (firstElement.Parent.Equals(secondElement.Parent))
            {
                var parent = firstElement.Parent;
                var firstIndex = parent._children.IndexOf(firstElement);
                var secondIndex = parent._children.IndexOf(secondElement);

                parent._children[firstIndex] = secondElement;
                parent._children[secondIndex] = firstElement;
            }
        }

        private Stack<T> GetPath(Tree<T> child)
        {
            var path = new Stack<T>();
            while (child.Parent != null)
            {
                path.Push(child.Value);

                child = child.Parent;
            }

            return path;
        }

        //can be void too
        private ICollection<T> GetValue(Tree<T> tree, ICollection<T> result)
        {
            foreach (var child in tree._children)
            {
                GetValue(child, result);
            }

            result.Add(tree.Value);

            return result;
        }

        private ICollection<T> DfsWithStack()
        {
            var result = new Stack<T>();
            var toTraverse = new Stack<Tree<T>>();

            toTraverse.Push(this);

            while (toTraverse.Count > 0)
            {
                var subTree = toTraverse.Pop();

                foreach (var child in subTree._children)
                {
                    toTraverse.Push(child);
                }

                result.Push(subTree.Value);
            }

            return new List<T>(result);
        }

        private void CheckEmptyNode(Tree<T> parent, T parentKey)
        {
            if (parent is null || !parent.Value.Equals(parentKey))
            {
                throw new ArgumentNullException();
            }
        }

        private Tree<T> FindParent(T parentKey, Tree<T> parent)
        {
            foreach (var child in parent._children)
            {
                var current = FindParent(parentKey, child);
                if (current.Value.Equals(parentKey))
                {
                    return current;
                }
            }

            return parent;
        }
    }
}
