using System;
using System.Collections.Generic;
using System.Linq;

namespace PlaylistsNET.Model
{
    public interface IBasePlaylist<out T> where T : BasePlaylistEntry
    {

    }

    public class BasePlaylist<T> : IBasePlaylist<T> where T : BasePlaylistEntry
    {
        public string Path { get; set; }
        public string FileName { get; set; }
        public List<T> PlaylistEntries { get; set; }
        
        public BasePlaylist()
        {
            PlaylistEntries = new List<T>();
        }
    }
}