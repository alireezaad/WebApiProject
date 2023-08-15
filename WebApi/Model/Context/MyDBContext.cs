using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace WebApi.Model.DBContext
{
    public class MyDBContext : DbContext
    {
        public MyDBContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Twitt> twitts { get; set; }
    }
}
