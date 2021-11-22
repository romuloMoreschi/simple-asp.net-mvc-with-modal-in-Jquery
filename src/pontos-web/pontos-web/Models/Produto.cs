using PontosWeb.Models;

namespace PontosWeb.Models
{
    public class Produto : EntidadeBase
    {
        public string Nome { get; set; }
        public int Pontos { get; set; }
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
    }
}
