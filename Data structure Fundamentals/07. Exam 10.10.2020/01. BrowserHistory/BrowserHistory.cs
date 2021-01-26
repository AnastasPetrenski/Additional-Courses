namespace _01._BrowserHistory
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using _01._BrowserHistory.Interfaces;

    public class BrowserHistory : IHistory
    {
        private Node<ILink> head;
        private Node<ILink> tail;

        public int Size { get; private set; }

        public void Open(ILink link)
        {
            var toInsert = new Node<ILink>(link);

            if (this.Size == 0)
            {
                this.head = this.tail = toInsert;
            }
            else
            {
                this.head.Previous = toInsert;
                toInsert.Next = this.head;
                this.head = toInsert;
            }

            this.Size++;
        }

        public void Clear()
        {
            this.Size = 0;
            this.head = this.tail = null;
        }

        public bool Contains(ILink link)
        {
            return GetByUrl(link.Url) != null;
        }

        public ILink DeleteFirst()
        {
            EnsureNotEmpty();
            var first = this.tail;

            if (this.Size == 1)
            {
                this.head = this.tail = null;
            }
            else
            {
                this.tail = this.tail.Previous;
                this.tail.Next = null;
            }

            this.Size--;
            return first.Value;
        }

        public ILink DeleteLast()
        {
            EnsureNotEmpty();
            var last = this.head;

            if (this.Size == 1)
            {
                this.head = this.tail = null;
            }
            else
            {
                this.head = this.head.Next;
                this.head.Previous = null;
            }

            this.Size--;
            return last.Value;
        }

        public ILink GetByUrl(string url)
        {
            return GetLinkByUrl(url);
        }

        public ILink LastVisited()
        {
            return this.head.Value;
        }

        public int RemoveLinks(string url)
        {
            var current = this.head;
            int count = 0;

            while (current != null)
            {
                if (current.Value.Url.Contains(url))
                {
                    var previous = current.Previous;
                    var next = current.Next;

                    if (previous != null)
                    {
                        previous.Next = next;
                    }
                    else
                    {
                        this.head = this.head.Next;
                    }

                    if (next != null)
                    {
                        next.Previous = previous;
                    }
                    else
                    {
                        this.tail = this.tail.Previous;
                    }

                    count++;
                    this.Size--;
                }

                current = current.Next;
            }

            if (count == 0)
            {
                throw new InvalidOperationException("No links with the given url!");
            }

            return count;
        }

        public ILink[] ToArray()
        {
            ILink[] array = new ILink[this.Size];
            int index = 0;
            var current = this.head;

            while (current != null)
            {
                array[index++] = current.Value;

                current = current.Next;
            }

            return array;
        }

        public List<ILink> ToList()
        {
            List<ILink> list = new List<ILink>(this.Size);
            var current = this.head;

            while (current != null)
            {
                list.Add(current.Value);

                current = current.Next;
            }

            return list;
        }

        public string ViewHistory()
        {
            if (this.Size == 0)
            {
                return "Browser history is empty!";
            }

            StringBuilder output = new StringBuilder();
            var current = this.head;

            while (current != null)
            {
                output.AppendLine(current.Value.ToString());

                current = current.Next;
            }


            return output.ToString();
        }

        private ILink GetLinkByUrl(string url)
        {
            var current = this.head;
            while (current != null)
            {
                if (current.Value.Url == url)
                {
                    return current.Value;
                }

                current = current.Next;
            }

            return null;
        }

        private void EnsureNotEmpty()
        {
            if (this.Size == 0)
            {
                throw new InvalidOperationException();
            }
        }
    }
}
