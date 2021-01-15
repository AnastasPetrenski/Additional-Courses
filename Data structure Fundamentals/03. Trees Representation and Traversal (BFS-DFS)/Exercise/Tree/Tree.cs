namespace Tree
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class Tree<T> : IAbstractTree<T>
    {
        private readonly List<Tree<T>> _children;

        public Tree(T key, params Tree<T>[] children)
        {
            this.Key = key;
            this._children = new List<Tree<T>>();

            foreach (var child in children)
            {
                this.AddChild(child);
                child.Parent = this;
            }
        }

        public T Key { get; private set; }

        public Tree<T> Parent { get; private set; }

        public IReadOnlyCollection<Tree<T>> Children
            => this._children.AsReadOnly();

        public void AddChild(Tree<T> child)
        {
            this._children.Add(child);
        }

        public void AddParent(Tree<T> parent)
        {
            this.Parent = parent;
        }

        public string GetAsString()
        {
            var distance = 0;
            var sb = new StringBuilder();
            string result = PrintTree(sb, distance, this);

            return result;
        }

        public Tree<T> GetDeepestLeftomostNode()
        {
            var elements = new List<Tree<T>>();
            elements = DFS(this, elements, leafPredicate);
            Tree<T> deepestNode = null;
            int depthest = 0;


            foreach (var node in elements)
            {
                int currentDepth = GetDepth(node);
                if (currentDepth > depthest)
                {
                    depthest = currentDepth;
                    deepestNode = node;
                }
            }

            return deepestNode;
        }


        public List<T> GetLeafKeys()
        {
            return BFSLeafs(this, leafPredicate);
        }

        public List<T> GetMiddleKeys()
        {
            return BFSLeafs(this, middlePredicate);
        }

        public List<T> GetLongestPath()
        {
            var path = new Stack<T>();
            var deepestNode = GetDeepestLeftomostNode();
            while (deepestNode != null)
            {
                path.Push(deepestNode.Key);

                deepestNode = deepestNode.Parent;
            }

            return new List<T>(path);
        }

        public List<T> GetLongestPathMyWay()
        {
            List<List<T>> allPaths = new List<List<T>>();
            List<T> singlePath = new List<T>();
            //allPaths = GetAllPaths(this, allPaths, singlePath);

            List<T> longestsPath = new List<T>();
            foreach (var path in allPaths)
            {
                if (path.Count > longestsPath.Count)
                {
                    longestsPath = path;
                }
            }

            return longestsPath;
        }

        public List<List<T>> PathsWithGivenSum(int sum)
        {
            List<List<T>> paths = new List<List<T>>();
            List<T> singlePath = new List<T>();
            int singlePathSum = 0;
            paths = GetAllPaths(this, paths, singlePath, singlePathSum, sum);

            return paths;
        }

        public List<Tree<T>> SubTreesWithGivenSumInitial(int sum)
        {
            List<Tree<T>> result = new List<Tree<T>>();

            foreach (var subtree in this._children)
            {
                if (subtreeSum.Invoke(subtree, sum))
                {
                    result.Add(subtree);
                }
            }

            return result;
        }

        public List<Tree<T>> SubTreesWithGivenSum(int sum)
        {
            return this.BFSsubtreeSum(subtreeSum, sum);
        }

        private int GetSubtreeSum(Tree<T> subtree)
        {
            int key = Convert.ToInt32(subtree.Key);
            int subtreeSum = 0;

            foreach (var child in subtree._children)
            {
                subtreeSum += GetSubtreeSum(child);
            }

            return subtreeSum + key;
        }

        private List<Tree<T>> DFS(Tree<T> tree, List<Tree<T>> elements, Func<Tree<T>, bool> predicate = null)
        {
            if (predicate(tree))
            {
                elements.Add(tree);
            }

            foreach (var subtree in tree._children)
            {
                DFS(subtree, elements, predicate);
            }

            return elements;
        }

        private List<T> BFSLeafs(Tree<T> tree, Func<Tree<T>, bool> predicate = null)
        {
            List<T> keys = new List<T>();
            var elements = new Queue<Tree<T>>();
            elements.Enqueue(tree);

            while (elements.Count > 0)
            {
                var current = elements.Dequeue();

                if (predicate.Invoke(current))
                {
                    keys.Add(current.Key);
                }

                foreach (var item in current._children)
                {
                    elements.Enqueue(item);
                }
            }

            return keys;
        }

        private List<Tree<T>> BFSsubtreeSum(Func<Tree<T>, int, bool> predicate, int sum)
        {
            var keys = new List<Tree<T>>();
            var elements = new Queue<Tree<T>>();
            elements.Enqueue(this);

            while (elements.Count > 0)
            {
                var current = elements.Dequeue();

                if (predicate.Invoke(current, sum))
                {
                    keys.Add(current);
                }

                foreach (var item in current._children)
                {
                    elements.Enqueue(item);
                }
            }

            return keys;
        }

        private string PrintTree(StringBuilder sb, int depth, Tree<T> tree)
        {
            var distance = new String(' ', depth);
            sb.AppendLine($"{distance}{tree.Key}");
            File.AppendAllText("../../../file.txt", $"{distance}{tree.Key}" + Environment.NewLine);
            foreach (var child in tree._children)
            {
                PrintTree(sb, depth + 2, child);
            }

            return sb.ToString().TrimEnd();
        }

        private List<List<T>> GetAllPaths(Tree<T> tree, List<List<T>> allPaths, List<T> singlePath, int singlePathSum, int sum)
        {
            singlePath.Add(tree.Key);
            singlePathSum += Convert.ToInt32(tree.Key);

            foreach (var subpath in tree._children)
            {
                GetAllPaths(subpath, allPaths, singlePath, singlePathSum, sum);
            }

            if (IsLeaf(tree) && singlePathSum == sum)
            {
                var current = new List<T>(singlePath);
                allPaths.Add(current);
            }

            singlePathSum -= Convert.ToInt32(tree.Key);
            singlePath.RemoveAt(singlePath.Count - 1);

            return allPaths;
        }

        private int GetDepth(Tree<T> node)
        {
            int depth = 1;

            while (node.Parent != null)
            {
                depth++;
                node = node.Parent;
            }

            return depth;
        }

        private bool IsLeaf(Tree<T> element)
        {
            return element._children.Count == 0;
        }

        private bool IsParent(Tree<T> element)
        {
            return element.Parent == null;
        }

        private bool IsMiddleElement(Tree<T> element)
        {
            return !IsLeaf(element) && !IsParent(element);
        }

        private bool HasGivenSum(Tree<T> element, int sum)
        {
            int total = GetSubtreeSum(element);

            return total == sum;
        }

        Func<Tree<T>, int, bool> subtreeSum = (element, sum) => element.HasGivenSum(element, sum);

        Func<Tree<T>, bool> isNotParentPredicate = (T) => !T.IsParent(T);

        Func<Tree<T>, bool> leafPredicate = (T) => T.IsLeaf(T);

        Func<Tree<T>, bool> middlePredicate = (T) => T.IsMiddleElement(T);
    }
}
