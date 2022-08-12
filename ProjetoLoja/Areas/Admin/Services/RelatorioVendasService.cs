using Microsoft.EntityFrameworkCore;
using ProjetoLoja.Context;
using ProjetoLoja.Models;

namespace ProjetoLoja.Areas.Admin.Services
{
    public class RelatorioVendasService
    {
        private readonly AppDbContext context;

        public RelatorioVendasService(AppDbContext _context)
        {
            context = _context;
        }

        public async Task<List<Pedido>> FindByDateAsync(DateTime? iniciaDate,DateTime? finalDate)
        {
            var resultado = from obj in context.Pedidos select obj;

            if (iniciaDate.HasValue)
            {
                resultado = resultado.Where(x => x.PedidoEnviado >= iniciaDate.Value);
            }
            if (finalDate.HasValue)
            {
                resultado = resultado.Where(x => x.PedidoEnviado <= finalDate.Value);
            }
            return await resultado
                         .Include(l => l.PedidoItens)
                         .ThenInclude(l => l.Produto)
                         .OrderByDescending(x => x.PedidoEnviado)
                         .ToListAsync();
        }
    }
}
