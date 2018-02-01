using CityInfo.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API
{
	public class CitiesDataStore
	{
		public static CitiesDataStore Current { get; } = new CitiesDataStore();

		public List<CityDto> Cities { get; set; }

		public CitiesDataStore()
		{
			// init dummy data
			Cities = new List<CityDto>() {
				new CityDto()
				{
					Id = 1
					, Name = "Boise"
					, Description = "City of Trees"
					, PointsOfInterest = new List<PointOfInterestDto>()
					{
						new PointOfInterestDto() {
							Id = 1
							, Name = "Old City Jail"
							, Description = "An old historical jail"
						}
						, new PointOfInterestDto() {
							Id = 2
							, Name = "Discovery Center of Idaho"
							, Description = "Children's Museums, Sciency stuff"
						}
						, new PointOfInterestDto() {
							Id = 3
							, Name = "Zoo Boise"
							, Description = "Zoo with animals"
						}
					}
				}
				, new CityDto()
				{
					Id = 2
					, Name = "Nampa"
					, Description = "Where I live."
					, PointsOfInterest = new List<PointOfInterestDto>()
					{
						new PointOfInterestDto() {
							Id = 1
							, Name = "George Norse Range"
							, Description = "Great place to go shooting"
						}
						, new PointOfInterestDto() {
							Id = 2
							, Name = "Warhawk Air Museum"
							, Description = "Museum on the history of aviation"
						}
						, new PointOfInterestDto() {
							Id = 3
							, Name = "Lions Park"
							, Description = "Park with sports, a playground & a pool"
						}
					}
				}
				, new CityDto()
				{
					Id = 3
					, Name = "Meridian"
					, Description = "Nice but pricey homes"
					, PointsOfInterest = new List<PointOfInterestDto>()
					{
						new PointOfInterestDto() {
							Id = 1
							, Name = "Roaring Springs Water Park"
							, Description = "Large water park. Fun in the sun."
						}
						, new PointOfInterestDto() {
							Id = 2
							, Name = "Eagle Island State Park"
							, Description = "Hiking trails, picnic areas & disc golf"
						}
						, new PointOfInterestDto() {
							Id = 3
							, Name = "Wahooz Familly Fun Zone"
							, Description = "Laser Tag, Go-carts, arcade games, bowling, etc."
						}
					}
				}
			};
		}
	}
}
