using Microsoft.AspNetCore.Mvc;
using ProjetoLoja.Models;
using ProjetoLoja.Repositories;
using ProjetoLoja.Repositories.Interfaces;
using ProjetoLoja.ViewModel;

namespace ProjetoLoja.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly IProdutoRepository _produtos;

        public ProdutoController(IProdutoRepository produtos)
        {
            _produtos = produtos;
        }

        public IActionResult List(string categoria)
        {
            IEnumerable<Produto> produtos;
            string categoriaAtual = string.Empty;
            if (string.IsNullOrEmpty(categoria))
            {
                produtos = _produtos.Produtos.OrderBy(p => p.Nome);
                categoriaAtual = "Todos os Produtos";
            }
            else
            {
                categoriaAtual = categoria;
                produtos = _produtos.Produtos.Where(p=> p.Categoria.CategoriaNome.Equals(categoria)).OrderBy(c=> c.Nome);
            }
            var produtoListViewModel = new ProdutoListViewModel();
            produtoListViewModel.Produtos = produtos;
            produtoListViewModel.CategoriaAtual = categoria;
            return View(produtoListViewModel);
        }

        public IActionResult Detalhes(int produtoId)
        {
            var produto = _produtos.Produtos.FirstOrDefault(p=> p.ProdutoId == produtoId);
            return View(produto);
        }

        public ViewResult Search(string searchString)
        {
            IEnumerable<Produto> produtos;
            string categoriaAtual = string.Empty;

            if (string.IsNullOrEmpty(searchString))
            {
                produtos = _produtos.Produtos.OrderBy(p => p.ProdutoId);
                categoriaAtual = "Todos os produtos";
            }
            else
            {
                produtos = _produtos.Produtos.Where(p=> p.Nome.ToLower().Contains(searchString.ToLower()));
                if (produtos.Any())
                    categoriaAtual = "Produtos";
                else
                    categoriaAtual = "Nenhum produto encontrado";
            }

            return View("~/Views/Produto/List.cshtml", new ProdutoListViewModel { Produtos = produtos, CategoriaAtual= categoriaAtual});
        }
    }
}
