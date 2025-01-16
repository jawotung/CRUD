using Application.Interfaces;
using Application.Models;
using Application.Models.Helpers;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepositories _repo;
        private readonly IMapper _mapper;
        public UserService(IUserRepositories repo, IMapper mapper) {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<ReturnStatusData<PaginatedList<UserDTO>>> GetList(int Page = 1)
        {
            ReturnStatusData<PaginatedList<UserDTO>> result = new() { Status = 1} ;
            try
            {
                PaginatedList<User, UserDTO> data = await _repo.GetList(Page);
                result.Data = new PaginatedList<UserDTO>(data.Data, data.CountData, data.PageIndex, data.TotalPages);
                result.Status = 0;
                result.Message = "";
            }
            catch(Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }
        public async Task<ReturnStatusData<UserDTO>> AddUser(UserDTO data)
        {
            ReturnStatusData<UserDTO> result = new() { Status = 1 };
            try
            {

                User user = _mapper.Map<User>(data);
                User userlist = await _repo.AddUser(user);
                result.Status = 0;
                result.Message = "Successfull! User was successfully added";

            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }
        public async Task<ReturnStatusData<UserDTO>> EditUser(int id, UserDTO data)
        {
            ReturnStatusData<UserDTO> result = new() { Status = 1 };
            try
            {
                if(id != data.Id)
                {
                    result.Message = "No user exist";
                    return result;
                }
                User user = _mapper.Map<User>(data);
                User userlist = await _repo.UpdateUser(user);
                result.Status = 0;
                result.Message = "Successfull! User was successfully updated";

            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }
        public async Task<ReturnStatus> DeleteUser(int id)
        {
            ReturnStatus result = new() { Status = 1 };
            try
            {
                await _repo.DeleteUser(id);
                result.Status = 0;
                result.Message = "Successfull! User was successfully deleted";
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }
    }
}
