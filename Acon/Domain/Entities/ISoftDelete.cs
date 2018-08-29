// ***********************************************************************
// Assembly         : Acon.Domain.Entities
// Author           : junwei.wu
// Created          : 2018/1/9 15:29:08
//
// Last Modified By : junwei.wu
// Last Modified On : 2018/1/9 15:29:08:
// ***********************************************************************
// <copyright file="ISoftDelete" company="ACON">
//     Copyright (c) ACON. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Acon.Domain.Entities
{
    public interface ISoftDelete
    {
        bool IsDeleted { get; set; }
    }
}
