using PontosWeb.Models;
using System.Linq;
using System.Threading.Tasks;

namespace PontosWeb.Services.Interfaces
{
    public interface IProdutoService 
    {
        Task<bool> Inserir(Produto produto);
        Task<bool> Atualizar(Produto produto, long id);
        Task<bool> Remover(long id);
        Task<Produto> Obter(long id);
        IQueryable<Produto> Obter();
        Task<int> TotalRegistro();
        IQueryable<Produto> ObterPorInstancia(Produto produto);
    }
}
