using LibraryCatalog.Data;
using LibraryCatalog.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<LibraryContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("LibraryContext")));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<LibraryContext>();
    if (!context.Books.Any())
    {
        var sampleBooks = new List<Book>
        {
            new Book { Title = "The Rise and Fall of the Roman Empire", Author = "Edward Gibbon", Genre = "Non-fiction", PublishedDate = new DateTime(1789)},
            new Book { Title = "A Distant Mirror: The Calamitous 14th Century", Author = "Barbara Tuchman", Genre = "Non-fiction", PublishedDate = new DateTime(1978)},
        };
        context.Books.AddRange(sampleBooks);
        context.SaveChanges();
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    _ = endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.UseAuthorization();

app.MapStaticAssets();

app.Run();
