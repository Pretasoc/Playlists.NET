namespace Playlists.NET.Model
{
    public class ZplPlaylist : BasePlaylist<ZplPlaylistEntry>
    {
        public string Title { get; set; }
        public int ItemCount { get; set; }
    }
}
