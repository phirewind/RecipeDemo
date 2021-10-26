using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecipeDemo.Data;
using RecipeDemo.Models;
using RecipeDemo.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeDemo.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly DataAccessLayer _data;
		private readonly IWebHostEnvironment _hostEnvironment;
		public HomeController(ILogger<HomeController> logger, RecipeContext context, IWebHostEnvironment environment)
		{
			_logger = logger;
			_data = new DataAccessLayer(context);
			_hostEnvironment = environment;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Recipe(long id)
		{
			var model = _data.GetRecipe(id);
			model.Id = model.Recipe.Id;
			return View(model);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		[HttpGet("Home/CreateRecipe")]
		public long CreateRecipe()
		{
			return _data.CreateRecipe();
		}

		[HttpGet("Home/CreateDetail/{recipeId}/{detailType}")]
		public long CreateDetail(long recipeId, int detailType)
		{
			return _data.CreateDetail(recipeId, detailType);
		}

		[HttpGet("Home/Search/{searchText}/{page}/{sortAscending}")]
		public IActionResult Search(string searchText = "", int page = 1, bool sortAscending = true)
		{
			var model = new RecipeListViewModel();
			searchText = searchText == "%20" ? "" : searchText;

			int pagecount = _data.CountPages(searchText);
			page = page < 1 ? 1 : page > pagecount ? pagecount : page;
			model.PageCount = pagecount;
			model.PageNumber = page;
			var pageNumbers = new List<int>();
			var min = Math.Max(1, page - 4);
			var max = Math.Min(pagecount, page + 4);
			for (int i = min; i <= max; i++)
				pageNumbers.Add(i);
			model.Pages = pageNumbers.ToArray();
			model.Recipes = _data.GetRecipes(searchText, page, sortAscending);
			model.SearchText = searchText;
			model.SortAscending = sortAscending;
			var result = PartialView("RecipeList", model);

			return result;
		}

		[HttpGet("Home/DeleteRecipe/{id}")]
		public bool DeleteRecipe(long id)
		{
			return _data.DeleteRecipe(id);
		}

		[HttpGet("Home/DeleteDetail/{id}")]
		public bool DeleteDetail(long id)
		{
			return _data.DeleteDetail(id);
		}

		[HttpGet("Home/UpdateTitle/{id}/{title}")]
		public bool UpdateTitle(long id, string title)
		{
			return _data.UpdateRecipeTitle(id, title);
		}

		[HttpGet("Home/UpdateDescription/{id}/{description}")]
		public bool UpdateDescription(long id, string description)
		{
			return _data.UpdateRecipeDescription(id, description);
		}

		[HttpGet("Home/UpdateDetail/{id}/{description}")]
		public bool UpdateDetail(long id, string description)
		{
			return _data.UpdateDetailDescription(id, description);
		}

		[HttpPost]
		public async Task<IActionResult> UploadImage([Bind("Id, ImageFile")] UploadImageModel model)
		{
			try
			{
				if (model.ImageFile != null)
				{
					string hostpath = _hostEnvironment.WebRootPath;
					string filename = Path.GetFileNameWithoutExtension(model.ImageFile.FileName);
					string extension = Path.GetExtension(model.ImageFile.FileName);
					filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
					string writePath = Path.Combine(hostpath + "/images/" + filename);
					using (var filestream = new FileStream(writePath, FileMode.Create))
					{
						model.ImageFile.CopyTo(filestream);
					}

					var oldfile = _data.GetRecipe(model.Id).Recipe.Image;
					var uploaded = await _data.UpdateImage(model.Id, filename);
					if (uploaded && oldfile != "default.png")
					{
						var oldpath = Path.Combine(_hostEnvironment.WebRootPath, "images", oldfile);
						if (System.IO.File.Exists(oldpath))
							System.IO.File.Delete(oldpath);
					}
				}
			}
			catch { }

			return RedirectToAction(nameof(Recipe), new { id = model.Id });
		}
	}
}
