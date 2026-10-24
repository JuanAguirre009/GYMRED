using GymRed.Application.Interfaces.Services;
using GymRed.Application.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Registro de servicios de aplicacion (capa de negocio)
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<ISedeService, SedeService>();
builder.Services.AddScoped<IInstructorService, InstructorService>();
builder.Services.AddScoped<IPlanService, PlanService>();
builder.Services.AddScoped<IMembresiaService, MembresiaService>();
builder.Services.AddScoped<IPagoService, PagoService>();
builder.Services.AddScoped<IClaseService, ClaseService>();
builder.Services.AddScoped<IReservaService, ReservaService>();
builder.Services.AddScoped<IAsistenciaService, AsistenciaService>();
builder.Services.AddScoped<IReporteService, ReporteService>();

WebApplication app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
