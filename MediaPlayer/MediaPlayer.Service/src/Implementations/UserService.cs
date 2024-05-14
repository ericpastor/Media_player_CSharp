using MediaPlayer.Core.src.Entities;
using MediaPlayer.Core.src.Interfaces;
using MediaPlayer.Service.src.Interfaces;

namespace MediaPlayer.Service.src.Implementations
{
    public class UserService : IUserService
    {
        private ICustomerRepo _repo;

        public UserService(ICustomerRepo repo)
        {
            _repo = repo;
        }

        public List<User> GetAllUsers()
        {
            var allUsers = _repo.GetAllUsers();

            if (allUsers == null || allUsers.Count == 0)
            {
                return new List<User>();
            }
            else
            {
                return allUsers;
            }
        }

        public List<User> EraseAllUsers()
        {
            return _repo.EraseAllUsers()!;
        }

        public User? GetUserByEmailAndPassword(string email, string password)
        {
            if (email == "" || password == "")
            {
                throw new InvalidDataException();
            }
            return _repo.GetUserByEmailAndPassword(email, password);
        }

        public User? GetUserById(int id)
        {
            if (id < 0)
            {
                throw new InvalidDataException();
            }
            return _repo.GetUserById(id);
        }

        public void AddUser(User user)
        {
            _repo.AddUser(user);
        }

        public void RemoveUser(User user)
        {
            _repo.RemoveUser(user);
        }

        public void UpdateUser(UserUpdateDTO user, int id)
        {
            _repo.UpdateUser(user, id);
        }
    }
}