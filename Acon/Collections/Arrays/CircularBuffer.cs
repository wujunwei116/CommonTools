// ***********************************************************************
// Assembly         : Acon.Collections.Arrays
// Author           : junwei.wu
// Created          : 2018/7/5 14:48:36
//
// Last Modified By : junwei.wu
// Last Modified On : 2018/7/5 14:48:36:
// ***********************************************************************
// <copyright file="CircularBuffer" company="ACON">
//     Copyright (c) ACON. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Acon.Collections.Arrays
{
    public class CircularBuffer<T> : ICircularBuffer<T>
    {
        private T[] buffer;

        private int front; //队头
        private int rear;  //队尾，始终指向空
        private int capacity;
        public CircularBuffer(int capacity, bool allowDilatition)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException("capacity", "must be positive");
            this.capacity = capacity;
            this.AllowDilatation = allowDilatition;

            buffer = new T[capacity];
            front = rear = 0;
        }
        public CircularBuffer() :
            this(128, false)
        {

        }


        #region IBuffer<T> 成员

        public int Count
        {
            get { return (this.rear - this.front + this.capacity) % this.capacity; }
        }


        public T this[int index]
        {
            get
            {
                if (index >= Count) throw new IndexOutOfRangeException();
                int pos = (index + front) % capacity;
                return buffer[pos];
            }
        }

        public void Put(T item)
        {
            Enqueue(item);
        }

        public void Put(T[] array)
        {
            Put(array, 0, array.Length);
        }

        public void Put(T[] array, int offset, int count)
        {
            if (array == null || array.Length < offset || array.Length < (offset + count)) throw new IndexOutOfRangeException();
            for (int i = 0; i < count; i++, offset++)
            {
                Enqueue(array[offset]);
            }
        }

        public T[] Get(int count)
        {
            T[] array = new T[count];
            Get(array);
            return array;
        }

        public int Get(T[] array)
        {
            return Get(array, 0, array.Length);
        }

        public int Get(T[] array, int offset, int count)
        {
            int realCount = Math.Min(count, Count);
            int dstIndex = offset;
            for (int i = 0; i < realCount; i++, dstIndex++)
            {
                array[dstIndex] = Dequeue();
            }
            return realCount;
        }

        public T[] GetBuffer()
        {
            return buffer;
        }

        public void Clear()
        {
            front = 0;
            rear = 0;
        }


        #endregion

        #region ICircularBuffer<T> 成员

        public virtual bool AllowDilatation { get; protected set; }

        public bool IsEmpty
        {
            get { return front == rear; }
        }

        public bool IsFull
        {
            get { return ((this.rear + 1) % this.capacity) == this.front; }
        }

        public void Enqueue(T item)
        {
            if (!AllowDilatation && IsFull) throw new InvalidOperationException("Buffer is full");
            if (AllowDilatation && IsFull) Expand();

            buffer[rear] = item;
            rear = (rear + 1) % capacity;
        }

        /// <summary>
        /// 扩容
        /// </summary>
        protected virtual void Expand()
        {
            int newlength = capacity * 2;
            T[] newarray = new T[newlength];
            if (front <= rear)
            {
                Array.Copy(buffer, front, newarray, 0, Count);
            }
            else
            {
                int half = buffer.Length - front;
                Array.Copy(buffer, front, newarray, 0, half);
                Array.Copy(buffer, 0, newarray, half, Count - half);
            }

            front = 0;
            rear = Count;
            capacity = newlength;
            buffer = newarray;
        }

        public T Dequeue()
        {
            if (IsEmpty) throw new InvalidOperationException("Buffer is empty");
            var item = buffer[front];
            front = (front + 1) % capacity;
            return item;
        }

        #endregion

        #region ICircularBuffer<T> 成员

        public T Peek()
        {
            return this.buffer[front];
        }

        #endregion


    }
}
