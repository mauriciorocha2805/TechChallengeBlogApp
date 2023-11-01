using App.Blog.Domain.Entities;

namespace App.Blog.Application.Interfaces
{
    public interface INoticiaService
    {
        Task<int> AtualizarAsync(Noticia noticia);
        Task<List<Noticia>> ConsultarAsync();
        Task<Noticia> ConsultarPorIdAsync(int Id);
        Task<int> IncluirAsync(Noticia noticia);

        public bool Adicionar(Noticia noticia);
        public bool Excluir(int id);

    }
}