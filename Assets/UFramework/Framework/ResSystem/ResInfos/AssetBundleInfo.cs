using UnityEngine;
using System.Collections;
using UFramework.Engine.Singleton;
using System.Xml;
using System.Collections.Generic;
using UFramework.Engine.IO;
using UFramework.Framework.ResSystem.Enums;

namespace UFramework.Framework.ResSystem.ResInfos
{
    public class AssetBundleInfo:SingletonT<AssetBundleInfo>
    {
        Dictionary<string, List<string>> _abDic = null;
        public override void OnInstanced()
        {
            ParseMD5File(FilePath.GetResPathInPersistentOrStream(""));
        }



        public ResType GetUrlResType(string url) {
            if (url.StartsWith("Resources/"))
            {
                return ResType.Internal;
            }

            //如果ab配置中有此url则为assetbundle
            if (_abDic != null && _abDic.ContainsKey(url)) {
                return ResType.AssetBundle;
            }

            //如果ab配置中有此url的子字串则为assetbundle中的asset
            foreach (string key in _abDic.Keys) {
                if(url.StartsWith(key) && key.Length != url.Length) {
                    return ResType.Asset;
                }
            }

            return ResType.Invailed;
        }



        Dictionary<string, List<string>> ParseMD5File(string fileName)
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
    }
}

