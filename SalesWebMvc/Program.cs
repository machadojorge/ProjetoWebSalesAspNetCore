using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SalesWebMvc.Data;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Obter a connectionString a partir da nossa classe de Context
string MyConnectionString = builder.Configuration.GetConnectionString("SalesWebMvcContext");

// Registar a Connection String nos Services do Programa
//A ultima string utilizada é o nome do projeto
builder.Services.AddDbContext<SalesWebMvcContext>(options =>
           options.UseMySql(MyConnectionString, ServerVersion.AutoDetect(MyConnectionString),p=> p.MigrationsAssembly("SalesWebMvc"))); 


/* builder.Services.AddDbContext<SalesWebMvcContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("SalesWebMvcContext") ?? throw new InvalidOperationException("Connection string 'SalesWebMvcContext' not found.")));
*/


// Add services to the container.
builder.Services.AddControllersWithViews();

        var app = builder.Build();

        // Teste para adicionar um serviço
        // Para adicionar um serviço no .NetFramework 6, é  so utilizar a palavra 
        // builder.service....

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        // Aqui ficava a parte do Configure, no 
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
