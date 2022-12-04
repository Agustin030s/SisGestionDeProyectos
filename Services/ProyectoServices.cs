using Model.Custom;
using Model.Domain;
using Persistanse;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Services
{
    public class ProyectoServices
    {
        public IEnumerable<ProyectosGrid> GetAll()
        {
            var result = new List<ProyectosGrid>();

            using (var db = new ApplicationDbContext())
            {                
                result = (from pr in db.Proyectos
                          from emp in db.Empleado.Where(x => x.Legajo == pr.Legajo)
                          from cat in db.Categorias.Where(x => x.Id_Categoria == pr.Id_Categoria)
                          select new ProyectosGrid
                          {
                              Id_Proyecto = pr.Id_Proyecto,
                              Titulo = pr.Titulo,
                              Descripcion = pr.Descripcion,
                              FechInicio = pr.FechInicio,
                              FechFin = pr.FechFin,
                              Avance = pr.Avance,
                              Encargado = emp.Nombre + " " + emp.Apellido,
                              Categoria = cat.Nombre,
                              Costo = pr.Costo
                          }
                          ).ToList();

            }

            return result;
        }

        public ProyectosGrid Get(int id)
        {
            var result = new ProyectosGrid();

            using (var db = new ApplicationDbContext())
            {
                result = (from pr in db.Proyectos.Where(x => x.Id_Proyecto == id)
                          from emp in db.Empleado.Where(x => x.Legajo == pr.Legajo)
                          from cat in db.Categorias.Where(x => x.Id_Categoria == pr.Id_Categoria)
                          select new ProyectosGrid
                          {
                              Id_Proyecto = pr.Id_Proyecto,
                              Titulo = pr.Titulo,
                              Descripcion = pr.Descripcion,
                              FechInicio = pr.FechInicio,
                              FechFin = pr.FechFin,
                              Avance = pr.Avance,
                              Encargado = emp.Nombre + " " + emp.Apellido,
                              Categoria = cat.Nombre,
                              Costo = pr.Costo
                          }
                          ).Single();
            }

            return result;
        }

        public Proyectos GetEdit(int id)
        {
            var result = new Proyectos();

            using (var db = new ApplicationDbContext())
            {
                result = db.Proyectos.Where(x => x.Id_Proyecto == id).Single();
            }

            return result;
        }

        public void Create(Proyectos model)
        {
            using (var db = new ApplicationDbContext())
            {
                var proyecto = new Proyectos();

                proyecto.Titulo = model.Titulo;
                proyecto.Descripcion = model.Descripcion;
                proyecto.FechInicio = model.FechInicio;
                proyecto.FechFin = model.FechFin;
                proyecto.Avance = 0;
                proyecto.Legajo = model.Legajo;
                proyecto.Id_Categoria = model.Id_Categoria;

                db.Proyectos.Add(proyecto);
                db.SaveChanges();
            }
        }

        public void Update(Proyectos model)
        {
            using(var db = new ApplicationDbContext())
            {
                var originalEntity = db.Proyectos.Where(x => x.Id_Proyecto == model.Id_Proyecto).Single();

                originalEntity.Titulo = model.Titulo;
                originalEntity.Descripcion = model.Descripcion;
                originalEntity.FechFin = model.FechFin;
                originalEntity.Legajo = model.Legajo;
                originalEntity.Id_Categoria = model.Id_Categoria;

                db.Entry(originalEntity).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            try
            {
                using(var db = new ApplicationDbContext())
                {
                    Proyectos proyectos = db.Proyectos.Where(x => x.Id_Proyecto == id).Single();

                    db.Proyectos.Remove(proyectos);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public IEnumerable<ProyectosGrid> Buscar(string palabra, string value)
        {
            var FechaActual = DateTime.Parse(DateTime.Today.ToShortDateString());
            var db = new ApplicationDbContext();
            IEnumerable<ProyectosGrid> proyecto;

            ProyectosGrid proyectos = new ProyectosGrid();

            using(db)
            {
                proyecto = GetAll();

                if (!String.IsNullOrEmpty(palabra))
                {
                    proyecto = from pr in db.Proyectos.Where(x => x.Titulo.ToUpper().Contains(palabra.ToUpper()))
                               from emp in db.Empleado.Where(x => x.Legajo == pr.Legajo)
                               from cat in db.Categorias.Where(x => x.Id_Categoria == pr.Id_Categoria)
                               select new ProyectosGrid
                               {
                                   Id_Proyecto = pr.Id_Proyecto,
                                   Titulo = pr.Titulo,
                                   Descripcion = pr.Descripcion,
                                   FechInicio = pr.FechInicio,
                                   FechFin = pr.FechFin,
                                   Avance = pr.Avance,
                                   Encargado = emp.Nombre + " " + emp.Apellido,
                                   Categoria = cat.Nombre,
                                   Costo = pr.Costo
                               };
                }

                if(value == "2")
                {

                    proyecto = from pr in db.Proyectos.Where(x => x.FechFin < FechaActual && x.Avance < 1)
                               from emp in db.Empleado.Where(x => x.Legajo == pr.Legajo)
                               from cat in db.Categorias.Where(x => x.Id_Categoria == pr.Id_Categoria)
                               select new ProyectosGrid
                               {
                                   Id_Proyecto = pr.Id_Proyecto,
                                   Titulo = pr.Titulo,
                                   Descripcion = pr.Descripcion,
                                   FechInicio = pr.FechInicio,
                                   FechFin = pr.FechFin,
                                   Avance = pr.Avance,
                                   Encargado = emp.Nombre + " " + emp.Apellido,
                                   Categoria = cat.Nombre,
                                   Costo = pr.Costo
                               };
                }

                if (value == "3")
                {

                    proyecto = from pr in db.Proyectos.Where(x => x.FechInicio > FechaActual)
                               from emp in db.Empleado.Where(x => x.Legajo == pr.Legajo)
                               from cat in db.Categorias.Where(x => x.Id_Categoria == pr.Id_Categoria)
                               select new ProyectosGrid
                               {
                                   Id_Proyecto = pr.Id_Proyecto,
                                   Titulo = pr.Titulo,
                                   Descripcion = pr.Descripcion,
                                   FechInicio = pr.FechInicio,
                                   FechFin = pr.FechFin,
                                   Avance = pr.Avance,
                                   Encargado = emp.Nombre + " " + emp.Apellido,
                                   Categoria = cat.Nombre,
                                   Costo = pr.Costo
                               };
                }

                proyecto = proyecto.ToList();
            }

            return proyecto;
        }

        public void UpdateAvance(int id, double Avance)
        {
            using(var db = new ApplicationDbContext())
            {
                var originalEntity = db.Proyectos.Where(x => x.Id_Proyecto == id).Single();

                originalEntity.Avance = Avance;

                db.Entry(originalEntity).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void UpdateCosto(int id, double costo)
        {
            using(var db = new ApplicationDbContext())
            {
                var originalEntity = db.Proyectos.Where(x => x.Id_Proyecto == id).Single();

                originalEntity.Costo = costo;

                db.Entry(originalEntity).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}
