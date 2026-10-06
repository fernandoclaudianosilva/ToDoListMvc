using Microsoft.EntityFrameworkCore;
using ToDoListMvc.Data;
using ToDoListMvc.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. PRIMEIRO: Registas todos os serviços (incluindo o DbContext e os Scopes)
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));

builder.Services.AddScoped<ITaskService, TaskService>();

// 2. DEPOIS: Constróis a aplicação
var app = builder.Build();

// 3. POR FIM: Configuras o pipeline HTTP (Middleware)
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Tasks}/{action=Index}/{id?}");

app.Run();