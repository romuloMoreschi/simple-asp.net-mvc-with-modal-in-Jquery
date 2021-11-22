using System.Linq;
using PontosWeb.Models;
using PontosWeb.DataBase;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PontosWeb.Repositorio.Interface;

namespace PontosWeb.Repositorio
{
    public class RepositorioBase<T> : IRepositorioBase<T> where T : EntidadeBase
    {
        private readonly PontosWebContext _context;

        public RepositorioBase(PontosWebContext context)
        {
            _context = context;
        }

        public virtual async Task<T> Inserir(T obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();

            return obj;
        }

        public virtual async Task<T> Atualizar(T obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return obj;
        }

        public virtual async Task<T> Obter(long id)
        {
            return await _context.Set<T>()
                                .AsNoTracking()
                                .Where(x => x.Id == id)
                                .SingleOrDefaultAsync();
        }

        public virtual async Task<IList<T>> Obter(int skip, int take)
        {
            return await _context.Set<T>()
                               .AsNoTracking()
                               .Skip(skip)
                               .Take(take)
                               .ToListAsync(); 
        }

        public virtual async Task Remover(long id)
        {
            var obj = await _context.Set<T>()
                                .Where(x => x.Id == id)
                                .SingleOrDefaultAsync();
            if (obj != null)
            {
                _context.Remove(obj);
                await _context.SaveChangesAsync();
            }
        }
    }
}
