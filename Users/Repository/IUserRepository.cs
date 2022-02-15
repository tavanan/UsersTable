using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Users.Models;

namespace Users.Repository
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAllUsers();

        void AddUser(User user);

        void DeleteUser(int id);

        void UpdateUser(User user);

        User GetSingleUser(int id);

        public bool UserExists(User user);

        public bool IdExists(int id);

    }
}
