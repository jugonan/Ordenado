using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace DEFINITIVO.Services
{
    public class Seed
    {
        public async Task CrearRoles(IServiceProvider serviceProvider)
        {
            RoleManager<IdentityRole> RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            UserManager<IdentityUser> UserManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            string[] nombreRoles = { "admin", "cliente", "vendedor" };
            foreach (string nombreRol in nombreRoles)
            {
                bool rolExists = await RoleManager.RoleExistsAsync(nombreRol);
                if (!rolExists)
                {
                    await RoleManager.CreateAsync(new IdentityRole(nombreRol));
                }
            }

            IdentityUser administrador = new IdentityUser
            {
                Email = "admin@admin.com",
                UserName = "admin@admin.com"
            };

            var result = await UserManager.CreateAsync(administrador, "123456");
            if (result.Succeeded)
            {
                await UserManager.AddToRoleAsync(administrador, "admin");
            }
        }
    }
}
