using MediaPlayer.Core.src.Entities;
using MediaPlayer.Service.src.Implementations;
using MediaPlayer.Service.src.Interfaces;

namespace MediaPlayer.Controller.src
{
    public class AuthentificationController
    {
        private readonly IUserService _userService;
        private readonly IMediaFileService _mediaFileService;

        public AuthentificationController(IUserService userService, IMediaFileService mediaFileService)
        {
            _userService = userService;
            _mediaFileService = mediaFileService;
        }

        public User? Login(string email, string password)
        {
            try
            {
                var user = _userService.GetUserByEmailAndPassword(email, password);
                if (user != null)
                {
                    user.IsLogged = true;
                }
                Console.WriteLine($"Welcome, {user?.FullName}!");
                return user;
            }
            catch (Exception e)
            {
                Console.WriteLine("Wrong credentials. Try again!");
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public void Logout(int id)
        {
            var user = _userService.GetUserById(id);
            if (user != null)
            {
                user.IsLogged = false;
                Console.WriteLine($"See you next time, {user?.FullName}!");
            }
        }
    }
}