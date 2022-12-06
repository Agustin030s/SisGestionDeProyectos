using Model.Custom;
using Model.Domain;
using Services;
using System;
using System.Collections.Generic;
using System.Web.Mvc;


namespace SisGestionDeProyectos.Controllers
{
    public class ProyectosController : Controller
    {
        private readonly ProyectoServices proyectoservice = new ProyectoServices();
        private readonly EmpleadoServices empleadoServices = new EmpleadoServices();
        private readonly CategoriaServices categoriaServices = new CategoriaServices();
        private readonly TareasServices tareasServices = new TareasServices();
        // GET: Proyectos
        [Authorize]
        public ActionResult Index()
        {
            List<SelectListItem> Fecha = new List<SelectListItem>()
            {
                new SelectListItem { Text="Todos", Value="1"},
                new SelectListItem { Text="Proyectos Atrasados", Value="2" },
                new SelectListItem { Text="Proyectos Proximos a Iniciar", Value="3" },
                new SelectListItem { Text="Proyectos Terminados", Value="4" },
                new SelectListItem { Text="Mayor Costo", Value="5" }
            };

            ViewBag.Fecha = Fecha;
            var listado = proyectoservice.GetAll();

            return View(listado);
        }
        [Authorize]
        public ActionResult Buscar(string palabra, string Fecha)
        {
            var proyectos = proyectoservice.Buscar(palabra, Fecha);

            List<SelectListItem> Fecha2 = new List<SelectListItem>()
            {
                new SelectListItem { Text="Todos", Value="1"},
                new SelectListItem { Text="Proyectos Atrasados", Value="2" },
                new SelectListItem { Text="Proyectos Proximos a Iniciar", Value="3" },
                new SelectListItem { Text="Proyectos Terminados", Value="4" },
                new SelectListItem { Text="Mayor Costo", Value="5" }
            };

            ViewBag.Fecha = Fecha2;

            Session["Palabra"] = palabra;
            Session["Fecha"] = Fecha;

            return View("Index", proyectos);
        }

        [Authorize]
        // GET: Proyectos/Details/5
        public ActionResult Details(int id)
        {
            var model = proyectoservice.Get(id);
            return View("Details", model);
        }

        [Authorize(Roles = "Admin")]
        // GET: Proyectos/Create
        public ActionResult Create()
        {
            ViewBag.Id_Categoria = new SelectList(categoriaServices.GetAll(), "Id_Categoria", "Nombre");
            ViewBag.Legajo = new SelectList(empleadoServices.GetAll(), "Legajo", "ApyNom");
            return View();
        }

        [Authorize(Roles = "Admin")]
        // POST: Proyectos/Create
        [HttpPost]
        public ActionResult Create(Proyectos proyecto)
        {
            if (ModelState.IsValid)
            {
                proyectoservice.Create(proyecto);
                return RedirectToAction("Index");
            }

            ViewBag.Id_Categoria = new SelectList(categoriaServices.GetAll(), "Id_Categoria", "Nombre", proyecto.Id_Categoria);
            ViewBag.Legajo = new SelectList(empleadoServices.GetAll(), "Legajo", "ApyNom", proyecto.Legajo);

            return View(proyecto);
        }

        [Authorize(Roles = "Admin")]
        // GET: Proyectos/Edit/5
        public ActionResult Edit(int id)
        {
            var model = proyectoservice.GetEdit(id);
            ViewBag.Id_Categoria = new SelectList(categoriaServices.GetAll(), "Id_Categoria", "Nombre");
            ViewBag.Legajo = new SelectList(empleadoServices.GetAll(), "Legajo", "ApyNom");
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        // POST: Proyectos/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Proyectos proyecto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    proyectoservice.Update(proyecto);
                }


                ViewBag.Id_Categoria = new SelectList(categoriaServices.GetAll(), "Id_Categoria", "Nombre", proyecto.Id_Categoria);
                ViewBag.Legajo = new SelectList(empleadoServices.GetAll(), "Legajo", "ApyNom", proyecto.Legajo);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        // GET: Proyectos/Delete/5
        public ActionResult Delete(int id)
        {
            var model = proyectoservice.Get(id);
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        // POST: Proyectos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                proyectoservice.Delete(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult UpdateCosto(int id)
        {
            List<SelectListItem> Fecha = new List<SelectListItem>()
            {
                new SelectListItem { Text="Todos", Value="1"},
                new SelectListItem { Text="Proyectos Atrasados", Value="2" },
                new SelectListItem { Text="Proyectos Proximos a Iniciar", Value="3" }
            };

            ViewBag.Fecha = Fecha;
            double costo = tareasServices.CostoProyecto(id);

            proyectoservice.UpdateCosto(id, costo);

            return RedirectToAction("Index");
        }

        //Reportes
        [Authorize]
        public ActionResult Reporte()
        {
            IEnumerable<ProyectosGrid> proyectos;

            if(Session["Palabra"] != null)
            {
                string palabra = Session["Palabra"].ToString();
                string Fecha = Session["Fecha"].ToString();

                proyectos = proyectoservice.Buscar(palabra, Fecha);
            } else
            {
                proyectos = proyectoservice.GetAll();
            }

            return View("Reporte", proyectos);
        }
    }
}
