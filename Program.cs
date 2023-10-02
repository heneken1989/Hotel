using Hotel.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// add db service to the container
builder.Services.AddDbContext<HotelDbContext>(o =>
{
    o.UseSqlServer(builder.Configuration.GetConnectionString("DemoConnectionString"));
});

// add authozied use cookie
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.LoginPath = "/unauthozied";
    options.AccessDeniedPath = "/unauthozied";
    options.ExpireTimeSpan = TimeSpan.FromDays(1);
});


builder.Services.AddMvc(options =>
{
    options.Filters.Add(new AuthorizeFilter());
});





var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
