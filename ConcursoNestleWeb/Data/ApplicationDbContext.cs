using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ConcursoNestleWeb.Models;

namespace ConcursoNestleWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ConcursoNestleWeb.Models.BloqueoDesbloqueo> BloqueoDesbloqueo { get; set; } = default!;
        public DbSet<ConcursoNestleWeb.Models.Usuario> Usuarios { get; set; } = default!;

    }
}
