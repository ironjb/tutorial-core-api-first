using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityInfo.API.Entities;

namespace CityInfo.API
{
	public static class CityInfoContextExtensions
	{
		// There isn't an official way to seed the data in .NET Core yet, so this is the prefered method for now.
		public static void EnsureSeedDataForContext(this CityInfoContext context) {
			if (context.Cities.Any()) {
				return;
			}

			// init seed data
			var cities = new List<City>() {
				new City()
				{
					Name = "Boise"
					, Description = "City of Trees"
					, PointsOfInterest = new List<PointOfInterest>()
					{
						new PointOfInterest() {
							Name = "Old City Jail"
							, Description = "An old historical jail"
						}
						, new PointOfInterest() {
							Name = "Discovery Center of Idaho"
							, Description = "Children's Museums, Sciency stuff"
						}
						, new PointOfInterest() {
							Name = "Zoo Boise"
							, Description = "Zoo with animals"
						}
					}
				}
				, new City()
				{
					Name = "Nampa"
					, Description = "Where I live."
					, PointsOfInterest = new List<PointOfInterest>()
					{
						new PointOfInterest() {
							Name = "George Norse Range"
							, Description = "Great place to go shooting"
						}
						, new PointOfInterest() {
							Name = "Warhawk Air Museum"
							, Description = "Museum on the history of aviation"
						}
						, new PointOfInterest() {
							Name = "Lions Park"
							, Description = "Park with sports, a playground & a pool"
						}
					}
				}
				, new City()
				{
					Name = "Meridian"
					, Description = "Plenty of fun places"
					, PointsOfInterest = new List<PointOfInterest>()
					{
						new PointOfInterest() {
							Name = "Roaring Springs Water Park"
							, Description = "Large water park. Fun in the sun."
						}
						, new PointOfInterest() {
							Name = "Eagle Island State Park"
							, Description = "Hiking trails, picnic areas & disc golf"
						}
						, new PointOfInterest() {
							Name = "Wahooz Familly Fun Zone"
							, Description = "Laser Tag, Go-carts, arcade games, bowling, etc."
						}
					}
				}
			};

			context.Cities.AddRange(cities);
			context.SaveChanges();
		}
	}
}
