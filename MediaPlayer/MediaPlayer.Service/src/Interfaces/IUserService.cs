using MediaPlayer.Core.src.Entities;

namespace MediaPlayer.Service.src.Interfaces
{
    public interface IUserService
    {
        public List<User> GetAllUsers();
        public List<User> EraseAllUsers();
        public User? GetUserByEmailAndPassword(string email, string password);
        public User? GetUserById(int id);
        public void AddUser(User user);
        public void RemoveUser(User user);
        public void UpdateUser(UserUpdateDTO user, int id);
    }
}