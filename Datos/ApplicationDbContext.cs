using Microsoft.EntityFrameworkCore;
using PoyFC_Aranda.Modelo;
using System.Collections.Generic;

namespace PoyFC_Aranda.Datos
{
    public class ApplicationDbContext: DbContext
    {
        public DbSet<Product> Product { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    }
}
