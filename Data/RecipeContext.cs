using Microsoft.EntityFrameworkCore;
using RecipeDemo.Models;

namespace RecipeDemo.Data
{
	public class RecipeContext : DbContext
	{
		public RecipeContext(DbContextOptions<RecipeContext> options) : base(options) { }

		public DbSet<Recipe> Recipes { get; set; }
		public DbSet<RecipeDetail> RecipeDetails { get; set; }
		public DbSet<Instruction> Instructions { get; set; }
		public DbSet<Ingredient> Ingredients { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Recipe>().ToTable("Recipe");
			modelBuilder.Entity<Instruction>().ToTable("Instruction");
			modelBuilder.Entity<Ingredient>().ToTable("Ingredient");
			modelBuilder.Entity<RecipeDetail>().ToTable("RecipeDetail");
		}
	}
}
