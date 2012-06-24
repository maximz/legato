using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using GeoCoding;
using GeoCoding.Google;

namespace Legato.Helpers
{
	/// <summary>
	/// Verifies that a certain property is geocodable; if true, writes the geocoding results to attributes of your choosing. To be applied to an entire class.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public class GeocodableAttribute : ValidationAttribute
	{
		/// <summary>
		/// Gets or sets a value indicating whether a null address is allowed.
		/// </summary>
		/// <value><c>true</c> if null is allowed; otherwise, <c>false</c>.</value>
		public bool AllowNull
		{
			get;
			set;
		}
		/// <summary>
		/// Gets or sets the name of the address property.
		/// </summary>
		/// <value>The name of the address property.</value>
		public string AddressPropertyName
		{
			get;
			set;
		}
        /// <summary>
        /// Gets or sets the name of the filtered address property.
        /// </summary>
        /// <value>The name of the filtered address property.</value>
        public string FilteredAddressPropertyName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the filter to zip code only property.
        /// </summary>
        /// <value>
        /// The filter to zip code only property.
        /// </value>
        public string FilterToZipCodeOnlyProperty
        {
            get;
            set;
        }

		/// <summary>
		/// Gets or sets the name of the latitude property.
		/// </summary>
		/// <value>The name of the latitude property.</value>
		public string LatitudePropertyName
		{
			get;
			set;
		}
		/// <summary>
		/// Gets or sets the name of the longitude property.
		/// </summary>
		/// <value>The name of the longitude property.</value>
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
		/// <summary>
		/// Initializes a new instance of the <see cref="GeocodableAttribute"/> class.
		/// </summary>
		public GeocodableAttribute()
			: base()
		{
			if(ErrorMessage.IsNullOrEmpty())
				ErrorMessage = "We weren't able to find that location.";
		}
		/// <summary>
		/// Determines whether the specified value is valid.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>
		/// 	<c>true</c> if the specified value is valid; otherwise, <c>false</c>.
		/// </returns>
		public override bool IsValid(object value)
		{
			var address = GetPropertyValue(value, AddressPropertyName);

			if (address.IsNullOrEmpty())
			{
				return AllowNull;
			}

            var geocoder = new GoogleGeoCoder("");
            var result = geocoder.GeoCode(address.Trim()).FirstOrDefault();
            if(result == null)
            {
                // No results
                return false;
            }

            SetPropertyValue(value, result.Coordinates.Latitude, LatitudePropertyName); // set latitude
            SetPropertyValue(value, result.Coordinates.Longitude, LongitudePropertyName); // set longitude
            var fullAddress = result.Street + ", " + result.City + " " + result.State + ", " + result.Country + " " + result.PostalCode;
            var zipAddress = result.City + " " + result.State + ", " + result.Country + " " + result.PostalCode;
            SetPropertyValue(value, fullAddress, AddressPropertyName); // set exact address

            if(!(FilteredAddressPropertyName == null && FilteredAddressPropertyName == null)) // if filtering has been enabled, execute filter
            {
                if (GetPropertyValue(value, FilterToZipCodeOnlyProperty)=="2") // Zip code only
                {
                    SetPropertyValue(value, zipAddress, FilteredAddressPropertyName);
                }
                else
                {
                    SetPropertyValue(value, null, FilteredAddressPropertyName); // null indicates that filtered address is the same as actual/exact address
                }
            }
            

			return true;
		}
	}
}