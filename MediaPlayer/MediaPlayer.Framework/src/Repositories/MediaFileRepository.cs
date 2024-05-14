using MediaPlayer.Core.src.Entities;
using MediaPlayer.Core.src.Interfaces;

namespace MediaPlayer.Framework.src.Repositories
{
    public class MediaFileRepository : IMediaFileRepo
    {
        private List<MediaFile> _mediaFiles;
        private List<IObserver> _observers;


        public MediaFileRepository(Store store)
        {
            _mediaFiles = store.MediaFiles;
            _observers = store.Observers;
        }

        public List<MediaFile> GetMediafileByTitle(string title)
        {
            return _mediaFiles.FindAll(m => m.Title.Contains(title));
        }

        public List<MediaFile> GetAllFiles(int offset, int limit)
        {
            return _mediaFiles.Skip(offset).Take(limit).ToList();
        }

        public List<MediaFile>? EraseAllFiles()
        {
            _mediaFiles.Clear();
            Console.WriteLine($"All files have been erased");
            return null;
        }

        public MediaFile GetMediaFileById(int id)
        {
            return _mediaFiles.Single(m => m.Id == id);
        }

        public void AddFileToMediaFiles(MediaFile mediaFileFactory)
        {
            _mediaFiles.Add(mediaFileFactory);
        }

        public void RemoveFileFromMediaFiles(MediaFile mediaFile)
        {
            _mediaFiles.Remove(mediaFile);
        }

        public void UpdateFileFromMediaFiles(MediaFile mediaFile, int id)
        {
            _mediaFiles.Select(m => m.id.Equals(id));
        }

        public MediaFile PlayFile(int id)
        {
            return _mediaFiles.Single(m => m.Id == id);
        }

        public MediaFile Volume(int id)
        {
            return _mediaFiles.Single(m => m.Id == id);
        }

        public MediaFile Brightness(int id)
        {
            return _mediaFiles.Single(m => m.Id == id);
        }

        public MediaFile SoundEffect(int id)
        {
            return _mediaFiles.Single(m => m.Id == id);
        }

        public List<IObserver> GetAllObservers()
        {
            return _observers.ToList();
        }

        public void Subscribe(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void UnSubscribe(IObserver observer)
        {
            _observers.Remove(observer);
        }
    }
}