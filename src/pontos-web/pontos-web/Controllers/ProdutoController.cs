﻿using System.Linq;
using PontosWeb.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PontosWeb.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        public async Task<IActionResult> Index()
        {
            return View(await _produtoService.Obter(0 ,10));
        }

        public async Task<IActionResult> Cadastrar()
        {
            var categorias = await _categoriaService.Obter();

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
                ViewBag.Categorias = new SelectList(await _categoriaService.Obter(), "Id", "Nome");

                if (await _produtoService.Inserir(produto))
                {
                    TempData["MSG_S"] = "Registro salvo com sucesso!";
                    return RedirectToAction(nameof(Index)); 
                }
                TempData["MSG_D"] = "Houve um problema!";
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Atualizar(int id)
        {
            ViewBag.Categorias = new SelectList(await _categoriaService.Obter(), "Id", "Nome");
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
                TempData["MSG_D"] = "Houve um problema!";

                ViewBag.Categorias = new SelectList(await _categoriaService.Obter(), "Id", "Nome");
                var EquipInfo = await _produtoService.Obter(id);
                return RedirectToAction(nameof(Index));
            }
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
    }
}