using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PontosWeb.DataBase;
using PontosWeb.Services;
using PontosWeb.Repositorio;
using PontosWeb.Repositorio.Interface;
using PontosWeb.Services.Interfaces;

namespace PontosWeb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews().AddJsonOptions(options => {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
            });

            services.AddDbContext<PontosWebContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DbConnection")));

            #region InjecaoDeDependencia

            services.AddScoped<IRepositorioProduto, RepositorioProduto>();
            services.AddScoped<IRepositorioCategoria, RepositorioCategoria>();

            services.AddScoped<IProdutoService, ProdutoService>();
            services.AddScoped<ICategoriaService, CategoriaService>();

            #endregion
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
