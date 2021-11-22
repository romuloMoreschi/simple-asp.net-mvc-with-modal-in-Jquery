using PontosWeb.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace PontosWeb.Services.Interfaces
{
    public interface IProdutoService 
    {
        Task<bool> Inserir(Produto produto);
        Task<bool> Atualizar(Produto produto, long id);
        Task<bool> Remover(long id);
        Task<Produto> Obter(long id);
        Task<IList<Produto>> Obter(int skip, int take);
    }
}
