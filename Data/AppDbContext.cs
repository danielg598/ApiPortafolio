using ApiPortafolio.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiPortafolio.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) { }
        
        public DbSet<EmpresaContacto> EmpresaContacto { get; set; }
    }
}
