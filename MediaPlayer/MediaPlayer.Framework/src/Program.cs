using MediaPlayer.Controller.src;
using MediaPlayer.Core.src.Entities;
using MediaPlayer.Framework.src;
using MediaPlayer.Framework.src.Repositories;
using MediaPlayer.Service.src.Implementations;

internal class Program
{
    private static void Main(string[] args)
    {
        var store = new Store();
        var customerRepo = new UserRepository(store);
        var userService = new UserService(customerRepo);
        var mediaFileRepo = new MediaFileRepository(store);
        var mediaFileService = new MediaFileService(mediaFileRepo);
        var mediaFileController = new MediaFileController(mediaFileService, userService);

        var result = mediaFileController.GetAllFiles(0, int.MaxValue);

        Console.WriteLine($"List of files:");
        if (result != null)
            foreach (var file in result)
            {
                Console.WriteLine($"{file.FileType}: {file.Title}");
            }
        Console.WriteLine($"----------------------------------------------------------");
        //Customer subscribes
        var userController = new UserController(userService, mediaFileService);
        var users = userController.GetAllUsers();

        var customer = userController.GetUserById(1);

        if (customer != null)
        {
            mediaFileController.Subscribe(customer);
        }

        Console.WriteLine($"Admin making changes to the MediaFiles:");

        mediaFileController.Login("air@something.com", "ernst123");
        Console.WriteLine($"Applying subscribed customers being notified");

        mediaFileController.AddFileToMediaFiles(2, MediaFileFactory.CreateAudio("NewSong1", "NewArtist2", 2023, "Jazz", new TimeSpan(0, 2, 39)));
        mediaFileController.RemoveFileToMediaFiles(2, 11);
        mediaFileController.UpdateFileFromMediaFiles(2, MediaFileFactory.CreateVideo("Movie666", "S.K", 2020, "Terror", new TimeSpan(2, 38, 40)), 15);

        Console.WriteLine($"----------------------------------------------------------");

        Console.WriteLine($"List of files AFTER CHANGES:");
        var result2 = mediaFileController.GetAllFiles(0, int.MaxValue);

        Console.WriteLine($"List of files:");
        if (result2 != null)
            foreach (var file in result2)
            {
                Console.WriteLine($"{file.FileType}: {file.Title}");
            }

        Console.WriteLine($"----------------------------------------------------------");
        Console.WriteLine($"----------------------------------------------------------");

        Console.WriteLine($"------CUSTOMERS-------------------------------------------");

        Console.WriteLine($"List of users:");

        if (users != null)
            foreach (var user in users)
            {
                Console.WriteLine($"{user.Role}:\n {user}");
            }

        Console.WriteLine($"----------------------------------------------------------");

        Console.WriteLine($"List of users after adding/updating one:");

        userController.AddUser(2, new Customer("ram123", "Ramon Pop", "ram@something.com", new List<MediaFile> { }));
        var customerWithFiles = new UserUpdateDTO("Ramon Trepat", "");
        userController.UpdateUser(2, customerWithFiles, 3);
        var listUsers = userController.GetAllUsers();
        if (listUsers != null)
            foreach (var user in listUsers)
            {
                Console.WriteLine($"{user.Role}:\n {user}");
            }

        mediaFileController.Logout(2);


        Console.WriteLine($"----------------------------------------------------------");

        mediaFileController.Login("pau@something.com", "pau123");
        Console.WriteLine($"Customer adding/removing files");

        Console.WriteLine($"Customer displaying own info:");
        var playingFileAudio2 = mediaFileController.PlayFile(1, 2);
        userController.DisplayCustomerInfo(1);
        userController.AddFileToCustomerPlaylist(1, 5);
        userController.RemoveFileToCustomerPlaylist(1, 1);
        userController.DisplayCustomerInfo(1);

        Console.WriteLine($"----------------------------------------------------------");



        Console.WriteLine($"------CUSTOMER PLAYING AN AUDIO---------------------------");
        var playingFileAudio = mediaFileController.PlayFile(1, 1);
        playingFileAudio?.Stop();
        playingFileAudio?.Play();
        playingFileAudio?.Pause();
        playingFileAudio?.Play();
        playingFileAudio?.Play();
        mediaFileController.Volume(1);
        mediaFileController.SoundEffect(1);
        mediaFileController.Brightness(1);
        mediaFileController.DecreaseLevel("volume");
        mediaFileController.DecreaseLevel("volume");
        mediaFileController.IncreaseLevel("brigntness");
        mediaFileController.ApplySoundEffect("Tremolo");
        mediaFileController.Volume(1);
        mediaFileController.SoundEffect(1);
        playingFileAudio?.Stop();
        Console.WriteLine($"----------------------------------------------------------");

        Console.WriteLine($"-------CUSTOMER PLAYING A VIDEO---------------------------");
        var playingFileVideo = mediaFileController.PlayFile(1, 15);
        playingFileVideo?.Stop();
        playingFileVideo?.Play();
        playingFileVideo?.Pause();
        playingFileVideo?.Play();
        playingFileVideo?.Play();
        mediaFileController.Volume(15);
        mediaFileController.SoundEffect(15);
        mediaFileController.Brightness(15);
        mediaFileController.DecreaseLevel("volume");
        mediaFileController.DecreaseLevel("volume");
        mediaFileController.IncreaseLevel("brightness");
        mediaFileController.ApplySoundEffect("Tremolo");
        mediaFileController.Volume(15);
        mediaFileController.Brightness(15);
        mediaFileController.SoundEffect(15);
    }
}