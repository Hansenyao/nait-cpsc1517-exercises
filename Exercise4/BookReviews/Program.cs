using BookReviews.Components;
using BookSystemDB.BLL;
using BookSystemDB.DAL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Injecting Services must comes before the builder is built!!
builder.Services.AddDbContext<eBooksContext>(options => 
        options.UseSqlServer(builder.Configuration.GetConnectionString("BookDB")));

// Add database services
builder.Services.AddScoped<GenreServices>();
builder.Services.AddScoped<AuthorServices>();
builder.Services.AddScoped<BookServices>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
