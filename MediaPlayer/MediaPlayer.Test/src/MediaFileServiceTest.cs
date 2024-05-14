using System.Data.Common;
using MediaPlayer.Core.src.Entities;
using MediaPlayer.Core.src.Interfaces;
using MediaPlayer.Service.src.Implementations;
using Moq;

namespace MediaPlayer.Test.src

{
    public class MediaFileServiceTest
    {
        [Fact]
        public void GetAllFiles_WithValidLimitAndOffset_ShouldInvokeRepoMethod()
        {
            // Arrange
            // var database = new Database();
            // var repo = new MediaFileRepository(database);
            // var repo = new CutomMediaFileRepo(); // do check any value from the fake --> mock

            var repo = new Mock<IMediaFileRepo>();
            var mediaFileService = new MediaFileService(repo.Object);

            // Act
            // var files = mediaFileService.GetAllFiles(5, 1);
            mediaFileService.GetAllFiles(4, 2);

            // Assert
            // Assert.Equal(2, repo.TimeToBeInvoked);
            repo.Verify(repo => repo.GetAllFiles(4, 2), Times.Once);
        }

        [Fact]
        public void EraseAllFiles_WithoutParameter_ShouldInvokeRepoMethod()
        {
            var repo = new Mock<IMediaFileRepo>();
            repo.Setup(repo => repo.EraseAllFiles()).Returns(new List<MediaFile>());
            var mediaFileService = new MediaFileService(repo.Object);

            mediaFileService.EraseAllFiles();

            repo.Verify(repo => repo.EraseAllFiles());
        }

        [Fact]
        public void GetMediaFileByTitle_WithTitle_ShouldInvokeRepoMethod()
        {
            // Arrange
            var repo = new Mock<IMediaFileRepo>();
            var mediaFileService = new MediaFileService(repo.Object);
            var fakeMediaFile = new Audio("audio1", "artits34", 2020, "rock", new TimeSpan(0, 4, 2));
            repo.Setup(repo => repo.GetMediafileByTitle("audio1")).Returns(new List<MediaFile> { fakeMediaFile });
            // Act
            mediaFileService.GetMediafileByTitle("audio1");
            // Assert
            repo.Verify(repo => repo.GetMediafileByTitle("audio1"), Times.Once);
        }

        [Fact]
        public void GetMediaFileById_WithId_ShouldInvokeRepoMethod()
        {
            // Arrange
            var repo = new Mock<IMediaFileRepo>();
            var mediaFileService = new MediaFileService(repo.Object);
            var fakeMediaFile = new Audio("audio1", "artits34", 2020, "rock", new TimeSpan(0, 4, 2));
            repo.Setup(repo => repo.GetMediaFileById(1)).Returns(fakeMediaFile);
            // Act
            mediaFileService.GetMediaFileById(1);
            // Assert
            repo.Verify(repo => repo.GetMediaFileById(1), Times.Once);
        }

        [Fact]
        public void AddFileToMediaFiles_WithRightData_ShouldInvokeRepoMethod()
        {
            // Arrange
            var repo = new Mock<IMediaFileRepo>();
            var mediaFileService = new MediaFileService(repo.Object);
            var fakeMediaFile = new Audio("audio1", "artits34", 2020, "rock", new TimeSpan(0, 4, 2));
            repo.Setup(repo => repo.AddFileToMediaFiles(fakeMediaFile));
            // Act
            mediaFileService.AddFileToMediaFiles(fakeMediaFile);
            // Assert
            repo.Verify(repo => repo.AddFileToMediaFiles(fakeMediaFile), Times.Once);
        }

        [Fact]
        public void RemoveFileFromMediaFiles_WithRightData_ShouldInvokeRepoMethod()
        {
            // Arrange
            var repo = new Mock<IMediaFileRepo>();
            var mediaFileService = new MediaFileService(repo.Object);
            var userToRemove = mediaFileService.GetMediaFileById(1);
            // Act
            if (userToRemove != null)
            {
                mediaFileService.RemoveFileFromMediaFiles(userToRemove);
            }
            // Assert
            if (userToRemove != null)
            {
                repo.Verify(repo => repo.RemoveFileFromMediaFiles(userToRemove), Times.Once);
            }
        }

