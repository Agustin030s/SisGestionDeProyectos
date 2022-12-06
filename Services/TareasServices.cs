using Model.Custom;
using Model.Domain;
using Persistanse;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Services
{
    public class TareasServices
    {
        private readonly ProyectoServices proyectoServices = new ProyectoServices();
        public IEnumerable<TareasGrid> GetAll()
        {
            var result = new List<TareasGrid>();

            using(var db = new ApplicationDbContext())
            {
                result = (from ta in db.Tareas
                          from pro in db.Proyectos.Where(x => x.Id_Proyecto == ta.Id_Proyecto)
                          select new TareasGrid
                          {
                              Id_Tarea = ta.Id_Tarea,
                              NomTarea = ta.NomTarea,
                              Descripcion = ta.Descripcion,
                              FechInicio = ta.FechInicio,
                              FechFin = ta.FechFin,
                              Avance = ta.Avance,
                              TituloProy = pro.Titulo,
                              Costo = ta.Costo
                          }).ToList();
            }

            return result;
        }

        public TareasGrid Get(int id)
        {
            var result = new TareasGrid();

            using(var db = new ApplicationDbContext())
            {
                result = (from ta in db.Tareas.Where(x => x.Id_Tarea == id)
                          from pro in db.Proyectos.Where(x => x.Id_Proyecto == ta.Id_Proyecto)
                          select new TareasGrid
                          {
                              Id_Tarea = ta.Id_Tarea,
                              NomTarea = ta.NomTarea,
                              Descripcion = ta.Descripcion,
                              FechInicio = ta.FechInicio,
                              FechFin = ta.FechFin,
                              Avance = ta.Avance,
                              TituloProy = pro.Titulo,
                              Costo = ta.Costo
                          }).Single();
            }

            return result;
        }

        public Tareas GetEdit(int id)
        {
            var result = new Tareas();

            using(var db = new ApplicationDbContext())
            {
                result = db.Tareas.Where(x => x.Id_Tarea == id).Single();
            }

            return result;
        }

        public IEnumerable<TareasGrid> GetCbo()
        {
            var result = new List<TareasGrid>();

            using (var db = new ApplicationDbContext())
            {
                result = (from ta in db.Tareas.Where(x => x.Avance < 1)
                          from pro in db.Proyectos.Where(x => x.Id_Proyecto == ta.Id_Proyecto)
                          select new TareasGrid
                          {
                              Id_Tarea = ta.Id_Tarea,
                              NomTarea = ta.NomTarea,
                              Descripcion = ta.Descripcion,
                              FechInicio = ta.FechInicio,
                              FechFin = ta.FechFin,
                              Avance = ta.Avance,
                              TituloProy = pro.Titulo,
                              Costo = ta.Costo
                          }).ToList();
            }

            return result;
        }

        public void Create (Tareas model)
        {
            using (var db = new ApplicationDbContext())
            {
                var tarea = new Tareas();

                tarea.NomTarea = model.NomTarea;
                tarea.Descripcion = model.Descripcion;
                tarea.FechInicio = model.FechInicio;
                tarea.FechFin = model.FechFin;
                tarea.Avance = 0.00;
                tarea.Id_Proyecto = model.Id_Proyecto;

                db.Tareas.Add(tarea);
                db.SaveChanges();
            }
        }

        public void Update(Tareas model)
        {
            double result;
            using(var db = new ApplicationDbContext())
            {
                var originalEntity = db.Tareas.Where(x => x.Id_Tarea == model.Id_Tarea).Single();

                originalEntity.NomTarea = model.NomTarea;
                originalEntity.Descripcion = model.Descripcion;
                originalEntity.FechFin = model.FechFin;
                originalEntity.Avance = model.Avance/100;

                db.Entry(originalEntity).State = EntityState.Modified;
                db.SaveChanges();

                result = CantidadDeTareas(model.Id_Proyecto);
                proyectoServices.UpdateAvance(model.Id_Proyecto, result);
            }
        }

        public void Delete(int id)
        {
            try
            {
                using(var db = new ApplicationDbContext())
                {
                    Tareas tarea = db.Tareas.Where(x => x.Id_Tarea == id).Single();

                    db.Tareas.Remove(tarea);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public IEnumerable<Tareas> TareasXProyecto(int id)
        {
            var result = new List<Tareas>();
            using(var db = new ApplicationDbContext())
            {
                result = db.Tareas.Where(x => x.Id_Proyecto == id).ToList();
            }

            return result;
        }

        public IEnumerable<TareasGrid> Buscar(string palabra, string opcion)
        {
            IEnumerable<TareasGrid> Tarea;
            var FechaActual = DateTime.Parse(DateTime.Today.ToShortDateString());

            using (var db = new ApplicationDbContext())
            {
                Tarea = GetAll();

                if (!String.IsNullOrEmpty(palabra))
                {
                    Tarea = (from ta in db.Tareas.Where(x => x.NomTarea.ToUpper().Contains(palabra.ToUpper()))
                            from pro in db.Proyectos.Where(x => x.Id_Proyecto == ta.Id_Proyecto)
                            select new TareasGrid
                            {
                                Id_Tarea = ta.Id_Tarea,
                                NomTarea = ta.NomTarea,
                                Descripcion = ta.Descripcion,
                                FechInicio = ta.FechInicio,
                                FechFin = ta.FechFin,
                                Avance = ta.Avance,
                                TituloProy = pro.Titulo
                            }).OrderBy(x => x.FechInicio).ToList();
                }

                if(opcion == "2")
                {
                    Tarea = (from ta in db.Tareas.Where(x => x.FechFin < FechaActual && x.Avance < 1)
                            from pro in db.Proyectos.Where(x => x.Id_Proyecto == ta.Id_Proyecto)
                            select new TareasGrid
                            {
                                Id_Tarea = ta.Id_Tarea,
                                NomTarea = ta.NomTarea,
                                Descripcion = ta.Descripcion,
                                FechInicio = ta.FechInicio,
                                FechFin = ta.FechFin,
                                Avance = ta.Avance,
                                TituloProy = pro.Titulo,
                                Costo = ta.Costo
                            }).OrderBy(x => x.FechInicio).ToList();
                }

                if(opcion == "3")
                {
                    Tarea = (from ta in db.Tareas.Where(x => x.Avance == 1)
                            from pro in db.Proyectos.Where(x => x.Id_Proyecto == ta.Id_Proyecto)
                            select new TareasGrid
                            {
                                Id_Tarea = ta.Id_Tarea,
                                NomTarea = ta.NomTarea,
                                Descripcion = ta.Descripcion,
                                FechInicio = ta.FechInicio,
                                FechFin = ta.FechFin,
                                Avance = ta.Avance,
                                TituloProy = pro.Titulo,
                                Costo = ta.Costo
                            }).OrderBy(x => x.FechInicio).ToList();
                }

                if (opcion == "4")
                {
                    Tarea = (from ta in db.Tareas.Where(x => x.Avance < 1)
                             from pro in db.Proyectos.Where(x => x.Id_Proyecto == ta.Id_Proyecto)
                             select new TareasGrid
                             {
                                 Id_Tarea = ta.Id_Tarea,
                                 NomTarea = ta.NomTarea,
                                 Descripcion = ta.Descripcion,
                                 FechInicio = ta.FechInicio,
                                 FechFin = ta.FechFin,
                                 Avance = ta.Avance,
                                 TituloProy = pro.Titulo,
                                 Costo = ta.Costo
                             }).OrderByDescending(x => x.Costo).ToList();
                }
            }

            var result = Tarea;

            return result;
        }

        public double CantidadDeTareas(int id)
        {
            int TotalTareas;
            int TareasTerminadas;

            using(var db = new ApplicationDbContext())
            {
                TotalTareas = db.Tareas.Where(x => x.Id_Proyecto == id).Count();
                TareasTerminadas = db.Tareas.Where(x => x.Id_Proyecto == id && x.Avance == 1).Count();
            }

            double Result = ((double)TareasTerminadas / (double)TotalTareas);

            return Result;
        }

        public void UpdateCosto(int id, double costo)
        {
            using(var db = new ApplicationDbContext())
            {
                var originalEntity = db.Tareas.Where(x => x.Id_Tarea == id).Single();

                originalEntity.Costo = costo;

                db.Entry(originalEntity).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public double CostoProyecto(int id)
        {
            var tareas = new List<Tareas>();

            using(var db = new ApplicationDbContext())
            {
                tareas = db.Tareas.Where(x => x.Id_Proyecto == id).ToList();
            }

            var costo = tareas.Sum(x => x.Costo);

            return costo;
        }
    }
}
