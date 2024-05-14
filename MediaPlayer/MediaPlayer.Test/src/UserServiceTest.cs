using MediaPlayer.Core.src.Entities;
using MediaPlayer.Core.src.Interfaces;
using MediaPlayer.Service.src.Implementations;
using Moq;

namespace MediaPlayer.Test.src
{
    public class UserServiceTest
    {
        [Fact]
        public void GetAllUsers_WithoutParameter_ShouldInvokeRepoMethod()
        {
            var repo = new Mock<ICustomerRepo>();
            repo.Setup(repo => repo.GetAllUsers()).Returns(new List<User>());
            var userService = new UserService(repo.Object);

            userService.GetAllUsers();

            repo.Verify(repo => repo.GetAllUsers());
        }

        [Fact]
        public void EraseAllUsers_WithoutParameter_ShouldInvokeRepoMethod()
        {
            var repo = new Mock<ICustomerRepo>();
            repo.Setup(repo => repo.EraseAllUsers()).Returns(new List<User>());
            var userService = new UserService(repo.Object);

            userService.EraseAllUsers();

            repo.Verify(repo => repo.EraseAllUsers());
        }

        [Fact]
        public void GetUserByEmailAndPassword_WithEmailAndPassword_ShouldInvokeRepoMethod()
        {
            // Arrange
            var repo = new Mock<ICustomerRepo>();
            var userService = new UserService(repo.Object);
            var fakeUser = new Customer("pol123", "Pol Dan", "pol@something.com", new List<MediaFile>());
            repo.Setup(repo => repo.GetUserByEmailAndPassword("pol@something.com", "pol123")).Returns(fakeUser);
            // Act
            userService.GetUserByEmailAndPassword("pol@something.com", "pol123");
            // Assert
            repo.Verify(repo => repo.GetUserByEmailAndPassword("pol@something.com", "pol123"), Times.Once);
        }

        [Fact]
        public void GetUserById_WithEmailAndPassword_ShouldInvokeRepoMethod()
        {
            // Arrange
            var repo = new Mock<ICustomerRepo>();
            var userService = new UserService(repo.Object);
            var fakeUser = new Customer("pol123", "Pol Dan", "pol@something.com", new List<MediaFile>());
            repo.Setup(repo => repo.GetUserById(1)).Returns(fakeUser);
            // Act
            userService.GetUserById(1);
            // Assert
            repo.Verify(repo => repo.GetUserById(1), Times.Once);
        }

        [Fact]
        public void AddUser_WithRightData_ShouldInvokeRepoMethod()
        {
            // Arrange
            var repo = new Mock<ICustomerRepo>();
            var userService = new UserService(repo.Object);
            var fakeUser = new Customer("pol123", "Pol Dan", "pol@something.com", new List<MediaFile>());
            repo.Setup(repo => repo.AddUser(fakeUser));
            // Act
            userService.AddUser(fakeUser);
            // Assert
            repo.Verify(repo => repo.AddUser(fakeUser), Times.Once);
        }

        [Fact]
        public void RemoveUser_WithRightData_ShouldInvokeRepoMethod()
        {
            // Arrange
            var repo = new Mock<ICustomerRepo>();
            var userService = new UserService(repo.Object);
            var userToRemove = userService.GetUserById(1);
            // Act
            if (userToRemove != null)
            {
                userService.RemoveUser(userToRemove);
            }
            // Assert
            if (userToRemove != null)
            {
                repo.Verify(repo => repo.RemoveUser(userToRemove), Times.Once);
            }
        }

        [Fact]
        public void UpdateUser_WithRightData_ShouldInvokeRepoMethod()
        {
            // Arrange
            var repo = new Mock<ICustomerRepo>();
            var userService = new UserService(repo.Object);
            var userUpdated = new UserUpdateDTO("Pol Dan", "pol@something.com");


            // Act
            if (userUpdated != null)
            {
                userService.UpdateUser(userUpdated, 1);
            }
            // Assert
            if (userUpdated != null)
            {
                repo.Verify(repo => repo.UpdateUser(userUpdated, 1), Times.Once);
            }
        }


        [Theory]
        [ClassData(typeof(GetAllUsersData))]
        public void GetAllUsers_WithoutParameter_ReturnValidData(List<User> result)
        {
            var repo = new Mock<ICustomerRepo>();
            var userService = new UserService(repo.Object);

            var users = userService.GetAllUsers();

            if (result == null)
            {
                Assert.Null(users);
            }
            else
            {
                Assert.Equal(result, users);
            }
        }

        public class GetAllUsersData : TheoryData<List<User>>
        {
            public GetAllUsersData()
            {
                Add(new List<User>());
            }
        }

