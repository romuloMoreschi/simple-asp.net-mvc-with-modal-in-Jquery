using PontosWeb.Models;
using PontosWeb.Repositorio.Interface;
using PontosWeb.Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PontosWeb.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IRepositorioProduto _produtoRepositorio;

        public ProdutoService(IRepositorioProduto produtoRepositorio)
        {
            _produtoRepositorio = produtoRepositorio;
        }

        public async Task<bool> Inserir(Produto produto)
        {
            var produtoExiste = await _produtoRepositorio.Obter(produto.Id);

            if (produtoExiste != null)
                throw new Exception("Já existe um produto cadastrado com o nome informado.");

            await _produtoRepositorio.Inserir(produto);

            return true;
        }
        public async Task<bool> Atualizar(Produto produto, long id)
        {
            var produtoExiste = await _produtoRepositorio.Obter(produto.Id);

            if (produtoExiste == null)
                throw new Exception("Não existe nenhum produto com o id informado!");

            if (produtoExiste.Id == id)
            {
                var produtoAtualizado = await _produtoRepositorio.Atualizar(produto);

                return true;
            }
            throw new Exception("O id informado não confere com o id do produto!");
        }

        public async Task<Produto> Obter(long id)
        {
            var produto = await _produtoRepositorio.Obter(id);

            return produto;
        }

        public IQueryable<Produto> Obter() => _produtoRepositorio.Obter(); 

        public async Task<bool> Remover(long id)
        {
            await _produtoRepositorio.Remover(id);

            return true;
        }

        public async Task<int> TotalRegistro()
        {
            return await _produtoRepositorio.TotalRegistro();
        }

        public IQueryable<Produto> ObterPorInstancia(Produto produto) => _produtoRepositorio.ObterPorInstancia(produto);
    }
}
