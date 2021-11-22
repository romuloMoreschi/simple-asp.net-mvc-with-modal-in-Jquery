using PontosWeb.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PontosWeb.Services.Interfaces;

namespace PontosWeb.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cadastrar(Categoria categoria)
        {
            if (ModelState.IsValid)
            {

                if (await _categoriaService.Inserir(categoria))
                {
                    TempData["MSG_S"] = "Registro salvo com sucesso!";
                    return View();
                }
                TempData["MSG_D"] = "Houve um problema!";
                return View();
            }

            return View(categoria);
        }
    }
}
