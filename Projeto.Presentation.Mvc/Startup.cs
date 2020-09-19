using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Projeto.Infra.Data.Contexts;
using Projeto.Infra.Data.Contracts;
using Projeto.Infra.Data.Repositories;

namespace Projeto.Presentation.Mvc
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //Habilitar o projeto para MVC
            services.AddControllersWithViews();

            //Configurar o EntityFramework
            services.AddDbContext<DataContext>
                (map => map.UseSqlServer(Configuration.GetConnectionString("DashboardSabado")));

            //injeção de dependencia para as interfaces do repositorio
            services.AddTransient<IContaRepository, ContaRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();

            //mapeamento da rota inicial do projeto
            app.UseEndpoints(
                endpoints => {
                    endpoints.MapControllerRoute(
                        name: "default", //define o padrão de navegação
                        pattern: "{controller=Home}/{action=Index}/{id?}"
                        );
                });
        }
    }
}
