﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Translator.Crypto;

namespace BooruDatasetTagManager
{
    public static class Extensions
    {

        public static void AddRange(this List<TagValue> list, IEnumerable<string> range)
        {
            foreach (var item in range)
                list.Add(new TagValue(item));
        }

        public static long GetHash(this string text)
        {
            return Adler32.GenerateHash(text);
        }

        public static object LoadDataSet(string path)
        {
            MemoryStream ms = new MemoryStream(File.ReadAllBytes(path));
            var obj = new BinaryFormatter().Deserialize((Stream)ms);
            ms.Close();
            return obj;
        }

        public static object LoadDataSet(byte[] data)
        {
            MemoryStream ms = new MemoryStream(data);
            var obj = new BinaryFormatter().Deserialize((Stream)ms);
            ms.Close();
            return obj;
        }


        public static void SaveDataSet(object lst, string path)
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ms, lst);
            File.WriteAllBytes(path, ms.ToArray());
            ms.Close();
        }

        public static byte[] SaveDataSet(object objItem)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, objItem);
                return ms.ToArray();
            }
        }

        public static string GetBetween(this string strSource, string strStart, string strEnd)
        {
            const int kNotFound = -1;

            var startIdx = strSource.IndexOf(strStart);
            if (startIdx != kNotFound)
            {
                startIdx += strStart.Length;
                var endIdx = strSource.IndexOf(strEnd, startIdx);
                if (endIdx > startIdx)
                {
                    return strSource.Substring(startIdx, endIdx - startIdx);
                }
            }
            return String.Empty;
        }

        public static string GetBetween(this string strSource, string strStart, string strEnd, int startIndex)
        {
            const int kNotFound = -1;

            var startIdx = strSource.IndexOf(strStart, startIndex);
            if (startIdx != kNotFound)
            {
                startIdx += strStart.Length;
                var endIdx = strSource.IndexOf(strEnd, startIdx);
                if (endIdx > startIdx)
                {
                    return strSource.Substring(startIdx, endIdx - startIdx);
                }
            }
            return String.Empty;
        }
    }
}
