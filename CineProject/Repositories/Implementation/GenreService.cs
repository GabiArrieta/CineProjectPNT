using CineProject.Models.Domain;
using CineProject.Repositories.Abstract;

namespace CineProject.Repositories.Implementation
{
    public class GenreService : IGenreService
    {
        private readonly DatabaseContext ctx;
        public GenreService(DatabaseContext ctx)
        {
            this.ctx = ctx;
        }
        public bool Add(Genre model)
        {
            try
            {
                ctx.Genre.Add(model);
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var data = this.GetById(id);
                if (data == null)
                    return false;
                ctx.Genre.Remove(data);
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Genre GetById(int id)
        {
            return ctx.Genre.Find(id);
        }

        public bool IsGenreNameExists(string genreName)
        {
            // Verificar si ya existe un género con el mismo nombre en la base de datos
            return ctx.Genre.Any(g => g.GenreName == genreName);
        }

        public IQueryable<Genre> List()
        {
            var data = ctx.Genre.AsQueryable();
            return data;
        }

        public bool Update(Genre model)
        {
            try
            {
                ctx.Genre.Update(model);
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}