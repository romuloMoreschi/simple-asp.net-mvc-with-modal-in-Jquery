using PontosWeb.Models;
using PontosWeb.Repositorio.Interface;
using PontosWeb.Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PontosWeb.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly IRepositorioCategoria _categoriaRepositorio;

        public CategoriaService(IRepositorioCategoria categoriaRepositorio)
        {
            _categoriaRepositorio = categoriaRepositorio;
        }
        public async Task<bool> Inserir(Categoria categoria)
        {
            var categoriaExiste = await _categoriaRepositorio.Obter(categoria.Id);

            if (categoriaExiste != null)
                throw new Exception("Já existe um produto cadastrado com o nome informado.");

            await _categoriaRepositorio.Inserir(categoria);

            return true;
        }

        public async Task<Categoria> Obter(long id)
        {
            var categoria = await _categoriaRepositorio.Obter(id);

            return categoria;
        }

        public IQueryable<Categoria> Obter() =>_categoriaRepositorio.Obter();
    }
}
