using App.Blog.Domain.Entities;

namespace App.Blog.Infra.Interfaces
{
    public interface INoticiaRepository
    {
        Task<int> AtualizarAsync(Noticia noticia);
        Task<List<Noticia>> ConsultarAsync();
        Task<Noticia> ConsultarPorIdAsync(int Id);
        Task<int> IncluirAsync(Noticia noticia);

        public bool Adicionar(Noticia noticia);
        public bool Excluir(int id);
    }
}