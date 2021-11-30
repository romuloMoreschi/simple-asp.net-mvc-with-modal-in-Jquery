using PontosWeb.Models;
using System.Threading.Tasks;

namespace PontosWeb.Repositorio.Interface
{
    public interface IRepositorioProduto : IRepositorioBase<Produto>
    {
        Task<int> TotalRegistro();
    }
}
