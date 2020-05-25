using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Heldu.Database.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DEFINITIVO.Services;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Heldu.Logic.Interfaces;
using Heldu.Logic.Services;
using HelperService = Heldu.Logic.Services.HelperService;
using MaxMind.GeoIP2;

namespace DEFINITIVO
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(
                    Configuration.GetConnectionString("DefaultConnection"), mysqlOptions =>
                    {
                        mysqlOptions
                            .CharSetBehavior(CharSetBehavior.AppendToAllColumns);
                        //.AnsiCharSet(CharSet.Latin1)
                        //.UnicodeCharSet(CharSet.Utf8Mb4);
                    }));
            services.AddDefaultIdentity<IdentityUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 1;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddSingleton<MessagesService>();
            services.AddAuthentication()
                    .AddGoogle(options =>
                    {
                        IConfigurationSection googleAuthNSection =
                            Configuration.GetSection("Authentication:Google");

                        options.ClientId = "736436250142-9gmlhu4rqjpeadqhjjtq0co0dnb78ae8.apps.googleusercontent.com";
                        options.ClientSecret = "twv7mWhWY13K-Uy7lizUfb-8";
                    })
                    .AddFacebook(facebookOptions =>
                    {
                        facebookOptions.AppId = "2246956105601249";
                        facebookOptions.AppSecret = "bfc93921a19a84bd39781ee8685c692b";
                    });

            //Nueva Arquitectura
            services.AddTransient<ICategoriasService, CategoriasService>();
            //services.AddTransient<ICondicionesService, CondicionesService>();
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

            //Servicio de GeoIP2 de MaxMind (https://maxmind.github.io/GeoIP2-dotnet/)
            // Configure to read configuration options from MaxMind section
            services.Configure<WebServiceClientOptions>(Configuration.GetSection("MaxMind"));
            // Configure dependency injection for WebServiceClient
            services.AddHttpClient<WebServiceClient>();
            services.AddTransient<GeoLocationService>();


            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            Seed seed = new Seed();
            seed.CrearRoles(serviceProvider).Wait();

        }
    }
}
