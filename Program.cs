using Microsoft.EntityFrameworkCore;
using MyBlazorServerApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Register EF Core with DbContextFactory (best for Blazor Server)
builder.Services.AddDbContextFactory<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found."),
        sqlOptions => sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(10),
            errorNumbersToAdd: null
        )
    )
);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var app = builder.Build();

// Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Ensure the database is created on startup (for development/testing)
using (var scope = app.Services.CreateScope())
{
    var dbFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<AppDbContext>>();
    using var db = dbFactory.CreateDbContext();
    db.Database.EnsureCreated(); // Creates the database and tables if they don't exist
}

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();