        [Theory]
        [ClassData(typeof(EraseAllUsersData))]
        public void EraseAllUsers_WithoutParameter_ReturnEmptyList(List<User> result)
        {
            var repo = new Mock<ICustomerRepo>();
            var userService = new UserService(repo.Object);


            var users = userService.GetAllUsers();
            userService.EraseAllUsers();

            if (result == null)
            {
                Assert.Null(users);
            }
            else
            {
                Assert.Equal(result, users);
            }
        }

        public class EraseAllUsersData : TheoryData<List<User>>
        {
            public EraseAllUsersData()
            {
                Add(new List<User>());
            }
        }

        [Theory]
        [ClassData(typeof(GetUserByEmailAndPasswordData))]
        public void GetUserByEmailAndPassword_WithEmailAndPassword_ReturnsUser(string email, string password, User user, Type exceptionType)
        {
            var repo = new Mock<ICustomerRepo>();
            var userService = new UserService(repo.Object);

            var userFound = userService.GetUserByEmailAndPassword("pol@something.com", "pol123");

            if (exceptionType is not null)
            {
                Assert.Throws(exceptionType, () => userService.GetUserByEmailAndPassword(email, password));
            }
            if (userFound is not null)
            {
                Assert.Equal(user, userFound);
            }

        }

        public class GetUserByEmailAndPasswordData : TheoryData<string, string, User, Type?>
        {

            public GetUserByEmailAndPasswordData()
            {
                var fakeUser = new Customer("pol123", "Pol Dan", "pol@something.com", new List<MediaFile>());
                Add("pol@something.com", "pol123", fakeUser, null);
                Add("", "pol123", fakeUser, typeof(InvalidDataException));
            }
        }

        [Theory]
        [ClassData(typeof(GetUserByIdData))]
        public void GetUserById_WithId_ReturnsUser(int id, User user, Type exceptionType)
        {
            var repo = new Mock<ICustomerRepo>();
            var userService = new UserService(repo.Object);

            var userFound = userService.GetUserById(1);

            if (exceptionType is not null)
            {
                Assert.Throws(exceptionType, () => userService.GetUserById(id));
            }
            if (userFound is not null)
            {
                Assert.Equal(user, userFound);
            }

        }

        public class GetUserByIdData : TheoryData<int, User, Type?>
        {

            public GetUserByIdData()
            {
                var fakeUser = new Customer("pol123", "Pol Dan", "pol@something.com", new List<MediaFile>());
                Add(1, fakeUser, null);
                Add(-1, fakeUser, typeof(InvalidDataException));
            }
        }

        [Theory]
        [ClassData(typeof(AddUserData))]
        public void AddUser_WithRightData_ReturnsUser(User fakeUser)
        {
            var repo = new Mock<ICustomerRepo>();
            var userService = new UserService(repo.Object);
            repo.Setup(repo => repo.GetAllUsers()).Returns(new List<User> { fakeUser });


            userService.AddUser(fakeUser);

            var usersAfterAddition = userService.GetAllUsers();
            Assert.Contains(fakeUser, usersAfterAddition);
        }

        public class AddUserData : TheoryData<User>
        {

            public AddUserData()
            {
                var fakeUser = new Admin("po23", "Pol Dan", "pol@soming.com");
                Add(fakeUser);
            }
        }

        [Theory]
        [ClassData(typeof(RemoveUserData))]
        public void RemoveUser_WithRightData_ReturnsUser(User userToRemove)
        {
            var repo = new Mock<ICustomerRepo>();
            var userService = new UserService(repo.Object);
            repo.Setup(repo => repo.GetUserById(1)).Returns(userToRemove);

            userService.RemoveUser(userToRemove);

            var usersAfterDelete = userService.GetAllUsers();
            Assert.DoesNotContain(userToRemove, usersAfterDelete);
        }

        public class RemoveUserData : TheoryData<User>
        {

            public RemoveUserData()
            {
                var userToRemove = new Customer("pol123", "Pol Dan", "pol@something.com", new List<MediaFile>());
                Add(userToRemove);
            }
        }

        [Theory]
        [ClassData(typeof(UpdateUserData))]
        public void UpdateUser_WithRightData_ReturnsUser(UserUpdateDTO updatedUser, User userToUpdate)
        {
            var repo = new Mock<ICustomerRepo>();
            var userService = new UserService(repo.Object);
            repo.Setup(repo => repo.GetUserById(8)).Returns(userToUpdate);


            userService.UpdateUser(updatedUser, 8);
            var userUpdated = userService.GetUserById(8);

            if (userUpdated != null)
            {
                Assert.Contains(userToUpdate.Email, userUpdated.Email);
            }
        }

        public class UpdateUserData : TheoryData<UserUpdateDTO, User>
        {
            public UpdateUserData()
            {
                var userToUpdate = new Customer("pol123", "Pol Dan", "pol@something.com", new List<MediaFile>());
                var updatedUser = new UserUpdateDTO("Po Dan", "pol@sothing.com");

                Add(updatedUser, userToUpdate);
            }
        }
    }
}


