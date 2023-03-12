using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TuLote.Abstracciones;
using TuLote.AccesoDatos;
using TuLote.Aplicacion;
using TuLote.Entidades;
using TuLote.Repositorio;
using TuLote.Seed;
using TuLote.Servicios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
//builder.Services.AddDbContext<ApplicationDbContext>(op =>
//op.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionLote"), b => b.MigrationsAssembly("TuLote")));

builder.Services.AddDbContext<ApplicationDbContext>(op =>
op.UseSqlServer(builder.Configuration.GetConnectionString("defaultconnectionSmartAsp"), b => b.MigrationsAssembly("TuLote")));

builder.Services.AddScoped(typeof(IAplicacion<>), typeof(Aplicacion<>));
builder.Services.AddScoped(typeof(IRepositorio<>), typeof(Repositorio<>));
builder.Services.AddScoped(typeof(IDbContext<>), typeof(DbContext<>));
builder.Services.AddScoped(typeof(ILoteRepository<Lote>), typeof(LoteRepository));
builder.Services.AddScoped(typeof(IBarrioRepositorio<Barrio>), typeof(BarrioRepository));

builder.Services.AddScoped<IServicio_API_Provincia, Servicio_API_Provincia>();
builder.Services.AddScoped<IServicio_API_Municipio, Servicio_API_Municipio>();
builder.Services.AddScoped<IServicio_API_Localidad, Servicio_API_Localidad>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));



//Se agrega servicio de autenticación  de usuario
builder.Services.AddAuthentication();
//se agrega el servicio Identity
builder.Services.AddIdentity<Usuario, IdentityRole>(opciones =>
opciones.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
//para poder crear mis propias vistas de login y registro sin usar las predeterminadas...la pagina inicia aqui
builder.Services.PostConfigure<CookieAuthenticationOptions>(IdentityConstants.ApplicationScheme,
    opciones =>
    {
        opciones.LoginPath = "/usuarios/login";
        opciones.AccessDeniedPath = "/usuarioslLogin";
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        var userManager = services.GetRequiredService<UserManager<Usuario>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        await ContextSeed.SeedRolesAsync(userManager, roleManager);
        await ContextSeed.SeedAdminAsync(userManager, roleManager); //lo agrego luego de sembrar lo anterior
    }
    catch (Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "An error occurred seeding the DB.");
    }


}
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

IWebHostEnvironment env = app.Environment;
app.Run();




