using Application.Models;
using Application.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<ReturnStatusData<PaginatedList<UserDTO>>> GetList(int Page = 1);
        Task<ReturnStatusData<UserDTO>> AddUser(UserDTO data);
        Task<ReturnStatusData<UserDTO>> EditUser(int id, UserDTO data);
        Task<ReturnStatus> DeleteUser(int id);
    }
}
