using MediaPlayer.Core.src.Entities;
using MediaPlayer.Core.src.Interfaces;

namespace MediaPlayer.Framework.src
{
    public sealed class Store

    {
        public List<MediaFile> MediaFiles { get; set; }
        public List<User> Users { get; set; }
        public List<IObserver> Observers;

        private static readonly Lazy<Store> lazyInstance = new Lazy<Store>(() => new Store());
        public static Store Instance => lazyInstance.Value;

        public Store()

        {
            MediaFiles = new List<MediaFile>
        {
            MediaFileFactory.CreateAudio("Callejón de la Luna", "Vicente Amigo", 2000, "Flamenco", new TimeSpan(0, 9, 32)),
            MediaFileFactory.CreateAudio("Old Love", "Eric Clapton", 1992, "Rock", new TimeSpan(0, 7, 32)),
            MediaFileFactory.CreateAudio("About A Girl", "Kurt Cobain", 1994, "Pazz & Jop", new TimeSpan(0, 9, 32)),
            MediaFileFactory.CreateAudio("Where Did You Sleep Last Night", "Pazz & Jop", 1994, " Pazz & Jop", new TimeSpan(0, 4, 32)),
            MediaFileFactory.CreateAudio("The Man Who Sold the World", "David Bowie", 1970, "Pop", new TimeSpan(0, 5, 38)),
            MediaFileFactory.CreateAudio("Tears in Heaven", "Eric Clapton", 1992, "Pop", new TimeSpan(0, 9, 32)),
            MediaFileFactory.CreateAudio("Cordoba", "Vicente Amigo", 2000, "Flamenco", new TimeSpan(0, 4, 32)),
            MediaFileFactory.CreateAudio("Bolero de Vicente", "Vicente Amigo", 2000, "Flamenco", new TimeSpan(0, 9, 32)),
            MediaFileFactory.CreateAudio("Fortunate Son", "John Fogerty", 1969, "Rock", new TimeSpan(0, 9, 32)),
            MediaFileFactory.CreateAudio("Run Through the Jungle", "John Fogerty", 1971, "Rock", new TimeSpan(0, 9, 32)),

            MediaFileFactory.CreateVideo("Movie 1", "John Dir", 2000, "Terror", new TimeSpan(1, 9, 32)),
            MediaFileFactory.CreateVideo("Movie 2", "Ernst Air", 1992, "Adventure", new TimeSpan(2, 7, 32)),
            MediaFileFactory.CreateVideo("Movie 3", "Maria Pio", 1994, "Action", new TimeSpan(1, 9, 32)),
            MediaFileFactory.CreateVideo("Movie 4", "Clara Ros", 1994, " Action", new TimeSpan(3, 4, 32)),
            MediaFileFactory.CreateVideo("Movie 5", "John Dir", 1970, "Drama", new TimeSpan(2, 5, 38)),
            MediaFileFactory.CreateVideo("Movie 6", "Maria Pio", 1992, "Drama", new TimeSpan(1, 9, 32)),
            MediaFileFactory.CreateVideo("Movie 8", "John Dir", 2000, "Terror", new TimeSpan(2, 9, 32)),
            MediaFileFactory.CreateVideo("Movie 9", "Maria Pio", 1969, "Adventure", new TimeSpan(1, 9, 32)),
            MediaFileFactory.CreateVideo("Movie 10", "Clara Ros", 1971, "Adventure", new TimeSpan(1, 9, 32)),
          };

            Users = new List<User>
            {
                 new Customer("pau123","Pau Dan", "pau@something.com", new List<MediaFile>
            {
                MediaFiles[0],
                MediaFiles[1]
}),
                new Admin("ernst123", "Ernst Air", "air@something.com"),
            };

            Observers = new List<IObserver>();
        }
    }
}



