using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SocialMediaSite.Data;
using SocialMediaSite.Interfaces;

namespace SocialMediaSite
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
			builder.Services.AddDbContext<ApplicationDbContext>(options =>
			{
				options.UseSqlServer(connectionString);
				options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
			});
			builder.Services.AddDatabaseDeveloperPageExceptionFilter();
			builder.Services.AddTransient<IDataAccessLayer, SocialMediaDAL>();

			builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
options.SignIn.RequireConfirmedAccount =false).AddDefaultUI().AddDefaultTokenProviders().AddEntityFrameworkStores<ApplicationDbContext>();
			builder.Services.AddRazorPages();

			builder.Services.Configure<IdentityOptions>(options =>
			{
				//password settings
				options.Password.RequireDigit = true;
				options.Password.RequireLowercase = true;
				options.Password.RequireNonAlphanumeric = true;
				options.Password.RequireUppercase = true;
				options.Password.RequiredLength = 6;
				options.Password.RequiredUniqueChars = 1;

				//lockout settings
				options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
				options.Lockout.MaxFailedAccessAttempts = 3;
				options.Lockout.AllowedForNewUsers = true;

				//user settings
				options.User.AllowedUserNameCharacters =
				  "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
				options.User.RequireUniqueEmail = true;
			}

			);

			builder.Services.AddControllersWithViews();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseMigrationsEndPoint();
			}
			else
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
				pattern: "{controller=Home}/{action=Index}/{id?}");
			app.MapRazorPages();

			app.Run();
		}
	}
}