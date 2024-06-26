﻿using Model.Custom;
using Model.Domain;
using Services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;

namespace SisGestionDeProyectos.Controllers
{
    [Authorize(Roles = "Admin")]
    public class EmpleadoController : Controller
    {
        private readonly EmpleadoServices _empleadoService = new EmpleadoServices();
        private readonly RolesServices rolesServices = new RolesServices();
        private readonly UserService userService = new UserService();


        // GET: Empleado
        public ActionResult Index()
        {
            var model = _empleadoService.GetAll();
            return View(model);
        }

        public ActionResult Buscar(string palabra)
        {
            var empleados = _empleadoService.Buscar(palabra);

            Session["Palabra"] = palabra;

            return View("Index",empleados);
        }
        // GET: Empleado/Details/5
        public ActionResult Details(int id)
        {
            var model = _empleadoService.Get(id);
            return View("Details", model);
        }

        // GET: Empleado/Create
        public ActionResult Create()
        {
            ViewBag.Id_RolServicio = new SelectList(rolesServices.GetAll(), "Id_Rol", "Nombre");
            return View();
        }

        // POST: Empleado/Create
        [HttpPost]
        public ActionResult Create(Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                _empleadoService.Create(empleado);
                 return RedirectToAction("Index");
            }

            ViewBag.Id_RolServicio = new SelectList(rolesServices.GetAll(), "Id_Rol", "Nombre", empleado.Id_RolServicio);
            return View(empleado);     
        }

        // GET: Empleado/Edit/5
        public ActionResult Edit(int id)
        {
            var model = _empleadoService.GetEdit(id);
            ViewBag.Id_RolServicio = new SelectList(rolesServices.GetAll(), "Id_Rol", "Nombre");
            return View(model);
        }

        // POST: Empleado/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Empleado empleado)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _empleadoService.Update(empleado);
                }

                ViewBag.Id_RolServicio = new SelectList(rolesServices.GetAll(), "Id_Rol", "Nombre", empleado.Id_RolServicio);

                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        // GET: Empleado/Delete/5
        public ActionResult Delete(int id)
        {
            var model = _empleadoService.Get(id);
            return View(model);
        }

        // POST: Empleado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                _empleadoService.Delete(id);

                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public ActionResult Get(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            int legajo = int.Parse(id);

            var model = _empleadoService.GetEdit(legajo);
            ViewBag.Usuario = userService.GetAll();
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        public ActionResult AddUserToEmp(int legajo, string user)
        {
            var usuario = _empleadoService.GetUser(legajo);

            if (usuario != null)
            {
                throw new Exception("El Empleado ya tiene un Usuario, se permite uno por Empleado");
            }

            _empleadoService.AddUserToEmp(legajo, user);
            return RedirectToAction("Index");
        }

        public ActionResult Reporte()
        {
            IEnumerable<EmpleadoGrid> empleados;

            if (Session["Palabra"] != null)
            {
                string palabra = Session["Palabra"].ToString();

                empleados = _empleadoService.Buscar(palabra);
            }
            else
            {
                empleados = _empleadoService.GetAll();
            }

            return View("Reporte", empleados);
        }
    }
}
