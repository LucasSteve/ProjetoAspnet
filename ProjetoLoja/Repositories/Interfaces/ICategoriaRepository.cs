using ProjetoLoja.Models;

namespace ProjetoLoja.Repositories.Interfaces
{
    public interface ICategoriaRepository
    {
        IEnumerable<Categoria> Categorias { get; }
    }
}
