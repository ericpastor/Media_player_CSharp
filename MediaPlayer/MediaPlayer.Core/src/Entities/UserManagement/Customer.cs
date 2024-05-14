namespace MediaPlayer.Core.src.Entities
{
    public class Customer : User
    {
        public List<MediaFile> Playlist { get; set; }

        public Customer(string password, string fullName, string email, List<MediaFile> playList)
            : base(password, fullName, email, Role.Customer)
        {
            Playlist = playList ?? new List<MediaFile>();
        }

        public override string ToString()
        {
            return $"ID: {Id}\n Name: {FullName}\n Email: {Email}\n";
        }

        public void AddToPlaylist(MediaFile mediaFile)
        {
            if (!Playlist.Contains(mediaFile))
            {
                Playlist.Add(mediaFile);
            }
            else
            {
                Console.WriteLine($"Already on the list");
            }
        }

        public void RemoveFromPlaylist(MediaFile mediaFile)
        {
            if (!Playlist.Contains(mediaFile))
            {
                Console.WriteLine($"File not found");
            }
            else
            {
                Playlist.Remove(mediaFile);
            }
        }
    }
}