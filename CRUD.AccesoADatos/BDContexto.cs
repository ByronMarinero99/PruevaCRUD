using CRUD.EntidadesDeNegocio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CRUD.EntidadesDeNegocio.Rol;

namespace CRUD.AccesoADatos
{
    internal class BDContexto : DbContext
    {
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            //Cadena de coneccion para conectarse al servidor local
            options.UseSqlServer(@"Data Source=.;Initial Catalog=PruevaCRUD;Integrated Security=True");
        }
    }
}
