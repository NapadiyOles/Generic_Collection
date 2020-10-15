using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Generic_Stack
{
    /// <summary>
    /// Represents a simple last-in-first-out (LIFO) generic collection of objects.
    /// </summary>
    /// <typeparam name="T">Type of objects in the stack</typeparam>
    public class Stack<T> : ICollection, IEnumerable<T>, INotifyCollectionChanged
    {
        /// <summary>
        /// The top object of the stack
        /// </summary>
        private Node<T> Head { get; set; }

        /// <summary>
        /// The least object of the stack
        /// </summary>
        private Node<T> Tail { get; set; }

        public int Count { get; private set; }

        public bool IsSynchronized => false;

        public object SyncRoot => this;

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        /// <summary>
        /// Inserts an object at the top of the Stack.
        /// </summary>
        /// <param name="Data">Object to insert</param>
        public void Push(T Data)
        {
            Node<T> Node = new Node<T>(Data);

            if (Head != null)
                Node.Next = Head;

            else
                Tail = Node;

            Head = Node;

            Count++;

            CollectionChanged?.Invoke(
                this, 
                new NotifyCollectionChangedEventArgs(
                    NotifyCollectionChangedAction.Add,  
                    Head.Data
                )
            );
        }

        /// <summary>
        /// Removes the object at the top of the Stack.
        /// </summary>
        /// <returns>Removed object</returns>
        public T Pop()
        {
            if (Head == null)
                throw new InvalidOperationException("Stack is empty");

            var Node = new Node<T>(Head.Data);

            Head = Head.Next;

            Count--;

            CollectionChanged?.Invoke(
                this, 
                new NotifyCollectionChangedEventArgs(
                    NotifyCollectionChangedAction.Remove, 
                    Node.Data
                )
            );

            return Node.Data;
        }

        /// <summary>
        /// Returns the object at the top of the stack without removing it.
        /// </summary>
        /// <returns>Top object</returns>
        public T Peek()
        {
            if (Head == null)
                throw new InvalidOperationException("Stack is empty");

            return Head.Data;
        }

        /// <summary>
        /// Replaces two the least objects of the stack
        /// </summary>
        public void Swap()
        {
            if (Head == Tail)
                throw new InvalidOperationException("Not enough items");

            Node<T> Node = Head;

            (Head, Head.Next) = (Head.Next, Head);

            (Head.Next, Node.Next) = (Node, Head.Next);

            CollectionChanged?.Invoke(
                this, 
                new NotifyCollectionChangedEventArgs(
                    NotifyCollectionChangedAction.Replace,
                    new List<T>
                    {
                        Head.Data,
                        Head.Next.Data
                    },
                    new List<T>
                    {
                        Head.Next.Data,
                        Head.Data
                    }
                )
            );
        }

        public void CopyTo(Array array, int index)
        {
            for (Node<T> Node = Head; Node != null; Node = Node.Next)
                array.SetValue(Node.Data, index++);
        }

        public struct Enumarator : IEnumerator<T>
        {
            public T Current { get; private set; }

            object IEnumerator.Current => Current;

            private Stack<T> Stack;

            private Node<T> Node;

            public Enumarator(Stack<T> Stack)
            {
                this.Stack = Stack;

                Node = new Node<T>(default);

                Node.Next = Stack.Head;

                Current = default;
            }

            public void Dispose() { }

            public bool MoveNext()
            {
                if (Node.Next != null)
                {
                    Node = Node.Next;
                    Current = Node.Data;

                    return true;
                }

                else
                    return false;
            }

            public void Reset()
            {
                Node = new Node<T>(default);

                Node.Next = Stack.Head;
                Current = default;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new Enumarator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }}
