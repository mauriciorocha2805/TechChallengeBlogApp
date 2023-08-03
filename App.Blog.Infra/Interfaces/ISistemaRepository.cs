namespace App.Blog.Infra.Interfaces
{
    public interface ISistemaRepository
    {
        bool VerificarChaveExiste(string chave);
    }
}