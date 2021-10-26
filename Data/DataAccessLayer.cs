using System.Collections.Generic;
using System.Linq;
using RecipeDemo.Models;
using System.Data;
using System;
using RecipeDemo.ViewModels;
using System.Threading.Tasks;

namespace RecipeDemo.Data
{
	public class DataAccessLayer
	{
		private RecipeContext _context;
		public int PageSize {get;} = 10;
		public DataAccessLayer(RecipeContext context)
		{
			_context = context;
		}

		public int CountPages(string searchText)
		{
			double count = string.IsNullOrEmpty(searchText) ? _context.Recipes.Count() : _context.Recipes.Count(r => r.Title.Contains(searchText));
			count = count == 0 ? 1 : count;

			return (int)Math.Ceiling(count / PageSize);
		}

		public List<Recipe> GetRecipes(string searchText, int page, bool sortAscending)
		{
			IQueryable<Recipe> query = _context.Recipes;
			if (!string.IsNullOrWhiteSpace(searchText))
				query = query.Where(r => r.Title.Contains(searchText) || r.Description.Contains(searchText) || _context.RecipeDetails.Where(d => d.RecipeId == r.Id && d.Description.Contains(searchText)).Any());

			query = (sortAscending ? query.OrderBy(r => r.Title) : query.OrderByDescending(r => r.Title))
				.Skip(PageSize * (page - 1))
				.Take(PageSize);
			return query.ToList();
		}

		public long CreateRecipe()
		{
			var recipe = new Recipe { Title = "A New Recipe", Description = "Describe your delicious foodstuffs.", Image = "default.png" };
			_context.Recipes.Add(recipe);
			_context.SaveChanges();
			return recipe.Id;
		}
		public RecipeViewModel GetRecipe(long id)
		{
			var recipe = _context.Recipes.FirstOrDefault(r => r.Id == id);
			var query = _context.Recipes
				.Where(r => r.Id == id)
				.Join(
					_context.RecipeDetails,
					recipe => recipe.Id,
					detail => detail.RecipeId,
					(recipe, detail) => new
					{
						recipe = recipe,
						detail = detail
					}
				).ToList();

			var model = new RecipeViewModel()
			{
				Recipe = recipe,
				Ingredients = query.Where(q => q.detail.DetailType == 1)?.Select(q => q.detail).ToList() ?? new(),
				Instructions = query.Where(q => q.detail.DetailType == 2)?.Select(q => q.detail).ToList() ?? new()
			};
			return model;
		}

		public bool DeleteRecipe(long id)
		{
			try
			{
				var recipe = _context.Recipes.FirstOrDefault(r => r.Id == id);
				if(recipe != null)
				{
					_context.Recipes.Remove(recipe);
					var details = _context.RecipeDetails.Where(d => d.RecipeId == id).ToList();
					if (details.Any())
						_context.RecipeDetails.RemoveRange(details);
					_context.SaveChanges();
				}
				return true;
			}
			catch
			{
				return false;
			}
		}

		public bool DeleteDetail(long id)
		{
			try
			{
				var detail = _context.RecipeDetails.FirstOrDefault(d => d.Id == id);
				if (detail != null)
				{
					_context.RecipeDetails.Remove(detail);
					_context.SaveChanges();
				}
				return true;
			}
			catch
			{
				return false;
			}
		}

		public bool UpdateRecipeTitle(long id, string title)
		{
			try
			{
				var recipe = _context.Recipes.FirstOrDefault(r => r.Id == id);
				if (recipe != null)
				{
					recipe.Title = title;
					_context.SaveChanges();
				}
				return true;
			}
			catch
			{
				return false;
			}
		}

		public bool UpdateRecipeDescription(long id, string description)
		{
			try
			{
				var recipe = _context.Recipes.FirstOrDefault(r => r.Id == id);
				if (recipe != null)
				{
					recipe.Description = description;
					_context.SaveChanges();
				}
				return true;
			}
			catch
			{
				return false;
			}
		}

		public bool UpdateRecipeImage(long id, string image)
		{
			try
			{
				var recipe = _context.Recipes.FirstOrDefault(r => r.Id == id);
				if (recipe != null)
				{
					recipe.Image = image;
					_context.SaveChanges();
				}
				return true;
			}
			catch
			{
				return false;
			}
		}

		public bool UpdateDetailDescription(long id, string description)
		{
			try
			{
				var detail = _context.RecipeDetails.FirstOrDefault(d => d.Id == id);
				if (detail != null)
				{
					detail.Description = description;
					_context.SaveChanges();
				}
				return true;
			}
			catch
			{
				return false;
			}
		}

		public long CreateDetail(long recipeId, int detailType)
		{
			var detail = new RecipeDetail { RecipeId = recipeId, DetailType = detailType, Description = "Detail Description" };
			_context.RecipeDetails.Add(detail);
			_context.SaveChanges();
			return detail.Id;
		}

		public async Task<bool> UpdateImage(long recipeId, string filename)
		{
			try
			{
				var recipe = _context.Recipes.FirstOrDefault(r => r.Id == recipeId);
				if(recipe != null)
				{
					recipe.Image = filename;
					await _context.SaveChangesAsync();
				}
				return true;
			}
			catch 
			{
				return false;
			}
		}
	}
}
