using Microsoft.EntityFrameworkCore;
using reviseAuthentication.Models;


namespace reviseAuthentication.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }

        public DbSet<reviseAuthentication.Models.Email>? Email { get; set; }

        public DbSet<reviseAuthentication.Models.StudentModel>? StudentModel { get; set; }
    }





}
