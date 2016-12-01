using System;
using System.IO;
using System.Text;
using PlaylistsNET.Model;
using System.Xml.Linq;

namespace PlaylistsNET.Content
{
    public class WplContent : IPlaylistContent<WplPlaylist>
    {
        public string Create(WplPlaylist playlist)
        {
            StringBuilder sb = new StringBuilder();
            XElement seq = new XElement("seq");
            foreach (var entry in playlist.PlaylistEntries)
            {
                XElement media = new XElement("media");
                XAttribute src = new XAttribute("src", entry.Path);
                media.Add(src);
                if (!String.IsNullOrEmpty(entry.AlbumArtist))
                {
                    XAttribute att = new XAttribute("albumTitle", entry.AlbumTitle);
                    media.Add(att);
                }
                if (!String.IsNullOrEmpty(entry.AlbumArtist))
                {
                    XAttribute att = new XAttribute("albumArtist", entry.AlbumArtist);
                    media.Add(att);
                }
                if (!String.IsNullOrEmpty(entry.TrackTitle))
                {
                    XAttribute att = new XAttribute("trackTitle", entry.TrackTitle);
                    media.Add(att);
                }
                if (!String.IsNullOrEmpty(entry.TrackArtist))
                {
                    XAttribute att = new XAttribute("trackArtist", entry.TrackArtist);
                    media.Add(att);
                }
                seq.Add(media);
            }
            XElement body = new XElement("body");
            body.Add(seq);
            XElement title = new XElement("title", playlist.Title);
            XElement head = new XElement("head");
            head.Add(title);
            XElement smil = new XElement("smil");
            smil.Add(head);
            smil.Add(body);
            XDocument doc = new XDocument();
            doc.Add(smil);
            sb.AppendLine(@"<?wpl version=""1.0""?>");
            sb.Append(doc.ToString());
            return sb.ToString();
        }

        public WplPlaylist GetFromStream(Stream stream)
        {
            WplPlaylist playlist = new WplPlaylist();

            XDocument doc = XDocument.Load(stream);
            XElement mainDocument = doc.Element("smil");
            XElement head = mainDocument.Element("head");
            playlist.Title = (string)head.Element("title") ?? "";
            var mediaElements = mainDocument.Elements("body").Elements("seq").Elements("media");
            foreach (var media in mediaElements)
            {
                string src = Utils.Utils.UnEscape(media.Attribute("src")?.Value);
                string trackTitle = Utils.Utils.UnEscape(media.Attribute("trackTitle")?.Value);
                string trackArtist = Utils.Utils.UnEscape(media.Attribute("trackArtist")?.Value);
                string albumTitle = Utils.Utils.UnEscape(media.Attribute("albumTitle")?.Value);
                string albumArtist = Utils.Utils.UnEscape(media.Attribute("albumArtist")?.Value);
                playlist.PlaylistEntries.Add(new WplPlaylistEntry()
                {
                    AlbumArtist = albumArtist,
                    AlbumTitle = albumTitle,
                    Path = src,
                    TrackArtist = trackArtist,
                    TrackTitle = trackTitle
                });
            }

            return playlist;
        }

        public string Update(WplPlaylist playlist, string contentToUpdate)
        {
            throw new NotImplementedException();
        }

        public void Update(WplPlaylist playlist, Stream stream)
        {
            throw new NotImplementedException();
        }
    }
}
