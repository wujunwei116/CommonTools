// ***********************************************************************
// Assembly         : Acon.Collections.Arrays
// Author           : junwei.wu
// Created          : 2018/7/5 14:45:41
//
// Last Modified By : junwei.wu
// Last Modified On : 2018/7/5 14:45:41:
// ***********************************************************************
// <copyright file="IBuffer" company="ACON">
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
    /// <summary>
    /// interface Buffer
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBuffer<T>
    {
        int Count { get; }

        T this[int index] { get; }

        void Put(T item);

        void Put(T[] array);

        void Put(T[] array, int offset, int count);

        T[] Get(int count);

        int Get(T[] array);

        int Get(T[] array, int offset, int count);

        T[] GetBuffer();

        void Clear();
    }
}
