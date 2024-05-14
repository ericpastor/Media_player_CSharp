namespace MediaPlayer.Core.src.Entities
{
    public class Audio : MediaFile
    {
        public string Artist { get; set; }

        public Audio(string title, string artist, int releaseYear, string genre, TimeSpan duration)
        : base(title, releaseYear, genre, duration, FileType.Audio)
        {
            this.Artist = artist;
        }

        public override string ToString()
        {
            return $"ID: {Id}\n Title: {Title}\n Artist:{Artist}\n Release year: {ReleaseYear}\n Genre: {Genre}\n Release year: {ReleaseYear}\n Duration: {Duration}\n";
        }
    }
}