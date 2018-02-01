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
		public IActionResult GetCities()
		{
			return Ok(CitiesDataStore.Current.Cities);
		}

		[HttpGet("{id}")]
		public IActionResult GetCity(int id) {
			var cityToReturn = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == id);

			if (cityToReturn == null)
			{
				return NotFound();
			}
			return Ok(cityToReturn);
		}
	}
}