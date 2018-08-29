// ***********************************************************************
// Assembly         : Acon
// Author           : junwei.wu
// Created          : 2018/4/12 15:56:27
//
// Last Modified By : junwei.wu
// Last Modified On : 2018/4/12 15:56:27:
// ***********************************************************************
// <copyright file="IGuidGenerator" company="ACON">
//     Copyright (c) ACON. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Acon
{
   public interface IGuidGenerator
    {
       Guid NewSequentialGuid();
    }
}
