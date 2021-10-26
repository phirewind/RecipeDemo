using System.ComponentModel.DataAnnotations;

namespace RecipeDemo.Models
{
	public class RecipeDetail
	{
		public long Id { get; set; }

		[StringLength(100)]
		public string Description { get; set; }
		public int DetailType { get; set; }
		public long? RecipeId { get; set; }
	}
}
