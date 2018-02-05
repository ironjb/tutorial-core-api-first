﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Entities
{
	public class CityInfoContext: DbContext
	{
		public CityInfoContext(DbContextOptions<CityInfoContext> options): base(options) {
			Database.EnsureCreated();
		}

		public DbSet<City> Cities { get; set; }
		public DbSet<PointOfInterest> PointsOfInterest { get; set; }

		// One way to provide connection string... other way is through contrutor
		//protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		//{
		//	optionsBuilder.UseSqlServer("connectionstring");
		//	base.OnConfiguring(optionsBuilder);
		//}
	}
}
