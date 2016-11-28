namespace Playlists.NET.Model
{
    public class M3uPlaylist : BasePlaylist<M3uPlaylistEntry>
    {
        public bool IsExtended { get; set; }
    }
}
