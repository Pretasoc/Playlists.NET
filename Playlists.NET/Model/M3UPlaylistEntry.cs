﻿using System;

namespace Playlists.NET.Model
{
    public class M3uPlaylistEntry : BasePlaylistEntry
    {
        public TimeSpan Duration { get; set; }
        public string Title { get; set; }
        public string Album { get; set; }
        public string AlbumArtist { get; set; }
    }
}
