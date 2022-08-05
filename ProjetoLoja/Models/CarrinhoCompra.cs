﻿using Microsoft.EntityFrameworkCore;
using ProjetoLoja.Context;

namespace ProjetoLoja.Models
{
    public class CarrinhoCompra
    {
        private readonly AppDbContext _context;

        public CarrinhoCompra(AppDbContext context)
        {
            _context = context;
        }

        public string CarrinhoCompraId { get; set; }
        public List<CarrinhoCompraItem> CarrinhoCompraItems { get; set; }


        public static CarrinhoCompra GetCarrinho(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            var context = services.GetService<AppDbContext>();

            string carrinhoId = session.GetString("CarrinhoId")??Guid.NewGuid().ToString();

            session.SetString("CarrinhoId",carrinhoId);
            
            return new CarrinhoCompra(context)
            {
                CarrinhoCompraId = carrinhoId
            };
        }


        public void AdicionarAoCarrinho(Produto produto)
        {
            var carrinhoCompraItem = _context.CarrinhoCompraItems
                .SingleOrDefault(s => s.Produto.ProdutoId == produto.ProdutoId && s.CarrinhoCompraId == CarrinhoCompraId);

            if (carrinhoCompraItem == null)
            {
                carrinhoCompraItem = new CarrinhoCompraItem
                {
                    CarrinhoCompraId = CarrinhoCompraId,
                    Produto = produto,
                    Quantidade = 1
                };
                _context.CarrinhoCompraItems.Add(carrinhoCompraItem);
            }
            else
            {
                carrinhoCompraItem.Quantidade++;
            }
            _context.SaveChanges();
        }

        public int RemoverDoCarrinho(Produto produto)
        {
            var carrinhoCompraItem = _context.CarrinhoCompraItems
                .SingleOrDefault(s => s.Produto.ProdutoId == produto.ProdutoId && s.CarrinhoCompraId == CarrinhoCompraId);

            var quantidadeLocal = 0;

            if (carrinhoCompraItem != null)
            {
                if(carrinhoCompraItem.Quantidade > 1)
                {
                    carrinhoCompraItem.Quantidade--;
                    quantidadeLocal = carrinhoCompraItem.Quantidade;
                }
                else
                {
                    _context.CarrinhoCompraItems.Remove(carrinhoCompraItem);
                }
            }
            _context.SaveChanges();
            return quantidadeLocal;
        }

        public List<CarrinhoCompraItem> GetCarrinhoComprasItens()
        {
            return CarrinhoCompraItems ?? (CarrinhoCompraItems = _context.CarrinhoCompraItems
                .Where(c => c.CarrinhoCompraId == CarrinhoCompraId)
                .Include(s => s.Produto)
                .ToList());
        }

        public void LimparCarrinho()
        {
            var carrinhoItens = _context.CarrinhoCompraItems.Where(carrinho => carrinho.CarrinhoCompraId == CarrinhoCompraId);
            _context.CarrinhoCompraItems.RemoveRange(carrinhoItens);
            _context.SaveChanges();
        }

        public decimal GetCarrinhoCompraTotal()
        {
           var total = _context.CarrinhoCompraItems
                .Where(x => x.CarrinhoCompraId == CarrinhoCompraId)
                .Select(c => c.Produto.Preco*c.Quantidade)
                .Sum();
            return total;
        }
    }
}
