// ***********************************************************************
// Assembly         : Acon.XML
// Author           : junwei.wu
// Created          : 2018/8/13 16:37:56
//
// Last Modified By : junwei.wu
// Last Modified On : 2018/8/13 16:37:56:
// ***********************************************************************
// <copyright file="XMLHelper" company="ACON">
//     Copyright (c) ACON. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Acon.XML
{
    public static class XMLHelper
    {
        public static string Warehouse { get; set; }
        static XMLHelper()
        {
            Warehouse = "Xml";
        }

        #region 序列化
        /// <summary>
        /// XML序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="o"></param>
        /// <param name="isnamespaces">是否需要命名空间true：需要 false:不需要</param>
        /// <returns></returns>
        public static string XmlSerializer<T>(this T o, bool isnamespaces) where T : class
        {
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(o.GetType());

            XmlWriterSettings settings = CreateXmlWriterSettings(isnamespaces);

            MemoryStream w = new MemoryStream();

            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add("", "");

            using (XmlWriter writer = XmlWriter.Create(w, settings))
            {
                serializer.Serialize(writer, o, namespaces);
                writer.Close();
            }

            return Encoding.UTF8.GetString(w.ToArray());
        }


        /// <summary>
        /// XML反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="XmlString"></param>
        /// <returns></returns>
        public static T XmlDeserialize<T>(this string XmlString)
        {
            if (string.IsNullOrEmpty(XmlString))
                throw new ArgumentNullException("s");

            XmlSerializer mySerializer = new XmlSerializer(typeof(T));

            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(XmlString)))
            {
                using (StreamReader sr = new StreamReader(ms, Encoding.UTF8))
                {
                    return (T)mySerializer.Deserialize(sr);
                }
            }
        }

        #endregion

        #region 文件存储
        public static void Save<T>(string fileName, T entity, bool isnamespaces = true) where T : class
        {
            var stopwatch = Stopwatch.StartNew();
            if (!Directory.Exists(Warehouse))
            {
                Directory.CreateDirectory(Warehouse);
            }
            fileName = Path.Combine(Warehouse, fileName);
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            XmlWriterSettings settings = CreateXmlWriterSettings(isnamespaces);
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add("", "");
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(entity.GetType());

            using (XmlWriter writer = XmlWriter.Create(fileName, settings))
            {
                serializer.Serialize(writer, entity);
            }
            stopwatch.Stop();
            Debug.WriteLine(string.Format("XMLHelper Save: executed in {0} milliseconds.",
              stopwatch.Elapsed.TotalMilliseconds.ToString("0.000")));
        }

        public static T Read<T>(string fileName) where T : class
        {
            T entity = default(T);

            fileName = Path.Combine(Warehouse, fileName);
            Type type = Type.GetType(typeof(T).FullName);
            if (!File.Exists(fileName))
                return entity;

            XmlSerializer mySerializer = new XmlSerializer(typeof(T));

            using (StreamReader sr = new StreamReader(fileName, Encoding.UTF8))
            {
                return (T)mySerializer.Deserialize(sr);
            }

        }

        private static XmlWriterSettings CreateXmlWriterSettings(bool isnamespaces)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.NewLineChars = "\r\n";
            settings.Encoding = Encoding.UTF8;
            settings.IndentChars = "   ";

            //不生成声明头
            settings.OmitXmlDeclaration = !isnamespaces;

            return settings;
        }
        #endregion
    }
}
