using App.Blog.Application.Interfaces;
using App.Blog.Domain.Entities;
using App.Blog.Infra.Interfaces;

namespace App.Blog.Application.Services
{
    public class NoticiaService : INoticiaService
    {
        private readonly INoticiaRepository _repository;

        public NoticiaService(INoticiaRepository repository)
        {
            _repository = repository;
        }

        public async Task<Noticia> ConsultarPorIdAsync(int Id)
        {
            if (Id <= 0)
                throw new Exception("Id precisa ser informado");

            return await _repository.ConsultarPorIdAsync(Id);
        }

        public async Task<List<Noticia>> ConsultarAsync()
        {
            return await _repository.ConsultarAsync();
        }

        public async Task<int> IncluirAsync(Noticia noticia)
        {
            if (noticia.Id > 0)
                throw new Exception("Id precisa ser igual a 0");

            return await _repository.IncluirAsync(noticia);
        }

        public async Task<int> AtualizarAsync(Noticia noticia)
        {
            if (noticia.Id <= 0)
                throw new Exception("Id precisa ser informado");

            return await _repository.AtualizarAsync(noticia);
        }

        public bool Adicionar(Noticia noticia)
        {
            return _repository.Adicionar(noticia);
        }

        public bool Excluir(int id)
        {
            return _repository.Excluir(id);
        }

    }
}