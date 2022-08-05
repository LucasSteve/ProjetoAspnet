using ProjetoLoja.Context;
using ProjetoLoja.Models;
using ProjetoLoja.Repositories.Interfaces;

namespace ProjetoLoja.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly CarrinhoCompra _carrinhoCompra;

        public PedidoRepository(AppDbContext appDbContext, CarrinhoCompra carrinhoCompra)
        {
            _appDbContext = appDbContext;
            _carrinhoCompra = carrinhoCompra;
        }

        public void CriarPedido(Pedido pedido)
        {
            pedido.PedidoEnviado = DateTime.Now;
            _appDbContext.Pedidos.Add(pedido);
            _appDbContext.SaveChanges();

            var carrinhoItens = _carrinhoCompra.CarrinhoCompraItems;

            foreach (var item in carrinhoItens)
            {
                var pedidoDetail = new PedidoDetalhe()
                {
                    Quantidade = item.Quantidade,
                    ProdutoId = item.Produto.ProdutoId,
                    PedidoId = pedido.PedidoId,
                    Preco = item.Produto.Preco
                };
                _appDbContext.PedidoDetalhes.Add(pedidoDetail);
            }
            _appDbContext.SaveChanges();
        }
    }
}
