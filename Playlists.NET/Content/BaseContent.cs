using PlaylistsNET.Model;
using System;
using System.IO;

namespace PlaylistsNET.Content
{
    public class BaseContent : IPlaylistContent<BasePlaylist<BasePlaylistEntry>>
    {
        public string Create(BasePlaylist<BasePlaylistEntry> playlist)
        {
            throw new NotImplementedException();
        }

        public BasePlaylist<BasePlaylistEntry> GetFromStream(Stream stream)
        {
            throw new NotImplementedException();
        }

        public string Update(BasePlaylist<BasePlaylistEntry> playlist, string contentToUpdate)
        {
            throw new NotImplementedException();
        }

        public void Update(BasePlaylist<BasePlaylistEntry> playlist, Stream stream)
        {
            throw new NotImplementedException();
        }
    }
}
