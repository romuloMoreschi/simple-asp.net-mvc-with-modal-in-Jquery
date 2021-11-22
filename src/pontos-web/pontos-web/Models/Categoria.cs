using System.Collections.Generic;

namespace PontosWeb.Models
{
    public class Categoria : EntidadeBase
    {
        public string Nome { get; set; }
        public IList<Produto> Produtos { get; set; }
    }
}
