// ***********************************************************************
// Assembly         : Acon.Collections.Extensions
// Author           : junwei.wu
// Created          : 2018/7/5 14:56:28
//
// Last Modified By : junwei.wu
// Last Modified On : 2018/7/5 14:56:28:
// ***********************************************************************
// <copyright file="ByteExtensions" company="ACON">
//     Copyright (c) ACON. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Acon.Collections.Extensions
{
   public static  class ByteExtensions
    {
       public static string HexString(this byte[] bytes)
       {
          return HexString(bytes, bytes.Length);
       }

       public static string HexString(this byte[] bytes, int len)
       {
           return DataFormatProcessor.BytesToHexString(bytes, len);
       }
    }
}
