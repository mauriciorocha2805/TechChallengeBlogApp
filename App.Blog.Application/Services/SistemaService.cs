using App.Application.Interfaces;
using App.Blog.Infra.Interfaces;

namespace App.Application.Services;

public class SistemaService : ISistemaService
{
    private readonly ISistemaRepository _repository;

    public SistemaService(ISistemaRepository repository)
    {
        _repository = repository;
    }

    public bool VerificarChaveExiste(string chave)
    {
        if (string.IsNullOrEmpty(chave))
            throw new Exception("Chave precisa ser informada");

        return _repository.VerificarChaveExiste(chave);
    }
}