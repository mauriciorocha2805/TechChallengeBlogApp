using App.Blog.Domain.Entities;
using App.Blog.Infra.Context;
using App.Blog.Infra.Interfaces;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace App.Blog.Infra.Repository
{
    public class NoticiaRepository : INoticiaRepository
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

        public bool Adicionar(Noticia noticia)
        {
            try
            {
                _context.Noticias.Add(noticia);
                _context.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Excluir(int Id)
        {
            var noticia = _context.Noticias.FirstOrDefault(c => c.Id.Equals(Id));

            try
            {
                if (Id != noticia.Id)
                {
                    return false;
                }
                _context.Entry(noticia).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}