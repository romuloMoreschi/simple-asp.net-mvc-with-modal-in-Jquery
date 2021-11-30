using System.Linq;
using PontosWeb.Models;
using System.Threading.Tasks;

namespace PontosWeb.Repositorio.Interface
{
    public interface IRepositorioBase<T> where T : EntidadeBase
    {
        Task<T> Inserir(T obj);
        Task<T> Atualizar(T obj);
        Task Remover(long id);
        Task<T> Obter(long id);
        IQueryable<T> Obter();
    }
}
