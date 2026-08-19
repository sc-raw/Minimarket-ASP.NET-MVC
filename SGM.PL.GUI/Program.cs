using SGM.Application.BL.BC.Service;
using SGM.Domain.Interfaces;
using SGM.Infrastructure.DL.DALC.Persistence;
using SGM.Infrastructure.DL.DALC.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// ===== Inyección de dependencias =====
// Conexión
builder.Services.AddSingleton<IBDConexion, BDConexion>();

// Repositorios
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<IProductoRepository, ProductoRepository>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IVentaRepository, VentaRepository>();

// Services (Business Components)
builder.Services.AddScoped<ICategoriaService, CategoriaBC>();
builder.Services.AddScoped<IProductoService, ProductoBC>();
builder.Services.AddScoped<IClienteService, ClienteBC>();
builder.Services.AddScoped<IUsuarioService, UsuarioBC>();
builder.Services.AddScoped<IVentaService, VentaBC>();
builder.Services.AddScoped<IReporteService, ReporteBC>();


builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
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
app.UseRouting();

//Sesion
app.UseSession();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