        [Fact]
        public void UpdateFileFromMediaFiles_WithRightData_ShouldInvokeRepoMethod()
        {
            // Arrange
            var repo = new Mock<IMediaFileRepo>();
            var mediaFileService = new MediaFileService(repo.Object);
            var fileUpdated = new Audio("audio333", "artits34", 2020, "rock", new TimeSpan(0, 4, 2));


            // Act
            if (fileUpdated != null)
            {
                mediaFileService.UpdateFileFromMediaFiles(fileUpdated, 1);
            }
            // Assert
            if (fileUpdated != null)
            {
                repo.Verify(repo => repo.UpdateFileFromMediaFiles(fileUpdated, 1), Times.Once);
            }
        }

        [Fact]
        public void GetAllObservers_WithoutParameter_ShouldInvokeRepoMethod()
        {
            var repo = new Mock<IMediaFileRepo>();
            repo.Setup(repo => repo.GetAllObservers()).Returns(new List<IObserver>());
            var mediaService = new MediaFileService(repo.Object);

            mediaService.GetAllObservers();

            repo.Verify(repo => repo.GetAllObservers());
        }

        [Fact]
        public void Subscribe_WithRightData_ShouldInvokeRepoMethod()
        {
            // Arrange
            var repoMediaFile = new Mock<IMediaFileRepo>();
            var mediaFileService = new MediaFileService(repoMediaFile.Object);
            var repoUser = new Mock<ICustomerRepo>();
            var userService = new UserService(repoUser.Object);
            var fakeUser = new Customer("pol123", "Pol Dan", "pol@something.com", new List<MediaFile>());
            repoUser.Setup(repo => repo.GetUserById(1)).Returns(fakeUser);
            var user = userService.GetUserById(1);
            // Act
            if (user is not null)
            {
                mediaFileService.Subscribe(user);
                // Assert
                repoMediaFile.Verify(repoMediaFile => repoMediaFile.Subscribe(user));
            }
        }

        [Fact]
        public void Unsubscribe_WithRightData_ShouldInvokeRepoMethod()
        {
            // Arrange
            var repoMediaFile = new Mock<IMediaFileRepo>();
            var mediaFileService = new MediaFileService(repoMediaFile.Object);
            var repoUser = new Mock<ICustomerRepo>();
            var userService = new UserService(repoUser.Object);
            var fakeUser = new Customer("pol123", "Pol Dan", "pol@something.com", new List<MediaFile>());
            repoUser.Setup(repo => repo.GetUserById(1)).Returns(fakeUser);
            var user = userService.GetUserById(1);
            // Act
            if (user is not null)
            {
                mediaFileService.UnSubscribe(user);
                // Assert
                repoMediaFile.Verify(repoMediaFile => repoMediaFile.UnSubscribe(user));
            }
        }

        [Fact]
        public void PlayFile_WithVlidId_ShouldInvokePlayFile()
        {
            // Arrange
            var repo = new Mock<IMediaFileRepo>();
            var mediaFileService = new MediaFileService(repo.Object);
            var fakeMediaFile = new Audio("audio1", "artits34", 2020, "rock", new TimeSpan(0, 4, 2));
            repo.Setup(repo => repo.PlayFile(1)).Returns(fakeMediaFile);
            // Act
            mediaFileService.PlayFile(1);
            // Assert
            repo.Verify(repo => repo.PlayFile(1), Times.Once);
        }

        [Fact]
        public void Volume_WithVlidId_ShouldInvokeVolume()
        {
            // Arrange
            var repo = new Mock<IMediaFileRepo>();
            var mediaFileService = new MediaFileService(repo.Object);
            var fakeMediaFile = new Audio("audio1", "artits34", 2020, "rock", new TimeSpan(0, 4, 2));
            repo.Setup(repo => repo.Volume(1)).Returns(fakeMediaFile);
            // Act
            mediaFileService.Volume(1);
            // Assert
            repo.Verify(repo => repo.Volume(1), Times.Once);
        }

