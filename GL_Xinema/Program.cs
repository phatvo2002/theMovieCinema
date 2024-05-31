using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using GL_Xinema.Data;
using GL_Xinema.Areas.Identity.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("GL_XinemaContextConnection") ?? throw new InvalidOperationException("Connection string 'GL_XinemaContextConnection' not found.");

builder.Services.AddDbContext<GL_XinemaContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<GL_XinemaUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<GL_XinemaContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
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

app.MapGet("/", context =>
{
    context.Response.Redirect("/Identity/Account/Login");
    return Task.CompletedTask;
});


app.MapRazorPages();
app.Run();
