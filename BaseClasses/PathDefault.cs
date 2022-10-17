using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BaseClasses
{
    public class PathDefault
    {
        public const string cExtensionConfig = ".config";
        public const string cExtensionXML = ".xml";
        public const string cExtensionSchemaXML = ".xsd";
        public const string cExtensionFiguresProjectConfig = ".fgp";
        public const string cExtensionLang = ".lang";

        public static string Get_xsd_FileName(string aXMLFileName)
        {
            return System.IO.Path.ChangeExtension(aXMLFileName, cExtensionSchemaXML);
        }
        public static string TrimFN(string aString, int aMaxLenght)
        {
            if (aString != null && aString.Length > aMaxLenght)
            {
                aString = aString.Substring(0, aMaxLenght - 1) + "~";
            }
            return aString;
        }
        public static string GetFnWithControlLenght(string aPath)
        {
            try
            {
                if (System.IO.Path.GetDirectoryName(aPath).Length > 248)
                {
                    aPath = System.IO.Path.GetDirectoryName(aPath).Substring(0, 247) + "~" + System.IO.Path.GetFileName(aPath);
                }
            }
            catch (Exception ex)
            {
            }
            try
            {
                if (aPath.Length > 260)
                {
                    aPath = aPath.Substring(0, 259 - System.IO.Path.GetExtension(aPath).Length) + "~" + System.IO.Path.GetExtension(aPath);
                }
            }
            catch (Exception ex)
            {
            }
            return aPath;
        }
        public static string GetDirectory(string DirName)
        {
            if (!IOMy.Directory.Exists(DirName))
            {
                try
                {
                    System.IO.Directory.CreateDirectory(DirName);
                }
                catch (Exception ex)
                {
                    DirName = TrimFN(DirName, 248);
                    try
                    {
                        System.IO.Directory.CreateDirectory(DirName);
                    }
                    catch (Exception ex2)
                    {
                        MessageBox.Show("Не удалось создать дирректорию: " + "\r\n" + DirName, "ОШИБКА!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            return DirName;
        }
        public static string GetRootPath()
        {
            return Application.StartupPath;
        }

        public static string GetLangDirName()
        {
            return GetDirectory(IOMy.Path.Combine(Application.StartupPath, "lang"));
        }
        public static string GetLangFileName(string langName)
        {
            return IOMy.Path.Combine(GetLangDirName(), langName + cExtensionLang);
        }

        public static string GetDesktopIniFileName(string directory, bool createDirectoryIfNotExists)
        {
            string GetDesktopIniFileName = "desktop.ini";
            if (directory != null)
            {
                if (createDirectoryIfNotExists)
                {
                    GetDesktopIniFileName = System.IO.Path.Combine(GetDirectory(directory), GetDesktopIniFileName);
                }
                else
                {
                    GetDesktopIniFileName = System.IO.Path.Combine(directory, GetDesktopIniFileName);
                }
            }
            return GetDesktopIniFileName;
        }
        public static string GetDirectoryUser()
        {
            return GetDirectory(System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + System.IO.Path.DirectorySeparatorChar + PathDefault.ReplaceErrorInFileName(Application.CompanyName) + System.IO.Path.DirectorySeparatorChar + PathDefault.ReplaceErrorInFileName(Application.ProductName) + System.IO.Path.DirectorySeparatorChar + PathDefault.ReplaceErrorInFileName(Application.ProductVersion));
        }
        public static string GetDirectoryUser_Icons()
        {
            return PathDefault.GetDirectory(System.IO.Path.Combine(GetDirectoryUser(), "Icons"));
        }

        public static string GetTempRootDirName()
        {
            return GetDirectory(System.IO.Path.Combine(GetRootPath(), "Temp"));
        }
        public static string GetDataDirName()
        {
            return GetDirectory(System.IO.Path.Combine(GetRootPath(), "Data"));
        }
        public static string GetUsersDirName()
        {
            return GetDirectory(System.IO.Path.Combine(GetRootPath(), "Users"));
        }
        public static string GetRecycleDirName()
        {
            return GetDirectory(System.IO.Path.Combine(GetRootPath(), "Recycle"));
        }
        public static string GetTemplatesDirName()
        {
            return GetDirectory(System.IO.Path.Combine(GetDataDirName(), "Templates"));
        }
        public static string GetSettingsDirName()
        {
            return GetDirectory(System.IO.Path.Combine(GetDataDirName(), "Settings"));
        }

        private static string[] GetNames(string[] fullNames)
        {
            string[] _names = fullNames;
            int i;
            for (i = _names.GetLowerBound(0); i <= _names.GetUpperBound(0); i++)
            {
                _names[i] = System.IO.Path.GetFileName(_names[i]);
            }
            return _names;
        }
        public static string[] GetPatternsFileNames()
        {
            return GetNames(GetPatternsFiles());
        }
        public static string[] GetPatternsFiles()
        {
            return System.IO.Directory.GetFiles(GetTemplatesDirName(), "*", System.IO.SearchOption.TopDirectoryOnly);
        }


        private string GetBeginNameFromNow()
        {
            return DateTime.Today.ToString("yyyy-MM-dd") + "_";
        }
        public static string GetTempDirName()
        {
            return GetDirectory(System.IO.Path.Combine(GetRootPath(), "Temp"));
        }
        public static System.String ReplaceErrorInPathOtnositelnyy(System.String path)
        {
            if (path == null)
                return null;
            path = path.Replace(":", "_");
            path = path.Replace("*", "_");
            path = path.Replace("?", "_");
            path = path.Replace("\"", "'");
            path = path.Replace("<", "_");
            path = path.Replace(">", "_");
            path = path.Replace("|", "_");
            return path;
        }
        public static System.String ReplaceErrorInPath(System.String path)
        {
            if (path == null)
                return null;
            path = path.Replace("*", "_");
            path = path.Replace("?", "_");
            path = path.Replace("\"", "'");
            path = path.Replace("<", "_");
            path = path.Replace(">", "_");
            path = path.Replace("|", "_");
            return path;
        }
        public static System.String ReplaceErrorInFileName(System.String fileName)
        {
            if (fileName == null)
                return null;
            fileName = fileName.Replace("\\", "_");
            fileName = fileName.Replace("/", "_");
            fileName = fileName.Replace(":", "_");
            fileName = fileName.Replace("*", "_");
            fileName = fileName.Replace("?", "_");
            fileName = fileName.Replace("\"", "'");
            fileName = fileName.Replace("<", "_");
            fileName = fileName.Replace(">", "_");
            fileName = fileName.Replace("|", "_");
            return fileName;
        }
        public static string ReplaceSecondSpaceInFileName(System.String fileName)
        {
            if (fileName == null)
                return null;
            while (fileName.IndexOf("  ") != -1)
            {
                fileName = fileName.Replace("  ", " ");
            }
            fileName = fileName.TrimStart(" ".ToCharArray()[0]).TrimEnd(" ".ToCharArray()[0]);
            return fileName;
        }
    }

}
