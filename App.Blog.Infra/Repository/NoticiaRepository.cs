using App.Blog.Domain;
using App.Blog.Infra.Context;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace App.Blog.Infra.Repository
{
    public class NoticiaRepository
    {
        private readonly NoticiaDBContext _context;

        public NoticiaRepository(NoticiaDBContext context)
        {
            _context = context;
        }

        public async Task<Noticia> ConsultarPorIdAsync(int Id)
        {
            return await _context.Noticias.FirstOrDefaultAsync(c => c.Id.Equals(Id));
        }

        public async Task<List<Noticia>> ConsultarAsync()
        {
            return await _context.Noticias.ToListAsync();
        }

        public async Task<int> IncluirAsync(Noticia noticia)
        {
            _context.Add(noticia);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> AtualizarAsync(Noticia noticia)
        {
            _context.Update(noticia);
            return await _context.SaveChangesAsync();
        }
    }
}