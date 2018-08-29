// ***********************************************************************
// Assembly         : Acon
// Author           : junwei.wu
// Created          : 2018/4/12 15:56:15
//
// Last Modified By : junwei.wu
// Last Modified On : 2018/4/12 15:56:15:
// ***********************************************************************
// <copyright file="SequentialGuidGenerator" company="ACON">
//     Copyright (c) ACON. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Acon
{
    /// <summary>
    /// 有序的GUID
    /// </summary>
    public class SequentialGuidGenerator : IGuidGenerator
    {
        private static readonly RNGCryptoServiceProvider _rng = new RNGCryptoServiceProvider();
        private static readonly object synObject = new object();

        private static SequentialGuidGenerator instance;

        public  SequentialGuidDatabaseType DatabaseType { get; set; } 

        public static SequentialGuidGenerator Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (synObject)
                    {
                        if (instance == null)
                            instance = new SequentialGuidGenerator();
                    }
                }

                return instance;
            }
        }

        #region IGuidGenerator 成员

        public Guid NewSequentialGuid()
        {
            return NewSequentialGuid(DatabaseType);
        }

        public Guid NewSequentialGuid(SequentialGuidDatabaseType databaseType)
        {
            switch (databaseType)
            {
                case SequentialGuidDatabaseType.SqlServer:
                    return NewSequentialGuid(SequentialGuidType.SequentialAtEnd);
                case SequentialGuidDatabaseType.Oracle:
                    return NewSequentialGuid(SequentialGuidType.SequentialAsBinary);
                case SequentialGuidDatabaseType.MySql:
                    return NewSequentialGuid(SequentialGuidType.SequentialAsString);
                case SequentialGuidDatabaseType.PostgreSql:
                    return NewSequentialGuid(SequentialGuidType.SequentialAsString);
                default:
                    throw new InvalidOperationException();

            }
        }
        public Guid NewSequentialGuid(SequentialGuidType guidType)
        {
            byte[] randomBytes = new byte[10];
            _rng.GetBytes(randomBytes);

            long timestamp = DateTime.UtcNow.Ticks / 10000L;
            byte[] timestampBytes = BitConverter.GetBytes(timestamp);

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(timestampBytes);
            }

            byte[] guidBytes = new byte[16];

            switch (guidType)
            {
                case SequentialGuidType.SequentialAsString:
                case SequentialGuidType.SequentialAsBinary:

                    // For string and byte-array version, we copy the timestamp first, followed
                    // by the random data.
                    Buffer.BlockCopy(timestampBytes, 2, guidBytes, 0, 6);
                    Buffer.BlockCopy(randomBytes, 0, guidBytes, 6, 10);

                    // If formatting as a string, we have to compensate for the fact
                    // that .NET regards the Data1 and Data2 block as an Int32 and an Int16,
                    // respectively.  That means that it switches the order on little-endian
                    // systems.  So again, we have to reverse.
                    if (guidType == SequentialGuidType.SequentialAsString && BitConverter.IsLittleEndian)
                    {
                        Array.Reverse(guidBytes, 0, 4);
                        Array.Reverse(guidBytes, 4, 2);
                    }

                    break;

                case SequentialGuidType.SequentialAtEnd:

                    // For sequential-at-the-end versions, we copy the random data first,
                    // followed by the timestamp.
                    Buffer.BlockCopy(randomBytes, 0, guidBytes, 0, 10);
                    Buffer.BlockCopy(timestampBytes, 2, guidBytes, 10, 6);
                    break;
            }

            return new Guid(guidBytes);
        }

        
        #endregion


    }
    public enum SequentialGuidDatabaseType
    {
        SqlServer,

        Oracle,

        MySql,

        PostgreSql,
    }

    public enum SequentialGuidType
    {
        /// <summary>
        /// The GUID should be sequential when formatted using the
        /// <see cref="Guid.ToString()" /> method.
        /// </summary>
        SequentialAsString,

        /// <summary>
        /// The GUID should be sequential when formatted using the
        /// <see cref="Guid.ToByteArray" /> method.
        /// </summary>
        SequentialAsBinary,

        /// <summary>
        /// The sequential portion of the GUID should be located at the end
        /// of the Data4 block.
        /// </summary>
        SequentialAtEnd
    }


}
