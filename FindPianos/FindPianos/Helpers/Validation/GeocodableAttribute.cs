using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FindPianos.Helpers
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public class GeocodableAttribute : ValidationAttribute
	{
		public string AddressPropertyName
		{
			get;
			set;
		}
		public string LatitudePropertyName
		{
			get;
			set;
		}
		public string LongitudePropertyName
		{
			get;
			set;
		}
		private static string GetPropertyValue(object obj, string propertyName)
		{
			if (obj == null) return null;
			var type = obj.GetType();
			var propertyInfo = type.GetProperty(propertyName);
			if (propertyInfo == null) return null;
			return propertyInfo.GetValue(obj, null) as string;
		}
		private static void SetPropertyValue(object obj, object value, string propertyName)
		{
			if (obj == null) throw new ArgumentNullException();
			var type = obj.GetType();
			var propertyInfo = type.GetProperty(propertyName);
			propertyInfo.SetValue(obj, value, null);
		}
		public GeocodableAttribute()
			: base()
		{
			ErrorMessage = "We weren't able to find that location.";
		}
		public override bool IsValid(object value)
		{
			var address = GetPropertyValue(value, AddressPropertyName);

			if (address.IsNullOrEmpty())
			{
				throw new ArgumentNullException();
			}

			var response = Geocoder.CallGeoWS(address.Trim());
			if(response.Status=="ZERO_RESULTS")
			{
				return false;
			}
			SetPropertyValue(value, response.Results[0].Geometry.Location.Lat, LatitudePropertyName);
			SetPropertyValue(value, response.Results[0].Geometry.Location.Lng, LongitudePropertyName);
			return true;
		}
	}
}