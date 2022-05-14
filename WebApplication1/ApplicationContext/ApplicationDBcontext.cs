using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.ApplicationContext
{
    public class ApplicationDBcontext : DbContext
    {
        public DbSet<shopcard> ShopCard { get; set; }
        public DbSet<userscard> Users { get; set; }
        public ApplicationDBcontext(DbContextOptions<ApplicationDBcontext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
