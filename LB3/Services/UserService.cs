using LB3.Models;
using System.Text.Json;

namespace LB3.Services
{
    public class UserService
    {

        private readonly string _filePath;
        private List<User> _users = new List<User>();

        public UserService(string filePath)
        {
            _filePath = filePath;

            if (File.Exists(_filePath))
            {
                _users = JsonSerializer.Deserialize<List<User>>(File.ReadAllText(_filePath));
            }
            else
            {
                _users = new List<User>();
                File.WriteAllText(_filePath, JsonSerializer.Serialize(_users));
            }
        }

        public User? get(string username)
        {
            return _users.FirstOrDefault(u => u.Username == username);
        }

        public User? create(User user)
        {
            if (_users.Any(u => u.Username == user.Username))
            {
                return null;
            }

            _users.Add(user);
            File.WriteAllText(_filePath, JsonSerializer.Serialize(_users));

            return user;
        }

        public User? update(string username, User user)
        {
            var existingUser = _users.FirstOrDefault(u => u.Username == username);

            if (existingUser == null)
            {
                return null;
            }

            existingUser.Username = user.Username == null ? user.Username : existingUser.Username;
            existingUser.Password = user.Password == null ? user.Password : existingUser.Password;
            existingUser.Email = user.Email == null ? user.Email : existingUser.Email;
            existingUser.Phone = user.Phone == null ? user.Phone : existingUser.Phone;

            File.WriteAllText(_filePath, JsonSerializer.Serialize(_users));

            return existingUser;
        }

        public bool delete(string username)
        {
            var existingUser = _users.FirstOrDefault(u => u.Username == username);

            if (existingUser == null)
            {
                return false;
            }

            _users.Remove(existingUser);
            File.WriteAllText(_filePath, JsonSerializer.Serialize(_users));

            return true;
        }
    }
}
