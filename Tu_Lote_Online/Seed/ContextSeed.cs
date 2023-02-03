using Microsoft.AspNetCore.Identity;
using TuLote.Entidades;
using TuLote.Enums;

namespace TuLote.Seed
{
    public class ContextSeed
    {
        public static async Task SeedRolesAsync(UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles

            await roleManager.CreateAsync(new IdentityRole(Roles.Administrador.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Agente.ToString()));

        }

        public static async Task SeedAdminAsync(UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default User
            var defaultUser = new Usuario
            {
                Nombre = "Matias",
                Apellido = "Boiero",
                Alias = "adm",
                Email = "admin@yopmail.com",
                UserName = "admin@yopmail.com",
                EmailConfirmed = true,


            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                //Le asigno todos los roles al superadministrador
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "Admin123*");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Administrador.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.Agente.ToString());


                }
            }
        }
    }
}
