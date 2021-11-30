using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PontosWeb.Services.Paginacao
{
    public class Paginacao<T> : List<T>
    {
        public int PaginaInicial { get; private set; }
        public int TotalPagina { get; private set; }

        public Paginacao(List<T> items, int total, int pageIndex, int tamanhaDaPagina)
        {
            PaginaInicial = pageIndex;
            TotalPagina = (int)Math.Ceiling(total / (double)tamanhaDaPagina);

            this.AddRange(items);
        }

        public bool TemPaginaAnterior
        {
            get
            {
                return (PaginaInicial > 1);
            }
        }

        public bool TemProximaPagina
        {
            get
            {
                return (PaginaInicial < TotalPagina);
            }
        }

        public static async Task<Paginacao<T>> Create(IQueryable<T> items, int total, int pageIndex, int pageSize)
        {
            var lista = await items.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new Paginacao<T>(lista, total, pageIndex, pageSize);
        }
    }
}
