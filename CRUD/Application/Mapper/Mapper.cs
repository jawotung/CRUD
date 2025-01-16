using Application.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebAPI;

namespace Application.Mapper
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();
            CreateMap<User, User>();
        }
    }
}
