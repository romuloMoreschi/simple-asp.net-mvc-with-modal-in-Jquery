using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PontosWeb.Models;
using PontosWeb.Services.Interfaces;
using PontosWeb.Services.Paginacao;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PontosWeb.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly IProdutoService _produtoService;
        private readonly ICategoriaService _categoriaService;

        public ProdutoController(IProdutoService produtoService, ICategoriaService categoriaService)
        {
            _produtoService = produtoService;
            _categoriaService = categoriaService;
        }

        public async Task<IActionResult> Index(int? pageNumber)
        {
            if (pageNumber < 0)
                pageNumber = null;

            var produtos = _produtoService.Obter();
            var totalRegistros = await _produtoService.TotalRegistro();

            return View(await Paginacao<Produto>.Create(produtos, totalRegistros, pageNumber ?? 1, 10));
        }

        public async Task<IActionResult> Cadastrar()
        {
            var categorias = await _categoriaService.Obter().ToListAsync();

            if (!categorias.Any())
            {
                TempData["MSG_D"] = "Nenhuma categoria foi encontrada, por favor cadastre antes do produto!";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categorias = new SelectList(categorias, "Id", "Nome");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cadastrar(Produto produto)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Categorias = new SelectList(await _categoriaService.Obter().ToListAsync(), "Id", "Nome");

                if (await _produtoService.Inserir(produto))
                {
                    TempData["MSG_S"] = "Registro salvo com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
            }

            TempData["MSG_D"] = "Houve um problema!";

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Atualizar(int id)
        {
            ViewBag.Categorias = new SelectList(await _categoriaService.Obter().ToListAsync(), "Id", "Nome");
            var produtos = await _produtoService.Obter(id);
            return View(produtos);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Atualizar(Produto produto, int id)
        {
            if (ModelState.IsValid)
            {
                if (await _produtoService.Atualizar(produto, id))
                {
                    TempData["MSG_S"] = "Registro salvo com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
            }

            TempData["MSG_D"] = "Houve um problema!";

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Excluir(int id)
        {
            if (await _produtoService.Remover(id))
            {
                TempData["MSG_S"] = "Registro removido com sucesso!";
                return RedirectToAction(nameof(Index));
            }

            TempData["MSG_D"] = "Houve um problema!";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Pesquisar(Produto produto)
        {
            if (ModelState.IsValid)
            {
                var produtos = _produtoService.ObterPorInstancia(produto).ToList();
                if (produtos != null)
                {
                    return Ok(produtos.ToArray());
                }
            }

            TempData["MSG_D"] = "Houve um problema!";

            return null;
        }
    }
}
