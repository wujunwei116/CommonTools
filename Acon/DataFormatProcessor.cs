// ***********************************************************************
// Assembly         : Acon
// Author           : junwei.wu
// Created          : 2018/7/5 14:57:53
//
// Last Modified By : junwei.wu
// Last Modified On : 2018/7/5 14:57:53:
// ***********************************************************************
// <copyright file="DataFormatProcessor" company="ACON">
//     Copyright (c) ACON. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Acon
{
    /// <summary>
    /// 进制转换，通信时常用。
    /// </summary>
    public class DataFormatProcessor
    {
        /// <summary>
        /// 字节转16进制字符串
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string ByteToHexChar(byte val)
        {
            return val.ToString("X2");  //Convert.ToString(input, 16).PadLeft(2, '0'); 
        }

        /// <summary>
        /// 字节转2进制字符串
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string ByteToBitString(byte val)
        {
            //Convert.ToString(input, 2).PadLeft(8, '0'); 可调用该方法

            if (val == 0)
                return "00000000";
            else if (val == 255)
                return "11111111";
            string ret = string.Empty;
            int bitval = 0;
            for (int bitidx = 7; bitidx > -1; --bitidx)
            {
                bitval = val & (1 << bitidx);
                if (bitval > 0)
                    ret = ret + "1";
                else
                    ret = ret + "0";

            }
            return ret;
        }


        /// <summary>
        /// 2个16进制字符串转换一个byte
        /// </summary>
        /// <param name="hexStr"></param>
        /// <returns></returns>
        public static byte HexStringToByte(string hexStr)
        {
            byte byteVal = 0;
            if (hexStr.Length != 2)
            {
                return byteVal;
            }
            byteVal = Convert.ToByte(hexStr, 16);
            return byteVal;
        }

        /// <summary>
        /// 2进制字符串转字节，二进制字符串长度为8. 如：00001100
        /// </summary>
        /// <param name="bitStr"></param>
        /// <returns></returns>
        public static byte BitStringToByte(string bitStr)
        {
            byte byteVal = 0;
            if (bitStr.Length != 8)
            {
                return byteVal;
            }
            byteVal = Convert.ToByte(bitStr, 2);
            return byteVal;
        }

        /// <summary>
        /// 16进制字符串转字节组
        /// </summary>
        /// <param name="hexStr"></param>
        /// <returns></returns>
        public static byte[] HexStringToBytes(string hexStr)
        {
            hexStr = Regex.Replace(hexStr, @"\s", "");
            byte[] bytes = null;
            if (hexStr == null || hexStr.Length < 2)
            {
                return bytes;
            }

            if (hexStr.Length % 2 == 1) { return bytes; }
            bytes = new Byte[hexStr.Length / 2];
            for (int bidx = 0; bidx < hexStr.Length / 2; bidx++)
            {
                bytes[bidx] = Convert.ToByte(hexStr.Substring(bidx * 2, 2), 16);
            }
            return bytes;
        }


        /// 字节组转16进制字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string BytesToHexString(byte[] bytes)
        {
            return BytesToHexString(bytes, bytes.Length);
        }

        public static string BytesToHexString(byte[] bytes, int len)
        {
            if (bytes == null || bytes.Length < 1)
                return string.Empty;
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < len; i++)
            {

                builder.Append(i > 0 ? " " + bytes[i].ToString("X2") : bytes[i].ToString("X2"));
            }
            return builder.ToString();
        }
    }
}
