using Microsoft.EntityFrameworkCore;
using ConcursoNestleWeb.Data;

var builder = WebApplication.CreateBuilder(args);

// Configurar el servicio de conexi�n a la base de datos
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConcursoNestleWebContext") 
    ?? throw new InvalidOperationException("Connection string 'ConcursoNestleWebContext' not found.")));

// Agregar servicios para controladores con vistas
builder.Services.AddControllersWithViews();

// Configurar sesiones
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Tiempo de expiraci�n de la sesi�n
    options.Cookie.HttpOnly = true; // Proteger la cookie de sesi�n
    options.Cookie.IsEssential = true; // Asegurar que la cookie es esencial para el funcionamiento
});

var app = builder.Build();

// Configuraci�n del pipeline de solicitudes HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Activar sesiones
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
