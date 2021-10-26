using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeDemo.Models
{
	public class UploadImageModel
	{
		public long Id { get; set; }
		public IFormFile ImageFile { get; set; }
	}
}
