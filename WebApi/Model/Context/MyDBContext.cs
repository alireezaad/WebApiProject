using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace WebApi.Model.DBContext
{
    public class MyDBContext : DbContext
    {
        public MyDBContext(DbContextOptions options) : base(options)
        {

        }
        public virtual DbSet<User> users { get; set; }
    }
}
