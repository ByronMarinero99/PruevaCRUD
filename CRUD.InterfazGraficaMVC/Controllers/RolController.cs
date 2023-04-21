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
    public class RolController : Controller
    {


        RolBL rolBL = new RolBL(); //INSTANCIA DE ACCESO A LOS METODOS DE LA CLASE RolBL

        // Acción que muestra la página principal de roles
        public async Task<IActionResult> Index(Rol pRol = null)
        {
            if (pRol == null)
                pRol = new Rol();
            if (pRol.top_aux == 0)//Si no ha puesto cuantos quiere mostrar
                pRol.top_aux = 10; //Trae 10 registros por default
            else if (pRol.top_aux == -1)//Reset
                pRol.top_aux = 0;

            var roles = await rolBL.BuscarAsync(pRol);
            ViewBag.Top = pRol.top_aux;

            return View(roles);
        }

        // Acción que muestra el detalle de un registro existente
        public async Task<IActionResult> Details(int id)
        {
            var rol = await rolBL.ObtenerPorIdAsync(new Rol { Id = id });
            return View(rol);
        }


        // Acción que muestra el formulario para agregar un nuevo registro
        public IActionResult Create()
        {
            ViewBag.Error = "";
            return View();
        }

        // Acción que recibe los datos del formulario y los envía a la bd
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Rol pRol)
        {
            try
            {
                int result = await rolBL.CrearAsync(pRol);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        //Acción que muestra el formulario con los datos cargados para modificar
        public async Task<IActionResult> Edit(Rol pRol)
        {
            var rol = await rolBL.ObtenerPorIdAsync(pRol);
            ViewBag.Error = "";
            return View(rol);
        }


        //Acción que recibe los datos modificados y los envía a la BD
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Rol pRol)
        {
            try
            {
                int result = await rolBL.ModificarAsync(pRol);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = "";
                return View(pRol);
            }
        }

        // accion que muestra los datos para eliminar
        public async Task<IActionResult> Delete(Rol pRol)
        {
            ViewBag.Error = "";

            var roles = await rolBL.ObtenerPorIdAsync(pRol);
            return View(roles);
        }

        // accion que elimina los datos
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Rol pRol)
        {
            try
            {
                int result = await rolBL.EliminarAsync(pRol);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pRol);
            }
        }
    }
}
