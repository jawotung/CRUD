using Application.Interfaces;
using Application.Models.Helpers;
using Application.Models;
using AutoMapper;
using Azure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI;

namespace Infrastructure.Repositories
{
    public class UserRepositories : IUserRepositories
    {
        private readonly CRUDDBContext _context;
        private readonly IMapper _mapper;
        public UserRepositories(CRUDDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PaginatedList<User, UserDTO>> GetList(int Page)
        {
            try
            {
                IQueryable<User> list = _context.Users.OrderBy(x => x.UserId);
                return await PaginatedList<User, UserDTO>.CreateAsync(list, _mapper, Page);
            }
            catch
            {
                throw;
            }
        }
        public async Task<User> AddUser(User data)
        {
            try
            {
                data.CreateDate = DateTime.Now;
                data.UpdateDate = DateTime.Now;
                _context.Users.Add(data);
                await _context.SaveChangesAsync();
                return data;
            }
            catch
            {
                throw;
            }
        }
        public async Task<User> UpdateUser(User data)
        {
            try
            {
                var existingUser = await _context.Users.FindAsync(data.Id);
                if (existingUser == null)
                    throw new InvalidOperationException("No user found");

                _mapper.Map(data, existingUser);
                existingUser.UpdateDate = DateTime.Now;
                existingUser.UpdateDate = DateTime.Now;
                _context.Entry(existingUser).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return data;
            }
            catch
            {
                throw;
            }
        }
        public async Task DeleteUser(int id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user.Id == null)
                    throw new InvalidOperationException("No user found");
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();

                //Option 2
                //var user = await _context.Users.FindAsync(id);
                //user.IsDeleted = 1;
                //user.UpdateDate = DateTime.Now;
                //_context.Entry(user).State = EntityState.Modified;
                //await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
