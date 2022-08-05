using ProjetoLoja.Context;
using ProjetoLoja.Models;
using ProjetoLoja.Repositories.Interfaces;

namespace ProjetoLoja.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext _context;

        public CategoriaRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Categoria> Categorias => _context.Categorias;
    }
}
