using System;
using AutoMapper;
using Heldu.Database.Data;
using Heldu.Logic.Interfaces;
using Heldu.Logic.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(
                    Configuration.GetConnectionString("DefaultConnection"), mysqlOptions =>
                    {
                        mysqlOptions
                            .CharSetBehavior(CharSetBehavior.AppendToAllColumns);
                    }));
            services.AddControllers().AddNewtonsoftJson(x => {
                x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                x.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                });
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddMemoryCache();

            services.AddTransient<ICategoriasService, CategoriasService>();
            services.AddTransient<IFavoritosService, FavoritosService>();
            services.AddTransient<IGustosUsuariosService, GustosUsuariosService>();
            services.AddTransient<IHelperService, HelperService>();
            services.AddTransient<IManejoProductosService, ManejoProductosService>();
            services.AddTransient<IMercadosService, MercadosService>();
            services.AddTransient<IOpcionesProductosService, OpcionesProductosService>();
            services.AddTransient<IProductoCategoriasService, ProductoCategoriasService>();
            services.AddTransient<IProductosService, ProductosService>();
            services.AddTransient<IProductosVendedoresService, ProductosVendedoresService>();
            services.AddTransient<IReviewsService, ReviewsService>();
            services.AddTransient<IVisitasService, VisitasService>();
            services.AddTransient<IUbicacionesUsuariosService, UbicacionesUsuarioService>();
            services.AddTransient<IUbicacionesVendedoresService, UbicacionesVendedoresService>();
            services.AddTransient<IUsuariosService, UsuariosService>();
            services.AddTransient<IVendedoresService, VendedoresService>();
            services.AddTransient<IImagenesProductosService, ImagenesProductosService>();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
