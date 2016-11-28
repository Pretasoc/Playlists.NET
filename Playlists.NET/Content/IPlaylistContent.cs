using Playlists.NET.Model;
using System.IO;

namespace Playlists.NET.Content
{
    public interface IPlaylistContentReader<out T> where T : IBasePlaylist<BasePlaylistEntry>
    {
        T GetFromStream(Stream stream);
    }

    public interface IPlaylistContent<T> : IPlaylistContentReader<T> where T : IBasePlaylist<BasePlaylistEntry>
    {
        string Create(T playlist);
        void Update(T playlist, Stream stream);
        string Update(T playlist, string contentToUpdate);
    }
}
