using PlaylistsNET.Model;
using System.IO;

namespace PlaylistsNET.Content
{
    public interface IPlaylistContentReader<out T> where T : IBasePlaylist<BasePlaylistEntry>
    {
        T GetFromStream(Stream stream);
    }

    public interface IPlaylistContent<T> : IPlaylistContentReader<T> where T : IBasePlaylist<BasePlaylistEntry>
    {
        string Create(T playlist);
        string Update(T playlist, Stream stream);
    }
}
