using Application.Interfaces;
using AutoMapper;
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
        public async Task<List<User>> GetList()
        {
            try
            {
                return await _context.Users.ToListAsync();
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
