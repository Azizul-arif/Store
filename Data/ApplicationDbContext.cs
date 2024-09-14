using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Emit;

namespace Ecommerce.Data
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{

		}

		public DbSet<Color> Colors { get; set; }
		public DbSet<Size> Sizes { get; set; }
		public DbSet<Category>Categories { get; set; }
		public DbSet<Product>Products { get; set; }
		public DbSet<ProductImages> ProductImages { get; set; }


		protected override void OnModelCreating(ModelBuilder modelbuilder)
		{
			base.OnModelCreating(modelbuilder);
			modelbuilder.Ignore<ApplicationUser>();
			modelbuilder.Entity<Color>().HasData(
				new Color { Id = 1, Name = "White" },
				new Color { Id = 2, Name = "Black" },
				new Color { Id = 3, Name = "Red" }
				);
			modelbuilder.Entity<Size>().HasData(
				new Size { Id = 1, Name = "Small" },
				new Size { Id = 2, Name = "Medium" },
				new Size { Id = 3, Name = "Large" },
				new Size { Id = 4, Name = "XL" }
				);
			modelbuilder.Entity<Category>().HasData(
				new Category { Id = 1,CategoryName="Mens Fashion",ParentID=1},
				new Category { Id = 2,CategoryName="Womens Fashion",ParentID=2},
				new Category { Id = 3,CategoryName="Footwear",ParentID=3}
			);
			modelbuilder.Entity<Product>().HasData(
				new Product { Id = 1, Name = "Shirt", Description = "Full Sleeve", Price = 1200, CategoryId = 1 },
				new Product { Id = 2, Name = "Tops", Description = "Summer Sleeve", Price = 1400, CategoryId = 2 },
				new Product { Id = 3, Name = "Shoes", Description = "Oxford SHoes", Price = 2000, CategoryId = 3 }
				);
		}
	}

	public class ApplicationUser : IdentityUser
	{
		public string? FullName { get; set; }
		public string? PicPath { get; set; }

		// [NotMapped] attribute is used to exclude this property from the database mapping
		[NotMapped]
		public IFormFile? ProfilePic { get; set; }
	}


}
