using System;
using System.IO;

namespace Playlists.NET.Utils
{
    public class Utils
    {
        public static string MakeAbsolutePath(string filePath, string folderPath)
        {
            return new Uri(new Uri(folderPath), filePath).ToString();
        }

        public  static string CreateFullFilePath(string filePath, string folderPath)
        {
            string fullpath = "";
            if (folderPath.EndsWith(@"\"))
            {
                folderPath = folderPath.Remove(folderPath.Length - 1);
            }
            if (filePath.StartsWith(@"\"))
            {
                fullpath = folderPath + filePath;
            }
            else
            {
                bool isRooted = false;
                try
                {
                    isRooted = Path.IsPathRooted(filePath);
                }
                catch (Exception)
                { }
                if (isRooted)
                {
                    fullpath = filePath;
                }
                else if (filePath.StartsWith(@"..\"))
                {
                    try
                    {
                        Uri uri = new Uri(new Uri(folderPath), filePath);
                        fullpath = uri.ToString();
                    }
                    catch (Exception)
                    { }
                }
                else
                {
                    fullpath = folderPath + @"\" + filePath;
                }
            }
            return fullpath;
        }

        public  static string UnEscape(string content)
        {
            if (content == null) return content;
            return content.Replace("&amp;", "&").Replace("&apos;", "'").Replace("&quot;", @"""").Replace("&gt;", ">").Replace("&lt;", "<");
        }

        public static string Escape(string content)
        {
            if (content == null) return null;
            return content.Replace("&", "&amp;").Replace("<", "&lt;");
        }
    }
}
