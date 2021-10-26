using Microsoft.AspNetCore.Http;
using RecipeDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeDemo.ViewModels
{
	public class RecipeViewModel
	{
		public Recipe Recipe { get; set; }
		public List<RecipeDetail> Ingredients { get; set; }
		public List<RecipeDetail> Instructions { get; set; }
		public IFormFile ImageFile { get; set; }
		public long Id { get; set; }
	}
}
