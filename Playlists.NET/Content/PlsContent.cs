using System;
using System.IO;
using System.Linq;
using System.Text;
using Playlists.NET.Model;

namespace Playlists.NET.Content
{
    public class PlsContent : IPlaylistContent<PlsPlaylist>
    {
        public string Create(PlsPlaylist playlist)
        {
            StringBuilder sb = new StringBuilder();
            int nr = 0;

            sb.AppendLine("[playlist]");
            sb.AppendLine();
            foreach(var entry in playlist.PlaylistEntries)
            {
                nr++;
                sb.AppendLine(ToFile(entry.Path, nr));
                if (!String.IsNullOrEmpty(entry.Title))
                {
                    sb.AppendLine(ToTitle(entry.Title, nr));
                }
                if (entry.Length != null)
                {
                    sb.AppendLine(ToLength(entry.Length, nr));
                }
                sb.AppendLine();
            }
            sb.Append("NumberOfEntries=").Append(nr).AppendLine();
            sb.AppendLine();
            sb.Append("Version=2");

            return sb.ToString();
        }

        public PlsPlaylist GetFromStream(Stream stream)
        {
            PlsPlaylist playlist = new PlsPlaylist();
            playlist.Version = 2;
            StreamReader streamReader = new StreamReader(stream);
            if (!streamReader.EndOfStream)
            {
                string header = streamReader.ReadLine().Trim();
                if (header.Trim() != "[playlist]")
                {
                    return playlist;
                }
            }
            while (!streamReader.EndOfStream)
            {
                string line = streamReader.ReadLine().Trim();
                int nr = GetNr(line);
                if (line.StartsWith("File"))
                {
                    string path = GetPath(line);
                    playlist.PlaylistEntries.Add(new PlsPlaylistEntry()
                    {
                        Path = path,
                        Nr = nr
                    });
                }
                else if (line.StartsWith("Title"))
                {
                    string title = GetTitle(line);
                    if (!String.IsNullOrEmpty(title))
                    {
                        var entry = playlist.PlaylistEntries.SingleOrDefault(e => e.Nr == nr);
                        entry.Title = title;
                    }
                }
                else if (line.StartsWith("Length"))
                {
                    int? length = GetLength(line);
                    if (length != null)
                    {
                        var entry = playlist.PlaylistEntries.SingleOrDefault(e => e.Nr == nr);
                        entry.Length = length;
                    }
                }
            }
            playlist.PlaylistEntries = playlist.PlaylistEntries.OrderBy(e => e.Nr).ToList();
            return playlist;
        }

        public string Update(PlsPlaylist playlist, string contentToUpdate)
        {
            throw new NotImplementedException();
        }

        public void Update(PlsPlaylist playlist, Stream stream)
        {
            throw new NotImplementedException();
        }

        private string ToFile(string path, int nr)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("File").Append(nr).Append("=").Append(path);
            return sb.ToString();
        }

        private string ToTitle(string title, int nr)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Title").Append(nr).Append("=").Append(title);
            return sb.ToString();
        }

        private string ToLength(int? length, int nr)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Length").Append(nr).Append("=").Append(length);
            return sb.ToString();
        }

        private int GetNr(string line)
        {
            int nr = -1;
            if (line.StartsWith("File"))
            {
                try
                {
                    //0123456
                    //File1=
                    //File10=
                    nr = Int32.Parse(line.Substring(4, line.IndexOf('=') - 4));
                }
                catch { }
            }
            else if (line.StartsWith("Title"))
            {
                try
                {
                    //01234567
                    //Title1=
                    //Title10=
                    nr = Int32.Parse(line.Substring(5, line.IndexOf('=') - 5));
                }
                catch { }
            }
            else if (line.StartsWith("Length"))
            {
                try
                {
                    //012345678
                    //Length1=
                    //Length10=
                    nr = Int32.Parse(line.Substring(6, line.IndexOf('=') - 6));
                }
                catch { }
            }
            return nr;
        }

        private string GetPath(string line)
        {
            string path = null;
            try
            {
                path = line.Substring(line.IndexOf('=') + 1);
            }
            catch { }
            return path;
        }

        private string GetTitle(string line)
        {
            string title = null;
            try
            {
                title = line.Substring(line.IndexOf('=') + 1);
            }
            catch { }
            return title;
        }

        private int? GetLength(string line)
        {
            int? length = null;
            try
            {
                length = Int32.Parse(line.Substring(line.IndexOf('=') + 1));
            }
            catch { }
            return length;
        }
    }
}
