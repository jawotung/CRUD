using Application.Models.Helpers;
using Application.Models;
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
        Task<PaginatedList<User, UserDTO>> GetList(int Page);
        Task<User> AddUser(User data);
        Task<User> UpdateUser(User data);
        Task DeleteUser(int id);
    }
}
