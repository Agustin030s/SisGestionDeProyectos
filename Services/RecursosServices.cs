using Model.Custom;
using Model.Domain;
using Persistanse;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Services
{
    public class RecursosServices
    {
        private readonly EmpleadoServices empleadoServices = new EmpleadoServices();
        private readonly TareasServices tareasServices = new TareasServices();
        public IEnumerable<RecursoGrid> GetAll()
        {
            var result = new List<RecursoGrid>();

            using (var db = new ApplicationDbContext())
            {
                result = (from re in db.Recursos
                          from ta in db.Tareas.Where(x => x.Id_Tarea == re.Id_Tarea)
                          from emp in db.Empleado.Where(x => x.Legajo == re.Legajo)
                          from rol in db.RolEmpleado.Where(x => x.Id_Rol == emp.Id_RolServicio)
                          select new RecursoGrid
                          {
                              Id_Recurso = re.Id_Recurso,
                              Presupuesto = re.Presupuesto,
                              IdTarea = ta.Id_Tarea,
                              NomTarea = ta.NomTarea,
                              FechInicio = ta.FechInicio,
                              FechFin = ta.FechFin,
                              ApyNom = emp.Nombre + " " + emp.Apellido,
                              Rol = rol.Nombre
                          }).OrderBy(x => x.IdTarea).ToList();
            }

            return result;
        }

        public RecursoGrid Get(int id)
        {
            var result = new RecursoGrid();

            using (var db = new ApplicationDbContext())
            {
                result = (from re in db.Recursos.Where(x => x.Id_Recurso == id)
                          from ta in db.Tareas.Where(x => x.Id_Tarea == re.Id_Tarea)
                          from emp in db.Empleado.Where(x => x.Legajo == re.Legajo)
                          from rol in db.RolEmpleado.Where(x => x.Id_Rol == emp.Id_RolServicio)
                          select new RecursoGrid
                          {
                              Id_Recurso = re.Id_Recurso,
                              Presupuesto = re.Presupuesto,
                              IdTarea = ta.Id_Tarea,
                              NomTarea = ta.NomTarea,
                              FechInicio = ta.FechInicio,
                              FechFin = ta.FechFin,
                              ApyNom = emp.Nombre + " " + emp.Apellido,
                              Rol = rol.Nombre
                          }).Single();
            }

            return result;
        }

        public Recurso GetEdit(int id)
        {
            var result = new Recurso();

            using(var db = new ApplicationDbContext())
            {
                result = db.Recursos.Where(x => x.Id_Recurso == id).Single();
            }

            return result;
        }

        public IEnumerable<RecursoGrid> RecursosXTarea(int id)
        {
            var result = new List<RecursoGrid>();

            using(var db = new ApplicationDbContext())
            {
                result = (from re in db.Recursos.Where(x => x.Id_Tarea == id)
                         from emp in db.Empleado.Where(x => x.Legajo == re.Legajo)
                         from rol in db.RolEmpleado.Where(x => x.Id_Rol == emp.Id_RolServicio)
                         select new RecursoGrid
                         {
                             Id_Recurso = re.Id_Recurso,
                             Presupuesto = re.Presupuesto,
                             ApyNom = emp.Nombre + " " + emp.Apellido,
                             Rol = rol.Nombre
                         }).ToList();
            }

            return result;
        }

        public IEnumerable<RecursoGrid> Buscar(string Opcion)
        {
            IEnumerable<RecursoGrid> recursos;

            if(Opcion == "2")
            {
                recursos = RecursoSobreasignado();
            } else if(Opcion == "3")
            {
                using(var db = new ApplicationDbContext())
                {
                    recursos = (from re in db.Recursos
                                from ta in db.Tareas.Where(x => x.Id_Tarea == re.Id_Tarea)
                                from emp in db.Empleado.Where(x => x.Legajo == re.Legajo)
                                from rol in db.RolEmpleado.Where(x => x.Id_Rol == emp.Id_RolServicio)
                                select new RecursoGrid
                                {
                                    Id_Recurso = re.Id_Recurso,
                                    Presupuesto = re.Presupuesto,
                                    IdTarea = ta.Id_Tarea,
                                    NomTarea = ta.NomTarea,
                                    FechInicio = ta.FechInicio,
                                    FechFin = ta.FechFin,
                                    ApyNom = emp.Nombre + " " + emp.Apellido,
                                    Rol = rol.Nombre
                                }).OrderByDescending(x => x.Presupuesto).ToList();
                }
                 
            }
            else
            {
                recursos = GetAll();
            }

            return recursos;
        }

        public void Create(Recurso model)
        {
            using (var db = new ApplicationDbContext())
            {
                var recurso = new Recurso();

                recurso.Id_Tarea = model.Id_Tarea;
                recurso.Legajo = model.Legajo;

                db.Recursos.Add(recurso);
                db.SaveChanges();

                var empleado = empleadoServices.GetEdit(model.Legajo);
                int Rol = empleado.Id_RolServicio;
                int dias = DiasEntreFechas(model.Id_Tarea);
                double presupuesto = Presupuesto(dias, Rol);
                UpdatePre(recurso.Id_Recurso, presupuesto);

                double costoTarea = CostoTarea(recurso.Id_Tarea);
                tareasServices.UpdateCosto(recurso.Id_Tarea, costoTarea);
            }
        }

        public Recurso ObtenerRecurso(int id, int legajo)
        {
            var result = new Recurso();

            using (var db = new ApplicationDbContext())
            {
                result = db.Recursos.Where(x => x.Id_Tarea == id && x.Legajo == legajo).FirstOrDefault();
            }

            return result;
        }
        public void Update(Recurso model) 
        { 
            using(var db = new ApplicationDbContext())
            {
                var originalEntity = db.Recursos.Where(x => x.Id_Recurso == model.Id_Recurso).Single();

                originalEntity.Legajo = model.Legajo;

                db.Entry(originalEntity).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void UpdatePre(int id, double presupuesto)
        {
            using (var db = new ApplicationDbContext())
            {
                var originalEntity = db.Recursos.Where(x => x.Id_Recurso == id).Single();

                originalEntity.Presupuesto = presupuesto;

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
                    Recurso recurso = db.Recursos.Where(x => x.Id_Recurso == id).Single();

                    db.Recursos.Remove(recurso);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public int DiasEntreFechas(int id)
        {
            int result;
            DateTime FechInicio;
            DateTime FechFin;

            using(var db = new ApplicationDbContext())
            {
                var tarea = db.Tareas.Where(x => x.Id_Tarea == id).Single();
                FechInicio = tarea.FechInicio;
                FechFin = tarea.FechFin;
            }

            TimeSpan difFechas = FechFin - FechInicio;
            result = difFechas.Days;

            return result;
        }

        public double Presupuesto(int dias, int id)
        {
            double presupuesto;
            double servicio;

            using(var db = new ApplicationDbContext())
            {
                var result = db.RolEmpleado.Where(x => x.Id_Rol == id).Single();
                servicio = result.PrecioServicio;
            }

            presupuesto = servicio * dias;

            return presupuesto;
        }

    public IEnumerable<RecursoGrid> RecursoSobreasignado()
    {
        var tareas = new List<RecursoGrid>();
        var sobreasignado = new List<RecursoGrid>();
        var par = new List<RecursoGrid>();

        using (var db = new ApplicationDbContext())
        {
                tareas = (from re in db.Recursos
                          from ta in db.Tareas.Where(x => x.Id_Tarea == re.Id_Tarea && x.Avance < 1)
                          from emp in db.Empleado.Where(x => x.Legajo == re.Legajo)
                          from rol in db.RolEmpleado.Where(x => x.Id_Rol == emp.Id_RolServicio)
                          select new RecursoGrid
                          {
                              Id_Recurso = re.Id_Recurso,
                              Presupuesto = re.Presupuesto,
                              IdTarea = ta.Id_Tarea,
                              NomTarea = ta.NomTarea,
                              FechInicio = ta.FechInicio,
                              FechFin = ta.FechFin,
                              ApyNom = emp.Nombre + " " + emp.Apellido,
                              Rol = rol.Nombre
                          }).OrderBy(x => x.ApyNom).ToList();

            foreach (var actual in tareas)
            {
                foreach (var siguiente in tareas)
                {
                    if (actual.IdTarea != siguiente.IdTarea)
                    {
                        if(actual.ApyNom == siguiente.ApyNom)
                        {
                                if (siguiente.FechInicio > actual.FechInicio && siguiente.FechInicio < actual.FechFin
                                    || siguiente.FechFin < actual.FechInicio && siguiente.FechFin > actual.FechFin)
                                {
                                    par.Add(actual);
                                    par.Add(siguiente);
                                }
                                sobreasignado.AddRange(par);
                        }
                    }
                }
            }
        }

        sobreasignado = sobreasignado.OrderBy(x => x.ApyNom).Distinct().ToList();

        return sobreasignado;
    }

    public double CostoTarea (int id)
    {
            var recursos = new List<Recurso>();

            using (var db = new ApplicationDbContext())
            {
                recursos = db.Recursos.Where(x => x.Id_Tarea == id).ToList();
            }

            var costo = recursos.Sum(x => x.Presupuesto);

            return costo;
    }
}
}
