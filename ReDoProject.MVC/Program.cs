using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using ReDoProject.Persistence.Contexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


//Authtenticationa Cookie ekleme ve şemayı düzenleme
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
    x => {
        x.LoginPath = "/Account/Login";
        x.LogoutPath = "/Instrument";

        // yetkisiz işlem erişim yaparsa bu sayfaya gitsin.
        x.AccessDeniedPath = "/Instrument";

        x.Cookie.Name = "Login";

        // 2 günlük çerez vermesi belki uzatma yapabilirsin not et
        x.Cookie.MaxAge = TimeSpan.FromDays(2);

        x.Cookie.IsEssential = true;

        

});

builder.Services.AddAuthorization(
    x => {
        x.AddPolicy("AdminPolicy", policy => {
            policy.RequireClaim(ClaimTypes.Role, "Admin");
        });
        x.AddPolicy("CustomerPolicy", policy => {
            policy.RequireClaim(ClaimTypes.Role, "Admin","Customer");
        });
    });

//    var connectionString = builder.Configuration.GetSection( key: "YetgenPostgreSQLDB").Value;
//    builder.Services.AddDbContext<ReDoMusicDbContext>(options => options.UseNpgsql(connectionString));


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
    pattern: "{controller=Instrument}/{action=Index}/{id?}");

app.Run();

