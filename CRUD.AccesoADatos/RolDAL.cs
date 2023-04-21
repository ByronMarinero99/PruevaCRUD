using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//----------------------------------
using Microsoft.EntityFrameworkCore;
using CRUD.EntidadesDeNegocio;
using static CRUD.EntidadesDeNegocio.Rol;
using static CRUD.EntidadesDeNegocio.Usuario;
using System.Data;

namespace CRUD.AccesoADatos
{
    public class RolDAL
    {
        // Proc Async  de acceso a datos para agregar
        public static async Task<int> CrearAsync(Rol pRol)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                bdContexto.Add(pRol);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }

        // Proc async  de acceso a datos para modoficar
        public static async Task<int> ModificarAsync(Rol pRol)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var rol = await bdContexto.Roles.FirstOrDefaultAsync(p => p.Id == pRol.Id);
                rol.Nombre = pRol.Nombre;
                rol.Descripcion = pRol.Descripcion;

                bdContexto.Update(rol);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }

        //Proc async de acceso a datos para eliminar registros
        public static async Task<int> EliminarAsync(Rol pRol)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var rol = await bdContexto.Roles.FirstOrDefaultAsync(s => s.Id == pRol.Id);
                bdContexto.Roles.Remove(rol);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }

        // proc async de acceso a datos para traer registros de la BD por el ID
        public static async Task<Rol> ObtenerPorIdAsync(Rol pRol)
        {
            var rol = new Rol();
            using (var bdContexto = new BDContexto())
            {
                rol = await bdContexto.Roles.FirstOrDefaultAsync(s => s.Id == pRol.Id);
            }
            return rol;
        }

        // proc async de acceso a datos para traer todos los registros de la BD
        public static async Task<List<Rol>> ObtenerTodosAsync()
        {
            var roles = new List<Rol>();
            using (var bdContexto = new BDContexto())
            {
                roles = await bdContexto.Roles.ToListAsync();
            }
            return roles;
        }

        // proc async de acceso a datos para buscar registros
        internal static IQueryable<Rol> QuerySelect(IQueryable<Rol> pQuery, Rol pRol)
        {
            if (pRol.Id > 0)
                pQuery = pQuery.Where(s => s.Id == pRol.Id);

            if (!string.IsNullOrWhiteSpace(pRol.Nombre))
                pQuery = pQuery.Where(s => s.Nombre.Contains(pRol.Nombre));

            if (!string.IsNullOrWhiteSpace(pRol.Descripcion))
                pQuery = pQuery.Where(s => s.Descripcion.Contains(pRol.Descripcion));

            pQuery = pQuery.OrderByDescending(s => s.Id).AsQueryable();

            if (pRol.top_aux > 0)
                pQuery = pQuery.Take(pRol.top_aux).AsQueryable();

            return pQuery;
        }

        // proc async de acceso para buscar
        public static async Task<List<Rol>> BuscarAsync(Rol pRol)
        {
            var roles = new List<Rol>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.Roles.AsQueryable();
                select = QuerySelect(select, pRol);
                roles = await select.ToListAsync();
            }
            return roles;
        }

    }
}
