namespace Problem03.Queue
{
    public class Node<T>
    {
        public Node(T item)
        {
            this.Value = item;
        }

        public T Value { get; set; }

        public Node<T> Head { get; set; }

        public override string ToString()
        {
            return $"NODE{Value} : {Head}";
        }
    }
}