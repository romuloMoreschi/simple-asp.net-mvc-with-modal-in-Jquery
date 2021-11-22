using PontosWeb.DataBase;
using PontosWeb.Models;
using PontosWeb.Repositorio;
using PontosWeb.Repositorio.Interface;

namespace PontosWeb.Repositorio
{
    public class RepositorioCategoria : RepositorioBase<Categoria>, IRepositorioCategoria
    {
        private readonly PontosWebContext _context;

        public RepositorioCategoria(PontosWebContext context) : base(context)
        {
            _context = context;
        }
    }
}