        [Fact]
        public void Brightness_WithVlidId_ShouldInvokeVolume()
        {
            // Arrange
            var repo = new Mock<IMediaFileRepo>();
            var mediaFileService = new MediaFileService(repo.Object);
            var fakeMediaFile = new Audio("audio1", "artits34", 2020, "rock", new TimeSpan(0, 4, 2));
            repo.Setup(repo => repo.Brightness(1)).Returns(fakeMediaFile);
            // Act
            mediaFileService.Brightness(1);
            // Assert
            repo.Verify(repo => repo.Brightness(1), Times.Once);
        }

        [Fact]
        public void SoundEffect_WithVlidId_ShouldInvokeVolume()
        {
            // Arrange
            var repo = new Mock<IMediaFileRepo>();
            var mediaFileService = new MediaFileService(repo.Object);
            var fakeMediaFile = new Audio("audio1", "artits34", 2020, "rock", new TimeSpan(0, 4, 2));
            repo.Setup(repo => repo.SoundEffect(1)).Returns(fakeMediaFile);
            // Act
            mediaFileService.SoundEffect(1);
            // Assert
            repo.Verify(repo => repo.SoundEffect(1), Times.Once);
        }



        [Theory]
        [ClassData(typeof(GetAllFilesData))]
        public void GetAllFiles_WithValidLimitAndOffset_ReturnValidData(int limit, int offset, List<MediaFile> result, Type exceptionType)
        {
            // Arrange
            // var database = new Database();
            // var repo = new MediaFileRepository(database);
            // var repo = new CutomMediaFileRepo(); // do not check the state or behaviour of the fake --> stub
            var repo = new Mock<IMediaFileRepo>();
            repo.Setup(repo => repo.GetAllFiles(It.IsAny<int>(), It.IsAny<int>())).Returns(new List<MediaFile>());
            var mediaFileService = new MediaFileService(repo.Object);

            if (exceptionType is not null)
            {
                // Act + Assert
                Assert.Throws(exceptionType, () => mediaFileService.GetAllFiles(limit, offset));
            }
            else
            {
                // Act
                var files = mediaFileService.GetAllFiles(limit, offset);

                // Assert
                Assert.Equal(result, files);
            }
        }

        public class GetAllFilesData : TheoryData<int, int, List<MediaFile>, Type?>
        {
            public GetAllFilesData()
            {
                Add(1, 1, new List<MediaFile>(), null);
                Add(1, 2, new List<MediaFile>(), null);
                Add(-1, 1, new List<MediaFile>(), typeof(InvalidDataException));
            }
        }

        [Theory]
        [ClassData(typeof(EraseAllFilesData))]
        public void EraseAllFiles_WithoutParameter_ReturnEmptyList(List<MediaFile> result, Audio audio)
        {
            // Arrange
            var repo = new Mock<IMediaFileRepo>();
            var mediaFileService = new MediaFileService(repo.Object);
            repo.Setup(repo => repo.GetAllFiles(It.IsAny<int>(), It.IsAny<int>())).Returns(result);

            mediaFileService.AddFileToMediaFiles(audio);

            // Act
            mediaFileService.EraseAllFiles();

            // Assert
            var actualMediaFiles = mediaFileService.GetAllFiles(0, int.MaxValue);
            Assert.Empty(actualMediaFiles);
        }

        public class EraseAllFilesData : TheoryData<List<MediaFile>, Audio>
        {
            public EraseAllFilesData()
            {
                Add(new List<MediaFile>(), new Audio("audio1", "artits34", 2020, "rock", new TimeSpan(0, 4, 2)));
            }
        }

        [Theory]
        [ClassData(typeof(GetMediafileByTitle))]
        public void GetMediafileByTitle_WithEmailAndPassword_ReturnsUser(string title, Audio file, List<MediaFile> mediaFiles, Type exceptionType)
        {
            var repo = new Mock<IMediaFileRepo>();
            var mediaFileService = new MediaFileService(repo.Object);

            var fileFound = mediaFileService.GetMediafileByTitle(file.Title);

            if (exceptionType is not null)
            {
                Assert.Throws(exceptionType, () => mediaFileService.GetMediafileByTitle(title));
            }
            if (fileFound is not null)
            {
                Assert.Equal(mediaFiles, fileFound);
            }
        }

