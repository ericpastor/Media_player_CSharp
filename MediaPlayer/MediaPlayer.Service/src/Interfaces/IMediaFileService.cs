using MediaPlayer.Core.src.Entities;
using MediaPlayer.Core.src.Interfaces;

namespace MediaPlayer.Service.src.Interfaces
{
    public interface IMediaFileService
    {
        public List<MediaFile> GetMediafileByTitle(string title);
        public List<MediaFile> GetAllFiles(int offset, int limit);
        public List<MediaFile> EraseAllFiles();
        public MediaFile? GetMediaFileById(int id);
        public void AddToPlaylist(Customer customer, MediaFile mediaFile);
        public void RemoveFromPlaylist(Customer customer, MediaFile mediaFile);
        public void AddFileToMediaFiles(MediaFile mediaFile);
        public void RemoveFileFromMediaFiles(MediaFile mediaFile);
        public void UpdateFileFromMediaFiles(MediaFile mediaFile, int id);
        public MediaFile PlayFile(int id);
        public MediaFile Volume(int id);
        public MediaFile Brightness(int id);
        public MediaFile SoundEffect(int id);
        public List<IObserver> GetAllObservers();
        public void Notify(string message);
        public void Subscribe(IObserver observer);
        public void UnSubscribe(IObserver observer);
    }
}