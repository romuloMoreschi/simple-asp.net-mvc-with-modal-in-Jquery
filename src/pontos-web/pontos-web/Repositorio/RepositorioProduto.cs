using Microsoft.EntityFrameworkCore;
using PontosWeb.DataBase;
using PontosWeb.Models;
using PontosWeb.Repositorio.Interface;
using System.Linq;
using System.Threading.Tasks;

namespace PontosWeb.Repositorio
{
    public class RepositorioProduto : RepositorioBase<Produto>, IRepositorioProduto
    {
        private readonly PontosWebContext _context;

        public RepositorioProduto(PontosWebContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<Produto> Obter(long id)
        {
            return await _context.Set<Produto>()
                                   .Where(x => x.Id == id)
                                   .Include(x => x.Categoria)
                                   .AsNoTracking()
                                   .FirstOrDefaultAsync();
        }
        public override IQueryable<Produto> Obter()
        {
            return _context.Set<Produto>()
                            .Include(x => x.Categoria)
                            .AsNoTracking();
        }

        public async Task<int> TotalRegistro()
        {
            return await _context.Set<Produto>()
                                .AsNoTracking()
                                .CountAsync();
        }

        public IQueryable<Produto> ObterPorInstancia(Produto produto)
        {
            if (produto == null) return null;

            var produtos = Obter();

            if (!string.IsNullOrWhiteSpace(produto.Nome))
                produtos = produtos.Where(n => n.Nome.ToLower().Contains(produto.Nome.ToLower()));
            if (produto.Pontos != 0)
                produtos = produtos.Where(n => n.Pontos == produto.Pontos);

            return produtos;
        }
    }
}
