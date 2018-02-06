﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CityInfo.API.Controllers
{
	[Route("api/cities")]
	public class PointsOfInterestController : Controller
	{
		private readonly ILogger<PointsOfInterestController> _logger;
		private readonly IMailService _mailService;
		private readonly ICityInfoRepository _cityInfoRepository;

		public PointsOfInterestController(ILogger<PointsOfInterestController> logger, IMailService mailService, ICityInfoRepository cityInfoRepository) {
			_logger = logger;
			_mailService = mailService;
			_cityInfoRepository = cityInfoRepository;
		}

		[HttpGet("{cityId}/pointsofinterest")]
		public IActionResult GetPointsOfInterest(int cityId) {
			try {
				if (!_cityInfoRepository.CityExists(cityId)) {
					_logger.LogInformation($"City with id {cityId} wasn't found when accessing points of interest");
					return NotFound();
				}

				var pointsOfInterestForCity = _cityInfoRepository.GetPointsOfInterestForCity(cityId);

				var pointsOfInterestForCityResults = Mapper.Map<IEnumerable<PointOfInterestDto>>(pointsOfInterestForCity);
				// var pointsOfInterestForCityResults = new List<PointOfInterestDto>();
				// foreach (var poi in pointsOfInterestForCity) {
				// 	pointsOfInterestForCityResults.Add(new PointOfInterestDto() {
				// 		Id = poi.Id
				// 		, Name = poi.Name
				// 		, Description = poi.Description
				// 	});
				// }

				return Ok(pointsOfInterestForCityResults);

				// throw new Exception("Exception example");
				// _logger.LogError($"Hello, you are looking for Points of Intereset for cityId {cityId}");

				// var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

				// if (city == null)
				// {
				// 	_logger.LogInformation($"City with id {cityId} wasn't found when accessing points of interest");
				// 	return NotFound();
				// }

				// return Ok(city.PointsOfInterest);
			} catch (Exception ex) {
				_logger.LogCritical($"Exception while getting points of interest for city with id {cityId}.", ex);
				return StatusCode(500, "A problem happened while handling your request.");
			}
		}

		[HttpGet("{cityId}/pointsofinterest/{id}", Name = "GetPointOfInterest")]
		public IActionResult GetPointOfInterest(int cityId, int id) {
			if (!_cityInfoRepository.CityExists(cityId)) {
				return NotFound();
			}

			var pointOfInterest = _cityInfoRepository.GetPointOfInterestForCity(cityId, id);

			if (pointOfInterest == null) {
				return NotFound();
			}

			var pointOfInterestResult = Mapper.Map<PointOfInterestDto>(pointOfInterest);
			// var pointOfInterestResult = new PointOfInterestDto() {
			// 	Id = pointOfInterest.Id
			// 	, Name = pointOfInterest.Name
			// 	, Description = pointOfInterest.Description
			// };

			return Ok(pointOfInterestResult);

			// var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
			// if (city == null) {
			// 	return NotFound();
			// }

			// var pointOfInterest = city.PointsOfInterest.FirstOrDefault(p => p.Id == id);
			// if (pointOfInterest == null) {
			// 	return NotFound();
			// }

			// return Ok(pointOfInterest);
		}

		[HttpPost("{cityId}/pointsofinterest")]
		public IActionResult CreatePointOfInterest(int cityId, [FromBody] PointOfInterestForCreationDto pointOfInterest) {
			if (pointOfInterest == null) {
				return BadRequest();
			}

			if (pointOfInterest.Description == pointOfInterest.Name) {
				ModelState.AddModelError("Description", "The provided description should be different from the name");
			}

			if (!ModelState.IsValid) {
				return BadRequest(ModelState);
			}

			if (!_cityInfoRepository.CityExists(cityId)) {
				return NotFound();
			}
			// var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
			// if (city == null) {
			// 	return NotFound();
			// }

			var finalPointOfInterest = Mapper.Map<Entities.PointOfInterest>(pointOfInterest);

			// var maxPointOfInterestId = CitiesDataStore.Current.Cities.SelectMany(c => c.PointsOfInterest).Max(p => p.Id);
			// var finalPointOfInterest = new PointOfInterestDto() {
			// 	Id = ++maxPointOfInterestId
			// 	, Name = pointOfInterest.Name
			// 	, Description = pointOfInterest.Description
			// };

			// city.PointsOfInterest.Add(finalPointOfInterest);

			_cityInfoRepository.AddPointOfInterestForCity(cityId, finalPointOfInterest);

			if (!_cityInfoRepository.Save()) {
				return StatusCode(500, "A problem happened while handling your request.");
			}

			var createdPointOfInterestToReturn = Mapper.Map<Models.PointOfInterestDto>(finalPointOfInterest);

			return CreatedAtRoute("GetPointOfInterest", new { cityId = cityId, id = createdPointOfInterestToReturn.Id }, createdPointOfInterestToReturn);
		}

		[HttpPut("{cityId}/pointsofinterest/{id}")]
		public IActionResult UpdatePointOfInterest(int cityId, int id, [FromBody] PointOfInterestForUpdateDto pointOfInterest) {
			if (pointOfInterest == null) {
				return BadRequest();
			}

			if (pointOfInterest.Description == pointOfInterest.Name) {
				ModelState.AddModelError("Description", "The provided description should be different from the name.");
			}

			if (!ModelState.IsValid) {
				return BadRequest(ModelState);
			}

			var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
			if (city == null) {
				return NotFound();
			}

			var pointOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(p => p.Id == id);
			if (pointOfInterestFromStore == null) {
				return NotFound();
			}

			// Update ALL fields
			pointOfInterestFromStore.Name = pointOfInterest.Name;
			pointOfInterestFromStore.Description = pointOfInterest.Description;

			return NoContent();
		}

		[HttpPatch("{cityId}/pointsofinterest/{id}")]
		public IActionResult PartiallyUpdatePointOfInterest(int cityId, int id, [FromBody] JsonPatchDocument<PointOfInterestForUpdateDto> patchDoc) {
			if (patchDoc == null) {
				return BadRequest();
			}

			var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
			if (city == null) {
				return NotFound();
			}

			var pointOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(p => p.Id == id);
			if (pointOfInterestFromStore == null) {
				return NotFound();
			}

			var pointOfInterestToPatch = new PointOfInterestForUpdateDto() {
				Name = pointOfInterestFromStore.Name
				, Description = pointOfInterestFromStore.Description
			};

			patchDoc.ApplyTo(pointOfInterestToPatch, ModelState);

			// Only checks if the JsonPatchDocument is valid
			if (!ModelState.IsValid) {
				return BadRequest(ModelState);
			}

			if (pointOfInterestToPatch.Description == pointOfInterestToPatch.Name) {
				ModelState.AddModelError("Description", "The provided description should be different from the name.");
			}

			// Now checks if the new patch is valid
			TryValidateModel(pointOfInterestToPatch);

			if (!ModelState.IsValid) {
				return BadRequest(ModelState);
			}

			pointOfInterestFromStore.Name = pointOfInterestToPatch.Name;
			pointOfInterestFromStore.Description = pointOfInterestToPatch.Description;

			return NoContent();
		}

		[HttpDelete("{cityId}/pointsofinterest/{id}")]
		public IActionResult DeletePointOfInterest(int cityId, int id) {
			var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
			if (city == null)
			{
				return NotFound();
			}

			var pointOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(p => p.Id == id);
			if (pointOfInterestFromStore == null)
			{
				return NotFound();
			}

			city.PointsOfInterest.Remove(pointOfInterestFromStore);

			_mailService.Send("Point of Interest deleted", $"Point of interest {pointOfInterestFromStore.Name} with id {pointOfInterestFromStore.Id} was deleted.");

			return NoContent();
		}
	}
}
