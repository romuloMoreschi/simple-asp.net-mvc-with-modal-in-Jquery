using PontosWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace PontosWeb.DataBase
{
    public class PontosWebContext : DbContext
    {
        public PontosWebContext(DbContextOptions<PontosWebContext> options) : base(options)
        {

        }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
    }
}
