using BlueWork.web.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Configure Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
    options.LoginPath = "/Account/Login"; // Redirect unauthenticated users here
    options.AccessDeniedPath = "/Account/AccessDenied"; // Redirect unauthorized users here
}) 
.AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
{
    options.ClientId = builder.Configuration.GetValue<string>("GoogleKeys:ClientId");
    options.ClientSecret = builder.Configuration.GetValue<string>("GoogleKeys:ClientSecret");
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme);
    


// Add services to the container
builder.Services.AddControllersWithViews();

// Register DbContexts
builder.Services.AddDbContext<BlueWorkDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("FirstConnection")));

// Build the app
var app = builder.Build();
// Configure middleware for error handling
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // This enables detailed error messages
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}


// Configure middleware
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// Map default controller route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Home}/{id?}");

// Run the application
app.Run();
