using PontosWeb.Models;
using System.Linq;
using System.Threading.Tasks;

namespace PontosWeb.Services.Interfaces
{
    public interface ICategoriaService
    {
        Task<bool> Inserir(Categoria produto);
        Task<Categoria> Obter(long id);
        IQueryable<Categoria> Obter();
    }
}
