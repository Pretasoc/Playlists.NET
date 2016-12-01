using PlaylistsNET.Model;

namespace PlaylistsNET.Content
{
    public class PlaylistContentFactory
    {
        public IPlaylistContentReader<IBasePlaylist<BasePlaylistEntry>> GetPlaylistContentReader(string fileType)
        {
            IPlaylistContentReader<IBasePlaylist<BasePlaylistEntry>> contentReader;
            fileType = fileType.ToLower();
            switch (fileType)
            {
                case ".m3u":
                    contentReader = new M3uContent();
                    break;
                case ".m3u8":
                    contentReader = new M3u8Content();
                    break;
                case ".pls":
                    contentReader = new PlsContent();
                    break;
                case ".wpl":
                    contentReader = new WplContent();
                    break;
                case ".zpl":
                    contentReader = new ZplContent();
                    break;
                default:
                    contentReader = new BaseContent();
                    break;
            }
            return contentReader;
        }
    }
}
