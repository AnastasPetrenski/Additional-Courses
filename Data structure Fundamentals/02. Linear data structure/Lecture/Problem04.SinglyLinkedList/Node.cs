namespace Problem04.SinglyLinkedList
{
    public class Node<T>
    {
        public Node(T item)
        {
            this.Value = item;
        }

        public T Value { get; set; }

        public Node<T> Head { get; set; }
    }
}