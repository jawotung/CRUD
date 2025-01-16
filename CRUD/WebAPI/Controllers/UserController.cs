using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _service;
        public UserController(IUserService service)
        {
            _service = service;
        }
        [HttpGet("GetList")]
        public async Task<IActionResult> GetList()
        {
            return Ok(await _service.GetList());
        }
        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser(UserDTO data)
        {
            return Ok(await _service.AddUser(data));
        }
        [HttpPut("EditUser/{id}")]
        public async Task<IActionResult> EditUser(int id, UserDTO data)
        {
            return Ok(await _service.EditUser(id, data));
        }
        [HttpDelete("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            return Ok(await _service.DeleteUser(id));
        }
    }
}
