using Common;
using Model.Custom;
using Model.Domain;
using Services;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SisGestionDeProyectos.Controllers
{
    public class TareasController : Controller
    {
        private readonly TareasServices tareasServices = new TareasServices();
        private readonly ProyectoServices proyectoServices = new ProyectoServices();
        // GET: Tareas
        [Authorize]
        public ActionResult Index()
        {
            List<SelectListItem> Opcion = new List<SelectListItem>()
            {
                new SelectListItem { Text="Todos", Value="1"},
                new SelectListItem { Text="Tareas Atrasadas", Value="2" },
                new SelectListItem { Text="Tareas Completadas", Value="3" },
                new SelectListItem { Text="Mayor Costo", Value="4" }
            };

            ViewBag.Opcion = Opcion;

            var listado = tareasServices.GetAll();
            return View(listado);
        }

        [Authorize]
        public ActionResult Buscar(string palabra, string Opcion)
        {
            var tareas = tareasServices.Buscar(palabra, Opcion);

            List<SelectListItem> Opcion2 = new List<SelectListItem>()
            {
                new SelectListItem { Text="Todos", Value="1"},
                new SelectListItem { Text="Tareas Atrasadas", Value="2" },
                new SelectListItem { Text="Tareas Completadas", Value="3" },
                new SelectListItem { Text="Mayor Costo", Value="4" }
            };

            ViewBag.Opcion = Opcion2;

            Session["Palabra"] = palabra;
            Session["Opcion"] = Opcion;

            return View("Index", tareas);
        }

        [Authorize]
        // GET: Tareas/Details/5
        public ActionResult Details(int id)
        {
            var model = tareasServices.Get(id);
            return View("Details", model);
        }

        [Authorize(Roles = "Admin, Empleado")]
        public ActionResult TareasXProyecto(int id)
        {
            var proy = proyectoServices.Get(id);
            ViewBag.ProyectoGrid = proy;

            Session["Id"] = id;

            var model = tareasServices.TareasXProyecto(id);
            return View(model);
        }

        public ActionResult Imprimir()
        {
            int id = int.Parse(Session["Id"].ToString());

            var proy = proyectoServices.Get(id);
            ViewBag.ProyectoGrid = proy;

            var model = tareasServices.TareasXProyecto(id);
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        // GET: Tareas/Create
        public ActionResult Create()
        {
            ViewBag.Id_Proyecto = new SelectList(proyectoServices.GetAll(), "Id_Proyecto", "Titulo");
            return View();
        }

        [Authorize(Roles = "Admin")]
        // POST: Tareas/Create
        [HttpPost]
        public ActionResult Create(Tareas tarea)
        {
            if (ModelState.IsValid)
            {
                tareasServices.Create(tarea);
                return RedirectToAction("Index");
            }

            ViewBag.Id_Proyecto = new SelectList(proyectoServices.GetAll(), "Id_Proyecto", "Titulo", tarea.Id_Proyecto);
            return View(tarea);
        }

        [Authorize(Roles = "Admin, Empleado")]
        // GET: Tareas/Edit/5
        public ActionResult Edit(int id)
        {
            var model = tareasServices.GetEdit(id);

            return View(model);
        }

        [Authorize(Roles = "Admin, Empleado")]
        // POST: Tareas/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Tareas tarea)
        {
            var tareas = tareasServices.GetEdit(id);

            if ((tareas.Avance * 100) > tarea.Avance)
            {
                throw new Exception("No puede ingresar un Avance menor al Avance anteriormente asignado");
            }
            else if (tarea.Avance < 0)
            {
                throw new Exception("No puede ingresar valores Negativos");
            }
            else if (tarea.Avance > 100)
            {
                throw new Exception("El avance no puede ser mayor a 100");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    tareasServices.Update(tarea);
                }
            }

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        // GET: Tareas/Delete/5
        public ActionResult Delete(int id)
        {
            var model = tareasServices.Get(id);
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        // POST: Tareas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                tareasServices.Delete(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Roles = "Empleado")]
        public ActionResult Reporte()
        {
            IEnumerable<TareasGrid> tareas;

            if (Session["Palabra"] != null)
            {
                string palabra = Session["Palabra"].ToString();
                string Opcion = Session["Opcion"].ToString();

                tareas = tareasServices.Buscar(palabra, Opcion);
            }
            else
            {
                tareas = tareasServices.GetAll();
            }

            return View("Reporte", tareas);
        }
    }
}
