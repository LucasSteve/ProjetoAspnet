using Microsoft.EntityFrameworkCore;
using ProjetoLoja.Context;
using ProjetoLoja.Models;
using ProjetoLoja.Repositories.Interfaces;

namespace ProjetoLoja.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly AppDbContext _context;
public ProdutoRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Produto> Produtos => _context.Produtos.Include(x => x.Categoria);

        public IEnumerable<Produto> ProdutosPreferido => _context.Produtos.Where(x => x.IsProdutoPreferido).Include(c => c.Categoria);

        public Produto GetProdutoById(int ProdutoId) => _context.Produtos.FirstOrDefault(i => i.ProdutoId == ProdutoId);

    }
}
