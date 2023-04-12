using AnotherTechblog.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();
/*
builder.Services.AddDbContext<AnotherTechblogDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AnotherTechblogDbContext") ?? throw new InvalidOperationException("Connection string 'AnotherTechblogDbContext' not found.")));
*/
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

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapControllerRoute(
        name: "decryption",
        pattern: "{controller=Decryption}/{action=DecryptCipher}/{id?}");
    endpoints.MapControllerRoute(
        name: "encryption",
        pattern: "{controller=Encryption}/{action=EncryptCipher}");
    });

app.Run();
