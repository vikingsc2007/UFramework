using UnityEngine;
using UnityEditor;
using System.IO;
using System.Xml;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System;

public class CreatMD5
{

    private static MD5CryptoServiceProvider m_MD5Generator = new MD5CryptoServiceProvider();
    public static void CreatMD5XML(string outputPath)
    {
        Dictionary<string, List<string>> DicFileMD5 = new Dictionary<string, List<string>>();
        WritePathMD5ToList(outputPath, outputPath, DicFileMD5);
        if (Directory.Exists(outputPath) == false)
            Directory.CreateDirectory(outputPath);

        XmlDocument XmlDoc = new XmlDocument();
        XmlElement XmlRoot = XmlDoc.CreateElement("Files");
        XmlDoc.AppendChild(XmlRoot);
        foreach (KeyValuePair<string, List<string>> pair in DicFileMD5)
        {
            XmlElement xmlElem = XmlDoc.CreateElement("File");
            XmlRoot.AppendChild(xmlElem);


            xmlElem.SetAttribute("name", pair.Key);
            xmlElem.SetAttribute("md5", pair.Value[0]);
            xmlElem.SetAttribute("tm", pair.Value[2]);
            xmlElem.SetAttribute("size", pair.Value[1]);
        }


        XmlDoc.Save(outputPath + "/versions.xml");
        XmlDoc = null;

        AssetDatabase.Refresh();
    }


    private static void WritePathMD5ToList(string rootdir, string path, Dictionary<string, List<string>> dicFileMD5) {

        foreach (string filePath in Directory.GetFiles(path))
        {
            if (filePath.Contains(".meta") || filePath.Contains("VersionMD5") || filePath.Contains(".xml"))
                continue;
            FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            byte[] hash = m_MD5Generator.ComputeHash(file);
            List<string> tempList = new List<string>();
            string strMD5 = "";
            // 循环遍历哈希数据的每一个字节并格式化为十六进制字符串  
            for (int i = 0; i < hash.Length; i++)
            {
                strMD5 += hash[i].ToString("x2");
            }

            string crt = DateTimeToUnixTimestamp(File.GetLastWriteTimeUtc(filePath)).ToString();
            string size = file.Length.ToString();
            tempList.Add(strMD5);
            tempList.Add(size);
            tempList.Add(crt);
            file.Close();
            string key = filePath.Substring(rootdir.Length + 1);
            if (dicFileMD5.ContainsKey(key) == false)
                dicFileMD5.Add(key, tempList);
            else
                Debug.LogWarning("<Two File has the same name> name = " + filePath);
        }

        foreach (string filedir in Directory.GetDirectories(path)) {
            WritePathMD5ToList(rootdir,filedir, dicFileMD5);
        }
    }

    static Dictionary<string, List<string>> ReadMD5File(string fileName)
    {
        Dictionary<string, List<string>> DicMD5 = new Dictionary<string, List<string>>();
        // 如果文件不存在，则直接返回
        if (System.IO.File.Exists(fileName) == false)
            return DicMD5;
        XmlDocument XmlDoc = new XmlDocument();
        XmlDoc.Load(fileName);
        XmlElement XmlRoot = XmlDoc.DocumentElement;


        foreach (XmlNode node in XmlRoot.ChildNodes)
        {
            if ((node is XmlElement) == false)
                continue;


            List<string> tempList = new List<string>();
            string file = (node as XmlElement).GetAttribute("name");
            string md5 = (node as XmlElement).GetAttribute("md5");
            string size = (node as XmlElement).GetAttribute("size");
            string tm = (node as XmlElement).GetAttribute("tm");
            tempList.Add(md5);
            tempList.Add(size);
            tempList.Add(tm);

            if (DicMD5.ContainsKey(file) == false)
            {
                DicMD5.Add(file, tempList);
            }
        }
        XmlRoot = null;
        XmlDoc = null;
        return DicMD5;
    }


    /// <summary>
    /// 日期转换成unix时间戳
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static long DateTimeToUnixTimestamp(DateTime dateTime)
    {
        var start = new DateTime(1970, 1, 1, 0, 0, 0, dateTime.Kind);
        return Convert.ToInt64((dateTime - start).TotalSeconds);
    }

    /// <summary>
    /// unix时间戳转换成日期
    /// </summary>
    /// <param name="unixTimeStamp">时间戳（秒）</param>
    /// <returns></returns>
    public static DateTime UnixTimestampToDateTime(DateTime target, long timestamp)
    {
        var start = new DateTime(1970, 1, 1, 0, 0, 0, target.Kind);
        return start.AddSeconds(timestamp);
    }

}