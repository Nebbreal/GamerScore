using Gamerscore.Core;
using Gamerscore.Core.Interfaces;
using GamerScore.DAL;
using GamerScore.Options;
var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.Configure<ConnectionStrings>(configuration.GetSection("ConnectionStrings"));
builder.Services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

//Dependency Injection
string connectionString = configuration.GetConnectionString("DBConnectionString");

builder.Services.AddSingleton<IAccountRepository>(new AccountRepository(connectionString));
builder.Services.AddSingleton<IGameRepository>(new GameRepository(connectionString));
builder.Services.AddSingleton<IGenreRepository>(new GenreRepository(connectionString));
builder.Services.AddSingleton<AccountManager>(sp => { 
    return new AccountManager(sp.GetRequiredService<IAccountRepository>()); 
});

builder.Services.AddControllersWithViews();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Home}/{id?}");

app.Run();
