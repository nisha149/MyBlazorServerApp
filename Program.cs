using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using MyBlazorServerApp.Data;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add logging for better debugging
builder.Services.AddLogging(logging =>
{
    logging.AddConsole();
    logging.AddDebug();
});

// Register EF Core with DbContextFactory for database access
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

// Add services for Blazor Server
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Configure Kestrel to use specified ports from launchSettings
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5134); // HTTP port
    options.ListenAnyIP(7256, listenOptions => listenOptions.UseHttps()); // HTTPS port
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection(); // Enforce HTTPS redirection
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Apply migrations on startup in development mode
if (app.Environment.IsDevelopment())
{
    try
    {
        using var scope = app.Services.CreateScope();
        var dbFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<AppDbContext>>();
        using var db = dbFactory.CreateDbContext();
        app.Logger.LogInformation("Applying database migrations...");
        db.Database.Migrate();
        app.Logger.LogInformation("Database migrations applied successfully.");
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex, "Migration failed: {Message}. Continuing without migration.", ex.Message);
    }
}

// Map Blazor endpoints
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();