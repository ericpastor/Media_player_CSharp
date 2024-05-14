using System.Reflection;
using MediaPlayer.Core.src.Entities;
using MediaPlayer.Core.src.Interfaces;
using MediaPlayer.Service.src.Interfaces;
using Type = System.Type;

namespace MediaPlayer.Controller.src
{
    public class MediaFileController : AuthentificationController
    {
        private readonly byte[]? level = new byte[11] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        private byte volume = 5;
        private byte brightness = 5;
        private readonly string[] soundEffect = new string[] { "Normal", "Reverb", "Delay", "Echo", "Chorus", "Tremolo", "Flanger", "Phaser" };
        private string currentSoundEffect = "Nomal";

        private IMediaFileService _mediaFileService;
        private IUserService _userService;

        public MediaFileController(IMediaFileService mediaFileService, IUserService userService) : base(userService, mediaFileService)
        {
            _mediaFileService = mediaFileService;
            _userService = userService;

        }

        public List<MediaFile>? GetAllFiles(params object[] options)
        {
            try
            {
                int.TryParse(options[0].ToString(), out int limit);
                int.TryParse(options[1].ToString(), out int offset);
                return _mediaFileService.GetAllFiles(limit, offset);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error happen, cannot fetch data");
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public List<MediaFile>? EraseAllFiles(int id)
        {
            try
            {
                var user = _userService.GetUserById(id);
                if (user != null && user is Admin && user.IsLogged)
                {
                    Console.WriteLine($"All files removed");

                    return _mediaFileService.EraseAllFiles();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error happen, cannot erase data");
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public List<MediaFile> FindByTitle(object title)
        {
            if (title is null || title.ToString() is null)
            {
                throw new ArgumentNullException();
            }
            else
            {
                var data = _mediaFileService.GetMediafileByTitle(title.ToString()!);
                if (data.Count == 0)
                {
                    Console.WriteLine($"No files at this moment");

                }
                return data;
            }
        }

        public MediaFile? GetMediaFileById(string id)
        {
            {
                try
                {
                    var files = _mediaFileService.GetAllFiles(0, int.MaxValue);
                    foreach (var file in files)
                        if (file.Id.Equals(id))
                        {
                            return file;
                        }
                        else
                        {
                            Console.WriteLine($"Not found");

                        }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error happen, cannot fetch data");
                    Console.WriteLine(e.Message);

                }
                return null;
            }
        }

        public void AddFileToMediaFiles(int id, MediaFile mediaFile)
        {
            var user = _userService.GetUserById(id);
            if (user != null && user is Admin && user.IsLogged)
            {
                _mediaFileService.AddFileToMediaFiles(mediaFile);
                Notify($"New {mediaFile.FileType}: '{mediaFile.Title}' has been added!");
            }
            else
            {
                Console.WriteLine($"We couldn't add this file.");
            }
        }

        public void RemoveFileToMediaFiles(int id, int fileId)
        {
            var user = _userService.GetUserById(id);
            var fileToRemove = _mediaFileService.GetMediaFileById(fileId);
            if (user != null && user is Admin && user.IsLogged && fileToRemove != null && fileToRemove.Id == fileId)
            {
                _mediaFileService.RemoveFileFromMediaFiles(fileToRemove);
                Notify($"{fileToRemove.FileType}: '{fileToRemove.Title}' has been removed from the store");
            }
            else
            {
                Console.WriteLine($"We couldn't remove this file.");
            }
        }

        public void UpdateFileFromMediaFiles(int id, MediaFile updatedMediaFile, int fileId)
        // int getting oldValue => set value to 0 to specify that not willing to change this value
        {
            var user = _userService.GetUserById(id);
            var fileToUpdate = _mediaFileService.GetMediaFileById(fileId);
            if (user != null && user is Admin && user.IsLogged && fileToUpdate != null && fileToUpdate.Id == fileId)
            {
                Type mediFileType = fileToUpdate.GetType();
                PropertyInfo[] properties = mediFileType.GetProperties();
                foreach (var property in properties)
                {
                    PropertyInfo updatedMediaFileProperty = updatedMediaFile.GetType().GetProperty(property.Name)!;

                    if (updatedMediaFileProperty != null)
                    {
                        object value = updatedMediaFileProperty.GetValue(updatedMediaFile)!;
                        if (value != null)
                        {
                            if (value is string v && string.IsNullOrWhiteSpace(v) || value is int vId && int.Equals(vId, vId))
                            {
                                object oldValue = property.GetValue(fileToUpdate)!;
                                property.SetValue(fileToUpdate, oldValue);
                            }
                            else
                            {
                                property.SetValue(fileToUpdate, value);
                            }
                        }
                        else
                        {
                            object oldValue = property.GetValue(fileToUpdate)!;
                            property.SetValue(fileToUpdate, oldValue);
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine($"Wrong credentials. Try again!");
            }
        }

        public MediaFile? PlayFile(int id, int fileId)
        {
            var user = _userService.GetUserById(id);
            var fileToPlay = _mediaFileService.PlayFile(fileId);
            if (user != null && user is Customer && user.IsLogged && fileToPlay != null && fileToPlay.Id == fileId)
            {
                fileToPlay.IsPlaying = true;
                fileToPlay.IsStopped = false;
                fileToPlay.IsPaused = false;
                Console.WriteLine($"Playing: {fileToPlay.Title}");
                return fileToPlay;
            }
            if (user != null && !user.IsLogged)
            {
                Console.WriteLine($"You must login fisrt! :)");
                return null;
            }
            else
            {
                Console.WriteLine($"We couldn't remove this file.");
                return null;
            }
        }

        public void IncreaseLevel(string feature)
        {
            if (level != null)
            {
                if (feature == "volume")
                {
                    if (volume < level.Length - 1)
                    {
                        volume++;
                    }
                }
                if (feature == "brightness")
                {
                    if (brightness < level.Length - 1)
                    {
                        brightness++;
                    }
                }
            }
        }

        public void DecreaseLevel(string feature)
        {
            if (level != null)
            {
                if (feature == "volume")
                {
                    if (volume < level.Length - 1)
                    {
                        volume++;
                    }
                }
                if (feature == "brightness")
                {
                    if (brightness < level.Length - 1)
                    {
                        brightness++;
                    }
                }
            }
        }

        public byte GetLevel(string feature)
        {
            if (level != null)
            {
                if (feature == "volume")
                {
                    return level[volume];
                }
                if (feature == "brightness")
                {
                    return level[brightness];
                }
            }
            else
            {
                Console.WriteLine($"Oops, something went wrong");
            }
            throw new Exception("Oops, something went wrong");
        }

        public void ApplySoundEffect(string soundEffectToApply)
        {
            if (soundEffect.Contains(soundEffectToApply))
            {
                currentSoundEffect = soundEffectToApply;
            }
            else
            {
                Console.WriteLine($"Sorry, but we do not have '{soundEffectToApply}' sonud effect");
            }
        }

        public string? GetSoundEffect()
        {
            if (soundEffect != null)
            {
                return currentSoundEffect;
            }
            return null;
        }

        public void Volume(int id)
        {
            var filePlaying = _mediaFileService.PlayFile(id);

            if (filePlaying.IsPlaying)
            {
                Console.WriteLine($"Volume {filePlaying.FileType} level: {GetLevel("volume")}");
            }
        }

        public void Brightness(int id)
        {
            var filePlaying = _mediaFileService.PlayFile(id);
            if (filePlaying.IsPlaying && filePlaying is Video)
            {
                Console.WriteLine($"Brightness {filePlaying.FileType} level: {GetLevel("brightness")}");
            }
            else
            {
                Console.WriteLine($"File must be playing and be a Video to apply brightness");
            }
        }

        public void SoundEffect(int id)
        {
            var filePlaying = _mediaFileService.PlayFile(id);
            if (filePlaying.IsPlaying && filePlaying is Audio)
            {
                Console.WriteLine($"Sound Effect {filePlaying.FileType}: {GetSoundEffect()}");
            }
            else
            {
                Console.WriteLine($"Only soundEffects for audios");
            }
        }

        public List<IObserver> GetAllObservers()
        {
            return _mediaFileService.GetAllObservers();
        }

        public void Notify(string message)
        {
            var observers = _mediaFileService.GetAllObservers();
            foreach (var observer in observers)
            {
                observer.React(message);
            }
        }

        public void Subscribe(IObserver observer)
        {
            _mediaFileService.Subscribe(observer);
        }

        public void UnSubscribe(IObserver observer)
        {
            _mediaFileService.UnSubscribe(observer);
        }
    }
}