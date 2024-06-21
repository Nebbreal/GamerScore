using Gamerscore.Core;
using Gamerscore.Core.Interfaces;
using Gamerscore.Core.Interfaces.Repositories;
using Gamerscore.Core.Interfaces.Services;
using GamerScore.DAL;
using GamerScore.Options;
using GamerScore.Services;
using Humanizer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.Configure<ConnectionStrings>(configuration.GetSection("ConnectionStrings"));
builder.Services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

//Dependency Injection
string connectionString = configuration.GetConnectionString("DBConnectionString");

builder.Services.AddSingleton<IAccountRepository>(new AccountRepository(connectionString));
builder.Services.AddSingleton<IGameRepository>(new GameRepository(connectionString));
builder.Services.AddSingleton<IGenreRepository>(new GenreRepository(connectionString));
builder.Services.AddSingleton<IReviewRepository>(new ReviewRepository(connectionString));

builder.Services.AddSingleton<IAccountService>(sp => new AccountService(sp.GetRequiredService<IAccountRepository>()));
builder.Services.AddSingleton<IGameService>(sp => new GameService(sp.GetRequiredService<IGameRepository>()));
builder.Services.AddSingleton<IGenreService>(sp => new GenreService(sp.GetRequiredService<IGenreRepository>()));
builder.Services.AddSingleton<IReviewService>(sp => new ReviewService(sp.GetRequiredService<IReviewRepository>()));
builder.Services.AddSingleton<ITokenService>(sp => new TokenService(sp.GetRequiredService<IOptions<JwtSettings>>()));


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
