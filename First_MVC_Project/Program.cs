using First_MVC_Project.Context;
using First_MVC_Project.Models;
using First_MVC_Project.ReposInterfaces;
using First_MVC_Project.Services;
using First_MVC_Project.utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<MyContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));
builder.Services.AddHttpContextAccessor();
builder.Services.AddIdentity<AppUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<MyContext>().AddDefaultTokenProviders();
//builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<myContext>();//.AddDefaultTokenProviders();
//builder.Services.ConfigureApplicationCookie(options =>
//{
//    options.LoginPath = $"/Identity/Account/Login";
//    options.LogoutPath= $"/Identity/Account/Logout";
//    options.AccessDeniedPath= $"/Identity/Account/AccessDenied";
//});


//builder.Services.AddDefaultIdentity<AppUser>
//    (options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<MyContext>();
builder.Services.AddRazorPages();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IEmailSender, EmailSender>();

//var googleClientId = builder.Configuration["secrets:google:ClientId"];
//var googleClientSecret = builder.Configuration["secrets:google:ClientSecret"];
//var facebookClientId = builder.Configuration["secrets:facebook:ClientId"];
//var facebookClientSecret = builder.Configuration["secrets:facebook:ClientSecret"];
//var twitterConsumerKey = builder.Configuration["secrets:twitter:ConsumerKey"];
//var twitterConsumerSecret = builder.Configuration["secrets:twitter:ConsumerSecret"];

//// Add authentication services
//builder.Services.AddAuthentication().AddFacebook(options =>
//{
//    options.ClientId = facebookClientId;
//    options.ClientSecret = facebookClientSecret;
//}).AddTwitter(options =>
//{
//    options.ConsumerKey = twitterConsumerKey;
//    options.ConsumerSecret = twitterConsumerSecret;
//}).AddGoogle(options =>
//{
//    options.ClientId = googleClientId;
//    options.ClientSecret = googleClientSecret;
//});
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
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();
