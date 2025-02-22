using FluentValidation.AspNetCore;
using MyBlogNight.BusinessLayer.Container;
using MyBlogNight.DataAccessLayer.Context;
using MyBlogNight.EntityLayer.Concrete;
using MyBlogNight.PresentationLayer.Models;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddDbContext<BlogContext>();
builder.Services.AddIdentity<AppUser, AppRole>()
    .AddEntityFrameworkStores<BlogContext>()
    .AddErrorDescriber<CustomIdentityErrorValidator>();

builder.Services.ContainerDependencies();
builder.Services.AddControllersWithViews().AddFluentValidation();

var app = builder.Build();

// Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// ROUTING DÜZENLEMESÝ
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
