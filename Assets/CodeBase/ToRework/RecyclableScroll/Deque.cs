using System;
using System.Collections;
using System.Collections.Generic;

namespace CodeBase.ToRework.RecyclableScroll
{
    public class Deque<T> : IEnumerable<T>
    {
        private DoublyNode<T> _head;
        private DoublyNode<T> _tail;

        public void AddLast(T data)
        {
            var node = new DoublyNode<T>(data);

            if (_head == null)
                _head = node;
            else
            {
                _tail.Next = node;
                node.Previous = _tail;
            }

            _tail = node;
            Count++;
        }

        public void AddFirst(T data)
        {
            var node = new DoublyNode<T>(data);
            var temp = _head;
            node.Next = temp;
            _head = node;
            if (Count == 0)
                _tail = _head;
            else
                temp.Previous = node;
            Count++;
        }

        public T RemoveFirst()
        {
            if (Count == 0)
                throw new InvalidOperationException();
            T output = _head.Data;
            if (Count == 1)
            {
                _head = _tail = null;
            }
            else
            {
                _head = _head.Next;
                _head.Previous = null;
            }

            Count--;
            return output;
        }

        public T RemoveLast()
        {
            if (Count == 0)
                throw new InvalidOperationException();
            T output = _tail.Data;
            if (Count == 1)
            {
                _head = _tail = null;
            }
            else
            {
                _tail = _tail.Previous;
                _tail.Next = null;
            }

            Count--;
            return output;
        }
        

        public T First
        {
            get
            {
                if (IsEmpty)
                    throw new InvalidOperationException();
                return _head.Data;
            }
        }

        public T Last
        {
            get
            {
                if (IsEmpty)
                    throw new InvalidOperationException();
                return _tail.Data;
            }
        }

        public int Count { get; private set; }

        public bool IsEmpty => Count == 0;

        public void Clear()
        {
            _head = null;
            _tail = null;
            Count = 0;
        }

        public bool Contains(T data)
        {
            DoublyNode<T> current = _head;
            while (current != null)
            {
                if (current.Data.Equals(data))
                    return true;
                current = current.Next;
            }

            return false;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this).GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            DoublyNode<T> current = _head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }
    }
}