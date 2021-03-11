using API.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace API.Data
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
		}

		public DbSet<Subscriber> Subscribers { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);


			var hasher = new PasswordHasher<User>();

			builder.Entity<User>().HasData(new User
			{
				Id = "a18be9c0-aa65-4af8-bd17-00bd9344e575",
				UserName = "Customer",
				NormalizedUserName = "CUSTOMER",
				Email = "customer@oopcoders.com",
				NormalizedEmail = "CUSTOMER@OOPCODERS.COM",
				EmailConfirmed = true,
				PasswordHash = hasher.HashPassword(null, "password"),
				SecurityStamp = string.Empty
			});

		}

	}
}