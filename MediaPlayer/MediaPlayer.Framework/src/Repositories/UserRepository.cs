using MediaPlayer.Core.src.Entities;
using MediaPlayer.Core.src.Interfaces;

namespace MediaPlayer.Framework.src.Repositories
{
    public class UserRepository : ICustomerRepo
    {
        private List<User> _customers;

        public UserRepository(Store store)
        {
            _customers = store.Users;
        }

        public List<User> GetAllUsers()
        {
            return _customers.ToList();
        }

        public List<User>? EraseAllUsers()
        {
            _customers.Clear();
            Console.WriteLine($"All users have been erased");
            return null;
        }

        public User GetUserById(int id)
        {
            return _customers.Single(c => c.Id == id);
        }

        public User GetUserByEmailAndPassword(string email, string password)
        {
            return _customers.Single(c => c.Email == email && c.Password == password);

        }

        public void AddUser(User user)
        {
            _customers.Add(user);
        }

        public void RemoveUser(User user)
        {
            _customers.Remove(user);
        }

        public void UpdateUser(UserUpdateDTO user, int id)
        {
            _customers.Select(m => m.id.Equals(id));
        }
    }
}