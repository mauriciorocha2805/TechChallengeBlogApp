using App.Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.Blog.Infra.Context
{
    public class NoticiaDBContext : DbContext
    {

        public virtual DbSet<Noticia> Noticias { get; set; }
        public virtual DbSet<Sistema> Sistemas { get; set; }
        public NoticiaDBContext(DbContextOptions<NoticiaDBContext> options) : base(options)
        {

        }

    }
}
