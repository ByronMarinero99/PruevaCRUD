using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CRUD.AccesoADatos;
using CRUD.EntidadesDeNegocio;
using CRUD.LogicaDeNegocio;

namespace CRUD.InterfazGraficaMVC.Controllers
{
    public class UsuarioController : Controller
    {
        UsuarioBL usuarioBL = new UsuarioBL();
        RolBL rolBL = new RolBL();

        // Acción que muestra la página Proveedor
        public async Task<IActionResult> Index(Usuario pUsuario = null)
        {
            if (pUsuario == null)
                pUsuario = new Usuario();

            if (pUsuario.top_aux == 0)
                pUsuario.top_aux = 10;

            else if (pUsuario.top_aux == -1)
                pUsuario.top_aux = 0;

            var usuarios = await usuarioBL.buscarincluirproductoasync(pUsuario);
            ViewBag.roles = await rolBL.ObtenerTodosAsync();
            ViewBag.Top = pUsuario.top_aux;
            return View(usuarios);
        }



        // Acción que muestra los detalles de un Producto
        public async Task<IActionResult> Details(int id)
        {
            var usuarios = await usuarioBL.ObtenerPorIDAsync(new Usuario { Id = id });
            usuarios.Roles = await rolBL.ObtenerPorIdAsync(new Rol { Id = usuarios.IdRoles });
            return View(usuarios);
        }

        
    }
}
