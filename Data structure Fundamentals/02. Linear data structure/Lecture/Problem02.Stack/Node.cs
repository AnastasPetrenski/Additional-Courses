﻿namespace Problem02.Stack
{
    public class Node<T>
    {
        public Node(T value)
        {
            this.Value = value;
        }

        public T Value { get; set; }

        public Node<T> Next { get; set; }

        public override string ToString()
        {
            return $"NODE{Value} : {Next}";
        }
    }
}