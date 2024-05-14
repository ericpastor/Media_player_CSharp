namespace MediaPlayer.Core.src.Entities
{
    public class Admin : User
    {
        public Admin(string password, string fullName, string email)
        : base(password, fullName, email, Role.Admin) { }

        public override string ToString()
        {
            return $"ID: {Id}\n Name: {FullName}\n Email: {Email}\n";
        }
    }
}