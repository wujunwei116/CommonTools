// ***********************************************************************
// Assembly         : Acon.Collections.Arrays
// Author           : junwei.wu
// Created          : 2018/7/5 14:47:04
//
// Last Modified By : junwei.wu
// Last Modified On : 2018/7/5 14:47:04:
// ***********************************************************************
// <copyright file="ICircularBuffer" company="ACON">
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
    public interface ICircularBuffer<T> : IBuffer<T>
    {
        /// <summary>
        /// 是否自动扩容
        /// </summary>
        bool AllowDilatation { get; }

        bool IsEmpty { get; }

        bool IsFull { get; }

        void Enqueue(T item);

        T Dequeue();

        T Peek();

    }
}
