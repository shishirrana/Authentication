using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using reviseAuthentication.Data;
using reviseAuthentication.Models;

namespace reviseAuthentication.Controllers.API
{
    /// <summary>
    /// API should always be created or used as static type of class and
    /// since it is static type there is no object 
    /// i.e., no constructor 
    /// </summary>
    public static class UserMinimalApiController
    {
        public static void MapEndpoints(this WebApplication app)
        {
            app.MapGet("api/UserMinimal/GetUsers", GetUsers);
            app.MapGet("api/UserMinimal/GetUser", GetUser);

        }

        [HttpGet]
        // API/UserMinimalApi/GetUsers
        public static async Task<List<User>> GetUsers(MyDbContext context)
        {
           return await context.Users.ToListAsync();
        }

        [HttpGet]
        // API/UserMinimalApi/GetUser/{id}
        public static async Task<User> GetUser(int id, MyDbContext context)
        {
            return await context.Users.FindAsync(id);
        }
    }
}