        public class GetMediafileByTitle : TheoryData<string, Audio, List<MediaFile>, Type?>
        {

            public GetMediafileByTitle()
            {
                var fakeMediaFile = new Audio("audio1", "artits34", 2020, "rock", new TimeSpan(0, 4, 2));
                var newListOfFiles = new List<MediaFile> { fakeMediaFile };
                Add("audio1", fakeMediaFile, newListOfFiles, null);
                Add("", fakeMediaFile, newListOfFiles, typeof(InvalidDataException));
            }
        }

        [Theory]
        [ClassData(typeof(GetMediaFileByIdData))]
        public void GetMediaFileById_WithId_ReturnsMediaFile(int id, MediaFile mediaFile, Type exceptionType)
        {
            var repo = new Mock<IMediaFileRepo>();
            var mediaFileService = new MediaFileService(repo.Object);

            var mediaFileFound = mediaFileService.GetMediaFileById(1);

            if (exceptionType is not null)
            {
                Assert.Throws(exceptionType, () => mediaFileService.GetMediaFileById(id));
            }
            if (mediaFileFound is not null)
            {
                Assert.Equal(mediaFile, mediaFileFound);
            }

        }
        public class GetMediaFileByIdData : TheoryData<int, MediaFile, Type?>
        {
            public GetMediaFileByIdData()
            {
                var fakeMediaFile = new Audio("audio1", "artits34", 2020, "rock", new TimeSpan(0, 4, 2));
                Add(1, fakeMediaFile, null);
                Add(-1, fakeMediaFile, typeof(InvalidDataException));
            }
        }

        [Theory]
        [ClassData(typeof(AddFileToMediaFilesData))]
        public void AddFileToMediaFiles_WithRightData_ReturnsUser(MediaFile fakeMediaFile, List<MediaFile> mediaFiles)
        {
            var repo = new Mock<IMediaFileRepo>();
            var mediaFileService = new MediaFileService(repo.Object);
            repo.Setup(repo => repo.GetAllFiles(0, int.MaxValue)).Returns(new List<MediaFile> { fakeMediaFile });

            mediaFileService.AddFileToMediaFiles(new Audio("audio1", "artits34", 2020, "rock", new TimeSpan(0, 4, 2)));
            repo.Verify(repo => repo.AddFileToMediaFiles(It.IsAny<MediaFile>()), Times.Once);


            var filesAfterAddition = mediaFileService.GetAllFiles(0, int.MaxValue);
            Assert.Equal(mediaFiles, filesAfterAddition);
        }

        public class AddFileToMediaFilesData : TheoryData<MediaFile, List<MediaFile>>
        {

            public AddFileToMediaFilesData()
            {
                var fakeMediaFile = new Audio("audio1", "artits34", 2020, "rock", new TimeSpan(0, 4, 2));
                Add(fakeMediaFile, new List<MediaFile> { fakeMediaFile });
            }
        }

        [Theory]
        [ClassData(typeof(RemoveFileFromMediaFilesData))]
        public void RemoveFileFromMediaFiles_WithRightData_ReturnsMediaFileUpdated(MediaFile fakeMediaFile, List<MediaFile> mediaFiles)
        {
            var repo = new Mock<IMediaFileRepo>();
            var mediaFileService = new MediaFileService(repo.Object);
            repo.Setup(repo => repo.GetAllFiles(It.IsAny<int>(), It.IsAny<int>())).Returns(mediaFiles);

            mediaFileService.AddFileToMediaFiles(fakeMediaFile);

            mediaFileService.RemoveFileFromMediaFiles(fakeMediaFile);

            var filesAfterDeletion = mediaFileService.GetAllFiles(0, int.MaxValue);

            Assert.Empty(filesAfterDeletion);
        }


        public class RemoveFileFromMediaFilesData : TheoryData<MediaFile, List<MediaFile>>
        {
            public RemoveFileFromMediaFilesData()
            {
                var fakeMediaFile = new Audio("audio1", "artits34", 2020, "rock", new TimeSpan(0, 4, 2));
                Add(fakeMediaFile, new List<MediaFile> { });
            }
        }

