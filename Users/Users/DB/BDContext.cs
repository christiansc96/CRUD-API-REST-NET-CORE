using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TareaSemana7_E1_ChristianSanchez.Models;

namespace TareaSemana7_E1_ChristianSanchez.DB
{
    public class BDContext:DbContext
    {
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLOCALDB;Database=TareaSemana7DB;Trusted_Connection=True;");
        }
    }
}
