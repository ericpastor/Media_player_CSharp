namespace MediaPlayer.Core.src.Entities
{
    public class Video : MediaFile
    {
        public string Director { get; set; }

        public Video(string title, string director, int releaseYear, string genre, TimeSpan duration)
        : base(title, releaseYear, genre, duration, FileType.Video)
        {
            this.Director = director;
        }

        public override string ToString()
        {
            return $"ID: {Id}\n Title: {Title}\n Artist:{Director}\n Release year: {ReleaseYear}\n Genre: {Genre}\n Release year: {ReleaseYear}\n Duration: {Duration}\n";
        }
    }
}