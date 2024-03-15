using Infrastructure.Data.Context;
using Infrastructure.Models.Identification;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Silicon.Helpers.Middlewares;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));
builder.Services.AddDefaultIdentity<ApplicationUser>(x =>
{
    x.User.RequireUniqueEmail = true;
    x.SignIn.RequireConfirmedEmail = false;
    x.Password.RequiredLength = 8;
}).AddEntityFrameworkStores<DataContext>();

builder.Services.ConfigureApplicationCookie(x =>
{
    x.Cookie.HttpOnly = true;
    x.LoginPath = "/signin";
    x.LogoutPath = "/signout";
    x.AccessDeniedPath = "/denied";
    x.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    x.SlidingExpiration = true;
    x.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});

builder.Services.AddScoped<AddressRepository>();
builder.Services.AddScoped<AddressManager>();

builder.Services.AddAuthentication().AddFacebook(x =>
{
    x.AppId = "399096742977803";
    x.AppSecret = "a0d28ebaff102027ddff6d2acdfdf9ec";
});

builder.Services.AddAuthentication().AddGoogle(x =>
{
    x.ClientId = "68485155341-v6i89d1k40kmt49cvteo2su5cs4nh8bk.apps.googleusercontent.com";
    x.ClientSecret = "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";
});

var app = builder.Build();

app.UseHsts();
app.UseStatusCodePagesWithReExecute("/error", "?statusCode={0}");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseUserSessionValidation();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
