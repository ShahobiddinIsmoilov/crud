using crud.Models;
using Microsoft.EntityFrameworkCore;

namespace crud.Data
{
    public class CrudDbContext : DbContext
    {
        public CrudDbContext(DbContextOptions<CrudDbContext> options)
            :base(options)
        {
        }

        public DbSet<Crud> Crudd { get; set; }
    }
}