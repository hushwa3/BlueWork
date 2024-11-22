using BlueWork.web.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Cookies;
using BlueWork.web.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
   .AddCookie(options =>
   {
       options.LoginPath = "/Account/Login";
   })
   .AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
   {
       options.ClientId = builder.Configuration.GetValue<string>("GoogleKeys:ClientId");
       options.ClientSecret = builder.Configuration.GetValue<string>("GoogleKeys:ClientSecret");
   });

// Add services to the container.  
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<BlueWorkDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("FirstConnection")));

builder.Services.AddDbContext<EntityDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SecondConnection")));

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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
   name: "default",
   pattern: "{controller=Home}/{action=Home}/{id?}");

app.Run();