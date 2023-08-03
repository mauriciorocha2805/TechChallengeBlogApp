using App.Blog.Infra.Context;
using App.Blog.Infra.Interfaces;

namespace App.Blog.Infra.Repository
{
    public class SistemaRepository : ISistemaRepository
    {
        private readonly NoticiaDBContext _context;

        public SistemaRepository(NoticiaDBContext context)
        {
            _context = context;
        }

        public bool VerificarChaveExiste(string chave)
        {
            return _context.Sistemas.Any(c => c.Chave.Equals(chave));
        }
    }
}