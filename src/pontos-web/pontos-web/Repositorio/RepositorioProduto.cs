using System.Linq;
using PontosWeb.Models;
using PontosWeb.DataBase;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PontosWeb.Repositorio.Interface;

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
        public override async Task<IList<Produto>> Obter(int skip, int take)
        {
            return await _context.Set<Produto>()
                                .Include(x => x.Categoria)
                                .AsNoTracking()
                                .Skip(skip)
                                .Take(take)
                                .ToListAsync();
        }
    }
}
