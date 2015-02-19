using System;
using System.Collections.Generic;
namespace ClubCloud.Afhangen.UILogic.Models
{
	public class LocationModelComparer : IEqualityComparer<LocationModel>
	{
		public bool Equals(LocationModel x, LocationModel y)
		{
			return x.LocationId == y.LocationId;
		}
		public int GetHashCode(LocationModel obj)
		{
			throw new NotImplementedException();
		}
	}
}
