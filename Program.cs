using Hotel.Data;
using Hotel.Dtos;
using Hotel.Filters;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// add db service to the container
builder.Services.AddDbContext<HotelDbContext>(o =>
{
    o.UseSqlServer(builder.Configuration.GetConnectionString("DemoConnectionString"));
});

builder.Services.AddScoped<BaseDataActionFilter>();

// add authozied use cookie
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.LoginPath = "/login";
    options.AccessDeniedPath = "/unauthozied";
    options.ExpireTimeSpan = TimeSpan.FromDays(1); 
});



builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("admin"));
});


builder.Services.AddMvc(options =>
{
	options.Filters.Add(typeof(BaseDataActionFilter));
	options.Filters.Add(new AuthorizeFilter());
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
	
}
else
{
	app.UseExceptionHandler("/Home/Error");

 
}
app.UseStaticFiles();

app.UseRouting();
app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
