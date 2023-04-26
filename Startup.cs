using AnotherTechblog.Data;
using Microsoft.EntityFrameworkCore;
using Npgsql;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {

        services.AddControllersWithViews();
        var connectionString = Configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<AnotherTechblogDbContext>(options =>
            options.UseNpgsql(connectionString));

    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            endpoints.MapControllerRoute(
                name: "features",
                pattern: "{controller=Decryption}/{action=DecryptCipher}/{id?}");
            endpoints.MapControllerRoute(
                name: "features",
                pattern: "{controller=Encryption}/{action=EncryptCipher}");
            endpoints.MapControllerRoute(
                name: "features",
                pattern: "{controller=WordCount}/{action=WordCount}"); 
            endpoints.MapControllerRoute(
                name: "features",
                pattern: "{controller=Maze}/{action=AStarMazeSolver}");
        });
    }
}