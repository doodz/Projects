﻿namespace YggClientCore
{
    public class YggTorrentItem
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public int Comments { get; set; }
        public string UploadedBy { get; set; }
        public string UrlUploader { get; set; }

        public int TorrentId { get; set; }

        public string Age { get; set; }
        public string Size { get; set; }
        public int Seeders { get; set; }
        public int Leechers { get; set; }

    }
}
