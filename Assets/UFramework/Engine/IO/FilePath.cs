﻿using System.Collections.Generic;
using UnityEngine;
using System.IO;
namespace UFramework.Engine.IO
{
    public class FilePath
    {
        private static string           m_PersistentDataPath;
        private static string           m_StreamingAssetsPath;
        
        
        private static string           m_PersistentDataPath_Res;
        private static string           m_PersistentDownloadCachePath;
        // 外部目录  
        public static string persistentDataPath
        {
            get
            {
                if (null == m_PersistentDataPath)
                {
                    m_PersistentDataPath = Application.persistentDataPath + "/";
                }

                return m_PersistentDataPath;
            }
        }

        // 内部目录
        public static string streamingAssetsPath
        {
            get
            {
                if (null == m_StreamingAssetsPath)
                {
#if UNITY_IPHONE && !UNITY_EDITOR
                    m_StreamingAssetsPath = Application.streamingAssetsPath + "/";
#elif UNITY_ANDROID && !UNITY_EDITOR
                    m_StreamingAssetsPath = Application.streamingAssetsPath + "/";
#elif (UNITY_STANDALONE_WIN) && !UNITY_EDITOR
                    m_StreamingAssetsPath = Application.streamingAssetsPath + "/";//GetParentDir(Application.dataPath, 2) + "/BuildRes/";
#elif UNITY_STANDALONE_OSX && !UNITY_EDITOR
                    m_StreamingAssetsPath = Application.streamingAssetsPath + "/";
#else
                    m_StreamingAssetsPath = Application.streamingAssetsPath + "/";
                    m_StreamingAssetsPath = m_StreamingAssetsPath.Replace("\\", "/");
#endif
                }

                return m_StreamingAssetsPath;
            }
        }

        public static string persistentDownloadCachePath
        {
            get
            {
                if (null == m_PersistentDownloadCachePath)
                {
                    m_PersistentDownloadCachePath = persistentDataPath + "Download/";

                    if (!Directory.Exists(m_PersistentDownloadCachePath))
                    {
                        Directory.CreateDirectory(m_PersistentDownloadCachePath);
#if UNITY_IPHONE && !UNITY_EDITOR
                        UnityEngine.iOS.Device.SetNoBackupFlag(m_PersistentDownloadCachePath);
#endif
                    }
                }

                return m_PersistentDownloadCachePath;
            }
        }

        public static string GetPersistentPath(string subFolder)
        {
            string resultPath = persistentDataPath + subFolder;

            if (!Directory.Exists(resultPath))
            {
                Directory.CreateDirectory(resultPath);
#if UNITY_IPHONE && !UNITY_EDITOR
                        UnityEngine.iOS.Device.SetNoBackupFlag(resultPath);
#endif
            }

            return resultPath;
        }

        // 外部资源目录
        public static string persistentDataPath_Res
        {
            get
            {
                if (null == m_PersistentDataPath_Res)
                {
                    m_PersistentDataPath_Res = persistentDataPath + "Res/";

                    if (!Directory.Exists(m_PersistentDataPath_Res))
                    {
                        Directory.CreateDirectory(m_PersistentDataPath_Res);
#if UNITY_IPHONE && !UNITY_EDITOR
                        UnityEngine.iOS.Device.SetNoBackupFlag(persistentDataPath_Res);
#endif
                    }
                }

                return m_PersistentDataPath_Res;
            }
        }

        // 资源路径，优先返回外存资源路径
        public static string GetResPathInPersistentOrStream(string relativePath)
        {
            string resPersistentPath = string.Format("{0}{1}", FilePath.persistentDataPath_Res, relativePath);

            if (File.Exists(resPersistentPath))
            {
                return resPersistentPath;
            }
            else
            {
                return FilePath.streamingAssetsPath + relativePath;
            }
        }

        // 上一级目录
        public static string GetParentDir(string dir, int floor = 1)
        {
            string subDir = dir;

            for (int i = 0; i < floor; ++i)
            {
                int last = subDir.LastIndexOf('/');
                subDir = subDir.Substring(0, last);
            }

            return subDir;
        }

        public static void GetFileInFolder(string dirName, string fileName, List<string> outResult)
        {
            if (outResult == null)
            {
                return;
            }

            DirectoryInfo dir = new DirectoryInfo(dirName);

            if (null != dir.Parent && dir.Attributes.ToString().IndexOf("System") > -1)
            {
                return;
            }

            FileInfo[] finfo = dir.GetFiles();
            string fname = string.Empty;
            for (int i = 0; i < finfo.Length; i++)
            {
                fname = finfo[i].Name;

                if (fname == fileName)
                {
                    outResult.Add(finfo[i].FullName);
                }
            }

            DirectoryInfo[] dinfo = dir.GetDirectories();
            for (int i = 0; i < dinfo.Length; i++)
            {
                GetFileInFolder(dinfo[i].FullName, fileName, outResult);
            }
        }

        public static void GetFolderInFolder(string dirName, string fileName, List<string> outResult)
        {
            if (outResult == null)
            {
                return;
            }

            DirectoryInfo dir = new DirectoryInfo(dirName);

            if (null != dir.Parent && dir.Attributes.ToString().IndexOf("System") > -1)
            {
                return;
            }
            
            string fname = string.Empty;
            
            DirectoryInfo[] dinfo = dir.GetDirectories();
            for (int i = 0; i < dinfo.Length; i++)
            {
                fname = dinfo[i].Name;
                if (fname == fileName)
                {
                    outResult.Add(dinfo[i].FullName);
                }
                GetFolderInFolder(dinfo[i].FullName, fileName, outResult);
            }
        }

    }
}