using System.ComponentModel.DataAnnotations;

namespace RecipeDemo.Models
{
	public class Recipe
	{
		public long Id { get; set; }

		[StringLength(50)]
		public string Title { get; set; }

		[StringLength(500)]
		public string Description { get; set; }
		
		[StringLength(50)]
		public string Image { get; set; }
	}
}
