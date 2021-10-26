using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RecipeDemo.Models;

namespace RecipeDemo.Data
{
	public static class DbInitializer
	{
		public static void Initialize(RecipeContext context)
		{
			context.Database.EnsureCreated();

			if (context.RecipeDetails.Any())
				return;

			var recipe = new Recipe { Title = "Steak", Description = "Delicious burnt meats.", Image = "steak.jpg" };
			context.Recipes.Add(recipe);
			context.SaveChanges();

			//var ingredients = new Ingredient[]
			//{
			//	new Ingredient { Description = "A steak", RecipeId = recipe.Id },
			//	new Ingredient { Description = "Salt", RecipeId = recipe.Id },
			//	new Ingredient { Description = "Pepper", RecipeId = recipe.Id }
			//};
			//foreach (var ingredient in ingredients)
			//	context.Ingredients.Add(ingredient);
			//context.SaveChanges();

			//var instructions = new Instruction[]
			//{
			//	new Instruction { Description = "Cover the steak in salt and pepper.", RecipeId = recipe.Id },
			//	new Instruction { Description = "Put it on the fire.", RecipeId = recipe.Id },
			//	new Instruction { Description = "Wait 4 minutes.", RecipeId = recipe.Id },
			//	new Instruction { Description = "Flip it.", RecipeId = recipe.Id },
			//	new Instruction { Description = "Wait 4 more minutes.", RecipeId = recipe.Id },
			//	new Instruction { Description = "Put it on a plate.", RecipeId = recipe.Id },
			//	new Instruction { Description = "Wait 5 more minutes.", RecipeId = recipe.Id },
			//	new Instruction { Description = "Eat it.", RecipeId = recipe.Id }
			//};

			//foreach (var instruction in instructions)
			//	context.Instructions.Add(instruction);
			var details = new RecipeDetail[]
			{
				new RecipeDetail { Description = "A steak", RecipeId = recipe.Id, DetailType = 1 },
				new RecipeDetail { Description = "Salt", RecipeId = recipe.Id, DetailType = 1 },
				new RecipeDetail { Description = "Pepper", RecipeId = recipe.Id, DetailType = 1 },
				new RecipeDetail { Description = "Cover the steak in salt and pepper.", RecipeId = recipe.Id, DetailType = 2 },
				new RecipeDetail { Description = "Put it on the fire.", RecipeId = recipe.Id, DetailType = 2 },
				new RecipeDetail { Description = "Wait 4 minutes.", RecipeId = recipe.Id, DetailType = 2 },
				new RecipeDetail { Description = "Flip it.", RecipeId = recipe.Id, DetailType = 2 },
				new RecipeDetail { Description = "Wait 4 more minutes.", RecipeId = recipe.Id, DetailType = 2 },
				new RecipeDetail { Description = "Put it on a plate.", RecipeId = recipe.Id, DetailType = 2 },
				new RecipeDetail { Description = "Wait 5 more minutes.", RecipeId = recipe.Id, DetailType = 2 },
				new RecipeDetail { Description = "Eat it.", RecipeId = recipe.Id, DetailType = 2 }
			};
			foreach (var detail in details)
				context.RecipeDetails.Add(detail); 

			context.SaveChanges();

		}
	}
}
