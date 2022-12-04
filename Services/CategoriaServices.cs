using Model.Domain;
using Persistanse;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class CategoriaServices
    {
        public IEnumerable<Categoria> GetAll()
        {
            var result = new List<Categoria>();

            using(var ctx = new ApplicationDbContext())
            {
                result = ctx.Categorias.OrderBy(x => x.Nombre).ToList();
            }

            return result;
        }

        public void create(Categoria model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var categoria = new Categoria();

                categoria.Nombre = model.Nombre;

                ctx.Categorias.Add(categoria);
                ctx.SaveChanges();
            }
        }

        public Categoria Get(int id)
        {
            var result = new Categoria();

            using (var ctx = new ApplicationDbContext())
            {
                result = ctx.Categorias.Where(x => x.Id_Categoria == id).Single();
            }

            return result;
        }

        public void delete(int id)
        {
            try
            {
                using(var ctx = new ApplicationDbContext())
                {
                    Categoria categoria = ctx.Categorias.Where(x => x.Id_Categoria == id).Single();

                    ctx.Categorias.Remove(categoria);
                    ctx.SaveChanges();
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}
