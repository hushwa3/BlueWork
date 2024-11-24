using BlueWork.web.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews();

// Configure Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
.AddCookie()
.AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
{
    options.ClientId = builder.Configuration.GetValue<string>("GoogleKeys:ClientId");
    options.ClientSecret = builder.Configuration.GetValue<string>("GoogleKeys:ClientSecret");
});

// Register DbContext
builder.Services.AddDbContext<BlueWorkDbContext>(options =>
{
    try
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("FirstConnection"))
               .EnableSensitiveDataLogging(); // Enable for debugging
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Database connection failed: {ex.Message}");
        throw;
    }
});

// Build the app
var app = builder.Build();

// Error handling
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler(errorApp =>
    {
        errorApp.Run(async context =>
        {
            var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
            var exception = exceptionHandlerPathFeature?.Error;

            // Log exception
            Console.WriteLine($"Unhandled exception: {exception?.Message}");
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync("An internal server error occurred.");
        });
    });
    app.UseHsts();
}

// Middleware configuration
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// Route mapping
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Home}/{id?}");

app.Run();
