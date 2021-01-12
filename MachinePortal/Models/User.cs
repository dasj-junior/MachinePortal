using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MachinePortal.Models
{
    public class User
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<Permission> Permissions { get; set; }

        public User()
        {
        }

        public User(int Id, string username, string password, string firstName, string lastName, string email)
        {
            ID = Id;
            Username = username;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public void AddPermission(Permission permission)
        {
            Permissions.Add(permission);
        }

        public void Remove(Permission permission)
        {
            Permissions.Remove(permission);
        }
    }
}