        [Theory]
        [ClassData(typeof(UpdateFileFromMediaFilesData))]
        public void UpdateFileFromMediaFiles_WithRightData_ReturnsTheFileUpdated(MediaFile updatedFile, MediaFile fileToUpdate)
        {
            var repo = new Mock<IMediaFileRepo>();
            var mediaFileService = new MediaFileService(repo.Object);
            repo.Setup(repo => repo.GetMediaFileById(8)).Returns(fileToUpdate);


            mediaFileService.UpdateFileFromMediaFiles(updatedFile, 8);
            var fileUpdated = mediaFileService.GetMediaFileById(8);

            if (fileUpdated != null)
            {
                Assert.Contains(fileToUpdate.Title, fileUpdated.Title);
            }
        }

        public class UpdateFileFromMediaFilesData : TheoryData<MediaFile, MediaFile>
        {
            public UpdateFileFromMediaFilesData()
            {
                var fileToUpdate = new Audio("audio1", "artits34", 2020, "rock", new TimeSpan(0, 4, 2));
                var updatedFile = new Audio("audio333", "artits34", 2020, "rock", new TimeSpan(0, 4, 2));

                Add(updatedFile, fileToUpdate);
            }
        }

        [Theory]
        [ClassData(typeof(GetAllObserversData))]
        public void GetAllObservers_WithoutParameter_ReturnValidData(List<IObserver> result)
        {
            var repo = new Mock<IMediaFileRepo>();
            var mediaFileService = new MediaFileService(repo.Object);

            var observers = mediaFileService.GetAllObservers();

            if (result == null)
            {
                Assert.Null(observers);
            }
            else
            {
                Assert.Equal(result, observers);
            }
        }
        public class GetAllObserversData : TheoryData<List<IObserver>>
        {
            public GetAllObserversData()
            {
                Add(new List<IObserver>());
            }
        }

        [Theory]
        [ClassData(typeof(SubscribeData))]

        public void Subscribe_WithRightData_ShouldSubscribeACustomer(Customer customer)
        {
            var repoMediaFile = new Mock<IMediaFileRepo>();
            var mediaFileService = new MediaFileService(repoMediaFile.Object);
            var repoUser = new Mock<ICustomerRepo>();
            var userService = new UserService(repoUser.Object);

            var observersList = new List<IObserver> { customer };
            repoMediaFile.Setup(repo => repo.GetAllObservers()).Returns(observersList);

            repoUser.Setup(repo => repo.GetUserById(1)).Returns(customer);

            var listObservers = mediaFileService.GetAllObservers();
            var user = userService.GetUserById(1);

            if (user is not null)
            {
                // Act
                mediaFileService.Subscribe(user);
                // Assert
                Assert.Contains(customer, listObservers);
            }
        }

        public class SubscribeData : TheoryData<Customer>
        {
            public SubscribeData()
            {
                var customer = new Customer("pol123", "Pol Dan", "pol@something.com", new List<MediaFile>());
                Add(customer);

            }
        }

        [Theory]
        [ClassData(typeof(UnSubscribData))]

        public void UnSubscribe_WithRightData_ShouldUnSubscribeACustomer(Customer customer)
        {
            var repoMediaFile = new Mock<IMediaFileRepo>();
            var mediaFileService = new MediaFileService(repoMediaFile.Object);
            var repoUser = new Mock<ICustomerRepo>();
            var userService = new UserService(repoUser.Object);

            repoMediaFile.Setup(repo => repo.GetAllObservers()).Returns(new List<IObserver> { });

            repoUser.Setup(repo => repo.GetUserById(1)).Returns(customer);

            var listObservers = mediaFileService.GetAllObservers();
            var user = userService.GetUserById(1);

            if (user is not null)
            {
                // Act
                mediaFileService.UnSubscribe(user);
                // Assert
                Assert.DoesNotContain(customer, listObservers);
            }
        }

        public class UnSubscribData : TheoryData<Customer>
        {
            public UnSubscribData()
            {
                var customer = new Customer("pol123", "Pol Dan", "pol@something.com", new List<MediaFile>());
                Add(customer);

            }
        }

