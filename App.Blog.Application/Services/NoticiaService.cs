using App.Blog.Domain;
using App.Blog.Infra.Repository;

namespace App.Blog.Application.Services
{
    public class NoticiaService
    {
        private readonly NoticiaRepository _repository;

        public NoticiaService(NoticiaRepository repository)
        {
            _repository = repository;
        }

        public async Task<Noticia> ConsultarPorIdAsync(int Id)
        {
            return await _repository.ConsultarPorIdAsync(Id);
        }

        public async Task<List<Noticia>> ConsultarAsync()
        {
            return await _repository.ConsultarAsync();
        }

        public async Task<int> IncluirAsync(Noticia noticia)
        {
            return await _repository.IncluirAsync(noticia);
        }

        public async Task<int> AtualizarAsync(Noticia noticia)
        {
            return await _repository.AtualizarAsync(noticia);
        }
    }
}