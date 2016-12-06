using System;
using System.IO;

namespace PlaylistsNET.Utils
{
    public class Utils
    {
        public static string MakeAbsolutePath(string folderPath, string filePath)
        {
            string path = Path.Combine(folderPath, filePath);
            path = Path.GetFullPath(path);
            return path;
        }

        public static String MakeRelativePath(string folderPath, string fileAbdolutePath)
        {
            if (String.IsNullOrEmpty(folderPath)) throw new ArgumentNullException("folderPath");
            if (String.IsNullOrEmpty(fileAbdolutePath)) throw new ArgumentNullException("filePath");

            if (!folderPath.EndsWith(Path.DirectorySeparatorChar.ToString()))
            {
                folderPath = folderPath + Path.DirectorySeparatorChar;
            }

            Uri folderUri = new Uri(folderPath);
            Uri fileAbsoluteUri = new Uri(fileAbdolutePath);

            if (folderUri.Scheme != fileAbsoluteUri.Scheme) { return fileAbdolutePath; } // path can't be made relative.

            Uri relativeUri = folderUri.MakeRelativeUri(fileAbsoluteUri);
            String relativePath = Uri.UnescapeDataString(relativeUri.ToString());

            if (fileAbsoluteUri.Scheme.Equals("file", StringComparison.CurrentCultureIgnoreCase))
            {
                relativePath = relativePath.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
            }

            return relativePath;
        }

        public  static string UnEscape(string content)
        {
            if (content == null) return content;
            return content.Replace("&amp;", "&").Replace("&apos;", "'").Replace("&quot;", "\"").Replace("&gt;", ">").Replace("&lt;", "<");
        }

        public static string Escape(string content)
        {
            if (content == null) return null;
            return content.Replace("&", "&amp;").Replace("'", "&apos;").Replace("\"", "&quot;").Replace(">", "&gt;").Replace("<", "&lt;");
        }
    }
}
