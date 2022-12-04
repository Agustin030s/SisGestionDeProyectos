using Model.Domain;
using Services;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SisControlCalidad_V2._0.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoriaController : Controller
    {
        private readonly CategoriaServices _categoriaServices = new CategoriaServices();
        // GET: Categoria
        public ActionResult Index()
        {
            var model = _categoriaServices.GetAll();
            return View(model); 
        }

        // GET: Categoria/Details/5
        public ActionResult Details(int id)
        {
            var model = _categoriaServices.Get(id);
            return View("Details", model);
        }

        // GET: Categoria/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categoria/Create
        [HttpPost]
        public ActionResult Create(Categoria model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _categoriaServices.create(model);
                }

                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        // GET: Categoria/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Categoria/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Categoria/Delete/5
        public ActionResult Delete(int id)
        {
            var model = _categoriaServices.Get(id);
            return View(model);
        }

        // POST: Categoria/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Categoria categoria)
        {
            try
            {
                if(categoria == null)
                {
                    return HttpNotFound();
                }
                _categoriaServices.delete(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Reporte()
        {
            IEnumerable<Categoria> categorias;

            categorias = _categoriaServices.GetAll();

            return View("Reporte", categorias);
        }
    }
}
