#region Services

using System.Text.Encodings.Web;
using System.Text.Unicode;
using BugFixer.Persistence;
using BugFixer.Persistence.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

#region Context

builder.Services.AddDbContext<BugFixerDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BugFixerConnection"))
);

#endregion

#region Encode

builder.Services.AddSingleton<HtmlEncoder>(
    HtmlEncoder.Create(allowedRanges: new[] { UnicodeRanges.All }));

#endregion

#region Dependency Registeration

DependencyContainer.RegisterDependncies(builder.Services);

#endregion

#endregion

#region Middlewares

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

#endregion