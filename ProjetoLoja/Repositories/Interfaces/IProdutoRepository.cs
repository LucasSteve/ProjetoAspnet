using ProjetoLoja.Models;

namespace ProjetoLoja.Repositories.Interfaces
{
    public interface IProdutoRepository
    {
        IEnumerable<Produto> Produtos { get; }
        IEnumerable<Produto> ProdutosPreferido { get;}
        Produto GetProdutoById(int ProdutoId);
    }
}
