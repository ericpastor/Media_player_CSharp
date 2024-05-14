namespace MediaPlayer.Core.src.Entities
{
    public class MediaFileFactory
    {
        public static Audio CreateAudio(string title, string artist, int releaseYear, string genre, TimeSpan duration)
        {
            return new Audio(title, artist, releaseYear, genre, duration);
        }

        public static Video CreateVideo(string title, string director, int releaseYear, string genre, TimeSpan duration)
        {
            return new Video(title, director, releaseYear, genre, duration);
        }
    }
}