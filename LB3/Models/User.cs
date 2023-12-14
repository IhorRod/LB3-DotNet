namespace LB3.Models
{
    public class User
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }

        public User(string username, string password, string email, string phone)
        {
            Username = username;
            Password = password;
            Email = email;
            Phone = phone;
        }
    }
}
