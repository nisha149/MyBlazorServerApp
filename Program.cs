using Microsoft.EntityFrameworkCore;
using MyBlazorServerApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Register EF Core with DbContextFactory
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

// Add services for Blazor Server and Razor Pages
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization(); // Added for potential authentication/authorization support

// Apply migrations on startup in development mode
if (app.Environment.IsDevelopment())
{
    try
    {
        using var scope = app.Services.CreateScope();
        var dbFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<AppDbContext>>();
        using var db = dbFactory.CreateDbContext();
        db.Database.Migrate();
    }
    catch (Exception ex)
    {
        // Log the error and continue (prevent app crash in development)
        Console.WriteLine($"An error occurred while applying migrations: {ex.Message}");
        // Optionally, throw only if critical: throw;
    }
}

// Map Blazor endpoints
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();