using MediaPlayer.Core.src.Interfaces;

namespace MediaPlayer.Core.src.Entities
{
    public enum Role
    {
        Customer,
        Admin
    }

    public class User : IObserver
    {
        private static int _lastId = 0;

        static int GenerateId()
        {
            return Interlocked.Increment(ref _lastId);
        }

        public int id = GenerateId();

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Password { get; private set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public Role Role { get; protected set; }
        public bool IsLogged { get; set; }


        public User(string password, string fullName, string email, Role role)
        {
            this.Password = password;
            this.Id = id;
            this.FullName = fullName;
            this.Email = email;
            this.Role = role;
            this.IsLogged = false;
        }

        public void ReceiveNoti(string message)
        {
            Console.WriteLine($"Hello {FullName}, you have receive a message from the store: {message}");
        }

        public void React(string message)
        {
            Console.WriteLine($"Hello {FullName}, you have receive a message from the store: {message}");
        }
    }

    public class UserUpdateDTO
    {
        public string FullName { get; set; }
        public string Email { get; set; }

        public UserUpdateDTO(string fullName, string email)
        {
            this.FullName = fullName;
            this.Email = email;
        }
    }
}