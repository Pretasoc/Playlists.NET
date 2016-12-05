using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlaylistsNET.Model;
using PlaylistsNET.Utils;
using System.Xml.Linq;

namespace PlaylistsNET.Content
{
    public class ZplContent : IPlaylistContent<ZplPlaylist>
    {
        public string Create(ZplPlaylist playlist)
        {
            StringBuilder sb = new StringBuilder();
            XElement seq = CreateSeqWithMedia(playlist);
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
            sb.AppendLine(@"<?zpl version=""2.0""?>");
            sb.Append(doc.ToString());
            return sb.ToString();
        }

        public ZplPlaylist GetFromStream(Stream stream)
        {
            ZplPlaylist playlist = new ZplPlaylist();

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
                int miliseconds = 0;
                Int32.TryParse(Utils.Utils.UnEscape(media.Attribute("duration")?.Value), out miliseconds);
                TimeSpan duration = TimeSpan.FromMilliseconds(miliseconds);
                playlist.PlaylistEntries.Add(new ZplPlaylistEntry()
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

        public string Update(ZplPlaylist playlist, Stream stream)
        {
            XDocument doc = XDocument.Load(stream);
            var gg = doc.ToString();
            XElement mainDocument = doc.Element("smil");
            XElement title = mainDocument.Element("head").Element("title");
            title.ReplaceWith(new XElement("title", playlist.Title));
            var seq = mainDocument.Elements("body").Elements("seq");
            XElement seqWithMedia = null;
            foreach (var s in seq)
            {
                var m3 = s.Elements("media");
                int i = 0;
                foreach (var a in m3) { i++; }
                if (i > 0)
                {
                    seqWithMedia = s;
                    break;
                }
            }
            if (seqWithMedia != null)
            {
                var newSeq = CreateSeqWithMedia(playlist);
                seqWithMedia.ReplaceWith(newSeq);
            }

            return doc.ToString();
        }

        private XElement CreateSeqWithMedia(ZplPlaylist playlist)
        {
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
                if (entry.Duration != null && entry.Duration != TimeSpan.Zero)
                {
                    XAttribute att = new XAttribute("duration", (int)entry.Duration.TotalMilliseconds);
                    media.Add(att);
                }
                seq.Add(media);
            }
            return seq;
        }
    }
}
