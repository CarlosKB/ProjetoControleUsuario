using ControleDeContatos.Data;
using ControleDeContatos.Helper;
using ControleDeContatos.Repositorio;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var services = builder.Services;

services.AddDbContext<BancoContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DataBase")));

services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
services.AddScoped<IContatoRepositorio, ContatoRepositorio>();
services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
services.AddScoped<ISessao, Sessao>();

services.AddSession(o =>
{
    o.Cookie.HttpOnly = true;
    o.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
