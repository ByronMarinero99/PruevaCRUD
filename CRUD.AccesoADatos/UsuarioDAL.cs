using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//----------------------------------
using Microsoft.EntityFrameworkCore;
using CRUD.EntidadesDeNegocio;
using static CRUD.EntidadesDeNegocio.Usuario;

namespace CRUD.AccesoADatos
{
    public class UsuarioDAL
    {
        // proc async de acceso a datos para agregar un nuevo registro
        public static async Task<int> CrearAsync(Usuario pUsuario)
        {
            int result = 0; //variable
            using (var bdContexto = new BDContexto())
            {
                bdContexto.Add(pUsuario);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }

        // proc async de acceso a datos para modificar
        public static async Task<int> ModificarAsync(Usuario pUsuario)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var usuario = await bdContexto.Usuarios.FirstOrDefaultAsync(p => p.Id == pUsuario.Id);
                usuario.Nombre = pUsuario.Nombre;
                usuario.Correo = pUsuario.Correo;
                usuario.Password = pUsuario.Password;
                usuario.IdRoles = pUsuario.IdRoles;
                bdContexto.Update(usuario);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }

        // proc async de acceso a datos para eliminar
        public static async Task<int> EliminarAsync(Usuario pUsuario)
        {
            int result = 0;
            using (var bdcontexto = new BDContexto())
            {
                var usuario = await bdcontexto.Usuarios.FirstOrDefaultAsync(p => p.Id == pUsuario.Id);
                bdcontexto.Usuarios.Remove(usuario);
                result = await bdcontexto.SaveChangesAsync();
            }
            return result;
        }

        // proc async de acceso a datos para traer registros de la BD por el id
        public static async Task<Usuario> ObtenerPorIdAsync(Usuario pUsuario)
        {
            var usuario = new Usuario();
            using (var bdcontexto = new BDContexto())
            {
                usuario = await bdcontexto.Usuarios.FirstOrDefaultAsync(p => p.Id == pUsuario.Id);
            }
            return usuario;
        }

        // proc async de acceso a datos para traer registros de la BD
        public static async Task<List<Usuario>> ObtenerTodosAsync()
        {
            var usuario = new List<Usuario>();
            using (var bdcontexto = new BDContexto())
            {
                usuario = await bdcontexto.Usuarios.ToListAsync();
            }
            return usuario;
        }

        // proc async de acceso a datos para traer registros de la BD
        internal static IQueryable<Usuario> QuerySelect(IQueryable<Usuario> pQuery, Usuario pUsuario)
        {
            if (pUsuario.Id > 0)
                pQuery = pQuery.Where(p => p.Id == pUsuario.Id);

            if (pUsuario.IdRoles > 0)
                pQuery.Where(p => p.Id == pUsuario.IdRoles);

            if (!string.IsNullOrWhiteSpace(pUsuario.Nombre))
                pQuery = pQuery.Where(p => p.Nombre.Contains(pUsuario.Nombre));
            if (!string.IsNullOrWhiteSpace(pUsuario.Correo))
                pQuery = pQuery.Where(p => p.Correo.Contains(pUsuario.Correo));
            if (!string.IsNullOrWhiteSpace(pUsuario.Password))
                pQuery = pQuery.Where(p => p.Password.Contains(pUsuario.Password));

            pQuery = pQuery.OrderByDescending(c => c.Id).AsQueryable();

            if (pUsuario.top_aux > 0)
                pQuery = pQuery.Take(pUsuario.top_aux).AsQueryable();

            return pQuery;
        }

        // proc async de acceso a datos para buscar
        public static async Task<List<Usuario>> BuscarAsync(Usuario pRopa)
        {
            var usuario = new List<Usuario>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.Usuarios.AsQueryable();
                select = QuerySelect(select, pRopa);
                usuario = await select.ToListAsync();
            }
            return usuario;
        }

        // proc proc async de acceso a datos para incluir datos foraneos
        public static async Task<List<Usuario>> BuscarIncluirProductoAsync(Usuario pUsuario)
        {
            var usuario = new List<Usuario>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.Usuarios.AsQueryable();
                select = QuerySelect(select, pUsuario).Include(p => p.Roles).AsQueryable();
                usuario = await select.ToListAsync();
            }
            return usuario;
        }
    }
}
