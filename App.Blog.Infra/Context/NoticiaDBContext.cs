using App.Blog.Domain;
using Microsoft.EntityFrameworkCore;

namespace App.Blog.Infra.Context
{
    public class NoticiaDBContext : DbContext
    {
        public NoticiaDBContext(DbContextOptions<NoticiaDBContext> options) : base(options)
        {

        }

        public virtual DbSet<Noticia> Noticias { get; set; }
    }
}
