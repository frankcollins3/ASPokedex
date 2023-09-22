using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL; // For PostgreSQL support
using aspokedex.PokemonModel; // Your DbContext and models namespace
using Microsoft.Extensions.Logging;
using aspokedex.Data;

// tinkering
// using Newtonsoft.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddRazorPages();

// Configure the DbContext and connection string
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql("Host=localhost;Port=5432;Database=pokedex;Username=postgres;Password=${mypassword}"));
    
// Add logging configuration
builder.Logging.AddConsole(); // This adds console logging    
builder.Services.AddControllers();
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient("PokeApi", client =>
{
    client.BaseAddress = new Uri("https://pokeapi.co/api/v2/");
    // Configure other HttpClient options if needed
});
// var httpClient = _httpClientFactory.CreateClient("PokeApi");         // this is then used through the rest of the app.


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
} 
else
{
    // app.UseMigrationsEndPoint();
    // "no accessible extension method error"
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
});

app.MapRazorPages();

app.Run();
