using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SalesWebMvc.Data;
using System.Configuration;
using SalesWebMvc.Services;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
       

        // Obter a connectionString a partir da nossa classe de Context
        string MyConnectionString = builder.Configuration.GetConnectionString("SalesWebMvcContext");

        // Registar a Connection String nos Services do Programa
        //A ultima string utilizada � o nome do projeto
        builder.Services.AddDbContext<SalesWebMvcContext>(options =>
                   options.UseMySql(MyConnectionString, ServerVersion.AutoDetect(MyConnectionString), p => p.MigrationsAssembly("SalesWebMvc")));


        /* builder.Services.AddDbContext<SalesWebMvcContext>(options =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString("SalesWebMvcContext") ?? throw new InvalidOperationException("Connection string 'SalesWebMvcContext' not found.")));
        */

        // Registar a nossa classe(servi�o) SeedingService, injeta o servi�o no sistema de dependencia da aplica�ao
        // Para se poder utilizar a inje��o de dependencias
        builder.Services.AddScoped<SeedingService>();
        builder.Services.AddScoped<SellerService>();

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        var app = builder.Build();



        /// Isto � usado para popular a Base de dados, passamos como parametro do metodo um objeto
        ///IServiceProvider, e na classe SeedingServices, declaramos o metodo est�tico
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            SeedingService.Seed(services);
        }


        // Teste para adicionar um servi�o
        // Para adicionar um servi�o no .NetFramework 6, �  so utilizar a palavra 
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
    }
}