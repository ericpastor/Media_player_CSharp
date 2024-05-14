using System.Reflection;
using MediaPlayer.Core.src.Entities;
using MediaPlayer.Service.src.Interfaces;
using Type = System.Type;

namespace MediaPlayer.Controller.src
{
    public class UserController : AuthentificationController
    {
        private IUserService _userService;
        private IMediaFileService _mediaFileService;

        public UserController(IUserService userService, IMediaFileService mediaFileService) : base(userService, mediaFileService)
        {
            _userService = userService;
            _mediaFileService = mediaFileService;

        }

        public List<User>? GetAllUsers()
        {
            try
            {
                return _userService.GetAllUsers();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error happen, cannot fetch data");
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public List<User>? EraseAllUsers()
        {
            try
            {
                return _userService.EraseAllUsers();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error happen, cannot erase data");
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public User? GetUserById(int id)
        {
            {
                try
                {
                    var customers = _userService.GetAllUsers();
                    foreach (var customer in customers)
                        if (customer.Id.Equals(id))
                        {
                            return customer;
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

        public User? GetUserByEmailAndPassword(string email, string password)
        {
            {
                try
                {
                    var customers = _userService.GetAllUsers();
                    foreach (var customer in customers)
                        if (customer.Email.Equals(email) && customer.Password.Equals(password))
                        {
                            return customer;
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

        public void DisplayCustomerInfo(int id)
        {
            var user = _userService.GetUserById(id);
            if (user != null && user.IsLogged && user is Customer customer)
            {
                Console.WriteLine($"Name: {customer.FullName}");
                Console.WriteLine($"Total media files in list: {customer.Playlist.Count()}");
                Console.WriteLine("Playlist:");
                foreach (var mediaFile in customer.Playlist)
                {
                    Console.WriteLine($"{mediaFile}");
                    if (mediaFile.IsPlaying)
                    {
                        Console.WriteLine($"'{mediaFile.Title} is playing'\n");
                    }
                    if (mediaFile.IsPaused)
                    {
                        Console.WriteLine($"'{mediaFile.Title} is paused'\n");
                    }
                }
            }
            else
            {
                Console.WriteLine($"Customer with email '{user?.Email}' not found or incorrect password.");
            }
        }

        public void AddUser(int id, User user)
        {
            var admin = _userService.GetUserById(id);
            var emailIsUnique = !_userService.GetAllUsers().Any(u => u.Email == user.Email);
            if (admin != null && admin.IsLogged && admin is Admin)
            {
                _userService.AddUser(user);
                Console.WriteLine($"User {user.FullName} added");

            }
            else if (!emailIsUnique)
            {
                Console.WriteLine($"Email must be unique.Try with another one");
            }
            else
            {
                Console.WriteLine($"Wrong credentials");
            }

        }

        public void RemoveUser(int id, int userId)
        {
            var admin = _userService.GetUserById(id);
            var userToRemove = _userService.GetUserById(id);
            if (admin != null && admin is Admin && userToRemove != null && userToRemove.Id == userId && admin.IsLogged)
            {
                _userService.RemoveUser(userToRemove);
            }
            if (admin == null)
            {
                Console.WriteLine($"We couldn't add this user.");
            }
            if (admin != null)
            {
                Console.WriteLine($"Wrong credentials");

            }
        }

        public void UpdateUser(int id, UserUpdateDTO updatedUser, int userId)
        // int getting oldValue => set value to 0 to specify that not willing to change this value
        {
            var user = _userService.GetUserById(id);
            var userUpdate = _userService.GetUserById(id);
            if (user != null && user is Admin && userUpdate != null && userUpdate.Id == userId && user.IsLogged)
            {
                Type mediFileType = userUpdate.GetType();
                PropertyInfo[] properties = mediFileType.GetProperties();
                foreach (var property in properties)
                {
                    PropertyInfo updatedUserProperty = updatedUser.GetType().GetProperty(property.Name)!;

                    if (updatedUserProperty != null)
                    {
                        object value = updatedUserProperty.GetValue(updatedUser)!;
                        if (value != null)
                        {
                            if (value is string v && string.IsNullOrWhiteSpace(v) || value is int vId && int.Equals(vId, vId))
                            {
                                object oldValue = property.GetValue(userUpdate)!;
                                property.SetValue(userUpdate, oldValue);
                            }
                            else
                            {
                                property.SetValue(userUpdate, value);
                            }
                        }
                        else
                        {
                            object oldValue = property.GetValue(userUpdate)!;
                            property.SetValue(userUpdate, oldValue);
                        }
                    }
                }
            }
        }

        public void AddFileToCustomerPlaylist(int id, int fileId)
        {
            var user = _userService.GetUserById(id);
            if (user != null && user is Customer customer && user.IsLogged)
            {
                var existingMediaFile = _mediaFileService.GetMediaFileById(fileId);
                if (existingMediaFile != null)
                {
                    customer.AddToPlaylist(existingMediaFile);
                    Console.WriteLine($"Hei, {customer.FullName}! You added {existingMediaFile.Title} to your playlist!");
                }
                else
                {
                    Console.WriteLine($"MediaFile with title '{id}' not found.");
                }
            }
        }

        public void RemoveFileToCustomerPlaylist(int id, int fileId)
        {
            var user = _userService.GetUserById(id);
            if (user != null && user is Customer customer && user.IsLogged)
            {
                var existingMediaFile = _mediaFileService.GetMediaFileById(fileId);
                if (existingMediaFile != null)
                {
                    customer.RemoveFromPlaylist(existingMediaFile);
                    Console.WriteLine($"Hei, {customer.FullName}! You removed {existingMediaFile.Title} from your playlist!");
                }
                else
                {
                    Console.WriteLine($"MediaFile with title '{id}' not found.");
                }
            }
        }
    }
}