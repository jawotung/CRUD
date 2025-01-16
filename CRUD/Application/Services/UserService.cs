using Application.Interfaces;
using Application.Models;
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
        public async Task<ReturnStatusList<UserDTO>> GetList()
        {
            ReturnStatusList<UserDTO> result = new() { Status = 1} ;
            try
            {

                List<User> userlist = await _repo.GetList();
                result.Data = _mapper.Map<List<UserDTO>>(userlist);
                result.Status = 0;
                result.Message = "Successfull";

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
                result.Message = "Successfull";

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
                result.Message = "Successfull";

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
                result.Message = "Successfull";
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }
    }
}
