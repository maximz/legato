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
        /// Gets or sets a value indicating whether the value of the input address property should be overridden with the address returned from the geocoder. Default: true.
        /// </summary>
        /// <value><c>true</c> if null is allowed; otherwise, <c>false</c>.</value>
        public bool OverwriteAddress
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

        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        /// <value>
        /// The result. Created in the attribute itself.
        /// </value>
        protected GoogleAddress _result;

        protected static string GetPropertyValue(object obj, string propertyName)
		{
			if (obj == null) return null;
			var type = obj.GetType();
			var propertyInfo = type.GetProperty(propertyName);
			if (propertyInfo == null) return null;
			return propertyInfo.GetValue(obj, null).ToString();
		}
		protected static void SetPropertyValue(object obj, object value, string propertyName)
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
				ErrorMessage = "We were unable to find that location. Please try again.";

            if(OverwriteAddress == null)
            {
                OverwriteAddress = true;
            }
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

            var geocoder = new GoogleGeoCoder(); // key not needed
            var result = geocoder.GeoCode(address.Trim()).FirstOrDefault();
            if(result == null)
            {
                // No results
                return false;
            }
            _result = result; // store so that other classes that inherit from this attribute can access the result without re-geocoding
            SetPropertyValue(value, result.Coordinates.Latitude, LatitudePropertyName); // set latitude
            SetPropertyValue(value, result.Coordinates.Longitude, LongitudePropertyName); // set longitude

            var fullAddress = result.FormattedAddress; // (result.Street.HasValue() && result.Street.Trim().HasValue() ? result.Street + ", " : "") + result.City + " " + result.State + ", " + result.Country + " " + result.PostalCode;
            SetPropertyValue(value, fullAddress, AddressPropertyName); // set exact address

			return true;
		}
	}

    /// <summary>
	/// Verifies that a certain property is geocodable; if true, writes the geocoding results to attributes of your choosing and filters address to zip code. To be applied to an entire class.
	/// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class GeocodableWithFilteredAddress : GeocodableAttribute
    {
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
        public string FilterToZipCodeOnlyPropertyName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name of the filtered latitude property.
        /// </summary>
        /// <value>The name of the filtered latitude property.</value>
        public string FilteredLatitudePropertyName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name of the filtered longitude property.
        /// </summary>
        /// <value>The name of the filtered longitude property.</value>
        public string FilteredLongitudePropertyName
        {
            get;
            set;
        }
        string ProcessAddressComponent(GoogleAddressComponent c)
        {
            return (c == null) ? "" : c.LongName + ", ";
        }

        public override bool IsValid(object value)
        {
            if(base.IsValid(value))
            {
                var city = _result.Components.Where(c => c.Types[0] == GoogleAddressType.Locality).FirstOrDefault();
                var state = _result.Components.Where(c => c.Types[0] == GoogleAddressType.AdministrativeAreaLevel1).FirstOrDefault();
                var country = _result.Components.Where(c => c.Types[0] == GoogleAddressType.Country).FirstOrDefault();
                var postalcode = _result.Components.Where(c => c.Types[0] == GoogleAddressType.PostalCode).FirstOrDefault();
                var zipAddress = ProcessAddressComponent(city) + ProcessAddressComponent(state) + ProcessAddressComponent(country) + ProcessAddressComponent(postalcode);

                if (!(FilterToZipCodeOnlyPropertyName == null && FilteredAddressPropertyName == null)) // if filtering has been enabled, execute filter
                {
                    if (GetPropertyValue(value, FilterToZipCodeOnlyPropertyName) == "2") // Zip code only
                    {
                        SetPropertyValue(value, zipAddress, FilteredAddressPropertyName);

                        // Find new lat-long
                        var geocoder = new GoogleGeoCoder(); // key not needed
                        var result = geocoder.GeoCode(zipAddress.Trim()).FirstOrDefault();
                        if(result != null) // we have results
                        {
                            SetPropertyValue(value, result.Coordinates.Latitude, FilteredLatitudePropertyName);
                            SetPropertyValue(value, result.Coordinates.Longitude, FilteredLongitudePropertyName);
                        }
                        else
                        {
                            SetPropertyValue(value, null, FilteredLatitudePropertyName);
                            SetPropertyValue(value, null, FilteredLongitudePropertyName);
                        }

                    }
                    else
                    {
                        SetPropertyValue(value, null, FilteredAddressPropertyName); // null indicates that filtered address is the same as actual/exact address
                    }
                }

                return true;
            }
            return false;
        }

        public GeocodableWithFilteredAddress()
			: base() {}
    }
}