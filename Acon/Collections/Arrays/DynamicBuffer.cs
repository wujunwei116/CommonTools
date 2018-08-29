// ***********************************************************************
// Assembly         : Acon.Collections.Arrays
// Author           : junwei.wu
// Created          : 2018/7/5 14:48:00
//
// Last Modified By : junwei.wu
// Last Modified On : 2018/7/5 14:48:00:
// ***********************************************************************
// <copyright file="DynamicBuffer" company="ACON">
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
    public class DynamicBuffer<T> : IBuffer<T>
    {
        private T[] buffer;
        public int Capacity { get; private set; }

        protected int ReserveCount { get { return Capacity - Count; } }


        public DynamicBuffer(int capacity)
        {
            if (capacity == 0) capacity = 128;

            buffer = new T[capacity];
            Count = 0;
            this.Capacity = capacity;
        }


        #region IBuffer<T> 成员

        public int Count { get; private set; }


        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= Capacity)
                    throw new IndexOutOfRangeException();

                return buffer[index];
            }
        }

        public void Put(T item)
        {
            if (Count >= Capacity - 1) throw new IndexOutOfRangeException();

            buffer[Count] = item;
            Count++;
        }

        public void Put(T[] array)
        {
            Put(array, 0, array.Length);
        }

        public void Put(T[] array, int offset, int count)
        {
            if (ReserveCount < count) Expand(Count+count);
            Array.Copy(array, offset, buffer, Count, count);
            Count = Count + count;
        }

        protected virtual void Expand(int newlength)
        {
            //int newlength = Capacity * 2;
            T[] newarray = new T[newlength];
            Array.Copy(buffer, 0, newarray, 0, Count);
            Capacity = newlength;
            buffer = newarray;
        }

        public T[] Get(int count)
        {
            T[] temp = new T[count];
            Get(temp);
            return temp;
        }

        public int Get(T[] array)
        {
            return Get(array, 0, array.Length);
        }

        public int Get(T[] array, int offset, int count)
        {
            int realCount = Math.Min(count, Count);
            int dstIndex = offset;
            Array.Copy(buffer, 0, array, offset, realCount);
            Remove(realCount);
            return realCount;
        }

        public T[] GetBuffer()
        {
            return buffer;
        }

        public void Clear()
        {
            Count = 0;
        }

        protected virtual void Remove(int count)
        {
            if (count >= Count)
                Count = 0;
            else
            {
                for (int i = 0; i < Count - count; i++) //否则后面的数据往前移
                {
                    buffer[i] = buffer[count + i];
                }
                Count = Count - count;
            }
        }

        #endregion
    }
}
