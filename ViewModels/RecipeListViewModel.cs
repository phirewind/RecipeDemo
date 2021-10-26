using RecipeDemo.Models;
using System.Collections.Generic;

namespace RecipeDemo.ViewModels
{
	public class RecipeListViewModel
	{
		public List<Recipe> Recipes = new List<Recipe>();

		public int PageCount { get; set; } = 0;
		public int PageNumber { get; set; } = 0;
		public int[] Pages { get; set; } = {  };

		public string SearchText = "";
		public bool SortAscending { get; set; } = true;
	}
}
