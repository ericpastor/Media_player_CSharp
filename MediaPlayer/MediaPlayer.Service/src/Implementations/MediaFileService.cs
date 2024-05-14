using MediaPlayer.Core.src.Entities;
using MediaPlayer.Core.src.Interfaces;
using MediaPlayer.Service.src.Interfaces;

namespace MediaPlayer.Service.src.Implementations
{
    public class MediaFileService : IMediaFileService, ISubject
    {
        private IMediaFileRepo _repo;

        public MediaFileService(IMediaFileRepo repo)
        {
            _repo = repo;
        }

        public List<MediaFile> GetAllFiles(int offset, int limit)
        {
            if (limit < 0 || offset < 0)
            {
                throw new InvalidDataException();
            }
            return _repo.GetAllFiles(offset, limit);
        }

        public List<MediaFile> EraseAllFiles()
        {
            return _repo.EraseAllFiles()!;
        }

        public List<MediaFile> GetMediafileByTitle(string title)
        {
            if (title == "")
            {
                throw new InvalidDataException();
            }
            return _repo.GetMediafileByTitle(title);
        }

        public MediaFile? GetMediaFileById(int id)
        {
            if (id <= 0)
            {
                throw new InvalidDataException();
            }
            return _repo.GetMediaFileById(id);
        }

        public void AddFileToMediaFiles(MediaFile mediaFile)
        {
            _repo.AddFileToMediaFiles(mediaFile);
        }

        public void RemoveFileFromMediaFiles(MediaFile mediaFile)
        {
            if (mediaFile == null)
            {
                throw new InvalidDataException();
            }
            _repo.RemoveFileFromMediaFiles(mediaFile);
        }

        public void UpdateFileFromMediaFiles(MediaFile mediaFile, int id)
        {
            _repo.UpdateFileFromMediaFiles(mediaFile, id);
        }

        public List<IObserver> GetAllObservers()
        {
            var allObservers = _repo.GetAllObservers();

            if (allObservers == null || allObservers.Count == 0)
            {
                return new List<IObserver>();
            }
            return _repo.GetAllObservers();
        }

        public void Subscribe(IObserver observer)
        {
            _repo.Subscribe(observer);
        }

        public void UnSubscribe(IObserver observer)
        {
            _repo.UnSubscribe(observer);
        }

        public void Notify(string message)
        {
            var observers = _repo.GetAllObservers();
            foreach (var observer in observers)
            {
                observer.React(message);
            }
        }

        public void AddToPlaylist(Customer customer, MediaFile mediaFile)
        {
            if (!customer.Playlist.Contains(mediaFile))
            {
                customer.Playlist.Add(mediaFile);
            }
        }

        public void RemoveFromPlaylist(Customer customer, MediaFile mediaFile)
        {
            customer.Playlist.Remove(mediaFile);
        }

        public MediaFile PlayFile(int id)
        {
            if (id <= 0)
            {
                throw new InvalidDataException();
            }
            return _repo.PlayFile(id);
        }

        public MediaFile Volume(int id)
        {
            if (id <= 0)
            {
                throw new InvalidDataException();
            }
            return _repo.Volume(id);
        }

        public MediaFile Brightness(int id)
        {
            if (id <= 0)
            {
                throw new InvalidDataException();
            }
            return _repo.Brightness(id);
        }

        public MediaFile SoundEffect(int id)
        {
            if (id <= 0)
            {
                throw new InvalidDataException();
            }
            return _repo.SoundEffect(id);
        }
    }
}