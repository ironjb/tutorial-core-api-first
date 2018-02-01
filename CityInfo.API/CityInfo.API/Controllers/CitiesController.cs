using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
	[Route("api/cities")]		// <-- could be "api/[controller]" (not recommended by course instructor)
	public class CitiesController : Controller
	{
		//public IActionResult Index()
		//{
		//	return View();
		//}
		//[HttpGet("api/cities")]		// <-- if not using Route attribute at controller level
		[HttpGet()]
		public JsonResult GetCities()
		{
			return new JsonResult(CitiesDataStore.Current.Cities);
			//return new JsonResult(new List<object>() {
			//	new { id = 1, Name = "Boise" }
			//	, new { id = 2, Name = "Nampa" }
			//});
		}

		[HttpGet("{id}")]
		public JsonResult GetCity(int id) {
			return new JsonResult(CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == id));
		}
	}
}