using System;
using System.Collections.Generic;
using System.Text;

namespace Generic_Stack
{
    class Node<T>
    {
        public T Data { get; }

        public Node<T> Next { get; set; }

        public Node(T Data)
        {
            this.Data = Data;
            Next = null;
        }
    }
}