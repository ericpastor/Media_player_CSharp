using MediaPlayer.Core.src.Entities;

namespace MediaPlayer.Core.src.Interfaces
{
    public interface IMediaFileRepo
    {
        public List<MediaFile> GetMediafileByTitle(string title);
        public List<MediaFile> GetAllFiles(int offset, int limit);
        public List<MediaFile>? EraseAllFiles();
        public MediaFile GetMediaFileById(int id);
        public void AddFileToMediaFiles(MediaFile mediaFileFactory);
        public void RemoveFileFromMediaFiles(MediaFile mediaFile);
        public void UpdateFileFromMediaFiles(MediaFile mediaFile, int id);
        public MediaFile PlayFile(int id);
        public MediaFile Volume(int id);
        public MediaFile Brightness(int id);
        public MediaFile SoundEffect(int id);
        public List<IObserver> GetAllObservers();
        public void Subscribe(IObserver observer);
        public void UnSubscribe(IObserver observer);
    }
}