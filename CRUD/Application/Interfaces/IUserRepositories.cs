using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI;

namespace Application.Interfaces
{
    public interface IUserRepositories
    {
        Task<List<User>> GetList();
        Task<User> AddUser(User data);
        Task<User> UpdateUser(User data);
        Task DeleteUser(int id);
    }
}
