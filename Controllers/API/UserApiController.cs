using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using reviseAuthentication.Data;
using reviseAuthentication.Models;

namespace reviseAuthentication.Controllers.API
{
    public class UserApiController : ControllerBase
    {
        private readonly MyDbContext _context;

        public UserApiController(MyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        // API/UserApi/GetUsers
        public async Task<List<User>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        [HttpGet]
        // API/UserApi/GetUser/{id}
        public async Task<User> GetUser(int id)
        {
            return await _context.Users.FindAsync(id);
        }
    }
}
