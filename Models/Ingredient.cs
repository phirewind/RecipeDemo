using System.ComponentModel.DataAnnotations;

namespace RecipeDemo.Models
{
	public class Ingredient
	{
		public long Id { get; set; }
		
		[StringLength(200)]
		public string Description { get; set; }
		public long? RecipeId { get; set; }
	}
}
