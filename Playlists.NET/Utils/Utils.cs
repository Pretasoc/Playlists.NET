using System;
using System.IO;

namespace Playlists.NET.Utils
{
    public class Utils
    {
        public static string MakeAbsolutePath(string folderPath, string filePath)
        {
            string path = Path.Combine(folderPath, filePath);
            path = Path.GetFullPath(path);
            return path;
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