        [Theory]
        [ClassData(typeof(PlayFileData))]
        public void PlayFile_WithVlidId_ReturnValidData(int id, MediaFile fakeMediaFile, Type exceptionType)
        {
            var repo = new Mock<IMediaFileRepo>();
            var mediaFileService = new MediaFileService(repo.Object);

            var mediaFileFound = mediaFileService.PlayFile(1);

            if (exceptionType is not null)
            {
                Assert.Throws(exceptionType, () => mediaFileService.PlayFile(id));
            }
            if (mediaFileFound is not null)
            {
                Assert.Equal(fakeMediaFile, mediaFileFound);
            }
        }

        public class PlayFileData : TheoryData<int, MediaFile, Type?>
        {
            public PlayFileData()
            {
                var fakeMediaFile = new Audio("audio1", "artits34", 2020, "rock", new TimeSpan(0, 4, 2));
                Add(1, fakeMediaFile, null);
                Add(-1, fakeMediaFile, typeof(InvalidDataException));
            }
        }

        [Theory]
        [ClassData(typeof(VolumeData))]
        public void Volume_WithVlidId_ReturnValidData(int id, MediaFile fakeMediaFile, Type exceptionType)
        {
            var repo = new Mock<IMediaFileRepo>();
            var mediaFileService = new MediaFileService(repo.Object);

            var mediaFileFound = mediaFileService.Volume(1);

            if (exceptionType is not null)
            {
                Assert.Throws(exceptionType, () => mediaFileService.Volume(id));
            }
            if (mediaFileFound is not null)
            {
                Assert.Equal(fakeMediaFile, mediaFileFound);
            }
        }

        public class VolumeData : TheoryData<int, MediaFile, Type?>
        {
            public VolumeData()
            {
                var fakeMediaFile = new Audio("audio1", "artits34", 2020, "rock", new TimeSpan(0, 4, 2));
                Add(1, fakeMediaFile, null);
                Add(-1, fakeMediaFile, typeof(InvalidDataException));
            }
        }

        [Theory]
        [ClassData(typeof(BrightnessData))]
        public void Brightness_WithVlidId_ReturnValidData(int id, MediaFile fakeMediaFile, Type exceptionType)
        {
            var repo = new Mock<IMediaFileRepo>();
            var mediaFileService = new MediaFileService(repo.Object);

            var mediaFileFound = mediaFileService.Brightness(1);

            if (exceptionType is not null)
            {
                Assert.Throws(exceptionType, () => mediaFileService.Brightness(id));
            }
            if (mediaFileFound is not null)
            {
                Assert.Equal(fakeMediaFile, mediaFileFound);
            }
        }

        public class BrightnessData : TheoryData<int, MediaFile, Type?>
        {
            public BrightnessData()
            {
                var fakeMediaFile = new Audio("audio1", "artits34", 2020, "rock", new TimeSpan(0, 4, 2));
                Add(1, fakeMediaFile, null);
                Add(-1, fakeMediaFile, typeof(InvalidDataException));
            }
        }

        [Theory]
        [ClassData(typeof(SoundEffectData))]
        public void SoundEffect_WithVlidId_ReturnValidData(int id, MediaFile fakeMediaFile, Type exceptionType)
        {
            var repo = new Mock<IMediaFileRepo>();
            var mediaFileService = new MediaFileService(repo.Object);

            var mediaFileFound = mediaFileService.SoundEffect(1);

            if (exceptionType is not null)
            {
                Assert.Throws(exceptionType, () => mediaFileService.SoundEffect(id));
            }
            if (mediaFileFound is not null)
            {
                Assert.Equal(fakeMediaFile, mediaFileFound);
            }
        }

        public class SoundEffectData : TheoryData<int, MediaFile, Type?>
        {
            public SoundEffectData()
            {
                var fakeMediaFile = new Audio("audio1", "artits34", 2020, "rock", new TimeSpan(0, 4, 2));
                Add(1, fakeMediaFile, null);
                Add(-1, fakeMediaFile, typeof(InvalidDataException));
            }
        }
    }
}