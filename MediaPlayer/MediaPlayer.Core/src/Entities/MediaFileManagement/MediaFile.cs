namespace MediaPlayer.Core.src.Entities
{
    public enum FileType
    {
        Audio,
        Video
    }
    public class MediaFile
    {
        private static int _lastId = 0;

        static int GenerateId()
        {
            return Interlocked.Increment(ref _lastId);
        }

        public int id = GenerateId();

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Title { get; set; }
        public string Genre { get; set; }
        public int ReleaseYear { get; set; }
        public TimeSpan Duration { get; set; }
        public FileType FileType { get; protected set; }
        public bool IsPlaying { get; set; }
        public bool IsStopped { get; set; }
        public bool IsPaused { get; set; }

        public MediaFile(string title, int releaseYear, string genre, TimeSpan duration, FileType fileType)
        {
            this.Id = id;
            this.Title = title;
            this.Genre = genre;
            this.Duration = duration;
            this.ReleaseYear = releaseYear;
            this.FileType = fileType;
            this.IsPlaying = false;
            this.IsStopped = true;
            this.IsPaused = false;
        }

        public void Play()
        {
            if (!IsStopped && !IsPaused)
            {
                Console.WriteLine($"{Title} is already playing");
            }
            if (IsPaused)
            {
                IsPlaying = true;
                IsStopped = false;
                IsPaused = false;
                Console.WriteLine($"{Title}: Playing again");
            }
            if (IsStopped)
            {
                IsPlaying = true;
                IsStopped = false;
                IsPaused = false;
                Console.WriteLine($"Playing: {Title}");
            }
        }

        public void Pause()
        {
            IsPlaying = false;
            IsStopped = false;
            IsPaused = true;
            Console.WriteLine($"{Title}: Paused");
        }

        public void Stop()
        {
            IsPlaying = false;
            IsPaused = false;
            IsStopped = true;
            Console.WriteLine($"{Title}: Stopped");
        }
    }
}