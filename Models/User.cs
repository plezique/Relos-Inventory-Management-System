using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Midterm_Lab.Models
{
    public enum UserRole
    {
        Admin,
        Staff
    }

    public class User
    {
        private static int _idCounter = 1;

        public int UserId { get; private set; }
        public string Username { get; set; }
        private string _passwordHash;
        public UserRole Role { get; set; }

        public User(string username, string password, UserRole role)
        {
            UserId = _idCounter++;
            Username = username;
            _passwordHash = HashPassword(password);
            Role = role;
        }

        private string HashPassword(string password)
        {
            // Simple hash for demo purposes (use BCrypt in production)
            int hash = 17;
            foreach (char c in password)
                hash = hash * 31 + c;
            return hash.ToString();
        }

        public bool ValidatePassword(string password)
        {
            return HashPassword(password) == _passwordHash;
        }

        public override string ToString()
        {
            return $"[{UserId}] {Username} | Role: {Role}";
        }
    }
}
