namespace Playlists.NET.Model
{
    public class PlsPlaylist : BasePlaylist<PlsPlaylistEntry>
    {
        public int Version { get; set; }
        public int NumberOfEntries { get; set; }
    }
}
