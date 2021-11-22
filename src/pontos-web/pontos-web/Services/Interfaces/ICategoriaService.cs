using PontosWeb.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace PontosWeb.Services.Interfaces
{
    public interface ICategoriaService
    {
        Task<bool> Inserir(Categoria produto);
        Task<Categoria> Obter(long id);
        Task<IList<Categoria>> Obter();
    }
}
