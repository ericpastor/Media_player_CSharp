using MediaPlayer.Core.src.Entities;

namespace MediaPlayer.Core.src.Interfaces
{
    public interface ICustomerRepo
    {
        public List<User> GetAllUsers();
        public List<User>? EraseAllUsers();
        public User GetUserByEmailAndPassword(string email, string password);
        public User GetUserById(int id);
        public void AddUser(User mediaFileFactory);
        public void RemoveUser(User mediaFile);
        public void UpdateUser(UserUpdateDTO User, int id);
    }
}